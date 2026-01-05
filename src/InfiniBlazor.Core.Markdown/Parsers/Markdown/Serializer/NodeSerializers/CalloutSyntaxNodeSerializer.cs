// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Parsers.Markdown.Serializer.RegexLib;
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class CalloutSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    [GeneratedRegex("""
        (?<callout>
            ^>(?:\[!(?<clType>[^\|\n]+)(?<clMod>\|[^\n]*)?\](?<clOption>\+|\-)?)[\ ]*(?<clTitle>[^\n]*)$
            (?:\n(?<clBody>>[^\n]*(?:\n>[^\n]*)*)$)?  
        )
        """, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex Syntax { get; }
    
    private static readonly int CalloutTypeId = Syntax.GroupNumberFromName(MdRegexGroupNames.CalloutType);
    private static readonly int CalloutModId = Syntax.GroupNumberFromName(MdRegexGroupNames.CalloutMod);
    private static readonly int CalloutTitleId = Syntax.GroupNumberFromName(MdRegexGroupNames.CalloutTitle);
    private static readonly int CalloutBodyId = Syntax.GroupNumberFromName(MdRegexGroupNames.CalloutBody);
    private static readonly int CalloutOptionId = Syntax.GroupNumberFromName(MdRegexGroupNames.CalloutOption);
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public Match Match(string input, int startPosition = 0) 
        => Syntax.Match(input, startPosition);
    
    public void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        CalloutMdSyntaxNode node = MdSyntaxNodePool<CalloutMdSyntaxNode>.Shared.Get();
        parentNode.AddChildNode(node);

        if (match.Groups[CalloutOptionId] is { Success: true, ValueSpan: { Length: > 0 } option }) {
            node.WithExpandOption(option);
        }

        if (match.Groups[CalloutTypeId] is { Success: true, Value: {} typeName }) {
            node.WithCalloutType(typeName);
        }

        if (match.Groups[CalloutModId] is { Success: true, Value: {} mods }) {
            node.WithModifier(MdSyntaxNodeModifier.FromString(mods));
        }

        if (match.Groups[CalloutTitleId] is { Success: true, Value: {} title }) {
            CalloutTitleMdSyntaxNode titleNode = MdSyntaxNodePool<CalloutTitleMdSyntaxNode>.Shared.Get();
            node.TrySetTitle(titleNode);

            stack.PushSingleLineMatchesToStack(title, titleNode);
        }

        // ReSharper disable once InvertIf
        if (match.Groups[CalloutBodyId] is { Success: true, ValueSpan: var calloutBody }) {
            CalloutBodyMdSyntaxNode bodyNode = MdSyntaxNodePool<CalloutBodyMdSyntaxNode>.Shared.Get();
            node.TrySetBody(bodyNode);

            stack.PushMultiLineMatchesToStack(
                LineNormalization.NormalizeBlockQuote(calloutBody, out _),
                bodyNode
            );
        }
    }
}
