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
public sealed partial class FrontmatterSyntaxNodeSerializer : BaseMdSyntaxNodeSerializer {
    [GeneratedRegex(@"\G(?<open>^-{3,})\ *(?<lang>.+)?\n(?<body>[\s\S]*?)\n\k<open>", DefaultMultiLineRegexOptions)]
    internal static partial Regex RegexRule { get; }
    protected override Regex Syntax { get; } = RegexRule;

    private static readonly char[] STriggerCharacters = ['-'];
    public override ReadOnlySpan<char> TriggerCharacters => STriggerCharacters;

    private static readonly int LangId = RegexRule.GroupNumberFromName("lang");
    private static readonly int BodyId = RegexRule.GroupNumberFromName("body");

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override void Serialize(IMdSyntaxFragmentStack stack, IMdSyntaxNode parentNode, Match match) {
        FrontMatterMdSyntaxNode node = MdSyntaxNodePool<FrontMatterMdSyntaxNode>.Shared.Get();
        if (match.Groups[LangId].TryGetValue(out string? lang)) node.WithLanguage(lang);
        if (match.Groups[BodyId].TryGetValue(out string? body)) node.WithContent(body);

        ReadOnlySpan<char> span = match.ValueSpan;
        int dashCount = 0;
        int spaceCount = 0;
        foreach (char t in span) {
            switch (t) {
                case '-':
                    dashCount++;
                    continue;
                case ' ':
                    spaceCount++;
                    continue;
            }
            break;
        }

        node.WithDashesCount(dashCount);
        node.WithLeadingSpaces(spaceCount);

        parentNode.AddChildNode(node);
    }
}
