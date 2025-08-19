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
[InjectableSingleton<IMdSyntaxNodeSerializer>(MdRegexGroupNames.Escaped)]
public sealed class EscapedSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    public MdSyntaxSerializerOrigin SkipOnOrigin => MdSyntaxSerializerOrigin.NotSkipped;
    private static readonly int EscapedId = MdRegexLib.GetGroupId(MdRegexGroupNames.Escaped);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMdSyntaxSerializerStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        MdSyntaxSerializerOrigin parentOrigin
    ) {
        char value = entireMatch.Groups[EscapedId].ValueSpan[1];
        EscapedCharacterMdSyntaxNode node = EscapedCharacterMdSyntaxNode.Pool.Get();
        node.ContentChar = value;
        parentNode.AddChildNode(node);
    }
}
