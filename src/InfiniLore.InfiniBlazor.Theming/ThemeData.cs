// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Theming;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record ThemeData(
    string Name,
    bool IsDark,
    bool IsLight
) : IThemeData {
    public static IThemeData LightMode { get; } = new ThemeData("light-mode", false, true);
    public static IThemeData DarkMode { get; } = new ThemeData("dark-mode", true, false);
}

