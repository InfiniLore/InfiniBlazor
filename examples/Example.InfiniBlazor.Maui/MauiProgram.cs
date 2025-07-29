// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.Logging;
using Serilog;

namespace Example.InfiniBlazor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class MauiProgram {
    public static MauiApp CreateMauiApp() {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .AsAnnaSasDevServerConsole(
                24,
                configure: asyncConsoleConfig => asyncConsoleConfig.ApplyThemeToRedirectedOutput = true
            )
            .CreateLogger();

        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();

        // WTF why do I need to set this to null? https://learn.microsoft.com/en-us/aspnet/core/blazor/hybrid/tutorials/maui-blazor-web-app?view=aspnetcore-9.0
        builder.Services.AddInfiniBlazor(config => config.SetRenderMode(null!));

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog();

        builder.Services.AddBlazorWebViewDeveloperTools();

        return builder.Build();
    }
}
