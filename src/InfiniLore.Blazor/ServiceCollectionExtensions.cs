// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.Blazor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ServiceCollectionExtensions {
    public static IServiceCollection AddInfiniLoreBlazor(this IServiceCollection services, Action<IInfiniLoreBlazorConfig>? configure = null) {
        var config = new InfiniLoreBlazorConfig();
        configure?.Invoke(config);
        
        return services;
    }
}
