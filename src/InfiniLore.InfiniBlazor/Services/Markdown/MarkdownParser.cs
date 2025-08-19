// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using InfiniLore.InfiniBlazor.Markdown.SyntaxDeserializer;
using InfiniLore.InfiniBlazor.Markdown.SyntaxSerializer;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownParser>]
public class MarkdownParser(IMdSyntaxDeserializer deserializer, IMdSyntaxSerializer serializer, ILogger<MarkdownParser> logger) : IMarkdownParser {
    public string ParseToString(string input) {
        MdSyntaxTree tree = MdSyntaxTree.Pool.Get();
        try {
            serializer.SerializeToTree(input, tree);
            return deserializer.DeserializeToString(tree);
        }
        catch (Exception ex) {
            logger.Error(ex, "Error parsing markdown");
            return string.Empty;
        }
        finally {
            MdSyntaxTree.Pool.Return(tree);
        }
    }

    public MarkupString ParseToMarkupString(string input) {
        MdSyntaxTree tree = MdSyntaxTree.Pool.Get();
        try {
            serializer.SerializeToTree(input, tree);
            return deserializer.DeserializeToMarkupString(tree);
        }
        catch (Exception ex) {
            logger.Error(ex, "Error parsing markdown");
            return default;
        }
        finally {
            MdSyntaxTree.Pool.Return(tree);
        }
    }
}

[InjectableSingleton<IMarkdownParser>("styled")]
public class StyledMarkdownParser(
    [FromKeyedServices("styled")] IMdSyntaxDeserializer treeConverter,
    IMdSyntaxSerializer syntaxParser,
    ILogger<StyledMarkdownParser> logger
) : MarkdownParser(treeConverter, syntaxParser, logger);
