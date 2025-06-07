// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Debugger;
using Microsoft.AspNetCore.Components;

namespace Infinilore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class InfiniComponentBase : ComponentBase, IAsyncDisposable {
    [Parameter] public string Class { get; set; } = string.Empty;
    
    [Inject] protected IVisualDebuggerProvider VisualDebugger { get; set; } = null!;
    [Parameter] public virtual DebugColor DebugColor { get; set; } = DebugColor.Red;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void OnInitialized() {
        base.OnInitialized();
        VisualDebugger.OnChange += StateHasChanged;
    }

    public virtual ValueTask DisposeAsync() {
        VisualDebugger.OnChange -= StateHasChanged;
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}
