// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Debugger;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IVisualDebuggerProvider {
    event Action? OnChange;

    bool IsEnabled();
    Task ToggleStateAsync();
    
    string GetAsStripes(DebugColor color);
    
    string GetAsBorder(DebugColor color);
    string GetAsBorderTop(DebugColor color);
    string GetAsBorderRight(DebugColor color);
    string GetAsBorderBottom(DebugColor color);
    string GetAsBorderLeft(DebugColor color);
}
