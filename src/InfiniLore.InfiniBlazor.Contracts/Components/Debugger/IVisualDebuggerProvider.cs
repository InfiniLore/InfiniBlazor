// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Components.Debugger;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IVisualDebuggerProvider {
    event Action? OnChange;
    event Func<Task>? OnChangeAsync;

    bool IsEnabled();
    Task ToggleStateAsync();
    Task SetStateAsync(DebuggerState state);

    void InitializeFromUrl();

    string? WithEnabled(string? onTrue, string? onFalse);
}
