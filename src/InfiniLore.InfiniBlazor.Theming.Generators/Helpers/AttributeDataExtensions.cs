// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once CheckNamespace
namespace Microsoft.CodeAnalysis;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class AttributeDataExtensions {
    public static bool IsDisplayName(this AttributeData attribute, string expected) {
        return attribute.AttributeClass?.IsDisplayName(expected) ?? false;
    }
}
