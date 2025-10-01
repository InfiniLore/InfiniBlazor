// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniButtonDark : InfiniButton {
    protected override string CssDefault { get; } = "overload-bg-infini-button-dark hover:overload-bg-infini-button-dark-hover text-infini-button-dark-text hover:text-infini-button-dark-text-hover hover:ring-2 hover:ring-infini-button-dark-ring";
    protected override string CssColor => ConcatClasses(
        Color.ToCssClassText(),
        Color.ToCssClassRingHover(),
        "overload-bg-infini-button-dark hover:overload-bg-infini-button-dark-hover hover:ring-2"
    );
}
