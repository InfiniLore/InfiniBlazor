// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components;

namespace InfiniLore.InfiniBlazor.Components;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniNavItemBase : InfiniComponentBase {
    [CascadingParameter] public bool NavBarCollapsed { get; set; }
    [CascadingParameter] public Size NavBarSize { get; set; }
    
    [Parameter] public bool HiddenOnCollapsed { get; set; }
    protected bool Hidden => NavBarCollapsed && HiddenOnCollapsed;
}
