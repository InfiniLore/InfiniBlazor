// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.MdNodes;
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
    IMdNodeTreeParser nodeTreeParser
) : IMarkdownParser {
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region Parsing Methods
    public void ParseToWriter<T>(string markdown, T writer) where T : TextWriter {
        if (serviceProvider.GetService<IMdNodeTreeToWriterConverter<T>>() is not {} converter) return;
        MdNodeTree nodeTree = PoolCache.MdNodeTreePool.Get();
        
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
        if (markdown.IsNullOrWhiteSpace()) return false;
        if (serviceProvider.GetService<IMdNodeTreeConverter<T>>() is not {} converter) return false;
        
        MdNodeTree nodeTree = PoolCache.MdNodeTreePool.Get();

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
