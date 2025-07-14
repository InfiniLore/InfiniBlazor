// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components;

namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class InfiniNavBase : InfiniComponentBase {
    [Parameter] public Size Size { get; set; } = Size.M;
    [Parameter] public NavLayoutLocation Location { get; set; }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected string NavLocationClasses => Location switch {
        NavLayoutLocation.Top => "border-b",
        NavLayoutLocation.Left => "border-r",
        NavLayoutLocation.Right => "border-l",
        NavLayoutLocation.Bottom => "border-t",
        _ => string.Empty
    };
}
