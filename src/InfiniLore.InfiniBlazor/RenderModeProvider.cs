// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace InfiniLore.InfiniBlazor;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class RenderModeProvider {
    public static IComponentRenderMode InfiniRenderMode { get; set; } = RenderMode.InteractiveServer;
}
