// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.GeneratorTools;

namespace InfiniLore.InfiniBlazor.Components;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public enum VerticalAlignImage {
    Baseline,
    Sub,
    Super,
    TextTop,
    TextBottom,
    Middle,
    Top,
    Bottom,
    Initial,
    Inherit,
    Revert,
    ReveryLayer,
    Unset
}

public static class VerticalAlignImageUtilities {
    public static bool TryGetFromString(string? input, out VerticalAlignImage verticalAlign) {
        verticalAlign = default;
        return !input.IsNullOrWhiteSpace() && Enum.TryParse(input, true, out verticalAlign);

    }
}
