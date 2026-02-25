// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed partial class TemplateSyntaxNodeSerializer : BaseMdSyntaxNodeSerializer {
    [GeneratedRegex(@"\G(?<!\])(?<open>\{)+(?<t>[^\s{}]+)(?<-open>\})+(?(open)(?!))", DefaultSingleLineRegexOptions)]
    private static partial Regex RegexRule { get; }
    protected override Regex Syntax { get; } = RegexRule;

    private static readonly char[] STriggerCharacters = ['{'];
    public override ReadOnlySpan<char> TriggerCharacters => STriggerCharacters;

    private static readonly int TemplateContentId = RegexRule.GroupNumberFromName("t");
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        string variableContent = match.Groups[TemplateContentId].Value;
        int variableLength = match.Length;

        TemplateMdSyntaxNode node = MdSyntaxNodePool<TemplateMdSyntaxNode>.Shared.Get();
        node.WithContent(variableContent)
            .WithBracesCount((variableLength - variableContent.Length) / 2);
        parentNode.AddChildNode(node);
    }
}
