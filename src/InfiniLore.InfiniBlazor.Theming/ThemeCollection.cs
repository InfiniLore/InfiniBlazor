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
    protected abstract Dictionary<IThemeMode, ITheme> Themes { get; }

    private FrozenDictionary<IThemeMode, ITheme>? _storage;
    private FrozenDictionary<IThemeMode, ITheme> ContainedModesStorage => _storage ??= Themes.ToFrozenDictionary(comparer:ThemeModeComparer.Instance);

    public IReadOnlyDictionary<IThemeMode, ITheme> ContainedModes => ContainedModesStorage.AsReadOnly();
    
    protected abstract IThemeMode[] Modes { get; }
    private int _currentModeIndex;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TryGetLightMode([NotNullWhen(true)] out ITheme? theme) 
        => ContainedModesStorage.TryGetValue(ThemeMode.LightMode, out theme);
    
    public bool TryGetDarkMode([NotNullWhen(true)] out ITheme? theme)
        => ContainedModesStorage.TryGetValue(ThemeMode.DarkMode, out theme);

    public bool TryGetTheme(IThemeMode mode, [NotNullWhen(true)] out ITheme? theme)
        => ContainedModesStorage.TryGetValue(mode, out theme);

    public bool TryGetThemeByName(string variantName, [NotNullWhen(true)] out ITheme? theme) {
        // ReSharper disable once InvertIf
        if (!ContainedModesStorage.TryGetAlternateLookup(out FrozenDictionary<IThemeMode, ITheme>.AlternateLookup<string> lookup)) {
            theme = null;
            return false;
        }
        return lookup.TryGetValue(variantName, out theme);
    }
    
    public bool TryGetThemeByName(ReadOnlySpan<char> variantName, [NotNullWhen(true)] out ITheme? theme) {
        // ReSharper disable once InvertIf
        if (!ContainedModesStorage.TryGetAlternateLookup(out FrozenDictionary<IThemeMode, ITheme>.AlternateLookup<ReadOnlySpan<char>> lookup)) {
            theme = null;
            return false;
        }
        return lookup.TryGetValue(variantName, out theme);
    }

    public bool TryGetNextTheme([NotNullWhen(true)] out ITheme? theme) {
        if (TryGetTheme(_currentModeIndex++, out theme)) return true;
        _currentModeIndex = 0;
        return TryGetTheme(_currentModeIndex, out theme);
    }
    
    public bool TryGetTheme(int index, [NotNullWhen(true)] out ITheme? theme) {
        if (index < 0 || index >= Modes.Length) {
            theme = null;
            return false;
        }
        theme = ContainedModesStorage[Modes[index]];
        return true;
    }
}
