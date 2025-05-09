// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Collections.Immutable;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace Microsoft.CodeAnalysis;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ISymbolExtensions {
    public static bool IsDisplayName<TSymbol>(this TSymbol? symbol, string expected) where TSymbol : ISymbol {
        return symbol?.ToDisplayString() == expected;
    }

    public static bool HasAttributeWithDisplayName<TSymbol>(this TSymbol symbol, string expectedName) where TSymbol : ISymbol {
        ImmutableArray<AttributeData> attributes = symbol.GetAttributes();
        if (attributes.Length == 0) return false;
        
        return attributes.Any(attr => attr.IsDisplayName(expectedName));
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
