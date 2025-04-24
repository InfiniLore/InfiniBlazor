// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableService<IMarkdownParser>(ServiceLifetime.Singleton)]
public class MarkdownParser(
    IServiceProvider serviceProvider,
    ILogger<MarkdownParser> logger,
    IMarkdownSyntaxTreeParser nodeTreeParser
) : IMarkdownParser {
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region Parsing Methods
    public void ParseToWriter<T>(string markdown, T writer) where T : TextWriter {
        if (serviceProvider.GetService<IMarkdownSyntaxTreeToWriterConverter<T>>() is not {} converter) return;
        MarkdownSyntaxTree nodeTree = PoolCache.MdNodeTreePool.Get();
        
        try {
            nodeTreeParser.ParseToNodeTree(markdown, nodeTree);
            converter.Convert(nodeTree, writer);
        }
        finally {
            PoolCache.MdNodeTreePool.Return(nodeTree);
        }
    }
    
    public bool TryParse<T>(string markdown, [NotNullWhen(true)] out T? output) where T : class {
        output = null;
        if (markdown.IsNullOrWhiteSpace()) {
            logger.LogWarning("Markdown is empty.");
            return false;
        }

        if (serviceProvider.GetService<IMarkdownSyntaxTreeConverter<T>>() is not {} converter) {
            logger.LogWarning("No converter found for type {Type}.", typeof(T));
            return false;
        }
        
        MarkdownSyntaxTree nodeTree = PoolCache.MdNodeTreePool.Get();

        try {
            nodeTreeParser.ParseToNodeTree(markdown, nodeTree);
            output = converter.Convert(nodeTree);
            return true;
        }
        catch (Exception e) {
            logger.LogError(e, "Error parsing markdown.");
            return false;
        }
        finally {
            PoolCache.MdNodeTreePool.Return(nodeTree);
        }
    }
    #endregion
}
