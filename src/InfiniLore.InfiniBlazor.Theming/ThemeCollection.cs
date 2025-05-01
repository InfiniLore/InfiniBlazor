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
    private FrozenDictionary<IThemeMode, ITheme> ContainedModesStorage => _storage ??= Modes.ToFrozenDictionary(comparer:ThemeModeComparer.Instance);

    public IReadOnlyDictionary<IThemeMode, ITheme> ContainedModes => ContainedModesStorage.AsReadOnly();
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TryGetLightMode([NotNullWhen(true)] out ITheme? theme) 
        => ContainedModesStorage.TryGetValue(ThemeMode.LightMode, out theme);
    
    public bool TryGetDarkMode([NotNullWhen(true)] out ITheme? theme)
        => ContainedModesStorage.TryGetValue(ThemeMode.DarkMode, out theme);

    public bool TryGetMode(IThemeMode mode, [NotNullWhen(true)] out ITheme? theme)
        => ContainedModesStorage.TryGetValue(mode, out theme);

    public bool TryGetModeByName(string variantName, [NotNullWhen(true)] out ITheme? theme) {
        // ReSharper disable once InvertIf
        if (!ContainedModesStorage.TryGetAlternateLookup(out FrozenDictionary<IThemeMode, ITheme>.AlternateLookup<string> lookup)) {
            theme = null;
            return false;
        }
        return lookup.TryGetValue(variantName, out theme);
    }
    
    public bool TryGetModeByName(ReadOnlySpan<char> variantName, [NotNullWhen(true)] out ITheme? theme) {
        // ReSharper disable once InvertIf
        if (!ContainedModesStorage.TryGetAlternateLookup(out FrozenDictionary<IThemeMode, ITheme>.AlternateLookup<ReadOnlySpan<char>> lookup)) {
            theme = null;
            return false;
        }
        return lookup.TryGetValue(variantName, out theme);
    }
}
