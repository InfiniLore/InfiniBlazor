// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniButtonOutlineSolid : InfiniButton {
    protected override string CssDefault { get; } =  "infini-bg-(--button-outline-solid) hover:infini-bg-(--button-outline-solid-hover) text-(--button-outline-solid-text) hover:text-(--button-outline-solid-text-hover) ring-1 ring-(--button-outline-solid-ring) hover:ring-(--button-outline-solid-ring-hover)";
    protected override string CssColor => ConcatClasses(
        "infini-bg-(--button-outline-solid) hover:infini-bg-(--button-outline-solid-hover) ring-1",
        Color.ToCssClassText(),
        Color.ToCssClassRing()
    );
}
