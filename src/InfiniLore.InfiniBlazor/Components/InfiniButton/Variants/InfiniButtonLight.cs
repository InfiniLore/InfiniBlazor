// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniButtonLight : InfiniButton {
    protected override string CssDefault { get; } =  "infini-bg-(--button-light) hover:infini-bg-(--button-light-hover) text-(--button-light-text) hover:text-(--button-light-text-hover) hover:ring-2 hover:ring-(--button-light-ring)";
    protected override string CssColor => ConcatClasses(
        Color.ToCssClassText(),
        Color.ToCssClassRingHover(),
        "infini-bg-(--button-light) hover:infini-bg-(--button-light-hover) hover:ring-2"
    );
}
