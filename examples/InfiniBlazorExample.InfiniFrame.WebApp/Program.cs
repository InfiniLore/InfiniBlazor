// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.AutoDocumentation;
using InfiniBlazorExample.InfiniFrame.WebApp.Components;
using InfiniBlazorExample.Shared;
using InfiniFrame;
using InfiniFrame.Js;
using InfiniFrame.WebServer;
using Serilog;

namespace InfiniBlazorExample.InfiniFrame.WebApp;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class Program {
    [STAThread]
    public static void Main(string[] args) {
        // -------------------------------------------------------------------------------------------------------------
        // Builder
        // -------------------------------------------------------------------------------------------------------------
        InfiniFrameWebApplicationBuilder builder = InfiniFrameWebApplication.CreateBuilder(args);
        WebApplicationBuilder appBuilder = builder.WebApp;
        
        appBuilder.Services.AddLogging(config => {
            config.ClearProviders();
            config.AddSerilog();
        });

        appBuilder.Services.AddSerilog(config => {
            config.WriteTo.Async(static c => c.Console())
                .MinimumLevel.Debug();
        });

        appBuilder.Services.AddInfiniBlazor(config => {
            config.AddAutoDocumentation(static config => 
                config.RegisterAutoDocumentationData<AutoDocumenterData_InfiniBlazorExampleClient>()
            );
        });

        appBuilder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();
        
        appBuilder.Services.AddHttpClient();

        appBuilder.Services.AddLucideIcons();
        
        appBuilder.Services.AddInfiniFrameJs();
        
        appBuilder.WebHost.UseStaticWebAssets();
        
        InfiniFrameWindowBuilder windowBuilder = builder.Window;
        windowBuilder
            .SetUseOsDefaultSize(true)
            .SetUseOsDefaultLocation(true)
            .SetTitle("InfiniBlazor.InfiniFrame Sample");

        // -------------------------------------------------------------------------------------------------------------
        // App
        // -------------------------------------------------------------------------------------------------------------
        InfiniFrameWebApplication app = builder.Build();
        WebApplication webApp = app.WebApp;

        webApp.UseRouting();

        webApp.UseAntiforgery();
        webApp.MapStaticAssets();

        webApp.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddAdditionalAssemblies(ISharedEntry.Assembly);

        app.Run();
    }
}
