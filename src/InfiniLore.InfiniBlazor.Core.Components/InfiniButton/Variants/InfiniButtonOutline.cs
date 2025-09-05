// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniButtonOutline : InfiniButton {
    protected override string CssDefault { get; } =  "infini-bg-(--button-outline) hover:infini-bg-(--button-outline-hover) text-(--button-outline-text) hover:text-(--button-outline-text-hover) ring-1 ring-(--button-outline-ring) hover:ring-(--button-outline-ring-hover)";
    protected override string CssColor => ConcatClasses(
        "infini-bg-(--button-outline) hover:infini-bg-(--button-outline-hover) ring-1",
        Color.ToCssClassText(),
        Color.ToCssClassRing()
    );
}
