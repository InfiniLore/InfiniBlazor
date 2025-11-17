// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.DependencyInjection;

namespace InfiniBlazor.Theming.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ServiceCollectionExtensions {
    public static IServiceCollection AddInfiniBlazorTheming(this IServiceCollection serviceCollection, Action<InfiniBlazorThemingConfig>? configure = null) {
        var themingConfig = new InfiniBlazorThemingConfig(serviceCollection);
        
        configure?.Invoke(themingConfig);
        
        return serviceCollection;
    }
}
