// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Parsers.Markdown.Serializer.RegexLib;
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class HeadingSimpleSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    private static readonly int HsTextId = MdRegexLib.GetGroupId(MdRegexGroupNames.HeadingSimpleText);
    private static readonly int HsIdentifierId = MdRegexLib.GetGroupId(MdRegexGroupNames.HeadingSimpleIdentifier);
    
    public Regex Syntax { get; } = MdRegexLib.HeadingSimpleRegex;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        if (!match.Groups[HsTextId].TryGetValue(out string? headerSimpleText)) return;
        if (!match.Groups[HsIdentifierId].TryGetValue(out string? headerIdentifierText)) return;

        HeadingSimpleMdSyntaxNode headingNode = MdSyntaxNodePool<HeadingSimpleMdSyntaxNode>.Shared.Get();
        headingNode.WithIdentifier(headerIdentifierText);

        parentNode.AddChildNode(headingNode);

        stack.PushSingleLineMatchesToStack(headerSimpleText, headingNode);
    }
}
