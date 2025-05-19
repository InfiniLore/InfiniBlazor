// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.GeneratorTools;
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
    public static bool TryExtractProperties(Compilation compilation, out ImmutableArray<IPropertySymbol> iThemeProperties) {
        if (IsInitialized) {
            iThemeProperties = Properties;
            return true;
        }

        INamedTypeSymbol? iThemeSymbol = compilation.GetTypeByMetadataName(TypeNames.IThemeInterface);
        if (iThemeSymbol is null) {
            Properties = ImmutableArray<IPropertySymbol>.Empty;
            IsInitialized = true;
            return false;
        }

        iThemeProperties = iThemeSymbol.GetMembers()
            .OfType<IPropertySymbol>()
            .Where(static symbol => symbol.HasAttributeWithDisplayName(TypeNames.CssDataAttribute))
            .ToImmutableArray();

        Properties = iThemeProperties;
        IsInitialized = true;
        return true;
    }
}
