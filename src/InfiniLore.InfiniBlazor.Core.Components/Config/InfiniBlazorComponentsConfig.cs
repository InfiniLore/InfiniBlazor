// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Core.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Components.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniBlazorComponentsConfig : IComponentsConfig {
    // -----------------------------------------------------------------------------------------------------------------
    // Constructors 
    // -----------------------------------------------------------------------------------------------------------------
    public InfiniBlazorComponentsConfig(IServiceCollection serviceCollection) {
        serviceCollection.RegisterServicesFromInfiniLoreInfiniBlazorCoreComponents();
        serviceCollection.AddSingleton<IComponentsConfig>(this);
    }
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public InfiniBlazorComponentsConfig SetRenderMode(IComponentRenderMode renderMode) {
        InfiniRenderModeProvider.InfiniRenderMode = renderMode;
        return this;
    }
}
