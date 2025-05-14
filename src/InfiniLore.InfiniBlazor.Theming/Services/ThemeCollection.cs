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
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
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

    public bool TryGetNextThemeMode(IThemeMode? lastKnownMode, [NotNullWhen(true)] out IThemeMode? themeMode) {
        if (Modes.Length == 0) {
            themeMode = null;
            return false;
        }

        if (Modes.Length == 1 || lastKnownMode == null) {
            themeMode = Modes[0];
            return true;
        }
        
        // Handles unknown modes as well, because we start at -1 if it cant be be found, and -1 + 1 = 0 
        int currentModeIndex = (Array.IndexOf(Modes, lastKnownMode) + 1) % Modes.Length;
        themeMode = Modes[currentModeIndex];
        return true;

    }
}
