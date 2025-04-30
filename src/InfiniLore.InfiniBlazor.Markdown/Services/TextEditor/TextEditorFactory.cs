// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Frozen;

namespace InfiniLore.InfiniBlazor.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class TextEditorFactory {
    public static ITextEditor CreateTextEditor(IServiceProvider provider) => CreateKeyedTextEditor(provider, null);
    public static ITextEditor CreateKeyedTextEditor(IServiceProvider provider, object? key) {
        IEnumerable<ITextModifier> modifiers = provider.GetKeyedServices<ITextModifier>(key);
        
        // Take the last modifier for each modifier name to handle duplicates
        FrozenDictionary<string, ITextModifier> modifierLookup = modifiers
            .GroupBy(modifier => modifier.ModifierName)
            .ToFrozenDictionary(
                group => group.Key,
                group => group.Last()
            );

        return new TextEditor {
            ModifierLookup = modifierLookup
        };

    }
}
