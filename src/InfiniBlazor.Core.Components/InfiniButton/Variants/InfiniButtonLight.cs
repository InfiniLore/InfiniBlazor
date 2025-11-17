// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniBlazor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniButtonLight : InfiniButton {
    protected override string CssDefault { get; } =  "overload-bg-infini-button-light hover:overload-bg-infini-button-light-hover text-infini-button-light-text hover:text-infini-button-light-text-hover hover:ring-2 hover:ring-infini-button-light-ring";
    protected override string CssColor => ConcatClasses(
        Color.ToCssClassText(),
        Color.ToCssClassRingHover(),
        "overload-bg-infini-button-light hover:overload-bg-infini-button-light-hover hover:ring-2"
    );
}
