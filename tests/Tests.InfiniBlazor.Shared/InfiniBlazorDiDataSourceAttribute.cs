// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor;
using InfiniLore.InfiniBlazor.Components;
using InfiniLore.InfiniBlazor.Components.DataLoaders;
using InfiniLore.InfiniBlazor.Config;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Tests.InfiniBlazor.Shared;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniBlazorDiDataSourceAttribute : DependencyInjectionDataSourceAttribute<IServiceScope> {
    private static readonly IServiceProvider ServiceProvider = CreateSharedServiceProvider();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override IServiceScope CreateScope(DataGeneratorMetadata dataGeneratorMetadata) => ServiceProvider.CreateScope();
    public override object? Create(IServiceScope scope, Type type) => scope.ServiceProvider.GetService(type);

    private static ServiceProvider CreateSharedServiceProvider() {
        var services = new ServiceCollection();

        services.AddLogging();
        services.AddLucideIcons();
        services.AddInfiniBlazor();
        
        // Special case for emoting, as the testing environment isn't a full Blazor app
        services.AddSingleton<IEmoteDataLoader>(static provider => {
            IEnumerable<string> paths = provider.GetRequiredService<IComponentsConfig>()
                .GetEmoteJsonLibFilePaths()
                .Select(static path => path.TrimStart('/')
                    .Replace("_content/InfiniLore.InfiniBlazor/", "InfiniLore.InfiniBlazor.wwwroot.")
                    .Replace("/", ".")
                );
            
            return new AssemblyEmoteDataLoader(
                typeof(InfiniBlazorConfig).Assembly,
                paths,
                provider.GetRequiredService<ILogger<AssemblyEmoteDataLoader>>()
            );
        });
        
        services.AddTransient<NavigationManager>(_ => Substitute.For<NavigationManager>());

        ServiceProvider provider = services.BuildServiceProvider();
        
        return provider;
    }
}
