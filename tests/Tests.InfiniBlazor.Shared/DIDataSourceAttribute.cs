// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor;
using InfiniLore.InfiniBlazor.Components;
using InfiniLore.InfiniBlazor.Components.DataLoaders;
using InfiniLore.InfiniBlazor.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Tests.InfiniBlazor.Shared;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once InconsistentNaming
public class DiDataSourceAttribute : DependencyInjectionDataSourceAttribute<IServiceScope> {
    private static readonly IServiceProvider ServiceProvider = CreateSharedServiceProvider();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override IServiceScope CreateScope(DataGeneratorMetadata dataGeneratorMetadata) => ServiceProvider.CreateAsyncScope();
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

        ServiceProvider provider = services.BuildServiceProvider();

        provider.GetRequiredService<IEmoteProvider>().InitializeAsync().GetAwaiter().GetResult();
        
        return provider;
    }
}
