// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;

namespace InfiniLore.InfiniBlazor.Markdown.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MarkdownParserConfig(IInfiniBlazorConfig config) {
    public Dictionary<Type, List<Type>> PreProcessors { get; } = new();
    public Dictionary<Type, List<Type>> PostProcessors { get; } = new();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public MarkdownParserConfig AddPreProcessor<TProcessor, TInput>() where TProcessor : IMarkdownPreProcessor<TInput> {
        if(PreProcessors.TryGetValue(typeof(TInput), out List<Type>? list)) list.Add(typeof(TProcessor));
        else PreProcessors.Add(typeof(TInput), new List<Type> { typeof(TProcessor) });
        return this;
    }
    
    public MarkdownParserConfig AddPostProcessor<TProcessor, TOutput>() where TProcessor : IMarkdownPostProcessor<TOutput> {
        if(PostProcessors.TryGetValue(typeof(TOutput), out List<Type>? list)) list.Add(typeof(TProcessor));
        else PostProcessors.Add(typeof(TOutput), new List<Type> { typeof(TProcessor) });
        return this;
    }
}
