// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniBlazor.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ServiceCollectionExtensions {
    
    // Used for the standalone version of the library
    [UsedImplicitly]
    public static IServiceCollection AddInfiniBlazorMarkdown(this IServiceCollection serviceCollection, Action<InfiniBlazorMarkdownConfig>? configure = null) {
        var themingConfig = new InfiniBlazorMarkdownConfig(serviceCollection);
        
        configure?.Invoke(themingConfig);
        
        return serviceCollection;
    }
}

