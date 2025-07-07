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
    
    private static readonly int ModTitleId = MdRegexLib.GetGroupId(MdRegexGroupNames.ModTitle);
    private static readonly int ModSizeId = MdRegexLib.GetGroupId(MdRegexGroupNames.ModSize);
    private static readonly int ModFitId = MdRegexLib.GetGroupId(MdRegexGroupNames.ModFit);
    
    
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
        if (!entireMatch.Groups[LnModsId].TryGetValue(out string? linkMods)) return;

        if (entireMatch.Groups[LnBangId].Success) {
            ImageMdSyntaxNode imgNode = ImageMdSyntaxNode.Pool.Get();
            imgNode.Href = linkHref;
            imgNode.AltText = NormalizeAltText.Replace(linkText, string.Empty);
            
            if (linkMods.IsNotNullOrWhiteSpace()) {
                imgNode.ContainsMods = true;
                Match mods = MdRegexLib.ModifierStructuresRegex.Match(linkMods);
                GroupCollection groups = mods.Groups;
                
                if (groups[ModTitleId] is { Success: true, Value: var title }) imgNode.ModTitle = title;

                if (groups[ModFitId].Success) imgNode.ModFit = true;
                
                if (groups[ModSizeId] is { Success: true, Value: var size }) {
                    if (size.Contains('x')) {
                        string[] split = size.Split('x');
                        imgNode.ModSize = (int.Parse(split[0]), int.Parse(split[1]));
                    }
                    else if (int.TryParse(size, out int sizeInt)) {
                        imgNode.ModSize = (sizeInt, sizeInt);
                    }
                }
            }
            
            parentNode.AddChildNode(imgNode);
            return ;
        }
        
        LinkMdSyntaxNode linkNode = LinkMdSyntaxNode.Pool.Get();
        linkNode.Href = linkHref;
        parentNode.AddChildNode(linkNode);

        stack.PushSingleLineMatchesToStack(linkText, linkNode, parentOrigin);
    }
}
