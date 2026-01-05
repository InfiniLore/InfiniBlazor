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
public partial class CalloutSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    [GeneratedRegex("""
        ^>(?:\[!(?<type>[^\|\n]+)(?<mod>\|[^\n]*)?\](?<option>\+|\-)?)[\ ]*(?<title>[^\n]*)$
        (?:\n(?<body>>[^\n]*(?:\n>[^\n]*)*)$)?
        """, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex Syntax { get; }
    
    private static readonly int CalloutTypeId = Syntax.GroupNumberFromName("type");
    private static readonly int CalloutModId = Syntax.GroupNumberFromName("mod");
    private static readonly int CalloutOptionId = Syntax.GroupNumberFromName("option");
    private static readonly int CalloutTitleId = Syntax.GroupNumberFromName("title");
    private static readonly int CalloutBodyId = Syntax.GroupNumberFromName("body");
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
