// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniButtonDark : InfiniButton {
    protected override string CssDefault { get; } = "infini-bg-(--button-dark) hover:infini-bg-(--button-dark-hover) text-(--button-dark-text) hover:text-(--button-dark-text-hover) hover:ring-2 hover:ring-(--button-dark-ring)";
    protected override string CssColor => ConcatClasses(
        Color.ToCssClassText(),
        Color.ToCssClassRingHover(),
        "infini-bg-(--button-dark) hover:infini-bg-(--button-dark-hover) hover:ring-2"
    );
}
