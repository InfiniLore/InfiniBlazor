// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;

namespace InfiniBlazor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableScoped<IVisualDebuggerProvider>]
public class VisualDebuggerProvider(IQueryParameterManager queryParameterManager ) : IVisualDebuggerProvider {
    private DebuggerState State { get; set; } = DebuggerState.Disabled;
    
    public event Action? OnChange;
    public event Func<Task>? OnChangeAsync;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool IsEnabled() 
        => State is DebuggerState.Enabled;
    
    public async Task ToggleStateAsync() {
        State = State switch {
            DebuggerState.Disabled => DebuggerState.Enabled,
            DebuggerState.Enabled => DebuggerState.Disabled,
            _ => throw new ArgumentOutOfRangeException()
        };
    
        if (State is DebuggerState.Disabled) queryParameterManager.RemoveParam(QueryParameterNames.Debug);
        else queryParameterManager.SetParam(QueryParameterNames.Debug, true);

        OnChange?.Invoke();
        if (OnChangeAsync is not null) await OnChangeAsync();
    }
    
    public void InitializeFromUrl() {
        bool enabled = queryParameterManager.GetParam<bool>(QueryParameterNames.Debug);
        State = enabled ? DebuggerState.Enabled : DebuggerState.Disabled;
    }

    public async Task SetStateAsync(DebuggerState state) {
        State = state;
        OnChange?.Invoke();
        if (OnChangeAsync is not null) await OnChangeAsync();
    }
    
    public string? WithEnabled(string? onTrue, string? onFalse) 
        => State switch {
            DebuggerState.Enabled => onTrue,
            DebuggerState.Disabled => onFalse,
            _ => throw new ArgumentOutOfRangeException()
        };
}
