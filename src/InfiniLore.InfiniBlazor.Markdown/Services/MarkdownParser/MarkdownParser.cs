// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Markdown.Processors;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using Microsoft.Extensions.Logging;
using System.Collections.Immutable;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MarkdownParser<TInput, TOutput> : IMarkdownParser<TInput, TOutput> {
    public required ImmutableArray<IMarkdownInputProcessor<TInput>> InputProcessors { private get; init; }
    public required ImmutableArray<IMarkdownPostProcessor<TInput>> PostProcessors { private get; init; }
    public required ImmutableArray<IMarkdownOutputProcessor<TOutput>> OutputProcessors { private get; init; }
    public required ILogger<MarkdownParser<TInput, TOutput>> Logger { private get; init; }
    public required IMarkdownSyntaxTreeParser<TInput> NodeTreeParser { private get; init; }
    public required IMarkdownSyntaxTreeConverter<TOutput> Converter { private get; init; }
    public required bool HasInputProcessors { private get; init; }
    public required bool HasPostProcessors { private get; init; }
    public required bool HasOutputProcessors { private get; init; }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async ValueTask<TOutput?> TryParseAsync(TInput input, CancellationToken ct = default) {
        MarkdownSyntaxTree nodeTree = PoolCache.MarkdownSyntaxTreePool.Get();

        try {
            TInput? processedInput = input;
            if (HasInputProcessors) {
                processedInput = await ExecuteInputProcessorsAsync(processedInput, ct);
                if (processedInput is null) return default;
                ct.ThrowIfCancellationRequested();
            }

            await NodeTreeParser.ParseToNodeTreeAsync(processedInput, nodeTree, ct);
            if (HasPostProcessors && !await ExecutePostProcessorsAsync(input, nodeTree, ct)) return default;
            ct.ThrowIfCancellationRequested();

            TOutput output = await Converter.ConvertAsync(nodeTree, ct);
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
            Logger.LogError(e, "Operation cancelled");
            return default;
        }
        
        catch (Exception e) {
            Logger.LogError(e, "Error parsing markdown.");
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
                Logger.LogWarning("Input Processor {processor} failed.", processor.GetType());
                return default;
            }
        }

        return inputProcessed;
    }
    
    private async ValueTask<bool> ExecutePostProcessorsAsync(TInput input, IMarkdownSyntaxTree syntaxTree, CancellationToken ct) {
        int count = PostProcessors.Length;
        for (int i = 0; i < count; i++) {
            IMarkdownPostProcessor<TInput> processor = PostProcessors[i];
            
            // ReSharper disable once InvertIf 
            if (!await processor.TryProcessAsync(input, syntaxTree, ct)) {
                Logger.LogWarning("PostProcessor {processor} failed.", processor.GetType());
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
                Logger.LogWarning("OutputProcessor {processor} failed.", processor.GetType());
                return default;
            }
        }
        
        return outputProcessed;
    }
}
