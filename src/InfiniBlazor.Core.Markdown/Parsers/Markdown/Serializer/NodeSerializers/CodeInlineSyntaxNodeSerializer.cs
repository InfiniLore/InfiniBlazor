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
public partial class CodeInlineSyntaxNodeSerializer : IMdSyntaxNodeSerializer{
    [GeneratedRegex(@"(?<code>(?<open>`+)(?<c>(?>[^`\\]+|\\.|`(?!\k<open>))+?)\k<open>)", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex Syntax { get; }
    
    private static readonly int CodeContentId = Syntax.GroupNumberFromName(MdRegexGroupNames.CodeInlineContent);
    private static readonly int CodeInlineId = Syntax.GroupNumberFromName(MdRegexGroupNames.CodeInline);
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public Match Match(string input, int startPosition = 0) 
        => Syntax.Match(input, startPosition);
    
    public void Serialize(IMdSyntaxFragmentStack stack, IMdSyntaxNode parentNode, Match match) {
        if (!match.Groups[CodeContentId].TryGetValue(out string? codeValue)) return;
        if (!match.Groups[CodeInlineId].TryGetValueSpan(out ReadOnlySpan<char> fullOriginalString)) return;

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
