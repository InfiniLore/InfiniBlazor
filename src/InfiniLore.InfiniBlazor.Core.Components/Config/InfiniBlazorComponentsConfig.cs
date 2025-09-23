// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Core.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Immutable;
using System.Reflection;

namespace InfiniLore.InfiniBlazor.Components.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniBlazorComponentsConfig : IComponentsConfig {
    private List<string> EmoteJsonLibFiles { get; } = [
        "/_content/InfiniLore.InfiniBlazor/libs/emotes/emotes_standard.json",
        "/_content/InfiniLore.InfiniBlazor/libs/emotes/emotes_lucide.json",
        "/_content/InfiniLore.InfiniBlazor/libs/emotes/emotes_duckies.json",
    ];
    
    private List<Type> EmbeddedResourceAssemblyEntryPoints { get; } = [];
        
    private Lazy<ImmutableArray<string>> EmoteJsonLibFilePathsLazy { get; }
    private Lazy<ImmutableArray<Assembly>> EmbeddedResourceAssembliesLazy { get; }
    
    
    // -----------------------------------------------------------------------------------------------------------------
    // Constructors 
    // -----------------------------------------------------------------------------------------------------------------
    public InfiniBlazorComponentsConfig(IServiceCollection serviceCollection) {
        serviceCollection.RegisterServicesFromInfiniLoreInfiniBlazorCoreComponents();
        serviceCollection.AddSingleton<IComponentsConfig>(this);
        
        EmoteJsonLibFilePathsLazy = new Lazy<ImmutableArray<string>>(() => EmoteJsonLibFiles.ToImmutableArray());
        EmbeddedResourceAssembliesLazy = new Lazy<ImmutableArray<Assembly>>(() => EmbeddedResourceAssemblyEntryPoints.Select(t => t.Assembly).ToImmutableArray());
    }
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public InfiniBlazorComponentsConfig SetRenderMode(IComponentRenderMode renderMode) {
        InfiniRenderModeProvider.InfiniRenderMode = renderMode;
        return this;
    }

    public InfiniBlazorComponentsConfig AddEmbeddedResourceAssembly<TEntrypoint>() {
        EmbeddedResourceAssemblyEntryPoints.Add(typeof(TEntrypoint));
        return this;
    }
    
    public ImmutableArray<string> GetEmoteJsonLibFilePaths()
        => EmoteJsonLibFilePathsLazy.Value;
    
    public bool TryGetEmbeddedResourceAssemblies(out ImmutableArray<Assembly> assemblies) {
        if (EmbeddedResourceAssembliesLazy.IsValueCreated || !EmbeddedResourceAssemblyEntryPoints.IsEmpty()) {
            assemblies = EmbeddedResourceAssembliesLazy.Value;
            return true;
        }

        assemblies = ImmutableArray<Assembly>.Empty;
        return false;
    }
}
