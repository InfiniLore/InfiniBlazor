// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.HtmlRendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text;

namespace InfiniLore.InfiniBlazor.DynamicMdComponents;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class DynamicMdRenderer {
    public static async Task<string> RenderComponentToStringAsync<TComponent>(
        IServiceProvider serviceProvider,
        IDictionary<string, object>? parameters = null
    ) where TComponent : IComponent {
        // Instantiate an HtmlRenderer from the DI container.
        var renderer = new HtmlRenderer(serviceProvider, serviceProvider.GetRequiredService<ILoggerFactory>());

        // Convert the dictionary of parameters to a ParameterView
        ParameterView parameterView = parameters is not null
            ? ParameterView.FromDictionary(parameters!)
            : ParameterView.Empty;

        await using var writer = new StringWriter(new StringBuilder());

        // Render the component asynchronously and write output to the writer
        Type component = typeof(TComponent);
        await renderer.Dispatcher.InvokeAsync(async () => {
            HtmlRootComponent result = await renderer.RenderComponentAsync(component, parameterView);
            result.WriteHtmlTo(writer);
        });

        return writer.ToString();
    }
}
