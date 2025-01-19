
using Microsoft.CodeAnalysis;

namespace rscg_Interface_to_null_object;
internal class DataFromExposeInterface
{
    public string Name { get; private set; }

    internal IPropertySymbol[] props;
    public IMethodSymbol[] functions;
    //private INamedTypeSymbol type;

    public string FullName { get; private set; }
    public DataFromExposeInterface(INamedTypeSymbol type)
    {
        //this.type = type;
        FullName = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        Name = type.Name;
        props = type.GetMembers()
                .OfType<IPropertySymbol>()
                .ToArray();
        functions = type.GetMembers()
                .OfType<IMethodSymbol>()
                .Where(x => x.MethodKind == MethodKind.Ordinary)
                .ToArray();
    }
}
