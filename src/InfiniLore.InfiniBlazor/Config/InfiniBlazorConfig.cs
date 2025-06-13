// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Theming;
using InfiniLore.InfiniBlazor.Theming.Collections;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Config;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniBlazorConfig(IServiceCollection collection) {
    public IServiceCollection Services { get; } = collection;
    
    internal Dictionary<string, IThemeCollection> RegisteredBaseThemeCollections { get; } = new();
    internal ThemeMode DefaultThemeMode { get; private set; } = ThemeMode.DarkMode;
    internal string DefaultThemeCollectionName { get; private set; } = DefaultThemeCollection.Name;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public InfiniBlazorConfig RegisterTheme<TTheme>(string themeName) where TTheme : class, IThemeCollection, new() {
        RegisteredBaseThemeCollections.AddOrUpdate(themeName, new TTheme());
        return this;
    }

    public InfiniBlazorConfig RegisterTheme(IThemeCollection themeType, string themeName) {
        RegisteredBaseThemeCollections.AddOrUpdate(themeName, themeType);
        return this;
    }
    
    public InfiniBlazorConfig SetDefaultThemeMode(ThemeMode mode) {
        if (mode.Variant == ThemeModeVariants.Undefined) return this;
        DefaultThemeMode = mode;
        return this;
    }
    
    public InfiniBlazorConfig SetDefaultThemeCollectionName(string modeName) {
        if (modeName.IsNullOrWhiteSpace()) return this;
        DefaultThemeCollectionName = modeName;
        return this;
    }

    public InfiniBlazorConfig RegisterExternalThemeCollectionProvider<TProvider>() where TProvider : class, IExternalThemeCollectionProvider {
        Services.AddScoped<IExternalThemeCollectionProvider, TProvider>();
        return this;
    }
}
