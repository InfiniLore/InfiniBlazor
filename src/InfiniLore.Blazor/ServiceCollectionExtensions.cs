// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Blazor;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ServiceCollectionExtensions {
    public static IServiceCollection AddInfiniLoreBlazor(this IServiceCollection services, Action<IInfiniLoreBlazorConfig>? configure = null) {
        var config = new InfiniLoreBlazorConfig(services);
        services.RegisterServicesFromInfiniLoreBlazor();
        
        configure?.Invoke(config);
        
        return services;
    }
}
