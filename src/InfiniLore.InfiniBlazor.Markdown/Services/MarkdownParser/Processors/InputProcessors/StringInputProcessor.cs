// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using JetBrains.Annotations;

namespace InfiniLore.InfiniBlazor.Markdown.Processors.InputProcessors;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UsedImplicitly]
public class StringInputProcessor : IMarkdownInputProcessor<string> {
    public ValueTask<string?> TryProcessInput(string input, CancellationToken ct = default) {
        if (input.IsNullOrWhiteSpace()) return ValueTask.FromResult<string?>(input);
        string output = input.Trim();
        return ValueTask.FromResult<string?>(output);
    }
}
