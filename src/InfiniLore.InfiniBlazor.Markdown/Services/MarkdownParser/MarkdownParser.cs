// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Processors;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using Microsoft.Extensions.Logging;
using System.Collections.Immutable;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MarkdownParser<TInput, TOutput> : IMarkdownParser<TInput, TOutput> {
    private readonly ImmutableArray<IMarkdownInputProcessor<TInput>> _inputProcessors;
    private readonly ImmutableArray<IMarkdownPostProcessor<TInput>> _postProcessors;
    private readonly ImmutableArray<IMarkdownOutputProcessor<TOutput>> _outputProcessors;
    private readonly ILogger<MarkdownParser<TInput, TOutput>> _logger;
    private readonly IMarkdownSyntaxTreeParser<TInput> _nodeTreeParser;
    private readonly IMarkdownSyntaxTreeConverter<TOutput> _converter;
    private readonly bool _hasInputProcessors;
    private readonly bool _hasPostProcessors;
    private readonly bool _hasOutputProcessors;

    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public MarkdownParser(
        ILogger<MarkdownParser<TInput, TOutput>> logger,
        IMarkdownSyntaxTreeParser<TInput> nodeTreeParser,
        IMarkdownSyntaxTreeConverter<TOutput> converter,
        IEnumerable<IMarkdownInputProcessor<TInput>> inputProcessors,
        IEnumerable<IMarkdownPostProcessor<TInput>> postProcessors,
        IEnumerable<IMarkdownOutputProcessor<TOutput>> outputProcessors
    ) {
        _logger = logger;
        _nodeTreeParser = nodeTreeParser;
        _converter = converter;
        _inputProcessors = inputProcessors.ToImmutableArray();
        _postProcessors = postProcessors.ToImmutableArray();
        _outputProcessors = outputProcessors.ToImmutableArray();
        _hasPostProcessors = _postProcessors.Length > 0;
        _hasInputProcessors = _inputProcessors.Length > 0;
        _hasOutputProcessors = _outputProcessors.Length > 0;
        
    }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async ValueTask<TOutput?> TryParseAsync(TInput input, CancellationToken ct = default) {
        MarkdownSyntaxTree nodeTree = PoolCache.MarkdownSyntaxTreePool.Get();

        try {
            TInput? processedInput = input;
            if (_hasInputProcessors) {
                processedInput = await ExecuteInputProcessorsAsync(processedInput, ct);
                if (processedInput is null) return default;
                ct.ThrowIfCancellationRequested();
            }

            await _nodeTreeParser.ParseToNodeTreeAsync(processedInput, nodeTree, ct);
            if (_hasPostProcessors && !await ExecutePostProcessorsAsync(input, nodeTree, ct)) return default;
            ct.ThrowIfCancellationRequested();

            TOutput output = await _converter.ConvertAsync(nodeTree, ct);
            TOutput? processedOutput = output;

            // ReSharper disable once InvertIf
            if (_hasOutputProcessors) {
                processedOutput = await ExecuteOutputProcessors(processedOutput, ct);
                if (processedOutput is null) return default;
                ct.ThrowIfCancellationRequested();
            }

            return processedOutput;
        }
        catch (OperationCanceledException e) {
            _logger.LogError(e, "Operation cancelled");
            return default;
        }
        
        catch (Exception e) {
            _logger.LogError(e, "Error parsing markdown.");
            return default;
        }
        finally {
            PoolCache.MarkdownSyntaxTreePool.Return(nodeTree);
        }
    }
    
    private async ValueTask<TInput?> ExecuteInputProcessorsAsync(TInput processedInput, CancellationToken ct) {
        int count = _inputProcessors.Length;
        TInput? inputProcessed = processedInput;
        for (int i = 0; i < count; i++) {
            IMarkdownInputProcessor<TInput> processor = _inputProcessors[i];
            inputProcessed = await processor.TryProcessInput(inputProcessed, ct);
            
            // ReSharper disable once InvertIf
            if (inputProcessed is null) {
                _logger.LogWarning("Input Processor {processor} failed.", processor.GetType());
                return default;
            }
        }

        return inputProcessed;
    }
    
    private async ValueTask<bool> ExecutePostProcessorsAsync(TInput input, IMarkdownSyntaxTree syntaxTree, CancellationToken ct) {
        int count = _postProcessors.Length;
        for (int i = 0; i < count; i++) {
            IMarkdownPostProcessor<TInput> processor = _postProcessors[i];
            
            // ReSharper disable once InvertIf 
            if (!await processor.TryProcessAsync(input, syntaxTree, ct)) {
                _logger.LogWarning("PostProcessor {processor} failed.", processor.GetType());
                return false;
            }
        }

        return true;
    }
    
    private async ValueTask<TOutput?> ExecuteOutputProcessors(TOutput output, CancellationToken ct) {
        int count = _outputProcessors.Length;
        TOutput? outputProcessed = output;
        for (int i = 0; i < count; i++) {
            IMarkdownOutputProcessor<TOutput> processor = _outputProcessors[i];
            outputProcessed = await processor.TryProcessOutputAsync(output, ct);
            
            // ReSharper disable once InvertIf
            if (outputProcessed is null) {
                _logger.LogWarning("OutputProcessor {processor} failed.", processor.GetType());
                return default;
            }
        }
        
        return outputProcessed;
    }
}
