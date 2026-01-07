// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Serilog;
using Serilog.Extensions.Logging;

namespace InfiniBlazorDocs;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
internal static class Program {
    public static async Task Main(string[] args) {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .AsAnnaSasDevServerConsole(
                24,
                static asyncConsoleConfig => asyncConsoleConfig.ApplyThemeToRedirectedOutput = true
            )
            .WriteTo.BrowserConsole()
            .CreateLogger();
        
        // -------------------------------------------------------------------------------------------------------------
        // Builder
        // -------------------------------------------------------------------------------------------------------------
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(static sp => {
            var navigationManager = sp.GetRequiredService<NavigationManager>();
            return new HttpClient { BaseAddress = new Uri(navigationManager.BaseUri) };
        });
        
        builder.Services.AddInfiniBlazor(
            static config => config.Components.SetRenderMode(RenderMode.InteractiveWebAssembly)
        );

        builder.Services.RegisterServicesFromInfiniBlazorDocs();
        
        #if DEBUG
        builder.Services.AddHttpClient<RazorMarkdownFileExtractor>(client => {
            client.BaseAddress = new Uri("https://localhost:7285/");
            client.Timeout = TimeSpan.FromSeconds(10);
            client.DefaultRequestHeaders.Add("Accept", "text/markdown");
        });
        #endif

        builder.Logging.ClearProviders();
        builder.Logging.AddProvider(new SerilogLoggerProvider(Log.Logger, dispose: false));
        
        // -------------------------------------------------------------------------------------------------------------
        // App
        // -------------------------------------------------------------------------------------------------------------
        WebAssemblyHost app = builder.Build();

        await app.RunAsync();
    }
}
