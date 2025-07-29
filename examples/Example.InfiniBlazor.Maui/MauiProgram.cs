// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Example.InfiniBlazor.Shared.Themes;
using InfiniLore.InfiniBlazor.Config;
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
                static asyncConsoleConfig => asyncConsoleConfig.ApplyThemeToRedirectedOutput = true
            )
            .CreateLogger();

        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();
        builder.ConfigureFonts(static fonts => {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
        });

        builder.Services.AddMauiBlazorWebView();

        builder.Services.AddHttpClient();
        builder.AddInfiniBlazor(config => {
            config.RegisterTheme<PrideThemeCollection>();
            config.AddMarkdownLogic();
        });

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog();

        builder.Services.AddBlazorWebViewDeveloperTools();

        return builder.Build();
    }
}
