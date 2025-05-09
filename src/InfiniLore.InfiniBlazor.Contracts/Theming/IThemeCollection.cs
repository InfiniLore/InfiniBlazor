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
    bool TryGetTheme(IThemeMode mode, [NotNullWhen(true)] out ITheme? theme);
    bool TryGetThemeByName(string variantName, [NotNullWhen(true)] out ITheme? theme);
    bool TryGetThemeByName(ReadOnlySpan<char> variantName, [NotNullWhen(true)] out ITheme? theme);
    bool TryGetNextThemeMode(IThemeMode? lastKnownMode, [NotNullWhen(true)] out IThemeMode? themeMode);
}
