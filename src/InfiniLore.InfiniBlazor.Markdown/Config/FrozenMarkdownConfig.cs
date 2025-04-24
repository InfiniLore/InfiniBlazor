// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;
using System.Collections.Frozen;
using System.Collections.Immutable;

namespace InfiniLore.InfiniBlazor.Markdown.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class FrozenMarkdownConfig : IMarkdownConfig {
    public required ImmutableArray<string> TextEditorModifierNames { get; init; }
    public required FrozenDictionary<Type, ImmutableArray<object>> ParserPostProcessors { get; init; } 
    public required FrozenDictionary<Type, ImmutableArray<object>> ParserPreProcessors { get; init; } 
    
    // -----------------------------------------------------------------------------------------------------------------
    // Constructor
    // -----------------------------------------------------------------------------------------------------------------
    public ImmutableArray<IMarkdownPreProcessor<T>> GetPreProcessors<T>() {
        if (ParserPreProcessors.TryGetValue(typeof(T), out ImmutableArray<object> preProcessors)) {
            return preProcessors.Cast<IMarkdownPreProcessor<T>>().ToImmutableArray();
        }

        return ImmutableArray<IMarkdownPreProcessor<T>>.Empty;
    }
    
    public ImmutableArray<IMarkdownPostProcessor<T>> GetPostProcessors<T>() {
        if (ParserPostProcessors.TryGetValue(typeof(T), out ImmutableArray<object> postProcessors)) {
            return postProcessors.Cast<IMarkdownPostProcessor<T>>().ToImmutableArray();
        }
        return ImmutableArray<IMarkdownPostProcessor<T>>.Empty;
    }
}
