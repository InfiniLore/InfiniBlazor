// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Emotes;
using InfiniLore.InfiniBlazor.TextEditor.TextModifiers;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.InfiniLore.InfiniBlazor;
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
        services.AddInfiniBlazor(static config => config.AddMarkdownLogic(
            static config => {
                config.AddTextEditor().AddDefaultModifiers();
                config.AddTextEditor("boldOnly").AddModifier<BoldModifier>();
            }));

        ServiceProvider provider = services.BuildServiceProvider();

        // Todo: Remove this with a better emote provider for testing that doesnt require initialization
        provider.GetRequiredService<IEmoteProvider>().InitializeAsync().GetAwaiter().GetResult();
        
        return provider;
    }
}
