// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Ganss.Xss;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Markdown.Processors.OutputProcessors;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UsedImplicitly]
public class StringOutputSanitizerProcessor(IHtmlSanitizer sanitizer, ILogger<StringOutputSanitizerProcessor> logger) : IMarkdownOutputProcessor<string> {
    public bool TryProcessOutput(string output,[NotNullWhen(true)] out string? refinedOutput) {
        try {
            refinedOutput = sanitizer.Sanitize(output);
            return true;
        }
        catch (Exception e) {
            logger.LogError(e, "Sanitize failed");
            refinedOutput = null;
            return false;
        }
    }
}
