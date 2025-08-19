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
[InjectableSingleton<IMdSyntaxNodeSerializer>(MdRegexGroupNames.Italic)]
public sealed class ItalicSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    private static readonly int ItalicId = MdRegexLib.GetGroupId(MdRegexGroupNames.ItalicContent);
    public MdSyntaxSerializerOrigin SkipOnOrigin => MdSyntaxSerializerOrigin.Italic;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMdSyntaxSerializerStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        MdSyntaxSerializerOrigin parentOrigin
    ) {
        if (!entireMatch.Groups[ItalicId].TryGetValue(out string? italicValue)) return ;

        ItalicMdSyntaxNode node = ItalicMdSyntaxNode.Pool.Get();
        parentNode.AddChildNode(node);
        stack.PushSingleLineMatchesToStack(italicValue, node, parentOrigin | SkipOnOrigin);
    }
}
