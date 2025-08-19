// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SyntaxSerializer.NodeSerializers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMdSyntaxNodeSerializer>(MdRegexGroupNames.Strike)]
public sealed class StrikeSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    private static readonly int SId = MdRegexLib.GetGroupId(MdRegexGroupNames.StrikeContent);
    public MdSyntaxSerializerOrigin SkipOnOrigin => MdSyntaxSerializerOrigin.Strike;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMdSyntaxSerializerStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        MdSyntaxSerializerOrigin parentOrigin
    ) {
        if (!entireMatch.Groups[SId].TryGetValue(out string? strikeValue)) return ;

        StrikeMdSyntaxNode node = StrikeMdSyntaxNode.Pool.Get();
        parentNode.AddChildNode(node);
        stack.PushSingleLineMatchesToStack(strikeValue, node, parentOrigin | SkipOnOrigin);
    }
}
