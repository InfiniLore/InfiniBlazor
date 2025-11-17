// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using FastEndpoints;

namespace InfiniBlazorDocs.DevApi;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class Program {
    private const string DocsCors = nameof(DocsCors);
    public static void Main(string[] args) {
        // -------------------------------------------------------------------------------------------------------------
        // Builder
        // -------------------------------------------------------------------------------------------------------------
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.Services.AddFastEndpoints();
        builder.Services.RegisterServicesFromInfiniBlazorDocsDevApi();

        builder.Services.AddCors(options => {
            options.AddPolicy(DocsCors, policy => {
                policy.AllowAnyOrigin();
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            });
        });

        // -------------------------------------------------------------------------------------------------------------
        // App
        // -------------------------------------------------------------------------------------------------------------
        WebApplication app = builder.Build();
        
        app.UseCors(DocsCors);

        app.UseHttpsRedirection();
        
        app.UseFastEndpoints();

        app.Run();
    }
}
