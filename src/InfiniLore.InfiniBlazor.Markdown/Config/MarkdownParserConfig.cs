// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Markdown.PostProcessors;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Markdown.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MarkdownParserConfig(IInfiniBlazorConfig config) {
    public MarkdownParserConfig AddSanitizerPostProcessor() {
        config.Services.AddSingleton<IMarkdownPostProcessor<string>, SanitizerPostProcessor>();
        return this;
    }
}
