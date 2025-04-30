// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Theming;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record ThemeMode(
    string Name,
    bool IsDark,
    bool IsLight
) : IThemeMode {
    public static IThemeMode LightMode { get; } = new ThemeMode("light-mode", false, true);
    public static IThemeMode DarkMode { get; } = new ThemeMode("dark-mode", true, false);
}

