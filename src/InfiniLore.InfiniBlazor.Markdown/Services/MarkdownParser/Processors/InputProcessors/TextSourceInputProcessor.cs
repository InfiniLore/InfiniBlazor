// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using JetBrains.Annotations;

namespace InfiniLore.InfiniBlazor.Markdown.Processors.InputProcessors;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UsedImplicitly]
public class TextSourceInputProcessor : IMarkdownInputProcessor<ITextSource> {
    public ValueTask<ITextSource?> TryProcessInput(ITextSource input, CancellationToken ct = default) {
        if (input.Length == 0 || input.TextSpan.IsWhiteSpace() || input.Lines.Count == 0) return ValueTask.FromResult<ITextSource?>(null);
        return ValueTask.FromResult<ITextSource?>(input);
    }
}
