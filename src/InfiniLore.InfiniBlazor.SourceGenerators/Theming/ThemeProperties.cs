// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.GeneratorTools;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace InfiniLore.InfiniBlazor.SourceGenerators.Theming;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ThemeProperties {
    private static ImmutableArray<IPropertySymbol> Properties { get; set; } = ImmutableArray<IPropertySymbol>.Empty;
    private static bool IsInitialized { get; set; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static bool TryExtractProperties(Compilation compilation, out ImmutableArray<IPropertySymbol> themeProperties) {
        if (IsInitialized) {
            themeProperties = Properties;
            return true;
        }

        INamedTypeSymbol? themeSymbol = compilation.GetTypeByMetadataName(TypeNames.CssDataInterface);
        if (themeSymbol is null) {
            Properties = ImmutableArray<IPropertySymbol>.Empty;
            IsInitialized = true;
            return false;
        }

        themeProperties = themeSymbol.GetMembers()
            .OfType<IPropertySymbol>()
            .Where(static symbol => symbol.HasAttributeWithDisplayName(TypeNames.CssDataAttribute))
            .ToImmutableArray();

        Properties = themeProperties;
        IsInitialized = true;
        return true;
    }
}
