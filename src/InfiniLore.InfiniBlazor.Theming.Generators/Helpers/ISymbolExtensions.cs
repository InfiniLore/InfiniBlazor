// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once CheckNamespace
namespace Microsoft.CodeAnalysis;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ISymbolExtensions {
    public static bool IsDisplayName<TSymbol>(this TSymbol? symbol, string expected) where TSymbol : ISymbol {
        return symbol?.ToDisplayString() == expected;
    }
}
