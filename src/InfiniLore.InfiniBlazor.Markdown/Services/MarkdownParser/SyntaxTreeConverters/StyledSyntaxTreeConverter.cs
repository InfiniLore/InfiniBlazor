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
        { MarkdownElement.Blockquote, HtmlTag.CreateWithClass("blockquote", "pl-4 border-l-4 border-gray-300 my-4 italic text-gray-700") },
        { MarkdownElement.Bold, HtmlTag.Create("strong") },
        { MarkdownElement.CheckboxSelected, HtmlTag.CreateVoid("input type=\"checkbox\" disabled checked class=\"form-checkbox h-4 w-4 text-blue-600 rounded focus:ring-blue-500\"") },
        { MarkdownElement.CheckboxUnselected, HtmlTag.CreateVoid("input type=\"checkbox\" disabled class=\"form-checkbox h-4 w-4 text-gray-400 rounded focus:ring-blue-500\"") },
        { MarkdownElement.CodeBlock, new HtmlTag("<pre class=\"bg-gray-100 rounded-lg p-4 my-4\"><code class=\"block text-sm font-mono\"", "</code></pre>") },
        { MarkdownElement.CodeInline, HtmlTag.CreateWithClass("code", "bg-gray-100 rounded px-1 font-mono text-sm") },
        { MarkdownElement.H1, HtmlTag.CreateWithClass("h1", "text-4xl font-bold mb-6 mt-8") },
        { MarkdownElement.H2, HtmlTag.CreateWithClass("h2", "text-3xl font-bold mb-5 mt-7") },
        { MarkdownElement.H3, HtmlTag.CreateWithClass("h3", "text-2xl font-bold mb-4 mt-6") },
        { MarkdownElement.H4, HtmlTag.CreateWithClass("h4", "text-xl font-bold mb-3 mt-5") },
        { MarkdownElement.H5, HtmlTag.CreateWithClass("h5", "text-lg font-bold mb-2 mt-4") },
        { MarkdownElement.H6, HtmlTag.CreateWithClass("h6", "text-base font-bold mb-2 mt-4") },
        { MarkdownElement.HorizontalRule, HtmlTag.CreateVoid("hr class=\"my-8 border-t border-gray-300\"") },
        { MarkdownElement.Image, HtmlTag.CreateVoid("img class=\"max-w-full h-auto rounded-lg shadow-lg\"") },
        { MarkdownElement.Italic, HtmlTag.Create("em") },
        { MarkdownElement.Link, HtmlTag.CreateWithClass("a", "text-blue-600 hover:text-blue-800 hover:underline") },
        { MarkdownElement.ListItem, HtmlTag.Create("li") },
        { MarkdownElement.ListOrdered, HtmlTag.CreateWithClass("ol", "list-decimal list-inside my-4 space-y-2") },
        { MarkdownElement.ListUnordered, HtmlTag.CreateWithClass("ul", "list-disc list-inside my-4 space-y-2") },
        { MarkdownElement.Paragraph, HtmlTag.CreateWithClass("p", "my-4 leading-relaxed") },
        { MarkdownElement.Strikethrough, HtmlTag.Create("s") },
        { MarkdownElement.Subscript, HtmlTag.Create("sub") },
        { MarkdownElement.Superscript, HtmlTag.Create("sup") },
        { MarkdownElement.Table, HtmlTag.CreateWithClass("table", "min-w-full border-collapse my-4") },
        { MarkdownElement.TableBody, HtmlTag.Create("tbody") },
        { MarkdownElement.TableCell, HtmlTag.CreateWithClass("td", "border px-4 py-2") },
        { MarkdownElement.TableHead, HtmlTag.Create("thead") },
        { MarkdownElement.TableHeadCell, HtmlTag.CreateWithClass("th", "bg-gray-100 border px-4 py-2 font-bold") },
        { MarkdownElement.TableRow, HtmlTag.CreateWithClass("tr", "hover:bg-gray-50") },
        { MarkdownElement.Tag, HtmlTag.CreateWithClass("span", "inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2") },
        { MarkdownElement.Underline, HtmlTag.CreateWithStyle("span", "text-decoration: underline;") }
    }.ToFrozenDictionary();
}
