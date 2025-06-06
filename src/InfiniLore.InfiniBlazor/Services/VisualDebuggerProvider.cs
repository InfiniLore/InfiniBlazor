// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Debugger;

namespace InfiniLore.InfiniBlazor.Services;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableScoped<IVisualDebuggerProvider>]
public class VisualDebuggerProvider : IVisualDebuggerProvider {
    private DebuggerState State { get; set; } = DebuggerState.Disabled;
    
    public event Action? OnChange;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool IsEnabled() => State is DebuggerState.Enabled;
    public Task ToggleStateAsync() {
        State = State switch {
            DebuggerState.Disabled => DebuggerState.Enabled,
            DebuggerState.Enabled => DebuggerState.Disabled,
            _ => throw new ArgumentOutOfRangeException()
        };
        OnChange?.Invoke();
        return Task.CompletedTask;
    }
}
