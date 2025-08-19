// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Theming;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IThemeCollection {
    bool IsEmpty { get; }
    bool IsBinary { get; }
    string CollectionName { get; }
    IEnumerable<string> AllModeNames { get; }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    bool TryGetCssData(string modeName, [NotNullWhen(true)] out ICssData? cssData);
    bool TryGetNextThemeMode(string? lastKnownModeName, out ThemeMode themeMode);
    
    bool ContainsModeName(string modeName);
    ThemeMode GetFirstMode();
}
