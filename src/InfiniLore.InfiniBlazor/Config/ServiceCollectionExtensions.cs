// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor;
using InfiniLore.InfiniBlazor.Callouts;
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Markdown.Parsers.Blazor;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer;
using InfiniLore.InfiniBlazor.Toasting;
using System.Collections.Frozen;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ServiceCollectionExtensions {
    public static IServiceCollection AddInfiniBlazor(this IServiceCollection services, Action<InfiniBlazorConfig>? configure = null) {
        var config = new InfiniBlazorConfig(services);
        services.RegisterServicesFromInfiniLoreInfiniBlazor();
        services.AddLucideIcons();

        // config.RegisterTheme<DefaultThemeCollection>();
        
        services.AddSingleton(InfiniBlazorMdComponentConverterFactory.Create);
        services.AddSingleton(CalloutStyleProviderFactory.Create);
        services.AddSingleton(MdStringMdSyntaxDeserializerFactory.Create);
        
        configure?.Invoke(config);
        
        // // Add all added stuff after the config is done
        // services.AddSingleton<IThemingConfig>(new FrozenThemingConfig {
        //     RegisteredBaseThemes = config.RegisteredBaseThemeCollections.ToFrozenDictionary(),
        //     DefaultThemeCollectionName = config.DefaultThemeCollectionName,
        //     DefaultThemeMode = config.DefaultThemeMode,
        // });

        services.AddSingleton<IToastingConfig>(new FrozenToastingConfig {
            AutoRemoveDuration = config.ToastDefaultDuration,
            AppearanceComponentMapping = config.ToastAppearanceComponentMappings.ToFrozenDictionary(),
        });

        return services;
    }
}
