// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using DevTools.MdTestHelper.Components;
using InfiniLore.InfiniBlazor.Config;

namespace DevTools.MdTestHelper;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class Program {
    public static void Main(string[] args) {
        // -------------------------------------------------------------------------------------------------------------
        // App
        // -------------------------------------------------------------------------------------------------------------
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddInfiniBlazor(config => 
            config.AddMarkdownLogic()
        );

        builder.Services.RegisterServicesFromDevToolsMdTestHelper();

        // -----------------------------------------------------------------------------------------------------------------
        // App
        // -----------------------------------------------------------------------------------------------------------------
        var app = builder.Build();

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
