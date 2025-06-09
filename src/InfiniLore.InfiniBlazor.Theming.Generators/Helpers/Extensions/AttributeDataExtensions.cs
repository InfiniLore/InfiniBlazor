// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Theming.Generators.Helpers.Extensions;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class AttributeDataExtensions {
    public static bool IsDisplayName(this AttributeData attribute, string expected) {
        return attribute.AttributeClass?.IsDisplayName(expected) ?? false;
    }
}
