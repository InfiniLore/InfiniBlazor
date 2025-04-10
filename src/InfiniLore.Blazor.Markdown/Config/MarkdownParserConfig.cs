// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Ganss.Xss;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.Blazor.Markdown.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MarkdownParserConfig(IInfiniLoreBlazorConfig config) {
    public MarkdownParserConfig AddDefaultSanitizer() {
        var sanitizer = new HtmlSanitizer();
        
        config.Services.AddSingleton<IHtmlSanitizer>(sanitizer);
        return this;
    }
}
