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
[InjectableSingleton<IMdSyntaxNodeSerializer>(MdRegexGroupNames.SubScript)]
public sealed class SubScriptSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    private static readonly int SbId = MdRegexLib.GetGroupId(MdRegexGroupNames.SubScriptContent);
    public MdSyntaxSerializerOrigin SkipOnOrigin => MdSyntaxSerializerOrigin.SubScript;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMdSyntaxSerializerStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        MdSyntaxSerializerOrigin parentOrigin
    ) {
        if (!entireMatch.Groups[SbId].TryGetValue(out string? subValue)) return ;

        SubScriptMdSyntaxNode node = SubScriptMdSyntaxNode.Pool.Get();
        parentNode.AddChildNode(node);
        stack.PushSingleLineMatchesToStack(subValue, node, parentOrigin | SkipOnOrigin);
    }
}
