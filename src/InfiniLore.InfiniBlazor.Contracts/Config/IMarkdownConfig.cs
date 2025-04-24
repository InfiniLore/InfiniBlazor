// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using System.Collections.Immutable;

namespace InfiniLore.InfiniBlazor.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMarkdownConfig {
    ImmutableArray<string> TextEditorModifierNames { get; }
    public ImmutableArray<IMarkdownPreProcessor<T>> GetPreProcessors<T>();
    public ImmutableArray<IMarkdownPostProcessor<T>> GetPostProcessors<T>();
}
