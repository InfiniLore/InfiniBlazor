// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Immutable;

namespace InfiniLore.InfiniBlazor.Markdown.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class FrozenMarkdownConfigFactory {
    public static FrozenMarkdownConfig FromServiceProvider(IServiceProvider serviceProvider) {
        var originalConfig = serviceProvider.GetRequiredService<MarkdownConfig>();
        
        return new FrozenMarkdownConfig {
            TextEditorModifierNames = originalConfig.TextEditor.GetModifierNames().OrderBy(name => name).ToImmutableArray(),
        };
    }
}
