// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Core.Theming;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace InfiniLore.InfiniBlazor.Theming.Config;

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
        serviceCollection.RegisterServicesFromInfiniLoreInfiniBlazorCoreTheming();
        serviceCollection.AddSingleton<IThemingConfig>(this);

        RegisterThemeResource(new ThemeResource(
            ThemeResourceLocation.EmbeddedResource,
            "InfiniLore.InfiniBlazor.Theming.wwwroot.theme_default.json"
        ));
        RegisterThemeResource(new ThemeResource(
            ThemeResourceLocation.EmbeddedResource, 
            "InfiniLore.InfiniBlazor.Theming.wwwroot.theme_pride.json"
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
