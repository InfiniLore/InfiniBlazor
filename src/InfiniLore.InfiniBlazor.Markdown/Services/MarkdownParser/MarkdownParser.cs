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
    IMdNodeTreeConverter<string> stringTreeConverter,
    ILogger<MarkdownParser> logger,
    IMdNodeTreeParser nodeTreeParser
) : IMarkdownParser {
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region Parsing Methods
    public bool TryParseToString(string markdown,[NotNullWhen(true)] out string? output) {
        output = null;
        if (markdown.IsNullOrWhiteSpace()) return false;
        MdNodeTree nodeTree = PoolCache.MdNodeTreePool.Get();

        try {
            nodeTreeParser.ParseToNodeTree(markdown, nodeTree);
            output = stringTreeConverter.Convert(nodeTree);
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

    public void ParseToWriter<T>(string markdown, T writer) where T : TextWriter {
        MdNodeTree nodeTree = PoolCache.MdNodeTreePool.Get();
        
        try {
            nodeTreeParser.ParseToNodeTree(markdown, nodeTree);
            var converter = serviceProvider.GetRequiredService<IMdNodeTreeToWriterConverter<T>>();
            converter.Convert(nodeTree, writer);
        }
        finally {
            PoolCache.MdNodeTreePool.Return(nodeTree);
        }
    }
    #endregion
}
