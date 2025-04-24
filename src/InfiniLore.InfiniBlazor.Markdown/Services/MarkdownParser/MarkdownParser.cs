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
    
    private readonly Lazy<IMarkdownPreProcessor<TInput>[]> PreProcessors = new (preProcessors.ToArray);
    private readonly Lazy<IMarkdownPostProcessor<TOutput>[]> PostProcessors = new (postProcessors.ToArray);
    
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
            ReadOnlySpan<IMarkdownPreProcessor<TInput>> preProcessorsSpan = PreProcessors.Value;
            foreach (IMarkdownPreProcessor<TInput> preProcessor in preProcessorsSpan) {
                // ReSharper disable once InvertIf
                if (!preProcessor.TryProcess(processedInput, out processedInput)) {
                    logger.LogWarning("Preprocessor {PreProcessor} failed.", preProcessor.GetType());
                    return false;
                }
            }
            
            nodeTreeParser.ParseToNodeTree(processedInput, nodeTree);
            output = converter.Convert(nodeTree);
            
            ReadOnlySpan<IMarkdownPostProcessor<TOutput>> postProcessorsSpan = PostProcessors.Value;
            foreach (IMarkdownPostProcessor<TOutput> postProcessor in postProcessorsSpan) {
                // ReSharper disable once InvertIf
                if (!postProcessor.TryProcess(output, out output)) {
                    logger.LogWarning("Postprocessor {PostProcessor} failed.", postProcessor.GetType());
                    return false;
                }
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
