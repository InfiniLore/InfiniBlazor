// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.RegexLib;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Handlers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMdSyntaxHandler>(MarkdownRegexGroupNames.Emote)]
public sealed class EmoteSyntaxHandler(IMdEmoteProvider mdEmoteProvider) : IMdSyntaxHandler {
    private static readonly int EId = MarkdownRegexLib.GetGroupId(MarkdownRegexGroupNames.E);
    public MdSyntaxHandlerOrigin SkipOnOrigin => MdSyntaxHandlerOrigin.Emote;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMdSyntaxParserStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        Group group,
        MdSyntaxHandlerOrigin origin
    ) {
        if (entireMatch.Groups[EId] is not {Success: true, ValueSpan: var span}) return ;
        if (!mdEmoteProvider.TryGetValue(span, out string? value)) {
            parentNode.WithContent(group.Value);
            return ;
        }

        EmoteMdSyntaxNode node = EmoteMdSyntaxNode.Pool.Get();
        node.ContentEmote = value;
        parentNode.AddChildNode(node);
    }
}
