// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniButtonOutlineSolid : InfiniButton {
    protected override string CssDefault { get; } =  "overload-bg-infini-button-outline-solid hover:overload-bg-infini-button-outline-solid-hover text-infini-button-outline-solid-text hover:text-infini-button-outline-solid-text-hover ring-1 ring-infini-button-outline-solid-ring hover:ring-infini-button-outline-solid-ring-hover";
    protected override string CssColor => ConcatClasses(
        "overload-bg-infini-button-outline-solid hover:overload-bg-infini-button-outline-solid-hover ring-1",
        Color.ToCssClassText(),
        Color.ToCssClassRing()
    );
}
