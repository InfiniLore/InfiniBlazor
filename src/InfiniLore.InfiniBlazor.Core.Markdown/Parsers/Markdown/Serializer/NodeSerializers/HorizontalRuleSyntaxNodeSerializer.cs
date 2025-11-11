// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown.Serializer.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class HorizontalRuleSyntaxNodeSerializer {
    private static readonly int HrId = MdRegexLib.GetGroupId(MdRegexGroupNames.HorizontalRuleContent);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        if (!match.Groups[HrId].TryGetValue(out string? hrContent)) return;

        HorizontalRuleMdSyntaxNode node = HorizontalRuleMdSyntaxNode.Pool.Get();
        node.WithIdentifier(hrContent);
        parentNode.AddChildNode(node);
    }
}
