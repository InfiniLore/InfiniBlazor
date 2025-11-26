// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.AutoDocumentation;
using InfiniBlazorExample.InfiniFrame.BlazorWebView.Components;
using InfiniFrame;
using InfiniFrame.BlazorWebView;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace InfiniBlazorExample.InfiniFrame.BlazorWebView;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class Program {
    [STAThread]
    public static void Main(string[] args) {
        // -------------------------------------------------------------------------------------------------------------
        // Builder
        // -------------------------------------------------------------------------------------------------------------
        var builder = InfiniFrameBlazorAppBuilder.CreateDefault(args);
        
        builder.Services.AddLogging(config => {
            config.ClearProviders();
            config.AddSerilog();
        });

        builder.Services.AddSerilog(config => {
            config.WriteTo.Async(static c => c.Console())
                .MinimumLevel.Debug();
        });

        builder.RootComponents.Add<App>("app");
        
        builder.WithInfiniFrameWindowBuilder(windowBuilder => {
            windowBuilder
                .SetUseOsDefaultSize(true)
                .SetUseOsDefaultLocation(true)
                .SetTitle("InfiniBlazor.InfiniFrame Sample");
        });
        
        builder.Services.AddInfiniBlazor(config => {
            config.AddAutoDocumentation(static config => 
                config.RegisterAutoDocumentationData<AutoDocumenterData_InfiniBlazorExampleClient>()
            );
            config.Components.SetRenderMode(null!);
        });
        
        builder.Services.AddHttpClient();

        builder.Services.AddLucideIcons();

        // -------------------------------------------------------------------------------------------------------------
        // App
        // -------------------------------------------------------------------------------------------------------------
        InfiniFrameBlazorApp app = builder.Build();
        
        app.Run();
    }
}
