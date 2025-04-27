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
public class TextSourceInputProcessor : IMarkdownInputProcessor<ITextSource> {
    public bool TryProcessInput(ITextSource input, [NotNullWhen(true)] out ITextSource? output) {
        output = input;
        if (input.Length == 0) return false;
        if (input.TextSpan.IsWhiteSpace()) return false;
        if (input.Lines.Count == 0) return false;
        return true;
    }
}
