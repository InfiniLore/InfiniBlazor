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
public partial class LinkSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    [GeneratedRegex("""
        (?<bang>!)?
        \[(?<text> (?:\ *!?\[.+?\]\(.+?\)\ *)|(?:[^\\\]]|\\\]|\\[^\]])*?)\]
        \(
          (?<href>\ *https?[^\ |]+)
          (?:\ ?\"(?<title>.+)\")?
          (?<mods>\|.*)?
        \)
        """, RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex Syntax { get; }
    
    private static readonly int LnBangId = Syntax.GroupNumberFromName("bang");
    private static readonly int LnTextId = Syntax.GroupNumberFromName("text");
    private static readonly int LnHrefId = Syntax.GroupNumberFromName("href");
    private static readonly int LnTitleId = Syntax.GroupNumberFromName("title");
    private static readonly int LnModsId = Syntax.GroupNumberFromName("mods");
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
        if (!match.Groups[LnTextId].TryGetValue(out string? linkText)) return;
        if (!match.Groups[LnHrefId].TryGetValue(out string? linkHref)) return;

        match.Groups[LnModsId].TryGetValue(out string? mods);
        match.Groups[LnTitleId].TryGetValue(out string? title);

        if (match.Groups[LnBangId].Success) {
            ImageMdSyntaxNode imgNode = MdSyntaxNodePool<ImageMdSyntaxNode>.Shared.Get();
            imgNode.WithAltText(linkText);
            imgNode.WithHref(linkHref);

            if (mods.IsNotNullOrWhiteSpace()) imgNode.WithModifier(MdSyntaxNodeModifier.FromString(mods));
            if (title.IsNotNullOrEmpty()) imgNode.WithTitle(title);

            parentNode.AddChildNode(imgNode);
            return;
        }

        LinkMdSyntaxNode linkNode = MdSyntaxNodePool<LinkMdSyntaxNode>.Shared.Get();
        linkNode.WithHref(linkHref);
        if (mods.IsNotNullOrWhiteSpace()) linkNode.WithModifier(MdSyntaxNodeModifier.FromString(mods));
        if (title.IsNotNullOrEmpty()) linkNode.WithTitle(title);

        parentNode.AddChildNode(linkNode);
        stack.PushSingleLineMatchesToStack(linkText, linkNode);
    }
}
