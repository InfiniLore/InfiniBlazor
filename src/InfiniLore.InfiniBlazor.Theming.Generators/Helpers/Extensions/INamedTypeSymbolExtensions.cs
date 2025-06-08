// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Theming.Generators.Helpers.Extensions;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[SuppressMessage("ReSharper", "InconsistentNaming")]
public static class INamedTypeSymbolExtensions {
    public static string GetTypeKind(this INamedTypeSymbol symbol) {
        return symbol switch {
            { IsRecord: true, TypeKind: TypeKind.Struct } => "record struct",
            { IsRecord: true, TypeKind: TypeKind.Class } => "record",
            { TypeKind: TypeKind.Class } => "class",
            { TypeKind: TypeKind.Struct } => "struct",
            { TypeKind: TypeKind.Interface } => "interface",
            _ => symbol.TypeKind.ToString().ToLowerInvariant()
        };
    }
}
