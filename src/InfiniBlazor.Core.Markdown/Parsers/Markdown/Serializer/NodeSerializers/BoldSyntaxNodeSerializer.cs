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
public sealed partial class BoldSyntaxNodeSerializer : BaseMdSyntaxNodeSerializer {
    [GeneratedRegex(
        """
            \G\*\*(?<b>
                (?:\\.
                | [^\\\*\n]
                | \*(?!\*) 
                | \*(?=\*\*)
                )+
            )\*\*
        """, RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace)]
    private static partial Regex RegexRule { get; }
    protected override Regex Syntax { get; } = RegexRule;

    private static readonly char[] STriggerCharacters = ['*'];
    public override ReadOnlySpan<char> TriggerCharacters => STriggerCharacters;

    private static readonly int BoldContentId = RegexRule.GroupNumberFromName("b");

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override void Serialize(IMdSyntaxFragmentStack stack, IMdSyntaxNode parentNode, Match match) {
        string boldValue = match.Groups[BoldContentId].Value;

        BoldMdSyntaxNode node = MdSyntaxNodePool<BoldMdSyntaxNode>.Shared.Get();
        parentNode.AddChildNode(node);
        stack.PushSingleLineMatchesToStack(boldValue, node);
    }
}
