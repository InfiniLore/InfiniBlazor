// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;
using Microsoft.Extensions.ObjectPool;
using System.Text;

namespace InfiniLore.InfiniBlazor.MarkdownParser.SyntaxTreeConverters.Converters;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class SimpleMdSyntaxNodeConverter : IMdSyntaxNodeConverter, IResettable {
    public StringBuilder Sb { get; set; } = null!;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public virtual void HandleOpenTag(IMdSyntaxNode node) {
        switch (node) {
            case BlockQuoteMdSyntaxNode: {
                Sb.Append("<blockquote>");
                break;
            }

            case BoldMdSyntaxNode: {
                Sb.Append("<strong>");
                break;
            }

            case CodeBlockMdSyntaxNode {Language: var lang} when lang.IsNotNullOrWhiteSpace(): {
                Sb.Append("<pre><code class=\"language-");
                Sb.Append(lang.AsSpan());
                Sb.Append("\">");
                break;
            }
            case CodeBlockMdSyntaxNode: {
                Sb.Append("<pre><code>");
                break;
            }

            case CodeInlineMdSyntaxNode: {
                Sb.Append("<code>");
                break;
            }
            
            case ContentHtmlMdSyntaxNode:break;
            case ContentMdSyntaxNode:
            case EmoteMdSyntaxNode:break;
            case EscapedCharacterMdSyntaxNode:break;

            case HeadingMdSyntaxNode { Level: var level and > 0 }: {
                Sb.Append("<h");
                Sb.Append(level);
                Sb.Append('>');
                break;
            }
            
            case HtmlSpanMdSyntaxNode { TagValue: var tagValue }: {
                Sb.Append(tagValue.AsSpan());
                break;
            }
            
            case HorizontalRuleMdSyntaxNode:break;

            case ImageMdSyntaxNode {AltText: var altText, Href: var href} imgNode: {
                Sb.Append("<img class\"inline-block\" src=\"");
                Sb.Append(href.AsSpan());
                Sb.Append('"');
                if (altText.IsNotNullOrWhiteSpace()) {
                    Sb.Append(" alt=\"");
                    Sb.Append(altText.AsSpan());
                    Sb.Append('"');
                }

                if (imgNode.ContainsMods) {
                    if (imgNode.Mod.Title.IsNotNullOrWhiteSpace()) {
                        Sb.Append(" title=\"");
                        Sb.Append(imgNode.Mod.Title.AsSpan());
                        Sb.Append('"');
                    }
                    
                    if (imgNode.Mod.Fit) {
                        Sb.Append(" style=\"width:auto;height:2em;vertical-align:baseline;object-fit:contain;\"");
                    }
                    
                    else if (imgNode.Mod is { Fit: false, Size: var (width, height) }) {
                        Sb.Append(" style=\"width: ");
                        Sb.Append(width);
                        Sb.Append("px; height: ");
                        Sb.Append(height);
                        Sb.Append("px;\"");
                    }
                }
                
                Sb.Append("/>");
                break;
            }

            case ItalicMdSyntaxNode: {
                Sb.Append("<em>");
                break;
            }

            case LinkMdSyntaxNode {Href: var href}: {
                Sb.Append("<a href=\"");
                Sb.Append(href.AsSpan());
                Sb.Append("\">");
                break;
            }

            case ListItemMdSyntaxNode {IsCheckable: true, IsChecked: var checkedState}: {
                Sb.Append("<li>");
                Sb.Append("<input type=\"checkbox\" disabled");
                if (checkedState) Sb.Append(" checked");
                Sb.Append("/>");
                break;
            }

            case ListItemMdSyntaxNode: {
                Sb.Append("<li>");
                break;
            }

            case ListMdSyntaxNode { IsOrdered: true }: {
                Sb.Append("<ol>");
                break;
            }

            case ListMdSyntaxNode { IsOrdered: false }: {
                Sb.Append("<ul>");
                break;
            }

            case ParagraphMdSyntaxNode: {
                Sb.Append("<p>");
                break;
            }
            case RootMdSyntaxNode:break;

            case StrikeMdSyntaxNode: {
                Sb.Append("<s>");
                break;
            }

            case SubScriptMdSyntaxNode: {
                Sb.Append("<sub>");
                break;
            }

            case SuperScriptMdSyntaxNode: {
                Sb.Append("<sup>");
                break;
            }

            case TableBodyMdSyntaxNode: {
                Sb.Append("<tbody>");
                break;
            }

            case TableCellMdSyntaxNode: {
                Sb.Append("<td>");
                break;
            }
            
            case TableHeadCellMdSyntaxNode: {
                Sb.Append("<th>");
                break;
            }

            case TableHeadMdSyntaxNode: {
                Sb.Append("<thead>");
                break;
            }

            case TableMdSyntaxNode: {
                Sb.Append("<table>");
                break;
            }

            case TableRowMdSyntaxNode: {
                Sb.Append("<tr>");
                break;
            }

            case TagMdSyntaxNode: {
                Sb.Append("<span class=\"md-tag\">");
                break;
            }

            case UnderlineMdSyntaxNode: {
                Sb.Append("<u>");
                break;
            }

            case CalloutMdSyntaxNode {Mod.Style: var style}: {
                Sb.Append("<div class=\"md-callout md-callout-");
                Sb.Append(style);
                Sb.Append("\">");
                break;
            }

            case CalloutTitleMdSyntaxNode: {
                Sb.Append("<div class=\"md-callout-title\">");
                break;
            }

            case CalloutBodyMdSyntaxNode: {
                Sb.Append("<div class=\"md-callout-body\">");
                break;
            }
        }
    }
    
