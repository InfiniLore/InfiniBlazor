// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;

namespace Docs.InfiniBlazor.Services;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<RazorMarkdownFileExtractor>]
public class RazorMarkdownFileExtractor(ILogger<RazorMarkdownFileExtractor> logger) {

    public async Task<string?> ExtractEmbeddedResourceAsync(string resourcePath, CancellationToken ct = default) {
        if (resourcePath.IsNullOrWhiteSpace()) {
            logger.Warning("Resource path is null or empty");
            return null;
        }

        try {
            await using Stream? stream = typeof(RazorMarkdownFileExtractor).Assembly.GetManifestResourceStream(resourcePath);
            if (stream == null) {
                logger.Warning("Resource stream could not be found for path: {ResourcePath}", resourcePath);
                return null;
            }

            using var reader = new StreamReader(stream);
            string result = await reader.ReadToEndAsync(ct);
            logger.Information("Valid markdown content extracted from resource: {ResourcePath}", resourcePath);
            return result;
        }
        catch (Exception ex) {
            logger.Error(ex, "Failed to extract markdown content from resource: {ResourcePath}", resourcePath);
            return null;
        }
    }
}
