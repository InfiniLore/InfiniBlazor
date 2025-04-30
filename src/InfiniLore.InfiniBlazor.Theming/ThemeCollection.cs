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
    protected abstract Dictionary<IThemeMode, ITheme> Modes { get; }

    private FrozenDictionary<IThemeMode, ITheme>? _storage;
    private FrozenDictionary<IThemeMode, ITheme> ContainedThemesStorage => _storage ??= Modes.ToFrozenDictionary();

    public IReadOnlyDictionary<IThemeMode, ITheme> ContainedThemes => ContainedThemesStorage.AsReadOnly();
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TryGetLightModeVariant([NotNullWhen(true)] out ITheme? theme) 
        => ContainedThemesStorage.TryGetValue(ThemeMode.LightMode, out theme);
    
    public bool TryGetDarkModeVariant([NotNullWhen(true)] out ITheme? theme)
        => ContainedThemesStorage.TryGetValue(ThemeMode.DarkMode, out theme);
}
