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
public partial class StrikeSyntaxNodeSerializer : IMdSyntaxNodeSerializer{
    [GeneratedRegex(@"(?<strike>~~(?<s>(?>[^\\~]+|\\~|~|(?<open>~~)|(?<-open>~~))+?~?)(?(open)(?!))~~)", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex Syntax { get; }
    
    private static readonly int StrikeContentId = Syntax.GroupNumberFromName(MdRegexGroupNames.StrikeContent);
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public Match Match(string input, int startPosition = 0) 
        => Syntax.Match(input, startPosition);
    
    public void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        if (!match.Groups[StrikeContentId].TryGetValue(out string? strikeValue)) return;

        StrikeMdSyntaxNode node = MdSyntaxNodePool<StrikeMdSyntaxNode>.Shared.Get();
        parentNode.AddChildNode(node);

        stack.PushSingleLineMatchesToStack(strikeValue, node);
    }
}
