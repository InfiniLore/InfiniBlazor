// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;

namespace Docs.InfiniBlazor.Api.Services;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<MarkdownFileProvider>]
public class MarkdownFileProvider {
    private static readonly string SolutionRoot = Path.Combine(Environment.CurrentDirectory.Split(@"\")[..^2]);
    private const string DocsPagesFolder = "docs/Docs.InfiniBlazor/Pages";
    
    public async Task<Stream?> GetMarkdownFileAsync(string path, CancellationToken ct = default) {
        string fullPath = Path.Combine(SolutionRoot, DocsPagesFolder, path);
        if (!File.Exists(fullPath)) return null;
        
        byte[] bytes = await File.ReadAllBytesAsync(fullPath, ct);
        return new MemoryStream(bytes);
    }
}
