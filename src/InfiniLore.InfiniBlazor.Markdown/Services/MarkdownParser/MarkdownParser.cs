// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MarkdownParser<TInput, TOutput>(
    IServiceProvider serviceProvider,
    ILogger<MarkdownParser<TInput, TOutput>> logger,
    IMarkdownSyntaxTreeParser<TInput> nodeTreeParser,
    IEnumerable<IMarkdownPreProcessor<TInput>> preProcessors,
    IEnumerable<IMarkdownPostProcessor<TOutput>> postProcessors
) : IMarkdownParser<TInput, TOutput> {
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TryParse(TInput input, [NotNullWhen(true)] out TOutput? output) {
        output = default;

        if (serviceProvider.GetService<IMarkdownSyntaxTreeConverter<TOutput>>() is not {} converter) {
            logger.LogWarning("No converter found for type {Type}.", typeof(TOutput));
            return false;
        }
        
        MarkdownSyntaxTree nodeTree = PoolCache.MdNodeTreePool.Get();

        try {
            TInput? processedInput = input;
            ReadOnlySpan<IMarkdownPreProcessor<TInput>> preProcessorsSpan = preProcessors.ToArray();
            foreach (IMarkdownPreProcessor<TInput> preProcessor in preProcessorsSpan) {
                if (!preProcessor.TryProcess(processedInput, out processedInput)) return false;
            }
            
            nodeTreeParser.ParseToNodeTree(processedInput, nodeTree);
            output = converter.Convert(nodeTree);
            
            ReadOnlySpan<IMarkdownPostProcessor<TOutput>> postProcessorsSpan = postProcessors.ToArray();
            foreach (IMarkdownPostProcessor<TOutput> postProcessor in postProcessorsSpan) {
                if (!postProcessor.TryProcess(output, out output)) return false;
            }
            return output is not null;
        }
        catch (Exception e) {
            logger.LogError(e, "Error parsing markdown.");
            return false;
        }
        finally {
            PoolCache.MdNodeTreePool.Return(nodeTree);
        }
    }
}
