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
[InjectableSingleton<IHtmlStringMdSyntaxNodeVisitor>("styled")]
public sealed class StyledMdSyntaxNodeVisitor(IEmoteProvider emoteProvider, ILucideService lucideService) : SimpleMdSyntaxNodeVisitor(emoteProvider, lucideService) {
    private readonly ILucideService _lucideService = lucideService;
    private readonly IEmoteProvider _emoteProvider = emoteProvider;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region Lucide Helpers
    private static void AddLucideIconToBuilder(string svgData, StringBuilder builder) {
        builder.Append("<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" stroke=\"currentColor\" fill=\"none\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\">");
        builder.Append(svgData);
        builder.Append("</svg>");
    }
    #endregion
    
    public override void HandleOpenTag(IMdSyntaxNode node, StringBuilder builder) {
        switch (node) {
            case BlockQuoteMdSyntaxNode: {
                builder.Append("<blockquote class=\"pl-4 border-l-4 border-(--border) my-4 bg-(--color-base-90) rounded-r text-(--color-base-20)\">");
                break;
            }

            case BoldMdSyntaxNode: {
                builder.Append("<strong>");
                break;
            }

            case CodeBlockMdSyntaxNode {Language: var lang} when lang.IsNotNullOrWhiteSpace(): {
                builder.Append("<div class=\"flex overflow-hidden min-h-0\">");
                builder.Append("<div class=\"relative infini-bg-(--codeblock) text-(--color-base-20) text-sm border border-(--border) rounded p-4 overflow-auto w-full min-h-0 infini-scrollbar\">");
                builder.Append("<pre class=\"whitespace-pre m-0 font-mono min-h-0\">");
                builder.Append("<code class=\"language-");
                builder.Append(lang);
                
                builder.Append("\">");
                break;
            }
            
            case CodeBlockMdSyntaxNode: {
                builder.Append("<div class=\"flex overflow-hidden min-h-0\">");
                builder.Append("<div class=\"relative infini-bg-(--codeblock) text-(--color-base-20) text-sm border border-(--border) rounded p-4 overflow-auto w-full min-h-0 infini-scrollbar>");
                builder.Append("<pre class=\"whitespace-pre m-0 font-mono min-h-0\">");
                builder.Append("<code>");
                break;
            }

            case CodeInlineMdSyntaxNode: {
                builder.Append("<code class=\"infini-bg-(--codeblock) text-(--color-base-20) rounded px-1 font-mono text-sm\">");
                break;
            }
            
            case HeadingMdSyntaxNode { Level: var level and > 0 }: {
                builder.Append("<h");
                builder.Append(level);
                builder.Append(" class=\"text-");
                builder.Append(level switch {
                    1 => "6xl",
                    2 => "5xl",
                    3 => "4xl",
                    4 => "3xl",
                    5 => "2xl",
                    _ => "xl"
                });
                builder.Append(" font-semibold ");
                builder.Append(level switch {
                    1 => "mb-6",
                    2 => "mb-5",
                    3 => "mb-4",
                    4 => "mb-3",
                    5 => "mb-2",
                    _ => "mb-1"
                });
                builder.Append("\">");
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

            case ImageMdSyntaxNode {NormalizedAltText: var altText, Href: var href} imgNode: {
                builder.Append("<img class=\"inline-block rounded-lg shadow-lg h-full w-auto object-contain\" src=\"");
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
                        builder.Append(" style=\"width:auto;height:1em;vertical-align:text-top;object-fit:contain;transform:translate(0,0.15em);\"");
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

            case WikiLinkMdSyntaxNode {Content: var href}: {
                builder.Append("<a class=\"text-(--color-accent) hover:text-(--color-accent-light) hover:underline\" href=\"");
                builder.Append(href);
                builder.Append("\">");
                break;
            }

            case LinkMdSyntaxNode {Href: var href}: {
                builder.Append("<a class=\"text-(--color-accent) hover:text-(--color-accent-light) hover:underline\" href=\"");
                builder.Append(href);
                builder.Append("\">");
                break;
            }

            case ListItemMdSyntaxNode {IsCheckable: true, IsChecked: var checkedState}: {
                builder.Append("<li class=\"my-2 [&:has(>input[type=checkbox])]:list-none [&>ul]:mt-2 [&>ol]:mt-2\">");
                builder.Append("<input type=\"checkbox\" disabled class=\"form-checkbox h-4 w-4 text-blue-600 rounded focus:ring-blue-500 -ml-6 mr-2 inline-block align-middle\"");
                if (checkedState) builder.Append(" checked");
                builder.Append('>');
                break;
            }

            case ListItemMdSyntaxNode: {
                builder.Append("<li class=\"my-2 [&:has(>input[type=checkbox])]:list-none [&>ul]:mt-2 [&>ol]:mt-2\">");
                break;
            }

            case ListOrderedMdSyntaxNode: {
                builder.Append("<ol class=\"list-decimal pl-8 my-4\">");
                break;
            }

            case ListUnOrderedMdSyntaxNode: {
                builder.Append("<ul class=\"list-disc pl-8 my-4\">");
                break;
            }

            case ParagraphMdSyntaxNode { Parent: CalloutBodyMdSyntaxNode }: {
                builder.Append("<p class=\"my-2 leading-relaxed\">");
                break;
            }
            
            case ParagraphMdSyntaxNode: {
                builder.Append("<p class=\"my-4 leading-relaxed\">");
                break;
            }

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
                builder.Append("<div class=\"rounded-2xl border border-(--border) flex flex-col overflow-hidden\">");
                builder.Append("<table class=\"w-full h-full table-auto shadow-sm rounded-2xl overflow-hidden\">");
                break;
            }

            case TableRowMdSyntaxNode when node.Parent is TableMdSyntaxNode tableNode && Equals(tableNode.GetChildAt(0), node): {
                builder.Append("<thead class=\"sticky top-0 infini-bg-(--table-header) border-(--border) z-10 border-b\"><tr>");
                break;
            }

            case TableRowMdSyntaxNode when node.Parent is TableMdSyntaxNode tableNode && Equals(tableNode.GetChildAt(1), node): {
                builder.Append("<tbody><tr class=\"group infini-bg-(--table-row) hover:infini-bg-(--table-row-hover) transition h-4\">");
                break;
            }

            case TableRowMdSyntaxNode: {
                builder.Append("<tr class=\"group infini-bg-(--table-row) hover:infini-bg-(--table-row-hover) transition h-4\">");
                break;
            }

            case TableCellMdSyntaxNode when node.Parent is TableRowMdSyntaxNode { Parent: TableMdSyntaxNode tableNode } rowNode && Equals(tableNode.GetChildAt(0), rowNode): {
                builder.Append("<th class=\"p-4\">");
                break;
            }

            case TableCellMdSyntaxNode: {
                builder.Append("<td class=\"p-4\">");
                break;
            }

            case TagMdSyntaxNode {Content: var contentTag}: {
                builder.Append("<span class=\"inline-block infini-bg-(--color-base-95) rounded-xl px-1.5 text-(--color-accent) md-tag md-tag-");
                builder.Append(contentTag.Replace('/', '-'));
                builder.Append("\">");
                break;
            }

            case UnderlineMdSyntaxNode: {
                builder.Append("<u>");
                break;
            }

            case CalloutMdSyntaxNode {CalloutType: {} calloutType}: {
                string typeClass = calloutType.ToLowerInvariant() switch {
                    "note" => "border-neutral-400 bg-zinc-900",
                    "warning" => "border-orange-500 bg-amber-950",
                    "tip" => "border-violet-500 bg-violet-950",
                    "danger" => "border-red-500 bg-red-950",
                    "info" => "border-blue-500 bg-blue-950",
                    "success" => "border-green-500 bg-green-950",
                    _ => "border-(--border) bg-(--color-base-90)"
                };

                builder.Append("<div class=\"flex flex-col min-w-full min-h-lh pl-3 border-l-4 ");
                builder.Append(typeClass);
                builder.Append(" my-4 rounded-r-lg text-(--color-base-20)\"");
                builder.Append(calloutType);
                builder.Append("\">");
                break;
            }

            case CalloutTitleMdSyntaxNode {Parent: CalloutMdSyntaxNode {CalloutType: {} calloutType} parentNode}: {
                string calloutName = calloutType.ToLowerInvariant();
                string calloutClass = calloutName switch {
                    "note" => "text-neutral-300",
                    "warning" => "text-orange-300",
                    "tip" => "text-violet-300",
                    "danger" => "text-red-300",
                    "info" => "text-blue-300",
                    "success" => "text-green-300",
                    _ => "text-white"
                };

                string? svgData = null;
                if (parentNode.Modifier is {} modifiers && modifiers.TryGetIconName(out string? name)) {
                    svgData = _lucideService.GetIconAsString(name);
                }
                if (svgData.IsNullOrWhiteSpace()) {
                    name = calloutName switch {
                        "note" => LucideNames.Quote,
                        "warning" => LucideNames.TriangleAlert,
                        "tip" => LucideNames.Lightbulb,
                        "danger" => LucideNames.Flame,
                        "info" => LucideNames.Info,
                        "success" => LucideNames.CircleCheck,
                        _ => string.Empty
                    };
                    svgData = _lucideService.GetIconAsString(name);
                }

                builder.Append("<div class=\"flex flex-row gap-2 pt-2 ");
                builder.Append(calloutClass);
                builder.Append("\">");
                
                AddLucideIconToBuilder(svgData, builder);
                
                builder.Append("<span>");
                break;
            }

            case CalloutBodyMdSyntaxNode: {
                builder.Append("<div>");
                break;
            }
        }
    }
    
    public override void HandleContent(IMdSyntaxNode node, StringBuilder builder) {
        switch (node) {
            case CodeBlockMdSyntaxNode { Content: var code }: {
                builder.Append(code);
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
                builder.Append(content);
                break;
            }

            case TextMdSyntaxNode { Content: var content }: {
                builder.Append(content);
                break;
            }

            case EmoteMdSyntaxNode emoteNode: {
                IEmoteEntry? entry = _emoteProvider.GetEntryAsync(emoteNode.EmoteKey);
                switch (entry?.ContentType) {
                    case EmoteContentType.Emoji: {
                        builder.Append(entry.Data);
                        break;
                    }

                    case EmoteContentType.LucideIconName when entry.Data is not null: {
                        string lucideIconSvg = _lucideService.GetIconAsString(entry.Data);
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
                builder.Append("<hr class=\"my-8 border-t border-(--border)\"/>");
                break;
            }

            case TagMdSyntaxNode { Content: var contentTag }: {
                builder.Append('#');
                builder.Append(contentTag);
                break;
            }
            
            case UserMdSyntaxNode { Content: var userName }: {
                builder.Append('@');
                builder.Append(userName);
                break;
            }
            
            case TemplateMdSyntaxNode { Content: var variable }: {
                builder.Append(variable.AsSpan());
                break;
            }

        }
    }
    
    public override void HandleCloseTag(IMdSyntaxNode node, StringBuilder builder) {
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
                builder.Append("</code></pre></div></div>");
                break;
            }

            case CodeInlineMdSyntaxNode: {
                builder.Append("</code>");
                break;
            }

            case HeadingMdSyntaxNode { Level: var level and > 0 }: {
                builder.Append("</h");
                builder.Append(level);
                builder.Append('>');
                break;
            }

            case HtmlSpanMdSyntaxNode: {
                builder.Append("</span>");
                break;
            }

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
                builder.Append("</tbody></table></div>");
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

            case CalloutTitleMdSyntaxNode: {
                builder.Append("</span>");
                builder.Append("</div>");
                break;
            }

            case CalloutMdSyntaxNode:
            case CalloutBodyMdSyntaxNode: {
                builder.Append("</div>");
                break;
            }
        }
    }
}
