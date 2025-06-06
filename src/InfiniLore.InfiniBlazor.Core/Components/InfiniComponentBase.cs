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
    [Inject] protected IVisualDebuggerProvider VisualDebugger { get; set; } = null!;
    [Parameter] public virtual DebugColor DebugColor { get; set; } = DebugColor.Red;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void OnInitialized() {
        base.OnInitialized();
        VisualDebugger.OnChange += StateHasChanged;
    }

    public ValueTask DisposeAsync() {
        VisualDebugger.OnChange -= StateHasChanged;
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}
