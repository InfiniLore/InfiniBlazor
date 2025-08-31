// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniTabContainerContext {
    public List<string> Tabs { get; } = new();
    public string? SelectedTabId { get; private set; }
    
    public Action? OnTabRegisterChange { get; set; }
    public Action? OnTabSelected { get; set; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void RegisterPage(string pageName) {
        Tabs.Add(pageName);
        SelectedTabId ??= pageName; // Define the first page as selected
        
        OnTabRegisterChange?.Invoke();
    }
    
    public void UnregisterPage(string pageName) {
        Tabs.Remove(pageName);
        if (SelectedTabId == pageName) SelectedTabId = Tabs.FirstOrDefault(); // Select the first page (if any)
        
        OnTabRegisterChange?.Invoke();
    }
    
    public void SelectPage(string pageName) {
        if (!Tabs.Contains(pageName)) {
            return;
        }
        
        SelectedTabId = pageName;
        OnTabSelected?.Invoke();
        
    }
}
