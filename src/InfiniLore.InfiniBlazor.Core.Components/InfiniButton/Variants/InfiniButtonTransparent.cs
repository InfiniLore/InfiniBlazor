// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniButtonTransparent : InfiniButton {
    protected override string CssDefault { get; } = "infini-bg-(--button-transparent) text-(--button-transparent-text) hover:text-(--button-transparent-text-hover) hover:infini-bg-(--button-transparent-hover)";
    protected override string CssColor => ConcatClasses(
        "infini-bg-(--button-transparent)  hover:infini-bg-(--button-transparent-hover)",
        Color.ToCssClassText()
    );
}
