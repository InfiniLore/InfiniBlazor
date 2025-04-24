// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Markdown.Config;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MarkdownConfig(IInfiniBlazorConfig infiniBlazorConfig) {
    public MarkdownTextEditorConfig TextEditor { get; } = new(infiniBlazorConfig);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public MarkdownParserConfig<TInput, TOutput> AddMarkdownParser<TInput, TOutput>(object? key = null)
        where TInput : class 
        where TOutput : class
    {
        var markdownParserConfig = new MarkdownParserConfig<TInput, TOutput>(infiniBlazorConfig);

        if (key is null) infiniBlazorConfig.Services.AddSingleton<IMarkdownParser<TInput, TOutput>, MarkdownParser<TInput, TOutput>>();
        else infiniBlazorConfig.Services.AddKeyedSingleton<IMarkdownParser<TInput, TOutput>, MarkdownParser<TInput, TOutput>>(key);

        return markdownParserConfig;
    }
}
