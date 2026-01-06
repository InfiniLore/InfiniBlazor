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
public partial class HorizontalRuleSyntaxNodeSerializer : IMdSyntaxNodeSerializer{
    [GeneratedRegex(@"^(?<hr>\ *?(\-{3,}?|_{3,}?)\ *?)$", RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex Syntax { get; }
    
    private static readonly int HrId = Syntax.GroupNumberFromName("hr");
    
    public char[] TriggerCharacters { get; } = {'-', ' ', '_'};
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
        string hrContent = match.Groups[HrId].Value;

        HorizontalRuleMdSyntaxNode node = MdSyntaxNodePool<HorizontalRuleMdSyntaxNode>.Shared.Get();
        node.WithIdentifier(hrContent);
        parentNode.AddChildNode(node);
    }
}
