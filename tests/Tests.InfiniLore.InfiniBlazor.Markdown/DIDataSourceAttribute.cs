// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Config;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once InconsistentNaming
public class DIDataSourceAttribute : DependencyInjectionDataSourceAttribute<IServiceScope> {
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
        services.AddInfiniBlazor(static config => config.AddMarkdown());

        return services.BuildServiceProvider();
    }
}
