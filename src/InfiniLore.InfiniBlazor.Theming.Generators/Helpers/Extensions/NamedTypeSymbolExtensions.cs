// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace InfiniLore.InfiniBlazor.Theming.Generators.Helpers.Extensions;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class NamedTypeSymbolExtensions {
    public static bool InheritsFrom(this INamedTypeSymbol symbol, INamedTypeSymbol baseType) {
        Stack<INamedTypeSymbol> stack = new();
        stack.Push(symbol);
        
        while (stack.TryPop(out INamedTypeSymbol? currentSymbol)) {
            if (SymbolEqualityComparer.Default.Equals(currentSymbol, baseType)) return true;
            foreach (INamedTypeSymbol @interface in currentSymbol.AllInterfaces) stack.Push(@interface);
            if (currentSymbol.BaseType == null) continue;
            stack.Push(currentSymbol.BaseType);
        }
        return false;
    }
}
