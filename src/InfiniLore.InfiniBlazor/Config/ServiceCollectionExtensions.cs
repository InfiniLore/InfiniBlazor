// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor;
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Theming.Config;
using InfiniLore.InfiniBlazor.Toasting.Config;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ServiceCollectionExtensions {
    public static IServiceCollection AddInfiniBlazor(this IServiceCollection services, Action<IInfiniBlazorConfig>? configure = null) {
        var config = new InfiniBlazorConfig(services);
        services.RegisterServicesFromInfiniLoreInfiniBlazor();
        services.AddLucideIcons();
        config.AddThemingLogic(); // Adds the basics for Theming
        config.AddToastingLogic(); // Adds the basics for Toasting

        configure?.Invoke(config);

        return services;
    }
}
