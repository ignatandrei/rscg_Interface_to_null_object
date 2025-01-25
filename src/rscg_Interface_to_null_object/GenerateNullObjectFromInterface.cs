using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;

namespace rscg_Interface_to_null_object;
[Generator]
public class Interface2NullObject: IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var emitReturns = context.AnalyzerConfigOptionsProvider.Select((provider, ct) =>
        {
            var lst = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            foreach (var item in provider.GlobalOptions.Keys)
            {
                if (item.StartsWith("build_property.I2NO",StringComparison.InvariantCultureIgnoreCase))
                {
                    var key = item.Substring("build_property.I2NO".Length);
                    if(key.StartsWith("_")) key = key.Substring(1);
                    if (provider.GlobalOptions.TryGetValue(item, out var val))
                    {
                        lst[key] = val;
                    }
                }
            }
            return lst;
        });

        

        var classesToApplySortable = context.SyntaxProvider.ForAttributeWithMetadataName(
"InterfaceToNullObject.ToNullObjectAttribute",
IsAppliedOnInterface,
FindAttributeDataExpose
)
.Collect()
.SelectMany((data, _) => data.Distinct())
.Collect()
;

        var dataToInterpret=classesToApplySortable.Combine(emitReturns);

        context.RegisterSourceOutput(dataToInterpret,
(spc, data) =>
ExecuteSortGeneric(spc, data));

    }

    private void ExecuteSortGeneric(SourceProductionContext spc, (ImmutableArray<DataFromExposeInterface?> Left, Dictionary<string, string> Right) data)
    {
        ExecuteSort(spc, data.Left, data.Right);
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
    private void ExecuteSort(SourceProductionContext spc, ImmutableArray<DataFromExposeInterface?> dataFromExposeClasses, Dictionary<string, string> defMethodReturns)
    {
        var arr = dataFromExposeClasses.ToArray()
            .Where(it => it != null)
            .Select(it => it!)
            .ToArray();
        foreach (var classData in arr)
        {
            classData.DefMethodReturns = defMethodReturns;
            var c = new NullObjectTemplate(classData);
            var text = c.Render();
            spc.AddSource($"{classData.Name}_null.cs", text);
        }
    }

}
