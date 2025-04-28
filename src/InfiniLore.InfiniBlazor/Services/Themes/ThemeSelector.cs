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
        { "light", new ThemeLight() },
        { "dark", new ThemeDark() },
    };
    
    public IInfiniLoreTheme? CurrentTheme { get; private set; }
    
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
    
    public string GetCurrentThemeCss() {
        CurrentTheme ??= Themes["light"];

        var sb = new StringBuilder();
        sb.Append("body {");
        foreach ((string key, string value) in CurrentTheme.Theme) {
            sb.Append($"{key}: {value};");
        }
        sb.Append('}');
        
        return sb.ToString();
    }
}
