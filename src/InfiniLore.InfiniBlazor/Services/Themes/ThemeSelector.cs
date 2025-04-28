// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text;

namespace InfiniLore.InfiniBlazor.Themes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IThemeSelector>]
public class ThemeSelector(ILogger<ThemeSelector> logger) : IThemeSelector {
    public event Action? ThemeChanged;

    private Dictionary<string, IInfiniLoreTheme> Themes { get; set; } = new() {
        { "default", new ThemeDefault() },
    };
    
    public IInfiniLoreTheme? CurrentTheme { get; private set; }
    public bool IsDarkMode { get; private set; }
    
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
        
        Dictionary<string, string> data = IsDarkMode ? CurrentTheme.DarkMode : CurrentTheme.LightMode;
        foreach ((string key, string value) in data) {
            sb.Append($"{key}: {value};");
        }
        sb.Append('}');
        
        return sb.ToString();
    }
}
