// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Parsers.Blazor;
using System.Collections.Frozen;

namespace InfiniLore.InfiniBlazor;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMarkdownConfig {
    FrozenDictionary<Type, IMdComponentRecord> GetComponentRecords();
    FrozenSet<Type> GetSkippedBlazorComponentTypes();
    bool RenderUnknownBlazorComponents { get; }
}
