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
public partial class UnderlineSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    [GeneratedRegex(@"(?<underline>_(?<u>(?>[^\\_]+|\\_|__|(?<open>_)|(?<-open>_))+)(?(open)(?!))_)", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex Syntax { get; }
    
    private static readonly int UId = Syntax.GroupNumberFromName(MdRegexGroupNames.UnderlineContent);
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
        if (!match.Groups[UId].TryGetValue(out string? underlineValue)) return;

        UnderlineMdSyntaxNode node = MdSyntaxNodePool<UnderlineMdSyntaxNode>.Shared.Get();
        parentNode.AddChildNode(node);
        stack.PushSingleLineMatchesToStack(underlineValue, node);
    }
}
