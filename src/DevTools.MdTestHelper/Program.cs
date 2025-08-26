// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using DevTools.MdTestHelper.Components;
using InfiniLore.InfiniBlazor.Config;
using System.Diagnostics.CodeAnalysis;
using Tests.Shared.InfiniLore.InfiniBlazor.Markdown;

namespace DevTools.MdTestHelper;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[SuppressMessage("Usage", "TUnit0034:Do not declare a main method")]
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
        builder.Services.RegisterServicesFromTestsSharedInfiniLoreInfiniBlazorMarkdown();

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
