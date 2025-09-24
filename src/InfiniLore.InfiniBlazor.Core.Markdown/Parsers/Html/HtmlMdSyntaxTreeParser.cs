// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Parsers.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.HtmlRendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Html;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IHtmlMdSyntaxTreeParser>]
public class HtmlMdSyntaxTreeParser(IServiceProvider provider, ILoggerFactory loggerFactory) : IHtmlMdSyntaxTreeParser {
    public async Task<string> DeserializeToStringAsync(IMdSyntaxTree tree, CancellationToken ct = default) {
        await using AsyncServiceScope scope = provider.CreateAsyncScope();
        await using var htmlRenderer = new HtmlRenderer(scope.ServiceProvider, loggerFactory);
        string output = await htmlRenderer.Dispatcher.InvokeAsync(async () => {
            await using var textWriter = new StringWriter();
            
            var parameters = new Dictionary<string, object?> {
                [nameof(HtmlMdSyntaxTreeParserRoot.SyntaxTree)] = tree
            };
            
            HtmlRootComponent output = await htmlRenderer.RenderComponentAsync<HtmlMdSyntaxTreeParserRoot>(ParameterView.FromDictionary(parameters));
            output.WriteHtmlTo(textWriter);
            return textWriter.ToString();
        });
        return output;
    }
}
