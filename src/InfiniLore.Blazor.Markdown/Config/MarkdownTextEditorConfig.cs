// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.Blazor.Markdown.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MarkdownTextEditorConfig(IInfiniLoreBlazorConfig config) {
    private readonly HashSet<string> ModifierNames = new();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public MarkdownTextEditorConfig AddModifier<TModifier>(string modifierKey) where TModifier : class, ITextModifier {
        config.Services.AddKeyedSingleton<ITextModifier, TModifier>(modifierKey);
        ModifierNames.Add(modifierKey);
        return this;
    }
    
    public MarkdownTextEditorConfig AddDefaultModifiers() {
        foreach (ServiceDescriptor descriptor in config.Services) {
            if (descriptor is not { IsKeyedService: true, ServiceKey: string instance, ServiceType: var type }) continue;
            if (!typeof(ITextModifier).IsAssignableFrom(type)) continue;

            ModifierNames.Add(instance);
        }
        return this;
    }
    
    public IEnumerable<string> GetModifierNames() => ModifierNames;
}
