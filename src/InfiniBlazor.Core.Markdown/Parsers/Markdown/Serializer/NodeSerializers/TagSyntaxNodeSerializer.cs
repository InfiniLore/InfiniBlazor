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
public partial class TagSyntaxNodeSerializer : IMdSyntaxNodeSerializer{
    [GeneratedRegex(@"\#(?<t>[\p{L}\p{N}\-_\/\.]+)", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex Syntax { get; }
    
    private static readonly int TextId = Syntax.GroupNumberFromName("t");
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
        string tagValue = match.Groups[TextId].Value;

        TagMdSyntaxNode node = MdSyntaxNodePool<TagMdSyntaxNode>.Shared.Get();
        node.WithContent(tagValue);
        parentNode.AddChildNode(node);
    }
}
