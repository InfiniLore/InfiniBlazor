// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Theming.Library;
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
public class ThemeSelector(IThemeConfig config, IServiceProvider provider, ILogger<ThemeSelector> logger) : IThemeSelector {
    public event Action? ThemeChanged;
    public IThemeCollection CurrentTheme { get; private set; } = provider.GetRequiredKeyedService<IThemeCollection>(null);
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
            themes.Add("default", new DefaultThemeCollection()); // this way there is always a default theme
        }
        
        logger.LogInformation("Found {Count} registered themes.", themes.Count);
        return themes.ToFrozenDictionary();
    }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TrySelectTheme(string themeName) {
        if (!Themes.TryGetValue(themeName, out IThemeCollection? theme)) {
            logger.LogWarning("Theme '{ThemeName}' not found.", themeName);
            return false;
        }

        CurrentTheme = theme;
        ThemeChanged?.Invoke();
        logger.LogInformation("Theme '{ThemeName}' selected.", themeName);
        return true;
    }
    
    public bool TryToggleDarkAndLightMode() {
        if (CurrentThemeMode == ThemeMode.DarkMode) CurrentThemeMode = ThemeMode.LightMode;
        else if (CurrentThemeMode == ThemeMode.LightMode) CurrentThemeMode = ThemeMode.DarkMode;
        else {
            logger.LogWarning("No opposite theme variant found for current mode '{Mode}'.", CurrentThemeMode.Name);
            return false;
        }
        ThemeChanged?.Invoke();
        logger.LogInformation("Theme mode toggled to {Mode}.", CurrentThemeMode.Name);
        return true;
    }

    public bool TryGetCurrentThemeCss([NotNullWhen(true)] out string? css) {
        ITheme? theme = null;
        if (CurrentThemeMode == ThemeMode.DarkMode) CurrentTheme.TryGetDarkMode(out theme);
        else if (CurrentThemeMode == ThemeMode.LightMode) CurrentTheme.TryGetLightMode(out theme);
        
        if (theme is null) {
            logger.LogWarning("No theme variant found for current mode '{Mode}'.", CurrentThemeMode.Name);
            css = null;
            return false;
        }
        
        var sb = new StringBuilder();
        sb.Append(":root {");
        foreach ((string key, string value) in theme.AsCssVariables()) {
            sb.Append($"{key}: {value};");
        }
        sb.Append('}');
        
        css = sb.ToString();
        return true;
    }
}
