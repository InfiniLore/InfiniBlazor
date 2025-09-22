// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using DevTools.MdTestHelper.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Diagnostics.CodeAnalysis;
using Tests.InfiniBlazor.Shared.Markdown;

namespace DevTools.MdTestHelper;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[SuppressMessage("Usage", "TUnit0034:Do not declare a main method")]
public class Program {
    public static void Main(string[] args) {
        // -------------------------------------------------------------------------------------------------------------
        // App
        // -------------------------------------------------------------------------------------------------------------
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddInfiniBlazor(config => {
            config.Components.SetRenderMode(RenderMode.InteractiveServer);
            config.Markdown.RenderUnknownBlazorComponents = true;
            config.Markdown.SkipBlazorRenderingOnComponent<NewLineMdSyntaxNode>();
        });

        builder.Services.RegisterServicesFromDevToolsMdTestHelper();
        builder.Services.RegisterServicesFromTestsInfiniBlazorSharedMarkdown();

        // -----------------------------------------------------------------------------------------------------------------
        // App
        // -----------------------------------------------------------------------------------------------------------------
        WebApplication app = builder.Build();

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
