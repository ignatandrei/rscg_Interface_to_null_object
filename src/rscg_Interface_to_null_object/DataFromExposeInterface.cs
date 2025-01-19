
using Microsoft.CodeAnalysis;

namespace rscg_Interface_to_null_object;
internal class DataFromExposeInterface
{
    public string Name { get; private set; }

    internal IPropertySymbol[] props;
    public IMethodSymbol[] functions;
    //private INamedTypeSymbol type;
    public string DisplayFunc(IMethodSymbol methodSymbol)
    {
        //string IEmployee.GetFullName()
        var sdf=new  SymbolDisplayFormat(
                globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Included,
                genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
                memberOptions:
                    SymbolDisplayMemberOptions.IncludeParameters |
                    SymbolDisplayMemberOptions.IncludeType |
                    SymbolDisplayMemberOptions.IncludeRef |
                    SymbolDisplayMemberOptions.IncludeContainingType,
                kindOptions:
                    SymbolDisplayKindOptions.IncludeMemberKeyword,
                parameterOptions:
                    SymbolDisplayParameterOptions.IncludeName |
                    SymbolDisplayParameterOptions.IncludeType |
                    SymbolDisplayParameterOptions.IncludeParamsRefOut |
                    SymbolDisplayParameterOptions.IncludeDefaultValue,
                localOptions: SymbolDisplayLocalOptions.IncludeType,
                miscellaneousOptions:
                    SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers |
                    SymbolDisplayMiscellaneousOptions.UseSpecialTypes |
                    SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier);

        var str = methodSymbol.ToDisplayString(sdf);
        //remove the interface name that can appear before arguments
        
        int dot = 0;
        do
        {
            var index = str.IndexOf('(');
            dot = str.IndexOf('.');
            if (dot < index && dot > 0) str = str.Substring(dot + 1);

        } while (dot > 0);
        

        return str;
    }
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
