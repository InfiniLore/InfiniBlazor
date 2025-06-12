// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Collections.Frozen;

namespace InfiniLore.InfiniBlazor.Markdown.SyntaxTreeConverters;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class StyledSyntaxTreeConverter : SimpleSyntaxTreeConverter {
    protected override FrozenDictionary<MarkdownElement, HtmlTag> MdElementLookup { get; } = new Dictionary<MarkdownElement, HtmlTag> {
        { MarkdownElement.Blockquote, HtmlTag.CreateWithClass("blockquote", "pl-4 border-l-4 border-(--border) my-4 bg-(--color-base-90) rounded-r text-(--color-base-20)") },
        { MarkdownElement.Bold, HtmlTag.Create("strong") },
        
        { MarkdownElement.CheckboxSelected, HtmlTag.CreateVoid("input type=\"checkbox\" disabled checked class=\"form-checkbox h-4 w-4 text-blue-600 rounded focus:ring-blue-500 -ml-6 mr-2 inline-block align-middle\"") },
        { MarkdownElement.CheckboxUnselected, HtmlTag.CreateVoid("input type=\"checkbox\" disabled class=\"form-checkbox h-4 w-4 text-gray-400 rounded focus:ring-blue-500 -ml-6 mr-2 inline-block align-middle\"") },
        { MarkdownElement.ListItem, HtmlTag.CreateWithClass("li", "my-2 [&:has(>input[type=checkbox])]:list-none [&>ul]:mt-2 [&>ol]:mt-2") },
        { MarkdownElement.ListOrdered, HtmlTag.CreateWithClass("ol", "list-decimal pl-8 my-4") },
        { MarkdownElement.ListUnordered, HtmlTag.CreateWithClass("ul", "list-disc pl-8 my-4") },

        { MarkdownElement.CodeBlock, new HtmlTag(
            "<div class=\"flex overflow-hidden min-h-0\"><div class=\"relative bg-(--color-base-70) text-(--color-base-20) text-sm border border-(--border) rounded p-4 overflow-auto w-full min-h-0 infini-scrollbar\"><pre class=\"whitespace-pre m-0 font-mono min-h-0\"><code", 
            "</code></pre></div></div>"
        )},

        { MarkdownElement.CodeInline, HtmlTag.CreateWithClass("code", "bg-(--color-base-70) text-(--color-base-20) rounded px-1 font-mono text-sm") },
        
        { MarkdownElement.H1, HtmlTag.CreateWithClass("h1", "text-6xl font-semibold mb-6") },
        { MarkdownElement.H2, HtmlTag.CreateWithClass("h2", "text-5xl font-semibold mb-5") },
        { MarkdownElement.H3, HtmlTag.CreateWithClass("h3", "text-4xl font-semibold mb-4") },
        { MarkdownElement.H4, HtmlTag.CreateWithClass("h4", "text-3xl font-semibold mb-3") },
        { MarkdownElement.H5, HtmlTag.CreateWithClass("h5", "text-2xl font-semibold mb-2") },
        { MarkdownElement.H6, HtmlTag.CreateWithClass("h6", "text-xl  font-semibold mb-1") },
        { MarkdownElement.HorizontalRule, HtmlTag.CreateVoid("hr class=\"my-8 border-t border-(--border)\"") },
        { MarkdownElement.Image, HtmlTag.CreateVoid("img class=\"max-w-full h-auto rounded-lg shadow-lg\"") },
        { MarkdownElement.Italic, HtmlTag.Create("em") },
        { MarkdownElement.Link, HtmlTag.CreateWithClass("a", "text-(--color-accent) hover:text-(--color-accent-light) hover:underline") },
        { MarkdownElement.Paragraph, HtmlTag.CreateWithClass("p", "my-4 leading-relaxed") },
        { MarkdownElement.Strikethrough, HtmlTag.Create("s") },
        { MarkdownElement.Subscript, HtmlTag.Create("sub") },
        { MarkdownElement.Superscript, HtmlTag.Create("sup") },

        { MarkdownElement.Table, HtmlTag.CreateWithClass("table", "w-full h-full table-auto shadow-sm rounded-2xl border border-(--border) overflow-hidden") },
        { MarkdownElement.TableBody, HtmlTag.Create("tbody") },
        { MarkdownElement.TableCell, HtmlTag.CreateWithClass("td", "p-4") },
        { MarkdownElement.TableHead, HtmlTag.CreateWithClass("thead", "sticky top-0 infini-bg-(--table-header-background) border-(--border) z-10 border-b") },
        { MarkdownElement.TableHeadCell, HtmlTag.CreateWithClass("th", "p-4") },
        { MarkdownElement.TableRow, HtmlTag.CreateWithClass("tr", "group hover:infini-bg-(--table-row-background-hover) transition h-4") },

        { MarkdownElement.Tag, new HtmlTag(
                "<span class=\"inline-block infini-bg-(--color-base-95) rounded-full px-2 font-semibold text-(--color-accent)\">#",
                "</span>" 
        ){
            HasClosingSmallerThan = true
        }},
        { MarkdownElement.Underline, HtmlTag.CreateWithStyle("span", "text-decoration: underline;") }
    }.ToFrozenDictionary();
}
