// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace InfiniLore.InfiniBlazor.Theming.Generators.Helpers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class PropertyNameComparer : IEqualityComparer<IPropertySymbol> {
    public static PropertyNameComparer Default { get; } = new();
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool Equals(IPropertySymbol? x, IPropertySymbol? y) {
        if (ReferenceEquals(x, y)) return true;
        if (x is null || y is null) return false;
        return x.Name == y.Name;
    }

    public int GetHashCode(IPropertySymbol obj) => obj.Name.GetHashCode();
}
