// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace
namespace Microsoft.CodeAnalysis;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class INamedTypeSymbolExtensions {
    public static string GetTypeKind(this INamedTypeSymbol symbol) {
        return symbol switch {
            {IsRecord: true} => "record",
            {TypeKind : TypeKind.Class} => "class",
            {TypeKind : TypeKind.Struct} => "struct",
            {TypeKind : TypeKind.Interface} => "interface",
            _ => symbol.TypeKind.ToString().ToLowerInvariant()
        };
    }
}
