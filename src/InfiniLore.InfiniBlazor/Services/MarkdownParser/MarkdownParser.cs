// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InfiniLore.InfiniBlazor.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownParser>]
public class MarkdownParser(IMdSyntaxTreeConverter treeConverter, IMdSyntaxParser syntaxParser, ILogger<MarkdownParser> logger) : IMarkdownParser {
    public string ParseToString(string input) {
        MdSyntaxTree tree = MdSyntaxTree.Pool.Get();
        try {
            syntaxParser.ParseToTree(input, tree);
            return treeConverter.ConvertToString(tree);
        }
        catch (Exception ex) {
            logger.Error(ex, "Error parsing markdown");
            return ex.Message;
        }
        finally {
            MdSyntaxTree.Pool.Return(tree);
        }
    }

    public MarkupString ParseToMarkupString(string input) {
        MdSyntaxTree tree = MdSyntaxTree.Pool.Get();
        try {
            syntaxParser.ParseToTree(input, tree);
            return treeConverter.ConvertToMarkupString(tree);
        }
        catch (Exception ex) {
            logger.Error(ex, "Error parsing markdown");
            return new MarkupString(ex.Message);
        }
        finally {
            MdSyntaxTree.Pool.Return(tree);
        }
    }
}

[InjectableSingleton<IMarkdownParser>("styled")]
public class StyledMarkdownParser(
    [FromKeyedServices("styled")] IMdSyntaxTreeConverter treeConverter,
    IMdSyntaxParser syntaxParser,
    ILogger<StyledMarkdownParser> logger
) : MarkdownParser(treeConverter, syntaxParser, logger);
