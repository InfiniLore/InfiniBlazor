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
public class UserSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    private static readonly int UsernameId = MdRegexLib.GetGroupId(MdRegexGroupNames.UserName);
    
    public Regex Syntax { get; } = MdRegexLib.UserRegex;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        if (!match.Groups[UsernameId].TryGetValue(out string? username)) return;

        UserMdSyntaxNode node = MdSyntaxNodePool<UserMdSyntaxNode>.Shared.Get();
        node.WithContent(username);
        parentNode.AddChildNode(node);
    }
}
