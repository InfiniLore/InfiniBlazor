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
public class TemplateSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    private static readonly int TemplateId = MdRegexLib.GetGroupId(MdRegexGroupNames.Template);
    private static readonly int TemplateContentId = MdRegexLib.GetGroupId(MdRegexGroupNames.TemplateContent);

    public Regex Syntax { get; } = MdRegexLib.TemplateRegex;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        if (!match.Groups[TemplateContentId].TryGetValue(out string? variableContent)) return;
        if (!match.Groups[TemplateId].TryGetLength(out int variableLength)) return;

        TemplateMdSyntaxNode node = TemplateMdSyntaxNode.Pool.Get();
        node.WithContent(variableContent)
            .WithBracesCount((variableLength - variableContent.Length) / 2);
        parentNode.AddChildNode(node);
    }
}
