// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Config;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniBlazor.AutoDocumentation;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class InfiniBlazorConfigExtensions {
    public static InfiniBlazorConfig AddAutoDocumentation(this InfiniBlazorConfig config, Action<AutoDocumentationConfig>? configure = null) {
        config.Services.RegisterServicesFromInfiniBlazorExtensionsAutoDocumentation();
        
        var autoDocumentationConfig = new AutoDocumentationConfig(config);
        
        configure?.Invoke(autoDocumentationConfig);
        return config;
    }
}
