// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Ganss.Xss;
using JetBrains.Annotations;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Markdown.PostProcessors;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UsedImplicitly]
public class SanitizerPostProcessor(IHtmlSanitizer sanitizer) : IMarkdownPostProcessor<string> {
    public bool TryProcess(string input, [NotNullWhen(true)] out string? output) {
        output = sanitizer.Sanitize(input);
        return true;
    }
}
