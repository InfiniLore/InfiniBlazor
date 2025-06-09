// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------s
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Linq;

namespace InfiniLore.InfiniBlazor.Theming.Generators.Helpers.Extensions;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once InconsistentNaming
public static class ISymbolExtensions {
    public static bool IsDisplayName<TSymbol>(this TSymbol? symbol, string expected) where TSymbol : ISymbol {
        if (symbol == null) return false;
        return symbol.ToDisplayString() == expected;
    }

    public static bool HasAttributeWithDisplayName<TSymbol>(this TSymbol symbol, string expectedName) where TSymbol : ISymbol {
        ImmutableArray<AttributeData> attributes = symbol.GetAttributes();
        if (attributes.Length == 0) return false;
        
        return attributes.Any(attr => attr.IsDisplayName(expectedName));
    }

    public static AttributeData? GetAttributeWithDisplayName(this ISymbol? symbol, string expectedName) {
        return symbol?.GetAttributes().FirstOrDefault(attribute => attribute.IsDisplayName(expectedName));
    }
    
    public static string GetAccessibility(this ISymbol symbol) {
        return symbol.DeclaredAccessibility switch {
            Accessibility.Private => "private",
            Accessibility.Protected => "protected",
            Accessibility.Internal => "internal",
            Accessibility.Public => "public",
            _ => "UNKNOWN"
        };
    }
}
