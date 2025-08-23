// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InfiniLore.InfiniBlazor.Components;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableTransient<InfiniTabContainerContext>]
public class InfiniTabContainerContext(ILogger<InfiniTabContainerContext> logger) {
    public List<string> Tabs { get; } = new();
    public string? SelectedTab { get; set; }
    
    public Action? OnTabRegisterChange { get; set; }
    public Action? OnTabSelected { get; set; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void RegisterPage(string pageName) {
        Tabs.Add(pageName);
        SelectedTab ??= pageName; // Define the first page as selected
        
        OnTabRegisterChange?.Invoke();
    }
    
    public void UnregisterPage(string pageName) {
        Tabs.Remove(pageName);
        if (SelectedTab == pageName) SelectedTab = Tabs.FirstOrDefault(); // Select the first page (if any)
        
        OnTabRegisterChange?.Invoke();
    }
    
    public void SelectPage(string pageName) {
        if (!Tabs.Contains(pageName)) {
            logger.Warning("Page '{TabName}' is not registered in the tab container.", pageName);
            return;
        }
        
        SelectedTab = pageName;
        OnTabSelected?.Invoke();
        logger.Warning("Page '{TabName}' selected.", pageName);
    }
}
