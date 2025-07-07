// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;

namespace InfiniLore.InfiniBlazor.MarkdownParser.SyntaxTreeConverters.Converters;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class StyledMdSyntaxNodeConverter : SimpleMdSyntaxNodeConverter {
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override void HandleOpenTag(IMdSyntaxNode node) {
        switch (node) {
            case BlockQuoteMdSyntaxNode: {
                Sb.Append("<blockquote class=\"pl-4 border-l-4 border-(--border) my-4 bg-(--color-base-90) rounded-r text-(--color-base-20)\">");
                break;
            }

            case BoldMdSyntaxNode: {
                Sb.Append("<strong>");
                break;
            }

            case CodeBlockMdSyntaxNode {Language: var lang} when lang.IsNotNullOrWhiteSpace(): {
                Sb.Append("<div class=\"flex overflow-hidden min-h-0\">");
                Sb.Append("<div class=\"relative infini-bg-(--codeblock) text-(--color-base-20) text-sm border border-(--border) rounded p-4 overflow-auto w-full min-h-0 infini-scrollbar\">");
                Sb.Append("<pre class=\"whitespace-pre m-0 font-mono min-h-0\">");
                Sb.Append("<code class=\"language-");
                Sb.Append(lang);
                
                Sb.Append("\">");
                break;
            }
            
            case CodeBlockMdSyntaxNode: {
                Sb.Append("<div class=\"flex overflow-hidden min-h-0\">");
                Sb.Append("<div class=\"relative infini-bg-(--codeblock) text-(--color-base-20) text-sm border border-(--border) rounded p-4 overflow-auto w-full min-h-0 infini-scrollbar>");
                Sb.Append("<pre class=\"whitespace-pre m-0 font-mono min-h-0\">");
                Sb.Append("<code>");
                break;
            }

            case CodeInlineMdSyntaxNode: {
                Sb.Append("<code class=\"infini-bg-(--codeblock) text-(--color-base-20) rounded px-1 font-mono text-sm\">");
                break;
            }
            
            case HeadingMdSyntaxNode { Level: var level and > 0 }: {
                Sb.Append("<h");
                Sb.Append(level);
                Sb.Append(" class=\"text-");
                Sb.Append(level switch {
                    1 => "6xl",
                    2 => "5xl",
                    3 => "4xl",
                    4 => "3xl",
                    5 => "2xl",
                    _ => "xl"
                });
                Sb.Append(" font-semibold");
                Sb.Append(level switch {
                    1 => "mb-6",
                    2 => "mb-5",
                    3 => "mb-4",
                    4 => "mb-3",
                    5 => "mb-2",
                    _ => "mb-1"
                });
                Sb.Append("\">");
                break;
            }
            
            case HtmlSpanMdSyntaxNode { TagValue: var tagValue }: {
                Sb.Append(tagValue.AsSpan());
                break;
            }

            case ImageMdSyntaxNode {AltText: var altText, Href: var href} imgNode: {
                Sb.Append("<img class=\"inline-block rounded-lg shadow-lg h-full w-auto object-contain\" src=\"");
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
                    
                    else if (imgNode.Mod is { Fit: false, Size: var (width, height) } ) {
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
                Sb.Append("<a class=\"text-(--color-accent) hover:text-(--color-accent-light) hover:underline\" href=\"");
                Sb.Append(href);
                Sb.Append("\">");
                break;
            }

            case ListItemMdSyntaxNode {IsCheckable: true, IsChecked: var checkedState}: {
                Sb.Append("<li class=\"my-2 [&:has(>input[type=checkbox])]:list-none [&>ul]:mt-2 [&>ol]:mt-2\">");
                Sb.Append("<input type=\"checkbox\" disabled class=\"form-checkbox h-4 w-4 text-blue-600 rounded focus:ring-blue-500 -ml-6 mr-2 inline-block align-middle\"");
                if (checkedState) Sb.Append(" checked");
                Sb.Append('>');
                break;
            }

            case ListItemMdSyntaxNode: {
                Sb.Append("<li class=\"my-2 [&:has(>input[type=checkbox])]:list-none [&>ul]:mt-2 [&>ol]:mt-2\">");
                break;
            }

            case ListMdSyntaxNode { IsOrdered: true }: {
                Sb.Append("<ol class=\"list-decimal pl-8 my-4\">");
                break;
            }

            case ListMdSyntaxNode { IsOrdered: false }: {
                Sb.Append("<ul class=\"list-disc pl-8 my-4\">");
                break;
            }

            case ParagraphMdSyntaxNode: {
                Sb.Append("<p class=\"my-4 leading-relaxed\">");
                break;
            }

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
                Sb.Append("<td class=\"p-4\">");
                break;
            }
            
            case TableHeadCellMdSyntaxNode: {
                Sb.Append("<th class=\"p-4\">");
                break;
            }

            case TableHeadMdSyntaxNode: {
                Sb.Append("<thead class=\"sticky top-0 infini-bg-(--table-header) border-(--border) z-10 border-b\">");
                break;
            }

            case TableMdSyntaxNode: {
                Sb.Append("<div class=\"rounded-2xl border border-(--border) flex flex-col overflow-hidden\">");
                Sb.Append("<table class=\"w-full h-full table-auto shadow-sm rounded-2xl overflow-hidden\">");
                break;
            }

            case TableRowMdSyntaxNode { Parent: TableHeadMdSyntaxNode}: {
                Sb.Append("<tr>");
                break;
            }
            
            case TableRowMdSyntaxNode { Parent: not TableHeadMdSyntaxNode}: {
                Sb.Append("<tr class=\"group infini-bg-(--table-row) hover:infini-bg-(--table-row-hover) transition h-4\">");
                break;
            }

            case TagMdSyntaxNode: {
                Sb.Append("<span class=\"inline-block infini-bg-(--color-base-95) rounded-full px-2 font-semibold text-(--color-accent) md-tag\">");
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
    
    public override void HandleContent(IMdSyntaxNode node) {
        switch (node) {
            case CodeBlockMdSyntaxNode { ContentCode: var code }: {
                Sb.Append(code);
                break;
            }

            case CodeInlineMdSyntaxNode {ContentCode: var code }: {
                Sb.Append(code);
                break;
            }

            case ContentHtmlMdSyntaxNode { ContentHtml: var content }: {
                Sb.Append(content);
                break;
            }

            case ContentMdSyntaxNode { Content: var content }: {
                Sb.Append(content);
                break;
            }

            case EmoteMdSyntaxNode { ContentEmote: var content }: {
                Sb.Append(content);
                break;
            }

            case EscapedCharacterMdSyntaxNode { ContentChar: var contentChar }: {
                Sb.Append(contentChar);
                break;
            }

            case HorizontalRuleMdSyntaxNode: {
                Sb.Append("<hr class=\"my-8 border-t border-(--border)\"/>");
                break;
            }

            case TagMdSyntaxNode { ContentTag: var contentTag }: {
                Sb.Append('#');
                Sb.Append(contentTag);
                break;
            }

        }
    }
    
    public override void HandleCloseTag(IMdSyntaxNode node) {
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
                Sb.Append("</code></pre></div></div>");
                break;
            }

            case CodeInlineMdSyntaxNode: {
                Sb.Append("</code>");
                break;
            }

            case HeadingMdSyntaxNode { Level: var level and > 0 }: {
                Sb.Append("</h");
                Sb.Append(level);
                Sb.Append('>');
                break;
            }

            case HtmlSpanMdSyntaxNode: {
                Sb.Append("</span>");
                break;
            }

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
                Sb.Append("</table></div>");
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
}
