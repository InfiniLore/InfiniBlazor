// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components;

namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class InfiniNavBarBase : InfiniComponentBase {
    [Parameter] public Size Size { get; set; } = Size.M;
    [Parameter] public NavBarLocation Location { get; set; }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected string NavLocationClasses => Location switch {
        NavBarLocation.Top => "border-b",
        NavBarLocation.Left => "border-r",
        NavBarLocation.Right => "border-l",
        NavBarLocation.Bottom => "border-t",
        _ => string.Empty
    };
}
