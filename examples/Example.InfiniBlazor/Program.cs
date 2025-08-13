// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Example.InfiniBlazor.Components;
using Example.InfiniBlazor.Shared;
using Example.InfiniBlazor.Shared.Themes;
using InfiniLore.InfiniBlazor.Config;
using Microsoft.AspNetCore.Components.Web;

namespace Example.InfiniBlazor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class Program {
    public static void Main(string[] args) {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddLogging();

        builder.Services.AddInfiniBlazor(config => {
            config.SetComponentRenderMode(RenderMode.InteractiveAuto);
            config.RegisterTheme<PrideThemeCollection>();
            config.AddMarkdownLogic();
        });
        
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents()
            .AddAuthenticationStateSerialization();
        
        builder.Services.AddHttpClient();

        builder.Services.AddLucideIcons();

        // -------------------------------------------------------------------------------------------------------------
        // App
        // -------------------------------------------------------------------------------------------------------------
        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment()) {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Client._Imports).Assembly, ISharedEntry.Assembly);

        app.Run();
    }
}