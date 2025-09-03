// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class CalloutSyntaxNodeSerializer  {
    // private static readonly int CalloutId = MdRegexLib.GetGroupId(MdRegexGroupNames.Callout);
    private static readonly int CalloutTypeId = MdRegexLib.GetGroupId(MdRegexGroupNames.CalloutType);
    private static readonly int CalloutModId = MdRegexLib.GetGroupId(MdRegexGroupNames.CalloutMod);
    private static readonly int CalloutTitleId = MdRegexLib.GetGroupId(MdRegexGroupNames.CalloutTitle);
    private static readonly int CalloutBodyId = MdRegexLib.GetGroupId(MdRegexGroupNames.CalloutBody);
    private static readonly int CalloutOptionId = MdRegexLib.GetGroupId(MdRegexGroupNames.CalloutOption);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        CalloutMdSyntaxNode node = CalloutMdSyntaxNode.Pool.Get();
        parentNode.AddChildNode(node);

        if (match.Groups[CalloutOptionId] is { Success: true, ValueSpan: {Length: > 0} option }) {
            node.WithExpandOption(option);
        }

        if (match.Groups[CalloutTypeId] is { Success: true, Value: {} typeName }) {
            node.CalloutType = typeName;
        }
        
        if (match.Groups[CalloutModId] is { Success: true, Value: {} mods }) {
            node.WithModifier(MdSyntaxNodeModifier.FromString(mods));
        }

        if (match.Groups[CalloutTitleId] is { Success: true, Value: {} title }) {
            CalloutTitleMdSyntaxNode titleNode = CalloutTitleMdSyntaxNode.Pool.Get();
            node.TrySetTitle(titleNode);
            
            stack.PushSingleLineMatchesToStack(title, titleNode);
        }

        // ReSharper disable once InvertIf
        if (match.Groups[CalloutBodyId] is { Success: true, ValueSpan: var calloutBody }) {
            CalloutBodyMdSyntaxNode bodyNode = CalloutBodyMdSyntaxNode.Pool.Get();
            node.TrySetBody(bodyNode);
            
            stack.PushMultiLineMatchesToStack(
                LineNormalization.NormalizeBlockQuote(calloutBody, out _),
                bodyNode
            );
        }
    }
}
