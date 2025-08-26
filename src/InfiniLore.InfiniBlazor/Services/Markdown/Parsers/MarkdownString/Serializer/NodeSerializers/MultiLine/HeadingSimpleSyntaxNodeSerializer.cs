// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.NodeSerializers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownStringMdSyntaxNodeSerializer>(MdRegexGroupNames.HeadingSimple)]
public sealed class HeadingSimpleSyntaxNodeSerializer : IMarkdownStringMdSyntaxNodeSerializer {
    private static readonly int HsTextId = MdRegexLib.GetGroupId(MdRegexGroupNames.HeadingSimpleText);
    private static readonly int HsIdentifierId = MdRegexLib.GetGroupId(MdRegexGroupNames.HeadingSimpleIdentifier);
    public MdSyntaxSerializerOrigin SkipOnOrigin => MdSyntaxSerializerOrigin.NotSkipped;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        MdSyntaxSerializerOrigin parentOrigin
    ) {
        if (!entireMatch.Groups[HsTextId].TryGetValue(out string? headerSimpleText)) return;
        if (!entireMatch.Groups[HsIdentifierId].TryGetValue(out string? headerIdentifierText)) return;

        HeadingSimpleMdSyntaxNode headingNode = HeadingSimpleMdSyntaxNode.Pool.Get();
        headingNode.ContentIdentifier = headerIdentifierText;
        
        parentNode.AddChildNode(headingNode);
        
        stack.PushSingleLineMatchesToStack(headerSimpleText, headingNode, parentOrigin);
    }
}
