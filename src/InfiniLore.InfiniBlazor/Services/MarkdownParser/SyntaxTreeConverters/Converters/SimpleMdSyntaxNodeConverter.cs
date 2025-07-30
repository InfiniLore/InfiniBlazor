// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.MarkdownParser.SyntaxTreeConverters.Converters;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMdSyntaxNodeConverter>]
public class SimpleMdSyntaxNodeConverter : IMdSyntaxNodeConverter {

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

            case CodeBlockMdSyntaxNode {Language: var lang} when lang.IsNotNullOrWhiteSpace(): {
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
            
            case ContentHtmlMdSyntaxNode:break;
            case ContentMdSyntaxNode:
            case EmoteMdSyntaxNode:break;
            case EscapedCharacterMdSyntaxNode:break;

            case HeadingMdSyntaxNode { Level: var level and > 0 }: {
                builder.Append("<h");
                builder.Append(level);
                builder.Append('>');
                break;
            }
            
            case HtmlSpanMdSyntaxNode { TagValue: var tagValue }: {
                builder.Append(tagValue.AsSpan());
                break;
            }
            
            case HorizontalRuleMdSyntaxNode:break;

            case ImageMdSyntaxNode {AltText: var altText, Href: var href} imgNode: {
                builder.Append("<img class\"inline-block\" src=\"");
                builder.Append(href.AsSpan());
                builder.Append('"');
                if (altText.IsNotNullOrWhiteSpace()) {
                    builder.Append(" alt=\"");
                    builder.Append(altText.AsSpan());
                    builder.Append('"');
                }

                if (imgNode.ContainsModifiers) {
                    if (imgNode.Modifiers.TryGetTitle(out string? title)) {
                        builder.Append(" title=\"");
                        builder.Append(title.AsSpan());
                        builder.Append('"');
                    }
                    
                    if (imgNode.Modifiers.TryGetFit(out bool state) && state) {
                        builder.Append(" style=\"width:auto;height:2em;vertical-align:baseline;object-fit:contain;\"");
                    }
                    
                    else if (imgNode.Modifiers.TryGetSize(out (int Width, int Height) size)) {
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

            case LinkMdSyntaxNode {Href: var href}: {
                builder.Append("<a href=\"");
                builder.Append(href.AsSpan());
                builder.Append("\">");
                break;
            }

            case ListItemMdSyntaxNode {IsCheckable: true, IsChecked: var checkedState}: {
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

            case ListMdSyntaxNode { IsOrdered: true }: {
                builder.Append("<ol>");
                break;
            }

            case ListMdSyntaxNode { IsOrdered: false }: {
                builder.Append("<ul>");
                break;
            }

            case ParagraphMdSyntaxNode: {
                builder.Append("<p>");
                break;
            }
            case RootMdSyntaxNode:break;

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

            case TableBodyMdSyntaxNode: {
                builder.Append("<tbody>");
                break;
            }

            case TableCellMdSyntaxNode: {
                builder.Append("<td>");
                break;
            }
            
            case TableHeadCellMdSyntaxNode: {
                builder.Append("<th>");
                break;
            }

            case TableHeadMdSyntaxNode: {
                builder.Append("<thead>");
                break;
            }

            case TableMdSyntaxNode: {
                builder.Append("<table>");
                break;
            }

            case TableRowMdSyntaxNode: {
                builder.Append("<tr>");
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

            case CalloutMdSyntaxNode {CalloutType: {} calloutType, Modifiers: var modifier}: {
                builder.Append("<div class=\"md-callout md-callout-");
                builder.Append(calloutType);
                if (modifier is not null && modifier.TryGetIconName(out string? iconName)) {
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

            case CodeBlockMdSyntaxNode { ContentCode: var code }: {
                builder.Append(code.AsSpan());
                break;
            }

            case CodeInlineMdSyntaxNode {ContentCode: var code }: {
                builder.Append(code.AsSpan());
                break;
            }

            case ContentHtmlMdSyntaxNode { ContentHtml: var content }: {
                builder.Append(content.AsSpan());
                break;
            }

            case ContentMdSyntaxNode { Content: var content }: {
                builder.Append(content.AsSpan());
                break;
            }

            case EmoteMdSyntaxNode { ContentEmote: var content }: {
                builder.Append(content.AsSpan());
                break;
            }

            case EscapedCharacterMdSyntaxNode { ContentChar: var contentChar }: {
                builder.Append(contentChar);
                break;
            }

            case HorizontalRuleMdSyntaxNode: {
                builder.Append("<hr>");
                break;
            }

            case TagMdSyntaxNode { ContentTag: var contentTag }: {
                builder.Append('#');
                builder.Append(contentTag.AsSpan());
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
            
            case ContentHtmlMdSyntaxNode:
            case ContentMdSyntaxNode:
            case EmoteMdSyntaxNode:
            case EscapedCharacterMdSyntaxNode:break;

            case HeadingMdSyntaxNode {Level: var level and > 0 }: {
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
            
            case ImageMdSyntaxNode:break;

            case ItalicMdSyntaxNode: {
                builder.Append("</em>");
                break;
            }

            case LinkMdSyntaxNode: {
                builder.Append("</a>");
                break;
            }

            case ListItemMdSyntaxNode: {
                builder.Append("</li>");
                break;
            }

            case ListMdSyntaxNode { IsOrdered: false }: {
                builder.Append("</ul>");
                break;
            }

            case ListMdSyntaxNode { IsOrdered: true }: {
                builder.Append("</ol>");
                break;
            }

            case ParagraphMdSyntaxNode: {
                builder.Append("</p>");
                break;
            }
            case RootMdSyntaxNode:break;

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

            case TableBodyMdSyntaxNode: {
                builder.Append("</tbody>");
                break;
            }

            case TableCellMdSyntaxNode: {
                builder.Append("</td>");
                break;
            }
            
            case TableHeadCellMdSyntaxNode: {
                builder.Append("</th>");
                break;
            }

            case TableHeadMdSyntaxNode: {
                builder.Append("</thead>");
                break;
            }

            case TableMdSyntaxNode: {
                builder.Append("</table>");
                break;
            }

            case TableRowMdSyntaxNode: {
                builder.Append("</tr>");
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
