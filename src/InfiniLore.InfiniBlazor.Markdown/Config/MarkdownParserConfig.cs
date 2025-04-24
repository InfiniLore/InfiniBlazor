// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Markdown.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MarkdownParserConfig<TInput, TOutput>(IInfiniBlazorConfig config) {
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public MarkdownParserConfig<TInput, TOutput> AddPreProcessor<TProcessor>() 
        where TProcessor : class, IMarkdownPreProcessor<TInput> 
    {
        config.Services.AddSingleton<IMarkdownPreProcessor<TInput>, TProcessor>();
        return this;
    }
    
    public MarkdownParserConfig<TInput, TOutput> AddPostProcessor<TProcessor>()
        where TProcessor : class, IMarkdownPostProcessor<TOutput> 
    {
        config.Services.AddSingleton<IMarkdownPostProcessor<TOutput>, TProcessor>();
        return this;
    }
}
