// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class VariableContentSyntaxNodeSerializer  {
    private static readonly int VariableId = MdRegexLib.GetGroupId(MdRegexGroupNames.Variable);
    private static readonly int VariableContentId = MdRegexLib.GetGroupId(MdRegexGroupNames.VariableContent);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        if (!match.Groups[VariableContentId].TryGetValue(out string? variableContent)) return ;
        if (!match.Groups[VariableId].TryGetLength(out int variableLength)) return ;

        VariableContentMdSyntaxNode node = VariableContentMdSyntaxNode.Pool.Get();
        node.Content = variableContent;
        node.BracesCount = (variableLength - variableContent.Length) / 2;
        parentNode.AddChildNode(node);
    }
}
