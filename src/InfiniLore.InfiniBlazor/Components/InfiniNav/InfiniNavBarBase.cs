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
        NavBarLocation.Top => "border-b infini-bg-(--nav-top)",
        NavBarLocation.Left => "border-r infini-bg-(--nav-left)",
        NavBarLocation.Right => "border-l infini-bg-(--nav-right)",
        NavBarLocation.Bottom => "border-t infini-bg-(--nav-bottom)",
        _ => string.Empty
    };
}
