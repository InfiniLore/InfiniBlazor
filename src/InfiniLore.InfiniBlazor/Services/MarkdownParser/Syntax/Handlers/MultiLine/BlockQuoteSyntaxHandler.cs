// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.RegexLib;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Handlers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMdSyntaxHandler>(MdRegexGroupNames.BlockQuote)]
public sealed class BlockQuoteSyntaxHandler : IMdSyntaxHandler {
    public MdSyntaxHandlerOrigin SkipOnOrigin => MdSyntaxHandlerOrigin.NotSkipped;
    private static readonly int BlockQuoteId = MdRegexLib.GetGroupId(MdRegexGroupNames.BlockQuote);
    private static readonly int BqModId = MdRegexLib.GetGroupId(MdRegexGroupNames.BqMods);
    private static readonly int BqBodyId = MdRegexLib.GetGroupId(MdRegexGroupNames.BqBody);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMdSyntaxParserStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        MdSyntaxHandlerOrigin parentOrigin
    ) {
        // If there are mods the entire structure looks a bit different, aka a callout
        //      We need to parse the mods to get the title etc.
        if (entireMatch.Groups[BqModId].TryGetValue(out string? mods) 
            && mods.IsNotNullOrWhiteSpace()
            && entireMatch.Groups[BqBodyId].TryGetValueSpan(out ReadOnlySpan<char> bodySpan)) 
        {
            CalloutMdSyntaxNode node = CalloutMdSyntaxNode.Pool.Get();
            parentNode.AddChildNode(node);
            
            CalloutTitleMdSyntaxNode titleNode = CalloutTitleMdSyntaxNode.Pool.Get();
            node.AddChildNode(titleNode);
            
            CalloutBodyMdSyntaxNode bodyNode = CalloutBodyMdSyntaxNode.Pool.Get();
            node.AddChildNode(bodyNode);
            
            MdSyntaxMod mod = MdSyntaxMod.FromString(mods);            
            node.Mod = mod;
            
            if (mod.Title.IsNotNullOrWhiteSpace()) {
                stack.PushSingleLineMatchesToStack(mod.Title, titleNode, parentOrigin|MdSyntaxHandlerOrigin.PreserveHtml);
            }
            
            ReadOnlySpan<char> normalized = LineNormalization.NormalizeBlockQuote(bodySpan);
            string adjustedBlockquote = LineNormalization.NormalizeLineIndentation(normalized);
            
            stack.PushMultiLineMatchesToStack(adjustedBlockquote, bodyNode, parentOrigin | MdSyntaxHandlerOrigin.PreserveHtml);
            return;
        }

        // ReSharper disable once InvertIf
        if (entireMatch.Groups[BlockQuoteId].TryGetValueSpan(out bodySpan)){
            BlockQuoteMdSyntaxNode node = BlockQuoteMdSyntaxNode.Pool.Get();
            parentNode.AddChildNode(node);
            
            ReadOnlySpan<char> normalized = LineNormalization.NormalizeBlockQuote(bodySpan);
            string adjustedBlockquote = LineNormalization.NormalizeLineIndentation(normalized);
            
            stack.PushMultiLineMatchesToStack(adjustedBlockquote, node, parentOrigin | MdSyntaxHandlerOrigin.PreserveHtml);  
            
        }

    }
}
