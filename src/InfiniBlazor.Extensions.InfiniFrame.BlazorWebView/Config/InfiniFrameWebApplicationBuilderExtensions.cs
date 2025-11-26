// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor;
using InfiniBlazor.Config;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace InfiniFrame.BlazorWebView;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class InfiniFrameBlazorAppBuilderExtensions {
    public static InfiniFrameBlazorAppBuilder AddInfiniBlazor(this InfiniFrameBlazorAppBuilder builder, Action<InfiniBlazorConfig>? configure = null) {
        builder.Services.AddInfiniBlazor(config => {
            configure?.Invoke(config);
        });
        
        // The same thing that MAUI has, the webview overtakes it as an Interactive WebView
        InfiniRenderModeProvider.InfiniRenderMode = null!;
        
        return builder;
    }
}
