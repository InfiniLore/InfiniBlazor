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
public partial class SubScriptSyntaxNodeSerializer : IMdSyntaxNodeSerializer{
    [GeneratedRegex(@"\G~(?<sb>(?>[^\\~\n]+|\\~|~~|(?<open>~)|(?<-open>~))+)(?(open)(?!))~", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex Syntax { get; }
    
    private static readonly int SbId = Syntax.GroupNumberFromName("sb");
    
    public char[] TriggerCharacters { get; } = ['~'];
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
        string subValue = match.Groups[SbId].Value;

        SubScriptMdSyntaxNode node = MdSyntaxNodePool<SubScriptMdSyntaxNode>.Shared.Get();
        parentNode.AddChildNode(node);
        stack.PushSingleLineMatchesToStack(subValue, node);
    }
}
