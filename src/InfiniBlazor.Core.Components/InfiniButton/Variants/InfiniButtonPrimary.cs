// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniBlazor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniButtonPrimary : InfiniButton {
    protected override string CssDefault { get; } = "overload-bg-infini-button-primary text-infini-button-primary-text hover:text-infini-button-primary-text-hover hover:overload-bg-infini-button-primary-hover";
    protected override string CssColor => ConcatClasses(
        "overload-bg-infini-button-primary hover:overload-bg-infini-button-primary-hover",
        Color.ToCssClassText()
    );
}
