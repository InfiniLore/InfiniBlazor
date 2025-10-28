// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniNavLinkBase : InfiniNavItemBase, IDisposable {
    [Inject] public IQueryParameterManager QueryParameterManager { get; set; } = null!;
    [Parameter] public bool DisableTrackedQueryParams { get; set; }
    
    [Parameter, EditorRequired] public required string Href { get; init; } = string.Empty;
    protected string ComputedHref { get; private set; } = string.Empty;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void OnInitialized() {
        if (!DisableTrackedQueryParams) NavigationManager.LocationChanged += OnLocationChanged;
        ComputeHref();
    }

    private void OnLocationChanged(object? sender, LocationChangedEventArgs e) {
        ComputeHref();
        InvokeAsync(StateHasChanged);
    }

    private void ComputeHref() {
        if (IsDisabled) {
            ComputedHref = string.Empty;
            return;
        }
        ComputedHref = !DisableTrackedQueryParams 
            ? QueryParameterManager.ApplyTrackedQueryParameters(Href)
            : Href;
    }

    public void Dispose() {
        if (!DisableTrackedQueryParams) NavigationManager.LocationChanged -= OnLocationChanged;
    }
    
}
