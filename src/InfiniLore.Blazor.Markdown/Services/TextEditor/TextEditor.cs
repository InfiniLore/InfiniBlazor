// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Frozen;

namespace InfiniLore.Blazor.Markdown.Services;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ITextEditor>]
public class TextEditor(IServiceProvider provider) : ITextEditor {
    public string Text { get; set; } = string.Empty;
    
    private FrozenDictionary<string, ITextModifier> ModifierLookup { get; } = ModifiersKeys.ToFrozenDictionary(
        name => name,
        provider.GetRequiredKeyedService<ITextModifier>
    );
    private static string[] ModifiersKeys { get; } = [
        "bold",
        "italic"
    ];
    public IEnumerable<ITextModifier> Modifiers => ModifierLookup.Values;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void Modify(string section, Range range) {
        if (!ModifierLookup.TryGetValue(section, out ITextModifier? modifier)) return;
        Text = modifier.Modify(Text, range);
    }
}
