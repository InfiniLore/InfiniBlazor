// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Processors;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MarkdownParserConfig<TInput, TOutput>(InfiniBlazorConfig config, object? key) {
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public MarkdownParserConfig<TInput, TOutput> AddInputProcessor<TProcessor>() 
        where TProcessor : class, IMarkdownInputProcessor<TInput> 
    {
        config.Services.AddKeyedSingleton<IMarkdownInputProcessor<TInput>, TProcessor>(key);
        return this;
    }
    
    public MarkdownParserConfig<TInput, TOutput> AddPostProcessor<TProcessor>()
        where TProcessor : class, IMarkdownPostProcessor<TInput> 
    {
        config.Services.AddKeyedSingleton<IMarkdownPostProcessor<TInput>, TProcessor>(key);
        return this;
    }
    
    public MarkdownParserConfig<TInput, TOutput> AddOutputProcessor<TProcessor>()
        where TProcessor : class, IMarkdownOutputProcessor<TOutput> 
    {
        config.Services.AddKeyedSingleton<IMarkdownOutputProcessor<TOutput>, TProcessor>(key);
        return this;
    }
}
