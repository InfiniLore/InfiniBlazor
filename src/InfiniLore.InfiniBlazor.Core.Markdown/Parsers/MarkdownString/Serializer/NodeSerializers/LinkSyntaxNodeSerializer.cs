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
public static class LinkSyntaxNodeSerializer  {
    private static readonly int LnTextId = MdRegexLib.GetGroupId(MdRegexGroupNames.LinkText);
    private static readonly int LnHrefId = MdRegexLib.GetGroupId(MdRegexGroupNames.LinkHref);
    private static readonly int LnModsId = MdRegexLib.GetGroupId(MdRegexGroupNames.LinkModifiers);
    private static readonly int LnBangId = MdRegexLib.GetGroupId(MdRegexGroupNames.LinkBang);
    private static readonly int LnTitleId = MdRegexLib.GetGroupId(MdRegexGroupNames.LinkTitle);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        // ReSharper disable once DuplicatedSequentialIfBodies
        if (!match.Groups[LnTextId].TryGetValue(out string? linkText)) return ;
        if (!match.Groups[LnHrefId].TryGetValue(out string? linkHref)) return;

        match.Groups[LnModsId].TryGetValue(out string? mods);
        match.Groups[LnTitleId].TryGetValue(out string? title);

        if (match.Groups[LnBangId].Success) {
            ImageMdSyntaxNode imgNode = ImageMdSyntaxNode.Pool.Get();
            imgNode.WithAltText(linkText);
            imgNode.WithHref(linkHref);
            
            if (mods.IsNotNullOrWhiteSpace()) imgNode.WithModifier(MdSyntaxNodeModifier.FromString(mods));
            
            parentNode.AddChildNode(imgNode);
            return ;
        }
        
        LinkMdSyntaxNode linkNode = LinkMdSyntaxNode.Pool.Get();
        linkNode.WithHref(linkHref);
        if (mods.IsNotNullOrWhiteSpace()) linkNode.WithModifier(MdSyntaxNodeModifier.FromString(mods));
        
        parentNode.AddChildNode(linkNode);
        stack.PushSingleLineMatchesToStack(linkText, linkNode);
    }
}
