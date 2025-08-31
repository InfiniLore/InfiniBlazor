// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniButtonPrimary : InfiniButton {
    protected override string CssDefault { get; } = "infini-bg-(--button-primary) text-(--button-primary-text) hover:text-(--button-primary-text-hover) hover:infini-bg-(--button-primary-hover)";
    protected override string CssColor => ConcatClasses(
        "infini-bg-(--button-primary) hover:infini-bg-(--button-primary-hover)",
        Color.ToCssClassText()
    );
}
