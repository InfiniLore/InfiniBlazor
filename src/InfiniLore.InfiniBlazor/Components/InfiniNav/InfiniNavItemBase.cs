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
    
    protected int IconSize => NavBarSize switch {
        Size.Xxs => 10,
        Size.Xs => 14,
        Size.S => 18,
        Size.M => 24,
        Size.L => 32,
        Size.Xl => 42,
        Size.Xxl => 48,
        _ => 24
    };
    
    protected static string ContainerClasses => ConcatClasses(ContainerClassesNoHover, "hover:bg-(--sidebar-nav-button-hover)");
    protected static string ContainerClassesNoHover => "flex flex-nowrap items-center infini-bg-(--sidebar-nav-button) rounded group justify-start p-2";
}
