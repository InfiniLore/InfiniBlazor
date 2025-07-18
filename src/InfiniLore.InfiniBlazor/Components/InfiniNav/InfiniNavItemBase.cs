// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components;

namespace InfiniLore.InfiniBlazor.Components;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class InfiniNavItemBase : InfiniComponentBase {
    [CascadingParameter] public bool NavBarCollapsed { get; set; }
    [CascadingParameter] public Size NavBarSize { get; set; }
    [CascadingParameter] public NavBarLocation NavBarLocation { get; set; }
    [CascadingParameter] public NavSubMenuContext? SubMenuContext { get; set; }
    
    [Parameter] public string? Text { get; set; }
    [Parameter] public string? IconName { get; set; }
    
    [Parameter] public bool HiddenOnCollapsed { get; set; }
    protected bool Hidden => NavBarCollapsed && HiddenOnCollapsed;
    
    protected static string ContainerClasses => ConcatClasses(ContainerClassesNoHover, "hover:bg-(--sidebar-nav-button-hover)");
    protected static string ContainerClassesNoHover => "flex flex-nowrap items-center infini-bg-(--sidebar-nav-button) rounded group justify-start p-2";
}
