// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using JetBrains.Annotations;

namespace InfiniBlazor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public enum VerticalAlignImage {
    [UsedImplicitly] Baseline,
    [UsedImplicitly] Sub,
    [UsedImplicitly] Super,
    [UsedImplicitly] TextTop,
    [UsedImplicitly] TextBottom,
    [UsedImplicitly] Middle,
    [UsedImplicitly] Top,
    [UsedImplicitly] Bottom,
    [UsedImplicitly] Initial,
    [UsedImplicitly] Inherit,
    [UsedImplicitly] Revert,
    [UsedImplicitly] RevertLayer,
    [UsedImplicitly] Unset
}

public static class VerticalAlignImageUtilities {
    public static bool TryGetFromString(string? input, out VerticalAlignImage verticalAlign) {
        verticalAlign = default;
        return !input.IsNullOrWhiteSpace() && Enum.TryParse(input, true, out verticalAlign);

    }
}
