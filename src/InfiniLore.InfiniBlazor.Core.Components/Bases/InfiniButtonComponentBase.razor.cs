// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.Debouncers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace InfiniLore.InfiniBlazor.Components;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class InfiniButtonComponentBase : InfiniHrefContentComponentBase {
    
    [Parameter] public string? IconName { get; set; }
    [Parameter] public string? Text { get; set; }
    [Parameter] public bool StopPropagation { get; set; } = true;
    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
    private EventCallbackDebouncer<MouseEventArgs> OnClickDebounced { get; set; } = EventCallbackDebouncer<MouseEventArgs>.Empty;
    [Parameter] public int DebounceMs { get; set; }
    [Parameter] public Size Size { get; set; } = Size.M;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override async Task OnParametersSetAsync() {
        base.OnParametersSet();
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (OnClickDebounced is not null) await OnClickDebounced.DisposeAsync();
        OnClickDebounced = DebounceMs > 0 
            ? OnClick.GetDebouncer(DebounceMs)
            : EventCallbackDebouncer<MouseEventArgs>.Empty;
    }
    protected async Task OnClickAsync(MouseEventArgs args) {
        if (IsDisabled) return;

        if (!OnClickDebounced.IsEmpty) await OnClickDebounced.InvokeDebouncedAsync(args);
        else await OnClick.InvokeAsync(args);
    }

    public override async ValueTask DisposeAsync() {
        await base.DisposeAsync();
        if (!OnClickDebounced.IsEmpty) await OnClickDebounced.DisposeAsync();
    }

    private string IconSizeClass => Size switch {
        Size.Xxs => "w-3 h-3 my-0.5",
        Size.Xs => "w-4 h-4 my-0.5",
        Size.S => "w-5 h-5 my-0.5",
        Size.M => "w-6 h-6 my-0.5",
        Size.L => "w-7 h-7 my-0.5",
        Size.Xl => "w-8 h-8 my-0.5",
        Size.Xxl => "w-9 h-9 my-0.5",
        _ => "w-6 h-6"
    };
    
}
