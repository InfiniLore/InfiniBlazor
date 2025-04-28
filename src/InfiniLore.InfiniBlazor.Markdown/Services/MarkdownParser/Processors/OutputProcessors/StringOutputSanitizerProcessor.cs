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
    public ValueTask<string?> TryProcessOutputAsync(string output, CancellationToken ct = default) {
        try {
            string refinedOutput = sanitizer.Sanitize(output);
            return ValueTask.FromResult<string?>(refinedOutput);
        }
        catch (Exception e) {
            logger.LogError(e, "Sanitize failed");
            return ValueTask.FromResult<string?>(null);
        }
    }
}
