// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Components;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using InfiniLore.Lucide;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.HtmlString.Deserializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IHtmlStringMdSyntaxNodeVisitor>]
public class SimpleMdSyntaxNodeVisitor(IEmoteProvider emoteProvider, ILucideService lucideService) : IHtmlStringMdSyntaxNodeVisitor {
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public virtual void HandleOpenTag(IMdSyntaxNode node, StringBuilder builder) {
        switch (node) {
            case BlockQuoteMdSyntaxNode: {
                builder.Append("<blockquote>");
                break;
            }

            case BoldMdSyntaxNode: {
                builder.Append("<strong>");
                break;
            }

            case CodeBlockMdSyntaxNode { Language: var lang } when lang.IsNotNullOrWhiteSpace(): {
                builder.Append("<pre><code class=\"language-");
                builder.Append(lang.AsSpan());
                builder.Append("\">");
                break;
            }

            case CodeBlockMdSyntaxNode: {
                builder.Append("<pre><code>");
                break;
            }

            case CodeInlineMdSyntaxNode: {
                builder.Append("<code>");
                break;
            }

            case HtmlMdSyntaxNode: break;
            case TextMdSyntaxNode:
            case EmoteMdSyntaxNode: break;
            case EscapedCharacterMdSyntaxNode: break;

            case HeadingSimpleMdSyntaxNode: {
                builder.Append("<h1>");
                break;
            }

            case HeadingMdSyntaxNode { Level: var level and > 0 }: {
                builder.Append("<h");
                builder.Append(level);
                builder.Append('>');
                break;
            }

            case HtmlSpanMdSyntaxNode { Attributes: var attributes }: {
                builder.Append("<span");
                if (attributes.IsNotNullOrEmpty()) {
                    builder.Append(' ');
                    builder.Append(attributes);
                }

                builder.Append('>');
                break;
            }

            case HorizontalRuleMdSyntaxNode: break;

            case ImageMdSyntaxNode { NormalizedAltText: var altText, Href: var href } imgNode: {
                builder.Append("<img class\"inline-block\" src=\"");
                builder.Append(href.AsSpan());
                builder.Append('"');
                if (altText.IsNotNullOrWhiteSpace()) {
                    builder.Append(" alt=\"");
                    builder.Append(altText.AsSpan());
                    builder.Append('"');
                }

                if (imgNode.Modifier is {} modifier) {
                    if (modifier.TryGetTitle(out string? title)) {
                        builder.Append(" title=\"");
                        builder.Append(title.AsSpan());
                        builder.Append('"');
                    }

                    if (modifier.TryGetFit(out bool state) && state) {
                        builder.Append(" style=\"width:auto;height:2em;vertical-align:baseline;object-fit:contain;\"");
                    }

                    else if (modifier.TryGetSize(out (int Width, int Height) size)) {
                        builder.Append(" style=\"width: ");
                        builder.Append(size.Width);
                        builder.Append("px; height: ");
                        builder.Append(size.Height);
                        builder.Append("px;\"");
                    }
                }

                builder.Append("/>");
                break;
            }

            case ItalicMdSyntaxNode: {
                builder.Append("<em>");
                break;
            }

            case LinkMdSyntaxNode { Href: var href }: {
                builder.Append("<a href=\"");
                builder.Append(href.AsSpan());
                builder.Append("\">");
                break;
            }

            case WikiLinkMdSyntaxNode { Content: var href }: {
                builder.Append("<a href=\"");
                builder.Append(href.AsSpan());
                builder.Append("\">");
                break;
            }

            case ListItemMdSyntaxNode { IsCheckable: true, IsChecked: var checkedState }: {
                builder.Append("<li>");
                builder.Append("<input type=\"checkbox\" disabled");
                if (checkedState) builder.Append(" checked");
                builder.Append("/>");
                break;
            }

            case ListItemMdSyntaxNode: {
                builder.Append("<li>");
                break;
            }

            case ListOrderedMdSyntaxNode: {
                builder.Append("<ol>");
                break;
            }

            case ListUnOrderedMdSyntaxNode: {
                builder.Append("<ul>");
                break;
            }

            case ParagraphMdSyntaxNode: {
                builder.Append("<p>");
                break;
            }

            case RootMdSyntaxNode: break;

            case StrikeMdSyntaxNode: {
                builder.Append("<s>");
                break;
            }

            case SubScriptMdSyntaxNode: {
                builder.Append("<sub>");
                break;
            }

            case SuperScriptMdSyntaxNode: {
                builder.Append("<sup>");
                break;
            }

            case TableMdSyntaxNode: {
                builder.Append("<table>");
                break;
            }

            case TableRowMdSyntaxNode when node.Parent is TableMdSyntaxNode tableNode && Equals(tableNode.GetChildAt(0), node): {
                builder.Append("<thead><tr>");
                break;
            }

            case TableRowMdSyntaxNode when node.Parent is TableMdSyntaxNode tableNode && Equals(tableNode.GetChildAt(1), node): {
                builder.Append("<tbody><tr>");
                break;
            }

            case TableRowMdSyntaxNode: {
                builder.Append("<tr>");
                break;
            }

            case TableCellMdSyntaxNode when node.Parent is TableRowMdSyntaxNode { Parent: TableMdSyntaxNode tableNode } rowNode && Equals(tableNode.GetChildAt(0), rowNode): {
                builder.Append("<th>");
                break;
            }

            case TableCellMdSyntaxNode: {
                builder.Append("<td>");
                break;
            }

            case TagMdSyntaxNode: {
                builder.Append("<span class=\"md-tag\">");
                break;
            }

            case UnderlineMdSyntaxNode: {
                builder.Append("<u>");
                break;
            }

            case CalloutMdSyntaxNode { CalloutType: {} calloutType } calloutNode: {
                builder.Append("<div class=\"md-callout md-callout-");
                builder.Append(calloutType);
                if (calloutNode.Modifier is {} modifier && modifier.TryGetIconName(out string? iconName)) {
                    builder.Append(" icon-");
                    builder.Append(iconName);
                }

                builder.Append("\">");
                break;
            }

            case CalloutMdSyntaxNode: {
                builder.Append("<div class=\"md-callout\">");
                break;
            }

            case CalloutTitleMdSyntaxNode: {
                builder.Append("<div class=\"md-callout-title\">");
                break;
            }

            case CalloutBodyMdSyntaxNode: {
                builder.Append("<div class=\"md-callout-body\">");
                break;
            }
        }
    }

