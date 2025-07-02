// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Debugger;
using Microsoft.AspNetCore.Components;

namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class InfiniComponentBase : ComponentBase, IAsyncDisposable {
    [Inject] protected NavigationManager NavigationManager { get; set; } = null!;
    [Inject] protected IVisualDebuggerProvider VisualDebugger { get; set; } = null!;
    
    [Parameter] public string Class { get; set; } = string.Empty;
    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public virtual DebugColor DebugColor { get; set; } = DebugColor.Red;

    // ReSharper disable once UnusedMemberInSuper.Global
    protected virtual bool IsDisabled => CascadedDisabled;
    [CascadingParameter(Name = nameof(CascadedDisabled))] public bool CascadedDisabled { get; set; }
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void OnParametersSet() {
        base.OnParametersSet();
        VisualDebugger.OnChange += StateHasChanged;
    }

    public virtual ValueTask DisposeAsync() {
        VisualDebugger.OnChange -= StateHasChanged;
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    protected static string ConcatClasses(params ReadOnlySpan<string?> classes)
        => string.Join(" ", classes);
}
