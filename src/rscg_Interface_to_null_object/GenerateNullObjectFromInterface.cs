using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;

namespace rscg_Interface_to_null_object;
[Generator]
public class GenerateNullObjectFromInterface: IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var classesToApplySortable = context.SyntaxProvider.ForAttributeWithMetadataName(
"InterfaceToNullObject.ToNullObjectAttribute",
IsAppliedOnInterface,
FindAttributeDataExpose
)
.Collect()
.SelectMany((data, _) => data.Distinct())
.Collect();
        context.RegisterSourceOutput(classesToApplySortable,
(spc, data) =>
ExecuteSort(spc, data));

    }

    private DataFromExposeInterface? FindAttributeDataExpose(GeneratorAttributeSyntaxContext context, CancellationToken token)
    {
        var data = context.TargetSymbol as INamedTypeSymbol;
        if (data != null)
        {
            
            return new DataFromExposeInterface(data);
        }

        return null;
    }

    private static bool IsAppliedOnInterface(
    SyntaxNode syntaxNode,
    CancellationToken cancellationToken)
    {
        var isOK = syntaxNode is InterfaceDeclarationSyntax ;
        return isOK;

    }
    private void ExecuteSort(SourceProductionContext spc, ImmutableArray<DataFromExposeInterface?> dataFromExposeClasses)
    {
        var arr = dataFromExposeClasses.ToArray()
            .Where(it => it != null)
            .Select(it => it!)
            .ToArray();
        foreach (var classData in arr)
        {
            
            var c = new NullObjectTemplate(classData);
            var text = c.Render();
            spc.AddSource($"{classData.Name}_null.cs", text);
        }
    }

}
