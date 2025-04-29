// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Theming.Library;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Frozen;
using System.Text;

namespace InfiniLore.InfiniBlazor.Theming;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IThemeSelector>]
public class ThemeSelector(IThemeConfig config, IServiceProvider provider, ILogger<ThemeSelector> logger) : IThemeSelector {
    public event Action? ThemeChanged;
    public IInfiniLoreTheme? CurrentTheme { get; private set; }
    public bool IsDarkMode { get; private set; }
    
    private FrozenDictionary<string, IInfiniLoreTheme> Themes { get; } = CollectThemes(config, provider, logger);
    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    private static FrozenDictionary<string, IInfiniLoreTheme> CollectThemes(IThemeConfig config, IServiceProvider provider, ILogger<ThemeSelector> logger) {
        IReadOnlyCollection<string> names = config.RegisteredThemes;
        var themes = new Dictionary<string, IInfiniLoreTheme>(names.Count);
        foreach (string name in names) {
            if (provider.GetKeyedService<IInfiniLoreTheme>(name) is not {} theme) {
                logger.LogWarning("Theme '{ThemeName}' not found.", name);
                continue;
            }
            themes.Add(name, theme);
        }
        
        if (!themes.ContainsKey("default")) {
            logger.LogWarning("No default theme found. Using first theme.");
            themes.Add("default", new DefaultTheme()); // this way there is always a default theme
        }
        
        logger.LogInformation("Found {Count} registered themes.", themes.Count);
        return themes.ToFrozenDictionary();
    }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TrySelectTheme(string themeName) {
        if (!Themes.TryGetValue(themeName, out IInfiniLoreTheme? theme)) {
            logger.LogWarning("Theme '{ThemeName}' not found.", themeName);
            return false;
        }

        CurrentTheme = theme;
        ThemeChanged?.Invoke();
        logger.LogInformation("Theme '{ThemeName}' selected.", themeName);
        return true;
    }
    
    public void ToggleMode() {
        IsDarkMode = !IsDarkMode;
        ThemeChanged?.Invoke();
        logger.LogInformation("Theme mode toggled to {Mode}.", IsDarkMode ? "dark" : "light");
    }

    public string GetCurrentThemeCss() {
        CurrentTheme ??= Themes["default"];

        var sb = new StringBuilder();
        sb.Append("body {");
        
        FrozenDictionary<string, string> data = IsDarkMode ? CurrentTheme.DarkMode : CurrentTheme.LightMode;
        foreach ((string key, string value) in data) {
            sb.Append($"{key}: {value};");
        }
        sb.Append('}');
        
        return sb.ToString();
    }
}
