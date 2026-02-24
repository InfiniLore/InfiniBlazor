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
public sealed partial class CodeInlineSyntaxNodeSerializer : BaseMdSyntaxNodeSerializer {
    [GeneratedRegex(@"\G(?<open>`+)(?<c>(?>[^`\\]+|\\.|`(?!\k<open>))+?)\k<open>", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex RegexRule { get; }
    protected override Regex Syntax { get; } = RegexRule;

    private static readonly char[] STriggerCharacters = ['`'];
    public override ReadOnlySpan<char> TriggerCharacters => STriggerCharacters;

    private static readonly int CodeContentId = RegexRule.GroupNumberFromName("c");

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override void Serialize(IMdSyntaxFragmentStack stack, IMdSyntaxNode parentNode, Match match) {
        string codeValue = match.Groups[CodeContentId].Value;
        ReadOnlySpan<char> fullOriginalString = match.ValueSpan;

        CodeInlineMdSyntaxNode node = MdSyntaxNodePool<CodeInlineMdSyntaxNode>.Shared.Get();
        node.WithContent(codeValue);

        // Calculate backtick count by comparing full string length to content length
        int totalLength = fullOriginalString.Length;
        int contentLength = codeValue.Length;
        int totalBackticks = totalLength - contentLength;
        int backtickCount = totalBackticks / 2;// Backticks on one side

        node.WithBackTickCount(backtickCount);
        parentNode.AddChildNode(node);
    }
}
