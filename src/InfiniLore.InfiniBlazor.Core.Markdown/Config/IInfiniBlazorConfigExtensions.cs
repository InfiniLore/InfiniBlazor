// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ServiceCollectionExtensions {
    public static IServiceCollection AddInfiniBlazorMarkdown(this IServiceCollection serviceCollection, Action<InfiniBlazorMarkdownConfig>? configure = null) {
        var themingConfig = new InfiniBlazorMarkdownConfig(serviceCollection);
        
        configure?.Invoke(themingConfig);
        
        return serviceCollection;
    }
}

