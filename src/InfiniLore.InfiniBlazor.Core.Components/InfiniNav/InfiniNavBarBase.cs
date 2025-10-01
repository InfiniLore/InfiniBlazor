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
        NavBarLocation.Top => "border-b overload-bg-infini-nav-top",
        NavBarLocation.Left => "border-r overload-bg-infini-nav-left",
        NavBarLocation.Right => "border-l overload-bg-infini-nav-right",
        NavBarLocation.Bottom => "border-t overload-bg-infini-nav-bottom",
        _ => string.Empty
    };
}
