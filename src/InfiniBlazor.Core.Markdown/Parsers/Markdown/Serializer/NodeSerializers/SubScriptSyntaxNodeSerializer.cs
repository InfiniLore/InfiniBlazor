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
public partial class SubScriptSyntaxNodeSerializer : IMdSyntaxNodeSerializer{
    [GeneratedRegex(@"(?<subScript>~(?<sb>(?>[^\\~\n]+|\\~|~~|(?<open>~)|(?<-open>~))+)(?(open)(?!))~)", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex Syntax { get; }
    
    private static readonly int SbId = Syntax.GroupNumberFromName(MdRegexGroupNames.SubScriptContent);
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
        if (!match.Groups[SbId].TryGetValue(out string? subValue)) return;

        SubScriptMdSyntaxNode node = MdSyntaxNodePool<SubScriptMdSyntaxNode>.Shared.Get();
        parentNode.AddChildNode(node);
        stack.PushSingleLineMatchesToStack(subValue, node);
    }
}
