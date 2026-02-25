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
public sealed partial class StrikeSyntaxNodeSerializer : BaseMdSyntaxNodeSerializer {
    [GeneratedRegex("""
        ~~(?<s>
            (?:\\.
            | [^\\\n~]
            | ~(?!~) 
            | ~(?=~~)
            )+
        )~~
        """, DefaultSingleLineRegexOptions)]
    private static partial Regex RegexRule { get; }
    protected override Regex Syntax { get; } = RegexRule;

    private static readonly char[] STriggerCharacters = ['~'];
    public override ReadOnlySpan<char> TriggerCharacters => STriggerCharacters;

    private static readonly int StrikeContentId = RegexRule.GroupNumberFromName("s");

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        string strikeValue = match.Groups[StrikeContentId].Value;

        StrikeMdSyntaxNode node = MdSyntaxNodePool<StrikeMdSyntaxNode>.Shared.Get();
        parentNode.AddChildNode(node);

        stack.PushSingleLineMatchesToStack(strikeValue, node);
    }
}
