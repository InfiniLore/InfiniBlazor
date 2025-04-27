// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using JetBrains.Annotations;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Markdown.Processors.InputProcessors;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UsedImplicitly]
public class StringInputProcessor : IMarkdownInputProcessor<string> {
    public bool TryProcessInput(string input, [NotNullWhen(true)] out string? output) {
        output = null;
        if (input.IsNullOrWhiteSpace()) return false;
        output = input.Trim();
        return true;
    }
}
