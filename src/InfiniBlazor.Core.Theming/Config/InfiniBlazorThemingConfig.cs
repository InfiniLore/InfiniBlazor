// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Config;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace InfiniBlazor.Theming.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniBlazorThemingConfig : IThemingConfig {
    private ConcurrentBag<ThemeResource> ThemeResources { get; } = new();
    public (string CollectionName, string ThemeName) DefaultThemeSelection { get; private set; } = ("Default", "Dark");
    public bool ThrowOnBrokenInitialization { get; set; } = true;
    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public InfiniBlazorThemingConfig(IServiceCollection serviceCollection) {
        serviceCollection.RegisterServicesFromInfiniBlazorCoreTheming();
        serviceCollection.AddSingleton<IThemingConfig>(this);

        RegisterThemeResource(new ThemeResource(
            ThemeResourceLocation.EmbeddedResource,
            "InfiniBlazor.Theming.wwwroot.theme_default.json"
        ));
        RegisterThemeResource(new ThemeResource(
            ThemeResourceLocation.EmbeddedResource, 
            "InfiniBlazor.Theming.wwwroot.theme_pride.json"
        ));
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public InfiniBlazorThemingConfig RegisterThemeResource(ThemeResource themeResource) {
        ThemeResources.Add(themeResource);
        return this;
    }
    
    public InfiniBlazorThemingConfig SetDefaultTheme(string collectionName, string themeName) {
        DefaultThemeSelection = (collectionName, themeName);
        return this;
    }
    
    public ThemeResource[] GetThemeResources() => ThemeResources.ToArray();
}
