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
[InjectableSingleton<IMdSyntaxNodeSerializer>(MdRegexGroupNames.BoldContent)]
public sealed class BoldSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    private static readonly int BId = MdRegexLib.GetGroupId(MdRegexGroupNames.Bold);
    public MdSyntaxSerializerOrigin SkipOnOrigin => MdSyntaxSerializerOrigin.Bold;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IMdSyntaxSerializerStack stack, IMdSyntaxNode parentNode, Match entireMatch, MdSyntaxSerializerOrigin parentOrigin) {
        if (!entireMatch.Groups[BId].TryGetValue(out string? boldValue)) return;

        BoldMdSyntaxNode node = BoldMdSyntaxNode.Pool.Get();
        parentNode.AddChildNode(node);
        stack.PushSingleLineMatchesToStack(boldValue, node, parentOrigin | SkipOnOrigin);
    }
}