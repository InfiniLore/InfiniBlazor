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
    [Inject] protected IDebuggerProvider Debugger { get; set; } = null!;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void OnInitialized() {
        base.OnInitialized();
        Debugger.OnChange += StateHasChanged;
    }

    public ValueTask DisposeAsync() {
        Debugger.OnChange -= StateHasChanged;
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}
