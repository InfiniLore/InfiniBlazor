// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor;
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Markdown.Config;
using InfiniLore.InfiniBlazor.Theming.Config;

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

        configure?.Invoke(config);

        return services;
    }

    public static IServiceCollection AddInfiniBlazor(
        this IServiceCollection services,
        Action<MarkdownConfig> configureMarkdown,
        Action<ThemeConfig> configureTheming
    ) {
        return services.AddInfiniBlazor(config => {
                config.AddMarkdown(configureMarkdown);
                config.AddTheming(configureTheming);
            }
        );
    }
}
