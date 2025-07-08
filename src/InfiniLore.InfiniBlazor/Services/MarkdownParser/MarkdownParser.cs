// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownParser>]
public class MarkdownParser(IMdSyntaxTreeConverter treeConverter, IMdSyntaxParser syntaxParser) : IMarkdownParser {
    public string ParseToString(string input) {
        MdSyntaxTree tree = MdSyntaxTree.Pool.Get();
        try {
            syntaxParser.ParseToTree(input, tree);
            return treeConverter.ConvertToString(tree);
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
        finally {
            MdSyntaxTree.Pool.Return(tree);
        }
    }
}

[InjectableSingleton<IMarkdownParser>("styled")]
public class StyledMarkdownParser(
    [FromKeyedServices("styled")] IMdSyntaxTreeConverter treeConverter,
    IMdSyntaxParser syntaxParser
) : MarkdownParser(treeConverter, syntaxParser);
