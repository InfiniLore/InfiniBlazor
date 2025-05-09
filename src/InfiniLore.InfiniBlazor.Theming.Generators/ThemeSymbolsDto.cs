// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace InfiniLore.InfiniBlazor.Theming.Generators;
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
    public ImmutableArray<IPropertySymbol> GetProperties(Compilation compilation) {
        if (_propertiesInitialized) return _properties;
        
        IThemeProperties.TryExtractProperties(compilation, out ImmutableArray<IPropertySymbol> iThemeProperties); 
        
        IEnumerable<IPropertySymbol> currentSymbolProperties = Symbol.GetMembers()
            .OfType<IPropertySymbol>()
            .Where(symbol => symbol.HasAttributeWithDisplayName(TypeNames.IncludeAsCssVariableAttribute));
        
        _properties = iThemeProperties.Concat(currentSymbolProperties)
            .ToImmutableArray();
        _propertiesInitialized = true;
        return _properties;
    }
}
