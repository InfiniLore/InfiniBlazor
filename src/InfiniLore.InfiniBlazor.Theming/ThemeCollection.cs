// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Theming;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class ThemeCollection : IThemeCollection {
    protected abstract Dictionary<IThemeData, ITheme> Themes { get; }

    private FrozenDictionary<IThemeData, ITheme>? _storage;
    private FrozenDictionary<IThemeData, ITheme> ContainedThemesStorage => _storage ??= Themes.ToFrozenDictionary();

    public IReadOnlyDictionary<IThemeData, ITheme> ContainedThemes => ContainedThemesStorage.AsReadOnly();
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TryGetLightModeVariant([NotNullWhen(true)] out ITheme? theme) 
        => ContainedThemesStorage.TryGetValue(ThemeData.LightMode, out theme);
    
    public bool TryGetDarkModeVariant([NotNullWhen(true)] out ITheme? theme)
        => ContainedThemesStorage.TryGetValue(ThemeData.DarkMode, out theme);
}
