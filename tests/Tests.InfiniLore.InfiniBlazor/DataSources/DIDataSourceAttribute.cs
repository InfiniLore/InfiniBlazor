// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Processors.InputProcessors;
using InfiniLore.InfiniBlazor.Processors.OutputProcessors;
using InfiniLore.InfiniBlazor.TextModifiers;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.InfiniLore.InfiniBlazor.DataSources;
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
                
                config.AddMarkdownParser<string,string>()
                    .AddInputProcessor<StringInputProcessor>();

                config.AddMarkdownParser<ITextSource, string>()
                    .AddInputProcessor<TextSourceInputProcessor>();
                
                config.AddMarkdownParser<string,string>("sanitized")
                    .AddInputProcessor<StringInputProcessor>()
                    .AddOutputProcessor<StringOutputSanitizerProcessor>();
                
                config.AddMarkdownParser<ITextSource, string>("sanitized")
                    .AddInputProcessor<TextSourceInputProcessor>()
                    .AddOutputProcessor<StringOutputSanitizerProcessor>();
            }));

        return services.BuildServiceProvider();
    }
}
