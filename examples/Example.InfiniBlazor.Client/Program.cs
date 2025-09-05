// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Example.InfiniBlazor.Shared.Themes;
using InfiniLore.InfiniBlazor.AutoDocumentation;

using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Emotes;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Example.InfiniBlazor.Client;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class Program {
    public static async Task Main(string[] args) {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        
        builder.Services.AddInfiniBlazor(config => {
            config.Theming.RegisterTheme<PrideThemeCollection>();
            config.AddMarkdownLogic();
            config.AddAutoDocumentation(static config => 
                config.RegisterAutoDocumentationData<AutoDocumenterData_ExampleInfiniBlazorShared>()
            );
        });
        
        builder.Services.AddHttpClient();

        builder.Services.AddAuthorizationCore();
        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddAuthenticationStateDeserialization();

        WebAssemblyHost app = builder.Build();
        await app.Services.GetRequiredService<IEmoteProvider>().InitializeAsync();
        
        await app.RunAsync();
    }
}