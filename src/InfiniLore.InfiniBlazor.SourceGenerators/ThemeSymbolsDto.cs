// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.GeneratorTools;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace InfiniLore.InfiniBlazor.SourceGenerators;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record ThemeSymbolsDto(
    INamedTypeSymbol Symbol
) {
    public string ClassName { get; } = Symbol.Name;
    public string NameSpace { get; } = Symbol.ContainingNamespace.ToDisplayString();
    public string Accessibility { get; } = Symbol.GetAccessibility();
    public string TypeKeyword { get; } = Symbol.GetTypeKind();

    public bool GenerateVariableStorage { get; } = Symbol.HasAttributeWithDisplayName(TypeNames.GenerateVariableNamesAttribute);
    
    private ImmutableArray<IPropertySymbol> _properties = ImmutableArray<IPropertySymbol>.Empty;
    private bool _propertiesInitialized;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public ImmutableArray<IPropertySymbol> GetCssDataProperties(Compilation compilation) {
        if (_propertiesInitialized) return _properties;
        
        ThemeProperties.TryExtractProperties(compilation, out ImmutableArray<IPropertySymbol> iThemeProperties); 
        
        IEnumerable<IPropertySymbol> currentSymbolProperties = Symbol.GetMembers()
            .OfType<IPropertySymbol>()
            .Where(symbol => symbol.HasAttributeWithDisplayName(TypeNames.CssDataAttribute));
        
        _properties = iThemeProperties
            .Concat(currentSymbolProperties)
            .GroupBy(p => p.Name)
            .Select(g => g.Last())  
            .ToImmutableArray();

        _propertiesInitialized = true;
        return _properties;
    }
}
