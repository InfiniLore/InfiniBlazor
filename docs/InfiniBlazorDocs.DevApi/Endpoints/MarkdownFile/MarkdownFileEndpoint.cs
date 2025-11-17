// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazorDocs.DevApi.Services;
using FastEndpoints;

namespace InfiniBlazorDocs.DevApi.Endpoints.MarkdownFile;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MarkdownFileEndpoint(MarkdownFileProvider markdownFileProvider) : Endpoint<MarkdownFileRequest> {
    public override void Configure() {
        Get("/api/markdown/{path}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(MarkdownFileRequest request, CancellationToken ct) {
        if (request.Path.IsNullOrWhiteSpace()) {
            await Send.NotFoundAsync(ct);
            return;
        }

        await using Stream? stream = await markdownFileProvider.GetMarkdownFileAsync(request.Path, ct);
        if (stream is null){
            await Send.NotFoundAsync(ct);
            return;
        }
        
        await Send.StreamAsync(
            stream,
            fileName: request.Path,
            fileLengthBytes: stream.Length,
            contentType: "text/markdown",
            cancellation: ct
        );
    }
}
