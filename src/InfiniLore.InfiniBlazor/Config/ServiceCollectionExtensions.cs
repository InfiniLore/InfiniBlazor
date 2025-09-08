// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor;
using InfiniLore.InfiniBlazor.Components;
using InfiniLore.InfiniBlazor.Components.DataLoaders;
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Core.Js;
using InfiniLore.InfiniBlazor.Markdown.Config;
using InfiniLore.InfiniBlazor.Markdown.Editors;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ServiceCollectionExtensions {
    public static IServiceCollection AddInfiniBlazor(this IServiceCollection services, Action<InfiniBlazorConfig>? configure = null) {
        var config = new InfiniBlazorConfig(services); // Handles a lot of the boilerplate for each core package
        services.RegisterServicesFromInfiniLoreInfiniBlazor();
        services.RegisterServicesFromInfiniLoreInfiniBlazorCoreJs();
        services.AddLucideIcons();

        services.AddSingleton(CalloutStyleProviderFactory.Create);
        services.AddSingleton(MdStringMdSyntaxDeserializerFactory.Create);
        services.AddSingleton(TextEditorFactory.CreateTextEditor);

        config.Markdown.WithDefaultEditorComponents();

        services.AddHttpClient(HttpClientNames.InfiniBlazor, static (serviceProvider, client) => {
            var config = serviceProvider.GetService<IConfiguration>();
            string? baseUrl = null;

            // In WASM, use NavigationManager to get the browser's origin
            try {
                if (serviceProvider.GetService<NavigationManager>() is {} navManager) {
                    baseUrl = navManager.BaseUri;
                }
            }
            catch (Exception) {
                // Ignore
            }
            
            // In Server, use BaseUrl, or parse ASPNETCORE_URLS
            if (baseUrl.IsNullOrWhiteSpace()) {
                baseUrl = config?["BaseUrl"];

                if (baseUrl.IsNullOrWhiteSpace()) {
                    string? urls = config?["ASPNETCORE_URLS"];
                    if (urls.IsNotNullOrWhiteSpace()) {
                        string[] urlsSplit = urls.Split(';', StringSplitOptions.RemoveEmptyEntries)
                            .Select(u => u.Trim()).ToArray();

                        baseUrl = urlsSplit.FirstOrDefault(u => u.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                            ?? urlsSplit.FirstOrDefault();
                    }
                }
            }

            if (baseUrl.IsNotNullOrWhiteSpace()) client.BaseAddress = new Uri(baseUrl.TrimEnd('/'), UriKind.Absolute);
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        configure?.Invoke(config);

        return services;
    }
}
