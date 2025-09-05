// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Core.Theming;
using InfiniLore.InfiniBlazor.Theming.ThemeCollections;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Frozen;

namespace InfiniLore.InfiniBlazor.Theming.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniBlazorThemingConfig : IThemingConfig {
    private readonly IServiceCollection _serviceCollection;
    private Dictionary<string, IThemeCollection> RegisteredBaseThemeCollections { get; } = new(4);
    public ThemeMode DefaultThemeMode { get; private set; } = ThemeMode.DarkMode;
    public string DefaultThemeCollectionName { get; private set; } = DefaultThemeCollection.Name;
    
    private Lazy<FrozenDictionary<string, IThemeCollection>> RegisteredBaseThemeCollectionsLazy { get; }

    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public InfiniBlazorThemingConfig(IServiceCollection serviceCollection) {
        _serviceCollection = serviceCollection;
        serviceCollection.RegisterServicesFromInfiniLoreInfiniBlazorCoreTheming();
        
        RegisteredBaseThemeCollectionsLazy = new Lazy<FrozenDictionary<string, IThemeCollection>>(() => {
            RegisteredBaseThemeCollections.TrimExcess();
            return RegisteredBaseThemeCollections.ToFrozenDictionary(
                pair => pair.Key, 
                IThemeCollection (pair) => pair.Value);
        });
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public InfiniBlazorThemingConfig RegisterTheme<TTheme>() where TTheme : class, IThemeCollection, new() {
        var theme = new TTheme();
        RegisteredBaseThemeCollections.AddOrUpdate(theme.CollectionName, theme);
        return this;
    }

    public InfiniBlazorThemingConfig SetDefaultThemeMode(ThemeMode mode) {
        if (mode.Variant == ThemeModeVariants.Undefined) return this;

        DefaultThemeMode = mode;
        return this;
    }

    public InfiniBlazorThemingConfig SetDefaultThemeCollectionName(string modeName) {
        if (modeName.IsNullOrWhiteSpace()) return this;

        DefaultThemeCollectionName = modeName;
        return this;
    }

    public InfiniBlazorThemingConfig RegisterExternalThemeCollectionProvider<TProvider>() where TProvider : class, IExternalThemeCollectionProvider {
        _serviceCollection.AddScoped<IExternalThemeCollectionProvider, TProvider>();
        return this;
    }
    
    public FrozenDictionary<string, IThemeCollection> GetRegisteredThemeCollections() => RegisteredBaseThemeCollectionsLazy.Value;
    
}
