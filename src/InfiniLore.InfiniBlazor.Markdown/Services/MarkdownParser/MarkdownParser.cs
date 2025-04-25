// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Immutable;
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
    
    private readonly ImmutableArray<IMarkdownPreProcessor<TInput>> PreProcessors = preProcessors.ToImmutableArray();
    private readonly ImmutableArray<IMarkdownPostProcessor<TOutput>> PostProcessors = postProcessors.ToImmutableArray();
    private bool? _hasPreProcessors;
    private bool? _hasPostProcessors;
    
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
            if (!TryExecutePreProcessors(ref processedInput)) return false;
            
            nodeTreeParser.ParseToNodeTree(processedInput!, nodeTree);
            output = converter.Convert(nodeTree);

            if (!TryExecutePostProcessors(ref output)) return false;
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
    private bool TryExecutePreProcessors([DisallowNull] ref TInput? processedInput) {
        bool state = _hasPreProcessors ??= !PreProcessors.IsEmpty();
        if (!state) return true; // Skip

        int count = PreProcessors.Length;
        for (int i = 0; i < count; i++) {
            IMarkdownPreProcessor<TInput> processor = PreProcessors[i];
            
            // ReSharper disable once InvertIf
            if (!processor.TryProcess(processedInput, out processedInput)) {
                logger.LogWarning("Preprocessor {PreProcessor} failed.", processor.GetType());
                return false;
            }
        }

        return true;
    }
    private bool TryExecutePostProcessors( [DisallowNull] ref TOutput? output) {
        bool state = _hasPostProcessors ??= !PostProcessors.IsEmpty();
        if (!state) return true; // Skip

        int count = PostProcessors.Length;
        for (int i = 0; i < count; i++) {
            IMarkdownPostProcessor<TOutput> processor = PostProcessors[i];
            
            // ReSharper disable once InvertIf
            if (!processor.TryProcess(output, out output)) {
                logger.LogWarning("Preprocessor {PreProcessor} failed.", processor.GetType());
                return false;
            }
        }
        
        return true;
    }
}
