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
public partial class ItalicSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    [GeneratedRegex(@"\G\*(?<i>(?>[^\\\*]+|\\\*|\*\*|(?<open>\*)|(?<-open>\*))+)(?(open)(?!))\*", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex Syntax { get; }
    
    private static readonly int ItalicContentId = Syntax.GroupNumberFromName("i");
    
    public char[] TriggerCharacters { get; } = ['*'];
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
        string italicValue = match.Groups[ItalicContentId].Value;

        ItalicMdSyntaxNode node = MdSyntaxNodePool<ItalicMdSyntaxNode>.Shared.Get();
        parentNode.AddChildNode(node);
        stack.PushSingleLineMatchesToStack(italicValue, node);
    }
}
