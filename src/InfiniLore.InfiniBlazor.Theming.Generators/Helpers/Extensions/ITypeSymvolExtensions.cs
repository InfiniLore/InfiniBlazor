// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using System.Linq;

namespace CodeOfChaos.GeneratorTools;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ITypeSymbolExtensions {
    public static bool HasInterfaceWithDisplayName<TSymbol>(this TSymbol symbol, string displayName) where TSymbol : ITypeSymbol {
        return symbol.AllInterfaces.Any(i => i.IsDisplayName(displayName));
    }
}
