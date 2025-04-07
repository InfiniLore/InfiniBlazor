// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.Blazor.Markdown.Config;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Frozen;

namespace InfiniLore.Blazor.Markdown.Services;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ITextEditor>]
public class TextEditor(MarkdownConfig config, IServiceProvider provider) : ITextEditor {
    private readonly FrozenDictionary<string, ITextModifier> Modifiers = config.TextEditor.Modifiers
        .Select(name => (name, provider.GetRequiredKeyedService<ITextModifier>(name)))
        .ToFrozenDictionary(
            box => box.name, 
            box => box.Item2
        );
    
    private Dictionary<string, string> CtrlToModifier { get; } = new();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    // public string Modify((string modifier, string key) input, Range range) {
    //     if (modifier is ctrl) {
    //         if (CtrlToModifier.TryGetValue(modifier, out string? modifierName)) return input;
    //         if (!Modifiers.TryGetValue(modifierName, out ITextModifier? modifier)) return input;
    //         return  modifier.Modify(input, range);   
    //     }
    //     
    //     // Same for shift, etc...
    //     return input
    // }
}
