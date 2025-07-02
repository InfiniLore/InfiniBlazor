// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Processors;
using JetBrains.Annotations;

namespace InfiniLore.InfiniBlazor.Processors.InputProcessors;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UsedImplicitly]
public class StringInputProcessor : IMarkdownInputProcessor<string> {
    public string TryProcessInput(string input) {
        if (input.IsNullOrWhiteSpace()) return input;
        string output = input.Trim();
        return output;
    }
}
