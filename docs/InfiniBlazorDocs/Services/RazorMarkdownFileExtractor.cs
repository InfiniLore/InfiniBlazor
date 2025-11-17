// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;

namespace InfiniBlazorDocs.Services;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<RazorMarkdownFileExtractor>]
public class RazorMarkdownFileExtractor(
    ILogger<RazorMarkdownFileExtractor> logger
    #if DEBUG
    ,HttpClient client
    #endif
) {

    public async Task<string?> ExtractEmbeddedResourceAsync(string resourcePath, CancellationToken ct = default) {
        if (resourcePath.IsNullOrWhiteSpace()) {
            logger.Warning("Resource path is null or empty");
            return null;
        }
        #if DEBUG
        string? fileName = resourcePath.Split('.', 4).LastOrDefault();
        try {
            using HttpResponseMessage response = await client.GetAsync($"/api/markdown/{fileName}", ct);
            if (response.IsSuccessStatusCode) {
                logger.Information("Valid markdown content extracted from resource: {fileName}", fileName);
                return await response.Content.ReadAsStringAsync(ct);
            }
            logger.Warning("Markdown file could not be found on API: {fileName}", fileName);
        }
        catch (Exception e) {
            logger.Error(e, "Failed to extract markdown content from resource: {fileName} defaulting to embeddded content", fileName);
        }
        #endif

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
    
    public bool IsValidResoucePath(string resourcePath, CancellationToken ct = default) => 
        !resourcePath.IsNullOrWhiteSpace() 
        && typeof(RazorMarkdownFileExtractor).Assembly.GetManifestResourceNames().Contains(resourcePath);
}
