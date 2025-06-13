// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Theming;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class ThemeCollection : IThemeCollection {
    protected abstract OrderedDictionary<ThemeMode, ICssData> Modes { get; }
    public abstract string CollectionName { get; }

    public bool IsEmpty => Modes.IsEmpty();
    public bool IsBinary => Modes.Count == 2;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TryGetCssData(string modeName, [NotNullWhen(true)] out ICssData? cssData) {
        ThemeMode mode = ThemeMode.AsUndefined(modeName);
        return Modes.TryGetValue(mode, out cssData);
    }

    public bool TryGetNextThemeMode(string? lastKnownModeName, out ThemeMode themeMode) {
        if (lastKnownModeName.IsNullOrWhiteSpace()) {
            themeMode = Modes.Keys.FirstOrDefault();
            return themeMode != default;
        }
        
        ThemeMode mode = ThemeMode.AsUndefined(lastKnownModeName);
        
        int index = Modes.IndexOf(mode);
        if (index == -1) {
            themeMode = ThemeMode.Empty;
            return false;
        }
        
        KeyValuePair<ThemeMode, ICssData> pair = Modes.GetAt((index + 1) % Modes.Count);
        themeMode = pair.Key;
        return themeMode != default;
    }

    public bool ContainsModeName(string modeName) {
        ThemeMode mode = ThemeMode.AsUndefined(modeName);
        return Modes.ContainsKey(mode);
    }
    
    public ThemeMode GetFirstMode() {
        return Modes.Keys.FirstOrDefault();
    }
}
