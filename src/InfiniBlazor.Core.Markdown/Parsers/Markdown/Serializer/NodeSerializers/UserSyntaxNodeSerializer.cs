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
public partial class UserSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    [GeneratedRegex(@"\@(?<u>[\p{L}\p{N}\-_\/\.]+)", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex Syntax { get; }
    
    private static readonly int UsernameId = Syntax.GroupNumberFromName("u");
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
        string username = match.Groups[UsernameId].Value;

        UserMdSyntaxNode node = MdSyntaxNodePool<UserMdSyntaxNode>.Shared.Get();
        node.WithContent(username);
        parentNode.AddChildNode(node);
    }
}
