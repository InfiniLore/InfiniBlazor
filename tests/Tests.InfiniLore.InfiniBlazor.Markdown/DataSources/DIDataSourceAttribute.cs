// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Config;
using InfiniLore.InfiniBlazor.Markdown.Processors.InputProcessors;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.DataSources;
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
        services.AddInfiniBlazor(static config => config.AddMarkdown(
            static config => {
                config.AddMarkdownParser<string,string>()
                    .AddInputProcessor<StringInputProcessor>();

                config.AddMarkdownParser<ITextSource, string>()
                    .AddInputProcessor<TextSourceInputProcessor>();
            }));

        return services.BuildServiceProvider();
    }
}