    public virtual void HandleContent(IMdSyntaxNode node, StringBuilder builder) {
        switch (node) {

            case CodeBlockMdSyntaxNode { Content: var code }: {
                builder.Append(code.AsSpan());
                break;
            }

            case CodeInlineMdSyntaxNode codeInlineNode: {
                Span<char> span = stackalloc char[codeInlineNode.Content.Length];
                if (codeInlineNode.TryGetContentWithoutEscapedBackTicks(span, out int length)) {
                    builder.Append(span[..length]);
                }
                else {
                    builder.Append(codeInlineNode.Content);
                }

                break;
            }

            case HtmlMdSyntaxNode { Content: var content }: {
                builder.Append(content.AsSpan());
                break;
            }

            case TextMdSyntaxNode { Content: var content }: {
                builder.Append(content.AsSpan());
                break;
            }

            case EmoteMdSyntaxNode emoteNode: {
                IEmoteEntry? entry = emoteProvider.GetEntryAsync(emoteNode.EmoteKey);
                switch (entry?.ContentType) {
                    case EmoteContentType.Emoji: {
                        builder.Append(entry.Data);
                        break;
                    }

                    case EmoteContentType.LucideIconName when entry.Data is not null: {
                        string lucideIconSvg = lucideService.GetIconAsString(entry.Data);
                        if (lucideIconSvg.IsNullOrWhiteSpace()) break;

                        builder.Append(lucideIconSvg);
                        break;
                    }

                    case EmoteContentType.SvgData when entry.Data is not null: {
                        builder.Append(entry.Data);
                        break;
                    }

                    default: throw new ArgumentOutOfRangeException();
                }

                builder.Append(emoteNode.OriginalEmote);
                break;
            }

            case EscapedCharacterMdSyntaxNode { Content: var contentChar }: {
                builder.Append(contentChar);
                break;
            }

            case HorizontalRuleMdSyntaxNode: {
                builder.Append("<hr>");
                break;
            }

            case TagMdSyntaxNode { Content: var contentTag }: {
                builder.Append('#');
                builder.Append(contentTag.AsSpan());
                break;
            }

            case UserMdSyntaxNode { Content: var userName }: {
                builder.Append('@');
                builder.Append(userName.AsSpan());
                break;
            }

            case TemplateMdSyntaxNode { Content: var variable }: {
                builder.Append(variable.AsSpan());
                break;
            }
        }
    }

