// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SyntaxSerializer.NodeSerializers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMdSyntaxNodeSerializer>(MdRegexGroupNames.Heading)]
public sealed class HeadingSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    private static readonly int HLevelId = MdRegexLib.GetGroupId(MdRegexGroupNames.HeadingLevel);
    private static readonly int HTextId = MdRegexLib.GetGroupId(MdRegexGroupNames.HeadingText);
    public MdSyntaxSerializerOrigin SkipOnOrigin => MdSyntaxSerializerOrigin.NotSkipped;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMdSyntaxSerializerStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        MdSyntaxSerializerOrigin parentOrigin
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
