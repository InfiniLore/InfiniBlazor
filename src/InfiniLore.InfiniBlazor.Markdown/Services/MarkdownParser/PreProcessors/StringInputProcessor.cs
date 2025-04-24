// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using JetBrains.Annotations;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Markdown.PreProcessors;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UsedImplicitly]
public class StringInputProcessor : IMarkdownPreProcessor<string> {
    public bool TryProcess(string input, [NotNullWhen(true)] out string? output) {
        output = null;
        if (input.IsNullOrWhiteSpace()) return false;
        output = input.Trim();
        return true;
    }
}
