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
public class ItalicSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    private static readonly int ItalicContentId = MdRegexLib.GetGroupId(MdRegexGroupNames.ItalicContent);
    
    public Regex Syntax { get; } = MdRegexLib.ItalicRegex;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        if (!match.Groups[ItalicContentId].TryGetValue(out string? italicValue)) return;

        ItalicMdSyntaxNode node = ItalicMdSyntaxNode.Pool.Get();
        parentNode.AddChildNode(node);
        stack.PushSingleLineMatchesToStack(italicValue, node);
    }
}
