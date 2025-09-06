// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Core.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Immutable;

namespace InfiniLore.InfiniBlazor.Components.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniBlazorComponentsConfig : IComponentsConfig {
    private List<string> EmoteJsonLibFiles { get; } = [
        "/_content/InfiniLore.InfiniBlazor/libs/emotes/emotes_standard.json",
        "/_content/InfiniLore.InfiniBlazor/libs/emotes/emotes_lucide.json"
    ];
    
    private Lazy<ImmutableArray<string>> EmoteJsonLibFilePathsLazy { get; }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public InfiniBlazorComponentsConfig(IServiceCollection serviceCollection) {
        serviceCollection.RegisterServicesFromInfiniLoreInfiniBlazorCoreComponents();
        serviceCollection.AddSingleton<IComponentsConfig>(this);
        
        EmoteJsonLibFilePathsLazy = new Lazy<ImmutableArray<string>>(() => EmoteJsonLibFiles.ToImmutableArray());
    }
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public InfiniBlazorComponentsConfig SetRenderMode(IComponentRenderMode renderMode) {
        InfiniRenderModeProvider.InfiniRenderMode = renderMode;
        return this;
    }
    
    public ImmutableArray<string> GetEmoteJsonLibFilePaths()
        => EmoteJsonLibFilePathsLazy.Value;
}
