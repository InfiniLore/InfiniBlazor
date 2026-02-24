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
public sealed partial class UserSyntaxNodeSerializer : BaseMdSyntaxNodeSerializer {
    [GeneratedRegex(@"\G\@(?<u>[\p{L}\p{N}\-_\/\.]+)", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex RegexRule { get; }
    protected override Regex Syntax { get; } = RegexRule;

    private static readonly int UsernameId = RegexRule.GroupNumberFromName("u");

    private static readonly char[] STriggerCharacters = ['@'];
    public override char[] TriggerCharacters => STriggerCharacters;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override void Serialize(
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
