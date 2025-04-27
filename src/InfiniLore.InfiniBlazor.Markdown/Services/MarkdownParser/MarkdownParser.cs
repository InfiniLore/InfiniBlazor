// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Processors;
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
    IEnumerable<IMarkdownInputProcessor<TInput>> inputProcessors,
    IEnumerable<IMarkdownPostProcessor<TInput>> postProcessors,
    IEnumerable<IMarkdownOutputProcessor<TOutput>> outputProcessors
    
) : IMarkdownParser<TInput, TOutput> {
    
    private readonly ImmutableArray<IMarkdownInputProcessor<TInput>> InputProcessors = inputProcessors.ToImmutableArray();
    private readonly ImmutableArray<IMarkdownPostProcessor<TInput>> PostProcessors = postProcessors.ToImmutableArray();
    private readonly ImmutableArray<IMarkdownOutputProcessor<TOutput>> OutputProcessors = outputProcessors.ToImmutableArray();
    private bool? _hasInputProcessors;
    private bool? _hasPostProcessors;
    private bool? _hasOutputProcessors;
    
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
            if (!TryExecuteInputProcessors(ref processedInput)) return false;
            
            nodeTreeParser.ParseToNodeTree(processedInput!, nodeTree);
            if (!TryExecutePostProcessors(input, nodeTree)) return false;
            
            output = converter.Convert(nodeTree);

            if (!TryExecuteOutputProcessors(ref output)) return false;
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
    private bool TryExecuteInputProcessors([DisallowNull] ref TInput? processedInput) {
        bool state = _hasInputProcessors ??= !InputProcessors.IsEmpty();
        if (!state) return true; // Skip

        int count = InputProcessors.Length;
        for (int i = 0; i < count; i++) {
            IMarkdownInputProcessor<TInput> processor = InputProcessors[i];
            
            // ReSharper disable once InvertIf
            if (!processor.TryProcessInput(processedInput, out processedInput)) {
                logger.LogWarning("Input Processor {processor} failed.", processor.GetType());
                return false;
            }
        }

        return true;
    }
    
    private bool TryExecutePostProcessors(TInput input, IMarkdownSyntaxTree syntaxTree) {
        bool state = _hasPostProcessors ??= !PostProcessors.IsEmpty();
        if (!state) return true; // Skip

        int count = PostProcessors.Length;
        for (int i = 0; i < count; i++) {
            IMarkdownPostProcessor<TInput> processor = PostProcessors[i];
            
            // ReSharper disable once InvertIf
            if (!processor.TryProcess(input, syntaxTree)) {
                logger.LogWarning("PostProcessor {processor} failed.", processor.GetType());
                return false;
            }
        }

        return true;
    }
    
    private bool TryExecuteOutputProcessors( [DisallowNull] ref TOutput? output) {
        bool state = _hasOutputProcessors ??= !OutputProcessors.IsEmpty();
        if (!state) return true; // Skip

        int count = OutputProcessors.Length;
        for (int i = 0; i < count; i++) {
            IMarkdownOutputProcessor<TOutput> processor = OutputProcessors[i];
            
            // ReSharper disable once InvertIf
            if (!processor.TryProcessOutput(output, out output)) {
                logger.LogWarning("OutputProcessor {processor} failed.", processor.GetType());
                return false;
            }
        }
        
        return true;
    }
}
