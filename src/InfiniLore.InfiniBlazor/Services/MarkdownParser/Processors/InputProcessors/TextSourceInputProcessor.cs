// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Processors;
using JetBrains.Annotations;

namespace InfiniLore.InfiniBlazor.MarkdownParser.Processors.InputProcessors;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UsedImplicitly]
public class TextSourceInputProcessor : IMarkdownInputProcessor<ITextSource> {
    public ITextSource? TryProcessInput(ITextSource input) {
        if (input.Length == 0 || input.TextSpan.IsWhiteSpace() || input.Lines.Count == 0) return null;
        return input;
    }
}