    public virtual void HandleContent(IMdSyntaxNode node) {
        switch (node) {

            case CodeBlockMdSyntaxNode { ContentCode: var code }: {
                Sb.Append(code.AsSpan());
                break;
            }

            case CodeInlineMdSyntaxNode {ContentCode: var code }: {
                Sb.Append(code.AsSpan());
                break;
            }

            case ContentHtmlMdSyntaxNode { ContentHtml: var content }: {
                Sb.Append(content.AsSpan());
                break;
            }

            case ContentMdSyntaxNode { Content: var content }: {
                Sb.Append(content.AsSpan());
                break;
            }

            case EmoteMdSyntaxNode { ContentEmote: var content }: {
                Sb.Append(content.AsSpan());
                break;
            }

            case EscapedCharacterMdSyntaxNode { ContentChar: var contentChar }: {
                Sb.Append(contentChar);
                break;
            }

            case HorizontalRuleMdSyntaxNode: {
                Sb.Append("<hr>");
                break;
            }

            case TagMdSyntaxNode { ContentTag: var contentTag }: {
                Sb.Append('#');
                Sb.Append(contentTag.AsSpan());
                break;
            }
        }
    }
    
    public virtual void HandleCloseTag(IMdSyntaxNode node) {
        switch (node) {
            case BlockQuoteMdSyntaxNode: {
                Sb.Append("</blockquote>");
                break;
            }

            case BoldMdSyntaxNode: {
                Sb.Append("</strong>");
                break;
            }

            case CodeBlockMdSyntaxNode: {
                Sb.Append("</code></pre>");
                break;
            }

            case CodeInlineMdSyntaxNode: {
                Sb.Append("</code>");
                break;
            }
            
            case ContentHtmlMdSyntaxNode:
            case ContentMdSyntaxNode:
            case EmoteMdSyntaxNode:
            case EscapedCharacterMdSyntaxNode:break;

            case HeadingMdSyntaxNode {Level: var level and > 0 }: {
                Sb.Append("</h");
                Sb.Append(level);
                Sb.Append('>');
                break;
            }
            
            case HorizontalRuleMdSyntaxNode: break;

            case HtmlSpanMdSyntaxNode: {
                Sb.Append("</span>");
                break;
            }
            
            case ImageMdSyntaxNode:break;

            case ItalicMdSyntaxNode: {
                Sb.Append("</em>");
                break;
            }

            case LinkMdSyntaxNode: {
                Sb.Append("</a>");
                break;
            }

            case ListItemMdSyntaxNode: {
                Sb.Append("</li>");
                break;
            }

            case ListMdSyntaxNode { IsOrdered: false }: {
                Sb.Append("</ul>");
                break;
            }

            case ListMdSyntaxNode { IsOrdered: true }: {
                Sb.Append("</ol>");
                break;
            }

            case ParagraphMdSyntaxNode: {
                Sb.Append("</p>");
                break;
            }
            case RootMdSyntaxNode:break;

            case StrikeMdSyntaxNode: {
                Sb.Append("</s>");
                break;
            }

            case SubScriptMdSyntaxNode: {
                Sb.Append("</sub>");
                break;
            }

            case SuperScriptMdSyntaxNode: {
                Sb.Append("</sup>");
                break;
            }

            case TableBodyMdSyntaxNode: {
                Sb.Append("</tbody>");
                break;
            }

            case TableCellMdSyntaxNode: {
                Sb.Append("</td>");
                break;
            }
            
            case TableHeadCellMdSyntaxNode: {
                Sb.Append("</th>");
                break;
            }

            case TableHeadMdSyntaxNode: {
                Sb.Append("</thead>");
                break;
            }

            case TableMdSyntaxNode: {
                Sb.Append("</table>");
                break;
            }

            case TableRowMdSyntaxNode: {
                Sb.Append("</tr>");
                break;
            }

            case TagMdSyntaxNode: {
                Sb.Append("</span>");
                break;
            }

            case UnderlineMdSyntaxNode: {
                Sb.Append("</u>");
                break;
            }

            case CalloutMdSyntaxNode:
            case CalloutTitleMdSyntaxNode:
            case CalloutBodyMdSyntaxNode: {
                Sb.Append("</div>");
                break;
            }
        }
    }
    
    public bool TryReset() {
        Sb.Clear();
        Sb = null!;
        return true;
    }
}
