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
    [Parameter] public string? Icon { get; set; }
    
    [Parameter] public bool HiddenOnCollapsed { get; set; }
    protected bool IsHidden => NavBarCollapsed && HiddenOnCollapsed;
    
    protected bool CollapsedState => NavBarCollapsed && (!SubMenuContext?.IgnoreCascadeCollapsedState ?? true);
    
    protected static string ContainerClassesDisabled => $"{BaseContainerClasses} opacity-50 cursor-not-allowed ";
    protected static string ContainerClasses => $"{BaseContainerClasses} hover:bg-infini-nav-button-hover";
    protected static string ContainerClassesNoHover => BaseContainerClasses;
    
    private const string BaseContainerClasses = "flex flex-nowrap items-center group justify-start p-2 rounded overload-bg-infini-nav-button";
}
