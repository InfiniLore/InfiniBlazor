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
public sealed partial class LinkSyntaxNodeSerializer : BaseMdSyntaxNodeSerializer {
    [GeneratedRegex("""
        \G
        (?<bang>!)?
        \[(?<text> (?:\ *!?\[.+?\]\(.+?\)\ *)|(?:[^\\\]]|\\\]|\\[^\]])*?)\]
        \(
          (?<href>\ *https?[^\ |]+)
          (?:\ ?\"(?<title>.+)\")?
          (?<mods>\|.*)?
        \)
        """, DefaultSingleLineRegexOptions)]
    private static partial Regex RegexRule { get; }
    protected override Regex Syntax { get; } = RegexRule;

    public override ReadOnlySpan<char> TriggerCharacters => ['!', '['];

    private static readonly int LnBangId = RegexRule.GroupNumberFromName("bang");
    private static readonly int LnTextId = RegexRule.GroupNumberFromName("text");
    private static readonly int LnHrefId = RegexRule.GroupNumberFromName("href");
    private static readonly int LnTitleId = RegexRule.GroupNumberFromName("title");
    private static readonly int LnModsId = RegexRule.GroupNumberFromName("mods");

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        string linkText = match.Groups[LnTextId].Value;
        string linkHref = match.Groups[LnHrefId].Value;
        string mods = match.Groups[LnModsId].Value;
        string title = match.Groups[LnTitleId].Value;

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
