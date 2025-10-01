// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniButtonOutline : InfiniButton {
    protected override string CssDefault { get; } =  "overload-bg-infini-button-outline hover:overload-bg-infini-button-outline-hover text-infini-button-outline-text hover:text-infini-button-outline-text-hover ring-1 ring-infini-button-outline-ring hover:ring-infini-button-outline-ring-hover";
    protected override string CssColor => ConcatClasses(
        "overload-bg-infini-button-outline hover:overload-bg-infini-button-outline-hover ring-1",
        Color.ToCssClassText(),
        Color.ToCssClassRing()
    );
}
