// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.SyntaxSerializer.NodeSerializers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownStringMdSyntaxNodeSerializer>(MdRegexGroupNames.Link)]
public sealed partial class LinkSyntaxNodeSerializer : IMarkdownStringMdSyntaxNodeSerializer {
    private static readonly int LnTextId = MdRegexLib.GetGroupId(MdRegexGroupNames.LinkText);
    private static readonly int LnHrefId = MdRegexLib.GetGroupId(MdRegexGroupNames.LinkHref);
    private static readonly int LnModsId = MdRegexLib.GetGroupId(MdRegexGroupNames.LinkModifiers);
    private static readonly int LnBangId = MdRegexLib.GetGroupId(MdRegexGroupNames.LinkBang);
    public MarkdownStringMdSyntaxSerializerOrigin SkipOnOrigin => MarkdownStringMdSyntaxSerializerOrigin.NotSkipped;
    
    [GeneratedRegex(@"\\(?!\\)")]
    private static partial Regex NormalizeAltText { get; }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMarkdownStringMdSyntaxSerializerStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        MarkdownStringMdSyntaxSerializerOrigin parentOrigin
    ) {
        // ReSharper disable once DuplicatedSequentialIfBodies
        if (!entireMatch.Groups[LnTextId].TryGetValue(out string? linkText)) return ;
        if (!entireMatch.Groups[LnHrefId].TryGetValue(out string? linkHref)) return;
        if (!entireMatch.Groups[LnModsId].TryGetValue(out string? mods)) return;

        if (entireMatch.Groups[LnBangId].Success) {
            ImageMdSyntaxNode imgNode = ImageMdSyntaxNode.Pool.Get();
            imgNode.Href = linkHref;
            imgNode.AltText = NormalizeAltText.Replace(linkText, string.Empty);

            if (mods.IsNotNullOrWhiteSpace()) {
                imgNode.WithModifier(MdSyntaxNodeModifier.FromString(mods));
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
