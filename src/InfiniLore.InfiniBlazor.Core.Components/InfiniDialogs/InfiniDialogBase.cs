// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components;

namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class InfiniDialogBase : InfiniComponentBase {
    [CascadingParameter] public DialogManagerContext DialogManagerContext { get; set; } = null!;
    [Parameter] public IDialogData DialogData { get; set; } = null!;
}
