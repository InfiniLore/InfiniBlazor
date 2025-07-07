// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.RegexLib;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Handlers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMdSyntaxHandler>(MdRegexGroupNames.Link)]
public sealed partial class LinkSyntaxHandler : IMdSyntaxHandler {
    private static readonly int LnTextId = MdRegexLib.GetGroupId(MdRegexGroupNames.LnText);
    private static readonly int LnHrefId = MdRegexLib.GetGroupId(MdRegexGroupNames.LnHref);
    private static readonly int LnModsId = MdRegexLib.GetGroupId(MdRegexGroupNames.LnMods);
    private static readonly int LnBangId = MdRegexLib.GetGroupId(MdRegexGroupNames.LnBang);
    public MdSyntaxHandlerOrigin SkipOnOrigin => MdSyntaxHandlerOrigin.NotSkipped;
    
    [GeneratedRegex(@"\\(?!\\)")]
    private static partial Regex NormalizeAltText { get; }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMdSyntaxParserStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        MdSyntaxHandlerOrigin parentOrigin
    ) {
        // ReSharper disable once DuplicatedSequentialIfBodies
        if (!entireMatch.Groups[LnTextId].TryGetValue(out string? linkText)) return ;
        if (!entireMatch.Groups[LnHrefId].TryGetValue(out string? linkHref)) return;
        if (!entireMatch.Groups[LnModsId].TryGetValue(out string? mods)) return;

        if (entireMatch.Groups[LnBangId].Success) {
            ImageMdSyntaxNode imgNode = ImageMdSyntaxNode.Pool.Get();
            imgNode.Href = linkHref;
            imgNode.AltText = NormalizeAltText.Replace(linkText, string.Empty);
            
            if (mods.IsNotNullOrWhiteSpace()) imgNode.Mod = MdSyntaxMod.FromString(mods);
            
            parentNode.AddChildNode(imgNode);
            return ;
        }
        
        LinkMdSyntaxNode linkNode = LinkMdSyntaxNode.Pool.Get();
        linkNode.Href = linkHref;
        parentNode.AddChildNode(linkNode);

        stack.PushSingleLineMatchesToStack(linkText, linkNode, parentOrigin);
    }
}
