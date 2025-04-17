// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;
using System.Collections.Immutable;

namespace InfiniLore.InfiniBlazor.Markdown.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class FrozenMarkdownConfig : IMarkdownConfig {
    public required ImmutableArray<string> TextEditorModifierNames { get; init; }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Constructor
    // -----------------------------------------------------------------------------------------------------------------
    public static FrozenMarkdownConfig FromConfig(MarkdownConfig config) {
        return new FrozenMarkdownConfig {
            TextEditorModifierNames = config.TextEditor.GetModifierNames().OrderBy(name => name).ToImmutableArray()
        };
    }
}
