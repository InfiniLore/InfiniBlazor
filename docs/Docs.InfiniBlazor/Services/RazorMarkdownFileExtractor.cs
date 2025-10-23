// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;

namespace Docs.InfiniBlazor.Services;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<RazorMarkdownFileExtractor>]
public class RazorMarkdownFileExtractor(ILogger<RazorMarkdownFileExtractor> logger, NavigationManager navigationManager, IHttpClientFactory? httpFactory = null) {
    #if DEBUG
    private readonly IHttpClientFactory _httpFactory = httpFactory ?? throw new ArgumentNullException(nameof(httpFactory));
    #endif

    public async Task<string?> ExtractEmbeddedResourceAsync(string resourcePath, CancellationToken ct = default) {
        if (resourcePath.IsNullOrWhiteSpace()) {
            logger.Warning("Resource path is null or empty");
            return null;
        }

        #if DEBUG
        try {
            string fileName = resourcePath.Split('.')[3..].Aggregate((a, b) => $"{a}.{b}");
            string url = $"/Pages/{fileName}";
            
            using HttpClient client = _httpFactory.CreateClient();
            client.BaseAddress = new Uri(navigationManager.BaseUri);
            logger.Warning("Fetching markdown from wwwroot: {Url}", new Uri(client.BaseAddress, url));
            
            string result = await client.GetStringAsync(url, ct);
            logger.Information("Markdown loaded from wwwroot: {Url}", url);
            return result;
        }
        catch (Exception ex) {
            logger.Warning(ex, "Could not fetch markdown from wwwroot, falling back to embedded resource: {ResourcePath}", resourcePath);
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
}
