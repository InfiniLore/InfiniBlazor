// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniButtonTransparent : InfiniButton {
    protected override string CssDefault { get; } = "overload-bg-infini-button-transparent text-infini-button-transparent-text hover:text-infini-button-transparent-text-hover hover:overload-bg-infini-button-transparent-hover";
    protected override string CssColor => ConcatClasses(
        "overload-bg-infini-button-transparent  hover:overload-bg-infini-button-transparent-hover",
        Color.ToCssClassText()
    );
}
