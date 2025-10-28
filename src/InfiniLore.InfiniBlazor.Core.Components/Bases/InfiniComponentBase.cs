// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components;

namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class InfiniComponentBase : ComponentBase, IAsyncDisposable {
    [Inject] protected NavigationManager NavigationManager { get; set; } = null!;
    [Inject] protected IVisualDebuggerProvider VisualDebugger { get; set; } = null!;
    
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string Class { get; set; } = string.Empty;
    [Parameter] public virtual Color DebugColor { get; set; } = Color.Red;
    [Parameter] public bool IsDisabled {
        get => CascadedDisabled;
        set => CascadedDisabled = value;
    }
    
    [CascadingParameter(Name = nameof(CascadedDisabled))] public bool CascadedDisabled { get; set; }
    
    protected bool SuppressRender { get; set; }
    protected bool IsDisposed { get; set; }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    // ReSharper disable once InvertIf
    protected override bool ShouldRender() {
        if (!base.ShouldRender()) return false;
        if (SuppressRender) {
            SuppressRender = false;
            return false;
        }
        
        return true;
    }

    protected override void OnParametersSet() {
        base.OnParametersSet();
        VisualDebugger.OnChange += StateHasChanged;
    }

    public virtual ValueTask DisposeAsync() {
        VisualDebugger.OnChange -= StateHasChanged;
        IsDisposed = true;
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    protected static string ConcatClasses(params ReadOnlySpan<string?> classes)
        => string.Join(' ', classes);

    protected static string HiddenIf(bool condition)
        => condition ? "hidden" : string.Empty;

    protected static string HiddenIfNot(bool condition)
        => HiddenIf(!condition);

    protected string WhenDisabled(string onEnabled, string onDisabled) {
        return IsDisabled ? onDisabled : onEnabled;
    }
    
    protected void SuppressRendering() {
        SuppressRender = true;
    }
}
