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
    bool TryGetNextTheme([NotNullWhen(true)] out ITheme? theme);
    bool TryGetTheme(int index, [NotNullWhen(true)] out ITheme? theme);
}
