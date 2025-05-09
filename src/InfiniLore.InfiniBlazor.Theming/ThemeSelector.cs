// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace InfiniLore.InfiniBlazor.Theming;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IThemeSelector>]
public class ThemeSelector(IThemeConfig config, IServiceProvider provider, ILogger<ThemeSelector> logger, IPoolCache pool) : IThemeSelector {
    public event Func<Task>? ThemeChangedAsync;
    public IThemeCollection CurrentThemeCollection { get; private set; } = provider.GetRequiredKeyedService<IThemeCollection>(null);
    public IThemeMode CurrentThemeMode { get; private set; } = config.DefaultThemeMode;
    
    private FrozenDictionary<string, IThemeCollection> Themes { get; } = CollectThemes(config, provider, logger);
    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    private static FrozenDictionary<string, IThemeCollection> CollectThemes(IThemeConfig config, IServiceProvider provider, ILogger<ThemeSelector> logger) {
        IReadOnlyCollection<string> names = config.RegisteredThemes;
        var themes = new Dictionary<string, IThemeCollection>(names.Count);
        foreach (string name in names) {
            if (provider.GetKeyedService<IThemeCollection>(name) is not {} theme) {
                logger.LogWarning("Theme '{ThemeName}' not found.", name);
                continue;
            }
            themes.Add(name, theme);
        }
        
        if (!themes.ContainsKey("default")) {
            logger.LogWarning("No default theme found. Using first theme.");
            themes.Add("default", new InfiniBlazorThemeCollection()); // this way there is always a default theme
        }
        
        logger.LogInformation("Found {Count} registered themes.", themes.Count);
        return themes.ToFrozenDictionary();
    }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async Task<bool> TrySelectThemeCollectionAsync(string themeName) {
        if (!Themes.TryGetValue(themeName, out IThemeCollection? theme)) {
            logger.LogWarning("Theme '{ThemeName}' not found.", themeName);
            return false;
        }

        CurrentThemeCollection = theme;
        if (ThemeChangedAsync is not null) await ThemeChangedAsync().ConfigureAwait(false);
        logger.LogInformation("Theme '{ThemeName}' selected.", themeName);
        return true;
    }
    
    public async Task<bool> TrySelectNextThemeModeAsync() {
        if (!CurrentThemeCollection.TryGetNextThemeMode(CurrentThemeMode, out IThemeMode? themeMode)) {
            logger.LogWarning("No next theme variant found for current mode '{Mode}'.", CurrentThemeMode.Name);
            return false;
        }

        CurrentThemeMode = themeMode;
        
        if (ThemeChangedAsync is not null) await ThemeChangedAsync().ConfigureAwait(false);
        logger.LogInformation("Theme mode toggled to {Mode}.", CurrentThemeMode.Name);
        return true;
    }

    public bool TryGetCurrentThemeModeCss([NotNullWhen(true)] out string? css) {
        if (!CurrentThemeCollection.TryGetTheme(CurrentThemeMode, out ITheme? theme)) {
            logger.LogWarning("No theme variant found for current mode '{Mode}'.", CurrentThemeMode.Name);
            css = null;
            return false;
        }
        
        StringBuilder sb = pool.StringBuilderPool.Get() ;
        try {
            sb.Append(":root {");
            foreach ((string key, string value) in theme.AsCssVariables()) {
                sb.Append($"{key}: {value};");
            }
            sb.Append('}');
        
            css = sb.ToString();
            return true;
        }
        finally {
            pool.StringBuilderPool.Return(sb);
        }
    }
}
