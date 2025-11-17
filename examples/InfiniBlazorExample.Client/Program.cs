// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.AutoDocumentation;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace InfiniBlazorExample.Client;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class Program {
    public static async Task Main(string[] args) {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        
        builder.Services.AddInfiniBlazor(config => {
            config.AddAutoDocumentation(static config => 
                config.RegisterAutoDocumentationData<AutoDocumenterData_InfiniBlazorExampleClient>()
            );
        });
        
        builder.Services.AddHttpClient();

        builder.Services.AddAuthorizationCore();
        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddAuthenticationStateDeserialization();

        WebAssemblyHost app = builder.Build();
        await app.RunAsync();
    }
}