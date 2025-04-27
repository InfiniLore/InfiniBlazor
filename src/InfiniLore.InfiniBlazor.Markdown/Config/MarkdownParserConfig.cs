// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Markdown.Processors;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Markdown.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MarkdownParserConfig<TInput, TOutput>(IInfiniBlazorConfig config) {
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public MarkdownParserConfig<TInput, TOutput> AddInputProcessor<TProcessor>() 
        where TProcessor : class, IMarkdownInputProcessor<TInput> 
    {
        config.Services.AddSingleton<IMarkdownInputProcessor<TInput>, TProcessor>();
        return this;
    }
    
    public MarkdownParserConfig<TInput, TOutput> AddOutputProcessor<TProcessor>()
        where TProcessor : class, IMarkdownOutputProcessor<TOutput> 
    {
        config.Services.AddSingleton<IMarkdownOutputProcessor<TOutput>, TProcessor>();
        return this;
    }
}
