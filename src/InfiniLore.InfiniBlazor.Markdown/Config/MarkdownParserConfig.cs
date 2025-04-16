// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Ganss.Xss;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Markdown.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MarkdownParserConfig(IInfiniBlazorConfig config) {
    public MarkdownParserConfig AddDefaultSanitizer() {
        var sanitizer = new HtmlSanitizer();
        
        config.Services.AddSingleton<IHtmlSanitizer>(sanitizer);
        return this;
    }
}
