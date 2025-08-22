// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.SyntaxSerializer.NodeSerializers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownStringMdSyntaxNodeSerializer>(MdRegexGroupNames.Heading)]
public sealed class HeadingSyntaxNodeSerializer : IMarkdownStringMdSyntaxNodeSerializer {
    private static readonly int HLevelId = MdRegexLib.GetGroupId(MdRegexGroupNames.HeadingLevel);
    private static readonly int HTextId = MdRegexLib.GetGroupId(MdRegexGroupNames.HeadingText);
    public MarkdownStringMdSyntaxSerializerOrigin SkipOnOrigin => MarkdownStringMdSyntaxSerializerOrigin.NotSkipped;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMarkdownStringMdSyntaxSerializerStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        MarkdownStringMdSyntaxSerializerOrigin parentOrigin
    ) {
        // ReSharper disable once DuplicatedSequentialIfBodies
        if (!entireMatch.Groups[HLevelId].TryGetLength(out int headingLevel)) return;
        if (!entireMatch.Groups[HTextId].TryGetValue(out string? headerText)) return;

        HeadingMdSyntaxNode headingNode = HeadingMdSyntaxNode.Pool.Get();
        headingNode.Level = headingLevel;
        parentNode.AddChildNode(headingNode);
        
        stack.PushSingleLineMatchesToStack(headerText, headingNode, parentOrigin);
    }
}
