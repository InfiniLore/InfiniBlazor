// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Handlers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMdSyntaxHandler>(MdRegexGroupNames.Callout)]
public sealed class CalloutSyntaxHandler : IMdSyntaxHandler {
    public MdSyntaxHandlerOrigin SkipOnOrigin => MdSyntaxHandlerOrigin.NotSkipped;
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
        IMdSyntaxParserStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        MdSyntaxHandlerOrigin parentOrigin
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
            node.Modifiers = MdSyntaxNodeModifier.FromString(mods);
        }

        if (entireMatch.Groups[CalloutTitleId] is { Success: true, Value: {} title }) {
            CalloutTitleMdSyntaxNode titleNode = CalloutTitleMdSyntaxNode.Pool.Get();
            node.AddChildNode(titleNode);
            
            stack.PushSingleLineMatchesToStack(title, titleNode, parentOrigin);
        }

        // ReSharper disable once InvertIf
        if (entireMatch.Groups[CalloutBodyId] is { Success: true, ValueSpan: var calloutBody }) {
            CalloutBodyMdSyntaxNode bodyNode = CalloutBodyMdSyntaxNode.Pool.Get();
            node.AddChildNode(bodyNode);
            
            stack.PushMultiLineMatchesToStack(
                LineNormalization.NormalizeBlockQuote(calloutBody),
                bodyNode,
                parentOrigin | MdSyntaxHandlerOrigin.PreserveHtml
            );
        }
    }
}
