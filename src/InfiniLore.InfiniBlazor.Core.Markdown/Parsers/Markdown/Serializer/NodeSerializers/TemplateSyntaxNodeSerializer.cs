// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown.Serializer.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class TemplateSyntaxNodeSerializer  {
    private static readonly int TemplateId = MdRegexLib.GetGroupId(MdRegexGroupNames.Template);
    private static readonly int TemplateContentId = MdRegexLib.GetGroupId(MdRegexGroupNames.TemplateContent);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        if (!match.Groups[TemplateContentId].TryGetValue(out string? variableContent)) return ;
        if (!match.Groups[TemplateId].TryGetLength(out int variableLength)) return ;

        TemplateMdSyntaxNode node = TemplateMdSyntaxNode.Pool.Get();
        node.WithContent(variableContent)
            .WithBracesCount((variableLength - variableContent.Length) / 2);
        parentNode.AddChildNode(node);
    }
}
