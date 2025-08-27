// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownStringMdSyntaxNodeSerializer>(MdRegexGroupNames.HorizontalRule)]
public sealed class HorizontalRuleSyntaxNodeSerializer : IMarkdownStringMdSyntaxNodeSerializer {
    private static readonly int HrId = MdRegexLib.GetGroupId(MdRegexGroupNames.HorizontalRuleContent);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch
    ) {
        if (!entireMatch.Groups[HrId].TryGetValue(out string? hrContent)) return;
        
        HorizontalRuleMdSyntaxNode node = HorizontalRuleMdSyntaxNode.Pool.Get();
        node.Identifier = hrContent;
        parentNode.AddChildNode(node);
    }
}
