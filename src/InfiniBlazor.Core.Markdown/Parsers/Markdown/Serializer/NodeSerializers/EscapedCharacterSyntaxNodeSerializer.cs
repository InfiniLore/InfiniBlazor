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
public partial class EscapedCharacterSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    [GeneratedRegex(@"\G\\\S")]
    private static partial Regex Syntax { get; }
    
    public char[] TriggerCharacters { get; } = ['\\'];
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
        char value = match.ValueSpan[1];
        EscapedCharacterMdSyntaxNode node = MdSyntaxNodePool<EscapedCharacterMdSyntaxNode>.Shared.Get();
        node.WithContent(value);
        parentNode.AddChildNode(node);
    }
}
