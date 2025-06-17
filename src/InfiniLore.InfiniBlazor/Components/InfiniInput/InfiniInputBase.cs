// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Infinilore.InfiniBlazor.Components;
using Microsoft.AspNetCore.Components;

namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class InfiniInputBase : InfiniComponentBase {
    [Parameter] public string Id { get; set; } = Guid.NewGuid().ToString("N");

    [Parameter] public string? Label { get; set; }
    [Parameter] public LabelLocation LabelLocation { get; set; } = LabelLocation.Left;

    [Parameter] public string? ValidationMessage { get; set; }

    [Parameter] public bool Disabled { get; set; }
    protected override bool IsDisabled => Disabled || base.IsDisabled;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void OnParametersSet() {
        base.OnParametersSet();
        CascadedDisabled = Disabled;
    }
}
