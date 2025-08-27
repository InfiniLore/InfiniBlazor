// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownStringMdSyntaxNodeSerializer>(MdRegexGroupNames.Callout)]
public sealed class CalloutSyntaxNodeSerializer : IMarkdownStringMdSyntaxNodeSerializer {
    public MdSyntaxSerializerOrigin SkipOnOrigin => MdSyntaxSerializerOrigin.NotSkipped;
    // private static readonly int CalloutId = MdRegexLib.GetGroupId(MdRegexGroupNames.Callout);
    private static readonly int CalloutTypeId = MdRegexLib.GetGroupId(MdRegexGroupNames.CalloutType);
    private static readonly int CalloutModId = MdRegexLib.GetGroupId(MdRegexGroupNames.CalloutMod);
    private static readonly int CalloutTitleId = MdRegexLib.GetGroupId(MdRegexGroupNames.CalloutTitle);
    private static readonly int CalloutBodyId = MdRegexLib.GetGroupId(MdRegexGroupNames.CalloutBody);
    private static readonly int CalloutOptionId = MdRegexLib.GetGroupId(MdRegexGroupNames.CalloutOption);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch
    ) {
        CalloutMdSyntaxNode node = CalloutMdSyntaxNode.Pool.Get();
        parentNode.AddChildNode(node);

        if (entireMatch.Groups[CalloutOptionId] is { Success: true, ValueSpan: {Length: > 0} option }) {
            node.WithExpandOption(option);
        }

        if (entireMatch.Groups[CalloutTypeId] is { Success: true, Value: {} typeName }) {
            node.CalloutType = typeName;
        }
        
        if (entireMatch.Groups[CalloutModId] is { Success: true, Value: {} mods }) {
            node.WithModifier(MdSyntaxNodeModifier.FromString(mods));
        }

        if (entireMatch.Groups[CalloutTitleId] is { Success: true, Value: {} title }) {
            CalloutTitleMdSyntaxNode titleNode = CalloutTitleMdSyntaxNode.Pool.Get();
            node.AddChildNode(titleNode);
            
            stack.PushSingleLineMatchesToStack(title, titleNode);
        }

        // ReSharper disable once InvertIf
        if (entireMatch.Groups[CalloutBodyId] is { Success: true, ValueSpan: var calloutBody }) {
            CalloutBodyMdSyntaxNode bodyNode = CalloutBodyMdSyntaxNode.Pool.Get();
            node.AddChildNode(bodyNode);
            
            stack.PushMultiLineMatchesToStack(
                LineNormalization.NormalizeBlockQuote(calloutBody, out _),
                bodyNode
            );
        }
    }
}
