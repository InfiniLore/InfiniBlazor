// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Extensions.AutoDocumentation;

namespace InfiniLore.InfiniBlazor.AutoDocumentation;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class InfiniBlazorConfigExtensions {
    public static InfiniBlazorConfig AddAutoDocumentation(this InfiniBlazorConfig config, Action<AutoDocumentationConfig>? configure = null) {
        config.Services.RegisterServicesFromInfiniLoreInfiniBlazorExtensionsAutoDocumentation();
        
        var autoDocumentationConfig = new AutoDocumentationConfig(config);
        
        configure?.Invoke(autoDocumentationConfig);
        return config;
    }
}
