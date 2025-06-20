// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Ganss.Xss;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace InfiniLore.InfiniBlazor.Markdown.Processors.OutputProcessors;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UsedImplicitly]
public class StringOutputSanitizerProcessor(IHtmlSanitizer sanitizer, ILogger<StringOutputSanitizerProcessor> logger) : IMarkdownOutputProcessor<string> {
    public string? TryProcessOutputAsync(string output) {
        try {
            string refinedOutput = sanitizer.Sanitize(output);
            return refinedOutput;
        }
        catch (Exception e) {
            logger.LogError(e, "Sanitize failed");
            return null;
        }
    }
}
