// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Config;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace InfiniFrame.WebServer;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class InfiniFrameWebApplicationBuilderExtensions {
    public static InfiniFrameWebApplicationBuilder AddInfiniBlazor(this InfiniFrameWebApplicationBuilder builder, Action<InfiniBlazorConfig>? configure = null) {
        builder.WebApp.Services.AddInfiniBlazor(config => {
            configure?.Invoke(config);
        });
        return builder;
    }
}
