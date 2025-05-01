// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Theming;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IThemeCollection {
    IReadOnlyDictionary<IThemeMode, ITheme> ContainedModes { get; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    bool TryGetLightMode([NotNullWhen(true)] out ITheme? theme);
    bool TryGetDarkMode([NotNullWhen(true)] out ITheme? theme);
    
    bool TryGetMode(IThemeMode mode, [NotNullWhen(true)] out ITheme? theme);
    bool TryGetModeByName(string variantName, [NotNullWhen(true)] out ITheme? theme);
    bool TryGetModeByName(ReadOnlySpan<char> variantName, [NotNullWhen(true)] out ITheme? theme);
}
