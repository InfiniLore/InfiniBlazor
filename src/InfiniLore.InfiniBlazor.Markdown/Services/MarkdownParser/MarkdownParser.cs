// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Processors;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Immutable;

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
    private bool HasInputProcessors => InputProcessors.Length > 0;
    private bool HasPostProcessors => PostProcessors.Length > 0;
    private bool HasOutputProcessors => OutputProcessors.Length > 0;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async ValueTask<TOutput?> TryParseAsync(TInput input, CancellationToken ct = default) {
        if (serviceProvider.GetService<IMarkdownSyntaxTreeConverter<TOutput>>() is not {} converter) {
            logger.LogWarning("No converter found for type {Type}.", typeof(TOutput));
            return default;
        }
        
        MarkdownSyntaxTree nodeTree = PoolCache.MarkdownSyntaxTreePool.Get();

        try {
            TInput? processedInput = input;
            if (HasInputProcessors) {
                processedInput = await ExecuteInputProcessorsAsync(processedInput, ct);
                if (processedInput is null) return default;
                ct.ThrowIfCancellationRequested();
            }

            await nodeTreeParser.ParseToNodeTreeAsync(processedInput, nodeTree, ct);
            if (HasPostProcessors && !await ExecutePostProcessorsAsync(input, nodeTree)) return default;
            ct.ThrowIfCancellationRequested();

            TOutput output = await converter.ConvertAsync(nodeTree, ct);
            TOutput? processedOutput = output;

            // ReSharper disable once InvertIf
            if (HasOutputProcessors) {
                processedOutput = await ExecuteOutputProcessors(processedOutput, ct);
                if (processedOutput is null) return default;
                ct.ThrowIfCancellationRequested();
            }


            return processedOutput;
        }
        catch (OperationCanceledException e) {
            logger.LogError(e, "Operation cancelled");
            return default;
        }
        
        catch (Exception e) {
            logger.LogError(e, "Error parsing markdown.");
            return default;
        }
        finally {
            PoolCache.MarkdownSyntaxTreePool.Return(nodeTree);
        }
    }
    private async ValueTask<TInput?> ExecuteInputProcessorsAsync(TInput processedInput, CancellationToken ct) {
        int count = InputProcessors.Length;
        TInput? inputProcessed = processedInput;
        for (int i = 0; i < count; i++) {
            IMarkdownInputProcessor<TInput> processor = InputProcessors[i];
            inputProcessed = await processor.TryProcessInput(inputProcessed, ct);
            
            // ReSharper disable once InvertIf
            if (inputProcessed is null) {
                logger.LogWarning("Input Processor {processor} failed.", processor.GetType());
                return default;
            }
        }

        return inputProcessed;
    }
    
    private async ValueTask<bool> ExecutePostProcessorsAsync(TInput input, IMarkdownSyntaxTree syntaxTree) {
        int count = PostProcessors.Length;
        for (int i = 0; i < count; i++) {
            IMarkdownPostProcessor<TInput> processor = PostProcessors[i];
            
            // ReSharper disable once InvertIf 
            if (!await processor.TryProcessAsync(input, syntaxTree)) {
                logger.LogWarning("PostProcessor {processor} failed.", processor.GetType());
                return false;
            }
        }

        return true;
    }
    
    private async ValueTask<TOutput?> ExecuteOutputProcessors(TOutput output, CancellationToken ct) {
        int count = OutputProcessors.Length;
        TOutput? outputProcessed = output;
        for (int i = 0; i < count; i++) {
            IMarkdownOutputProcessor<TOutput> processor = OutputProcessors[i];
            outputProcessed = await processor.TryProcessOutputAsync(output, ct);
            
            // ReSharper disable once InvertIf
            if (outputProcessed is null) {
                logger.LogWarning("OutputProcessor {processor} failed.", processor.GetType());
                return default;
            }
        }
        
        return outputProcessed;
    }
}
