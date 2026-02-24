// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed partial class CalloutSyntaxNodeSerializer : BaseMdSyntaxNodeSerializer {
    [GeneratedRegex("""
        \G
        ^>(?:\[!(?<type>[^\|\n]+)(?<mod>\|[^\n]*)?\](?<option>\+|\-)?)[\ ]*(?<title>[^\n]*)$
        (?:\n(?<body>>[^\n]*(?:\n>[^\n]*)*)$)?
        """, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex RegexRule { get; }
    protected override Regex Syntax { get; } = RegexRule;

    public override char[] TriggerCharacters { get; } = ['>'];
    
    private static readonly int CalloutTypeId = RegexRule.GroupNumberFromName("type");
    private static readonly int CalloutModId = RegexRule.GroupNumberFromName("mod");
    private static readonly int CalloutOptionId = RegexRule.GroupNumberFromName("option");
    private static readonly int CalloutTitleId = RegexRule.GroupNumberFromName("title");
    private static readonly int CalloutBodyId = RegexRule.GroupNumberFromName("body");
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override void Serialize(
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
