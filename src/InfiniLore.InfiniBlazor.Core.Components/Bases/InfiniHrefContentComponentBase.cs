// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace InfiniLore.InfiniBlazor.Components;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class InfiniHrefContentComponentBase : InfiniComponentBase {
    [Inject] protected IQueryParameterManager QueryParameterManager { get; set; } = null!;
    [Parameter] public string? Href { get; set; }
    [Parameter] public bool DisableTrackedQueryParams { get; set; }
    
    protected string? ComputedHref { get; private set; }
    private bool LocationChangedRegistered { get; set; }
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void OnInitialized() {
        base.OnInitialized();
        if (Href is not null && !DisableTrackedQueryParams && !LocationChangedRegistered) {
            NavigationManager.LocationChanged += OnLocationChanged;
            LocationChangedRegistered = true;
        }
        ComputeHref();
    }
    
    private void OnLocationChanged(object? sender, LocationChangedEventArgs e) {
        ComputeHref();
        InvokeAsync(StateHasChanged);
    }

    public override async ValueTask DisposeAsync() {
        await base.DisposeAsync();
        if (Href is not null && !DisableTrackedQueryParams) NavigationManager.LocationChanged -= OnLocationChanged;
    }

    protected void HrefChanged() {
        ComputeHref();
        if (ComputedHref.IsNullOrWhiteSpace()
            || DisableTrackedQueryParams
            || LocationChangedRegistered) return;
        NavigationManager.LocationChanged += OnLocationChanged;
    }
    
    private void ComputeHref() {
        ComputedHref = Href is not null && !DisableTrackedQueryParams
            ? QueryParameterManager.ApplyTrackedQueryParameters(Href)
            : Href;
    }
}
