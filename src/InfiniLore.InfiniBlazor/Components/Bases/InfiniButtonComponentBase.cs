// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.Debouncers;
using InfiniLore.InfiniBlazor.QueryParameters;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;

namespace InfiniLore.InfiniBlazor.Components;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniButtonComponentBase : InfiniComponentBase {
    [Inject] protected IQueryParameterManager QueryParameterManager { get; set; } = null!;
    
    [Parameter] public string? Href { get; set; }
    [Parameter] public string? IconName { get; set; }
    [Parameter] public string? Text { get; set; }
    [Parameter] public bool StopPropagation { get; set; } = true;
    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
    private EventCallbackDebouncer<MouseEventArgs> OnClickDebounced { get; set; } = EventCallbackDebouncer<MouseEventArgs>.Empty;
    [Parameter] public int DebounceMs { get; set; }
    [Parameter] public bool DisableTrackedQueryParams { get; set; }
    
    protected string? ComputedHref { get; private set; }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void OnInitialized() {
        base.OnInitialized();
        if (Href is not null && !DisableTrackedQueryParams) NavigationManager.LocationChanged += OnLocationChanged;
        ComputeHref();
    }

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

    private void OnLocationChanged(object? sender, LocationChangedEventArgs e) {
        ComputeHref();
        InvokeAsync(StateHasChanged);
    }

    private void ComputeHref() {
        ComputedHref = Href is not null && !DisableTrackedQueryParams
            ? QueryParameterManager.ApplyTrackedQueryParameters(Href)
            : Href;
    }

    public override async ValueTask DisposeAsync() {
        await base.DisposeAsync();
        if (Href is not null && !DisableTrackedQueryParams) NavigationManager.LocationChanged -= OnLocationChanged;
        if (!OnClickDebounced.IsEmpty) await OnClickDebounced.DisposeAsync();
    }
}
