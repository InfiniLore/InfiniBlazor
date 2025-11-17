// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Parsers.Blazor;
using System.Collections.Frozen;

namespace InfiniBlazor.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMarkdownConfig {
    FrozenDictionary<Type, IMdComponentRecord> GetComponentRecords();
    FrozenSet<Type> GetSkippedBlazorComponentTypes();
    bool RenderUnknownBlazorComponents { get; }
    Type? HtmlRendererFootnoteWrapperType { get; }
}
