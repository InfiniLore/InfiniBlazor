// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Linq;

namespace InfiniLore.InfiniBlazor.Theming.Generators;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class IThemeProperties {
    private static ImmutableArray<IPropertySymbol> Properties { get; set; } = ImmutableArray<IPropertySymbol>.Empty;
    private static bool IsInitialized { get; set; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static bool TryExtractroperties(Compilation compilation, out ImmutableArray<IPropertySymbol> iThemeProperties) {
        if (IsInitialized) {
            iThemeProperties = Properties;
            return true;
        }

        INamedTypeSymbol? iThemeSymbol = compilation.GetTypeByMetadataName(TypeNames.IThemeInterfaceFullName);
        if (iThemeSymbol is null) {
            Properties = ImmutableArray<IPropertySymbol>.Empty;
            IsInitialized = true;
            return false;
        }

        iThemeProperties = iThemeSymbol.GetMembers()
            .OfType<IPropertySymbol>()
            .Where(symbol => symbol.GetAttributes().Any(static attr => attr.AttributeClass?.ToDisplayString() == TypeNames.IncludeAsCssVariableAttributeFullName))
            .ToImmutableArray();

        Properties = iThemeProperties;
        IsInitialized = true;
        return true;
    }
}
