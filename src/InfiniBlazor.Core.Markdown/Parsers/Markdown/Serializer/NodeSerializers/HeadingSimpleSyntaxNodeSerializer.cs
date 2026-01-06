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
public partial class HeadingSimpleSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    [GeneratedRegex(@"\G^(?<text>.+?)\n(?<id>[\ ]*(?:={3,}?|-{3,}?)[\ ]*$)", RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex Syntax { get; }
    
    private static readonly int HsTextId = Syntax.GroupNumberFromName("text");
    private static readonly int HsIdentifierId = Syntax.GroupNumberFromName("id");
    
    public char[] TriggerCharacters { get; } = [];
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
        string headerSimpleText = match.Groups[HsTextId].Value;
        string headerIdentifierText = match.Groups[HsIdentifierId].Value;

        HeadingSimpleMdSyntaxNode headingNode = MdSyntaxNodePool<HeadingSimpleMdSyntaxNode>.Shared.Get();
        headingNode.WithIdentifier(headerIdentifierText);

        parentNode.AddChildNode(headingNode);

        stack.PushSingleLineMatchesToStack(headerSimpleText, headingNode);
    }
}