    public virtual void HandleCloseTag(IMdSyntaxNode node, StringBuilder builder) {
        switch (node) {
            case BlockQuoteMdSyntaxNode: {
                builder.Append("</blockquote>");
                break;
            }

            case BoldMdSyntaxNode: {
                builder.Append("</strong>");
                break;
            }

            case CodeBlockMdSyntaxNode: {
                builder.Append("</code></pre>");
                break;
            }

            case CodeInlineMdSyntaxNode: {
                builder.Append("</code>");
                break;
            }

            case HtmlMdSyntaxNode:
            case TextMdSyntaxNode:
            case EmoteMdSyntaxNode:
            case EscapedCharacterMdSyntaxNode: break;


            case HeadingSimpleMdSyntaxNode: {
                builder.Append("</h1>");
                break;
            }

            case HeadingMdSyntaxNode { Level: var level and > 0 }: {
                builder.Append("</h");
                builder.Append(level);
                builder.Append('>');
                break;
            }

            case HorizontalRuleMdSyntaxNode: break;

            case HtmlSpanMdSyntaxNode: {
                builder.Append("</span>");
                break;
            }

            case ImageMdSyntaxNode: break;

            case ItalicMdSyntaxNode: {
                builder.Append("</em>");
                break;
            }

            case WikiLinkMdSyntaxNode:
            case LinkMdSyntaxNode: {
                builder.Append("</a>");
                break;
            }

            case ListItemMdSyntaxNode: {
                builder.Append("</li>");
                break;
            }

            case ListOrderedMdSyntaxNode: {
                builder.Append("</ol>");
                break;
            }

            case ListUnOrderedMdSyntaxNode: {
                builder.Append("</ul>");
                break;
            }

            case ParagraphMdSyntaxNode: {
                builder.Append("</p>");
                break;
            }

            case RootMdSyntaxNode: break;

            case StrikeMdSyntaxNode: {
                builder.Append("</s>");
                break;
            }

            case SubScriptMdSyntaxNode: {
                builder.Append("</sub>");
                break;
            }

            case SuperScriptMdSyntaxNode: {
                builder.Append("</sup>");
                break;
            }

            case TableMdSyntaxNode: {
                builder.Append("</tbody></table>");
                break;
            }

            case TableRowMdSyntaxNode when node.Parent is TableMdSyntaxNode tableNode && Equals(tableNode.GetChildAt(0), node): {
                builder.Append("</tr></thead>");
                break;
            }

            case TableRowMdSyntaxNode: {
                builder.Append("</tr>");
                break;
            }

            case TableCellMdSyntaxNode when node.Parent is TableRowMdSyntaxNode { Parent: TableMdSyntaxNode tableNode } rowNode && Equals(tableNode.GetChildAt(0), rowNode): {
                builder.Append("</th>");
                break;
            }

            case TableCellMdSyntaxNode: {
                builder.Append("</td>");
                break;
            }

            case TagMdSyntaxNode: {
                builder.Append("</span>");
                break;
            }

            case UnderlineMdSyntaxNode: {
                builder.Append("</u>");
                break;
            }

            case CalloutMdSyntaxNode:
            case CalloutTitleMdSyntaxNode:
            case CalloutBodyMdSyntaxNode: {
                builder.Append("</div>");
                break;
            }
        }
    }
}
