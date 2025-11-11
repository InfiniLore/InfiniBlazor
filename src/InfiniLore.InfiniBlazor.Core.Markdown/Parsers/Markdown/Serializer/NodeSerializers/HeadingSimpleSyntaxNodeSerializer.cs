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
public static class HeadingSimpleSyntaxNodeSerializer {
    private static readonly int HsTextId = MdRegexLib.GetGroupId(MdRegexGroupNames.HeadingSimpleText);
    private static readonly int HsIdentifierId = MdRegexLib.GetGroupId(MdRegexGroupNames.HeadingSimpleIdentifier);
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        if (!match.Groups[HsTextId].TryGetValue(out string? headerSimpleText)) return;
        if (!match.Groups[HsIdentifierId].TryGetValue(out string? headerIdentifierText)) return;

        HeadingSimpleMdSyntaxNode headingNode = HeadingSimpleMdSyntaxNode.Pool.Get();
        headingNode.WithIdentifier(headerIdentifierText);

        parentNode.AddChildNode(headingNode);

        stack.PushSingleLineMatchesToStack(headerSimpleText, headingNode);
    }
}
