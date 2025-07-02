// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Processors;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax;
using Microsoft.Extensions.Logging;
using System.Collections.Immutable;

namespace InfiniLore.InfiniBlazor.MarkdownParser;
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
    public TOutput? TryParse(TInput input, CancellationToken ct = default) {
        MarkdownSyntaxTree nodeTree = MarkdownSyntaxTree.Pool.Get();

        try {
            TInput? processedInput = input;
            if (HasInputProcessors) {
                processedInput = ExecuteInputProcessorsAsync(processedInput);
                if (processedInput is null) return default;
                ct.ThrowIfCancellationRequested();
            }

            NodeTreeParser.ParseToNodeTreeAsync(processedInput, nodeTree);
            if (HasPostProcessors && !ExecutePostProcessorsAsync(input, nodeTree)) return default;
            ct.ThrowIfCancellationRequested();

            TOutput output = Converter.Convert(nodeTree);
            TOutput? processedOutput = output;

            // ReSharper disable once InvertIf
            if (HasOutputProcessors) {
                processedOutput = ExecuteOutputProcessors(processedOutput);
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
            MarkdownSyntaxTree.Pool.Return(nodeTree);
        }
    }
    
    private TInput? ExecuteInputProcessorsAsync(TInput processedInput) {
        int count = InputProcessors.Length;
        TInput? inputProcessed = processedInput;
        for (int i = 0; i < count; i++) {
            IMarkdownInputProcessor<TInput> processor = InputProcessors[i];
            inputProcessed = processor.TryProcessInput(inputProcessed);
            
            // ReSharper disable once InvertIf
            if (inputProcessed is null) {
                Logger.LogWarning("Input Processor {processor} failed.", processor.GetType());
                return default;
            }
        }

        return inputProcessed;
    }
    
    private bool ExecutePostProcessorsAsync(TInput input, IMarkdownSyntaxTree syntaxTree) {
        int count = PostProcessors.Length;
        for (int i = 0; i < count; i++) {
            IMarkdownPostProcessor<TInput> processor = PostProcessors[i];
            
            // ReSharper disable once InvertIf 
            if (!processor.TryProcessAsync(input, syntaxTree)) {
                Logger.LogWarning("PostProcessor {processor} failed.", processor.GetType());
                return false;
            }
        }

        return true;
    }
    
    private TOutput? ExecuteOutputProcessors(TOutput output){
        int count = OutputProcessors.Length;
        TOutput? outputProcessed = output;
        for (int i = 0; i < count; i++) {
            IMarkdownOutputProcessor<TOutput> processor = OutputProcessors[i];
            outputProcessed = processor.TryProcessOutputAsync(output);
            
            // ReSharper disable once InvertIf
            if (outputProcessed is null) {
                Logger.LogWarning("OutputProcessor {processor} failed.", processor.GetType());
                return default;
            }
        }
        
        return outputProcessed;
    }
}
