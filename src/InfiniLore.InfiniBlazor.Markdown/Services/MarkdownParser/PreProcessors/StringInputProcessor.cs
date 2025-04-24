// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Markdown.PreProcessors;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class StringInputProcessor : IMarkdownPreProcessor<string> {
    public bool TryProcess(string input, [NotNullWhen(true)] out string? output) {
        output = input;
        if (input.IsNullOrWhiteSpace()) return false;
        return true;
    }
}
