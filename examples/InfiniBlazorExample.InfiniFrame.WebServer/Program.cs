// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.AutoDocumentation;
using InfiniBlazorExample.InfiniFrame.WebServer.Components;
using InfiniBlazorExample.Shared;
using InfiniFrame;
using InfiniFrame.Js;
using InfiniFrame.Js.MessageHandlers;
using InfiniFrame.WebServer;
using Serilog;

namespace InfiniBlazorExample.InfiniFrame.WebServer;
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

        builder.AddInfiniBlazor(config => {
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
            .SetTitle("InfiniBlazor.InfiniFrame Sample")
            .RegisterOpenExternalTargetWebMessageHandler();

        // -------------------------------------------------------------------------------------------------------------
        // App
        // -------------------------------------------------------------------------------------------------------------
        InfiniFrameWebApplication application = builder.Build();
        application.UseAutoServerClose();
        WebApplication webApp = application.WebApp;

        webApp.UseRouting();

        webApp.UseAntiforgery();
        webApp.MapStaticAssets();

        webApp.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddAdditionalAssemblies(ISharedEntry.Assembly);

        application.Run();
    }
}
