// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Processors;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Immutable;

namespace InfiniLore.InfiniBlazor.Markdown; 
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class MarkdownParserFactory {
    public static IMarkdownParser<TInput, TOutput> CreateParser<TInput, TOutput>(IServiceProvider provider) => CreateKeyedParser<TInput, TOutput>(provider, null);
    public static IMarkdownParser<TInput, TOutput> CreateKeyedParser<TInput, TOutput>(IServiceProvider provider, object? key) {
        ImmutableArray<IMarkdownInputProcessor<TInput>> inputProcessors = provider.GetRequiredKeyedService<IEnumerable<IMarkdownInputProcessor<TInput>>>(key)
            .ToImmutableArray();
        ImmutableArray<IMarkdownPostProcessor<TInput>> postProcessors = provider.GetRequiredKeyedService<IEnumerable<IMarkdownPostProcessor<TInput>>>(key)
            .ToImmutableArray();
        ImmutableArray<IMarkdownOutputProcessor<TOutput>> outputProcessors = provider.GetRequiredKeyedService<IEnumerable<IMarkdownOutputProcessor<TOutput>>>(key)
            .ToImmutableArray();
        
        var logger = provider.GetRequiredService<ILogger<MarkdownParser<TInput, TOutput>>>();
        var nodeTreeParser = provider.GetRequiredService<IMarkdownSyntaxTreeParser<TInput>>();
        var converter = provider.GetRequiredService<IMarkdownSyntaxTreeConverter<TOutput>>();

        return new MarkdownParser<TInput, TOutput> {
            InputProcessors = inputProcessors,
            PostProcessors = postProcessors,
            OutputProcessors = outputProcessors,
            Logger = logger,
            NodeTreeParser = nodeTreeParser,
            Converter = converter,
            HasInputProcessors = inputProcessors.Length > 0,
            HasPostProcessors = postProcessors.Length > 0,
            HasOutputProcessors = outputProcessors.Length > 0,
        };
    }
}
