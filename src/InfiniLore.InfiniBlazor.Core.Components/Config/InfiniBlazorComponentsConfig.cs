// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Components.CodeBlock;
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Core.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Frozen;

namespace InfiniLore.InfiniBlazor.Components.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniBlazorComponentsConfig : IComponentsConfig {
    private Dictionary<string, Type> CodeBlockLanguageHandlersDictionary { get; } = new();
    private Lazy<FrozenDictionary<string, Type>> CodeBlockLanguageHandlers { get; }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Constructors 
    // -----------------------------------------------------------------------------------------------------------------
    public InfiniBlazorComponentsConfig(IServiceCollection serviceCollection) {
        serviceCollection.RegisterServicesFromInfiniLoreInfiniBlazorCoreComponents();
        serviceCollection.AddSingleton<IComponentsConfig>(this);
        
        CodeBlockLanguageHandlers = new Lazy<FrozenDictionary<string, Type>>(() => CodeBlockLanguageHandlersDictionary.ToFrozenDictionary(
            StringComparer.OrdinalIgnoreCase
        ));

        this.AddMermaidCodeBlockHandler();
    }
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public InfiniBlazorComponentsConfig SetRenderMode(IComponentRenderMode renderMode) {
        InfiniRenderModeProvider.InfiniRenderMode = renderMode;
        return this;
    }
    
    public InfiniBlazorComponentsConfig AddCodeBlockLanguageInjection<T>(string language) where T : ComponentBase, ICodeBlockLanguageHandler {
        CodeBlockLanguageHandlersDictionary.AddOrUpdate(language, typeof(T));
        return this;
    }
    
    public InfiniBlazorComponentsConfig RemoveCodeBlockLanguageInjection(string language) {
        CodeBlockLanguageHandlersDictionary.Remove(language);
        return this;
    }
    
    public FrozenDictionary<string, Type> GetCodeBlockLanguageHandlers() => CodeBlockLanguageHandlers.Value;
}
