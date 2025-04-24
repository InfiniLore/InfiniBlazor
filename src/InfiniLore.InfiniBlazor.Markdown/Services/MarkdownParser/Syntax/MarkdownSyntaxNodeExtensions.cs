// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class MarkdownSyntaxNodeExtensions {
    private static IMarkdownSyntaxNode AddNodeWithContent(IMarkdownSyntaxNode node, MarkdownElement element, string? content = null) {
        IMarkdownSyntaxNode newNode = node.AddChildNode(element);
        if (content != null) newNode.WithContent(content);
        return newNode;
    }

    public static IMarkdownSyntaxNode AddBlockquote(this IMarkdownSyntaxNode node, string? content = null) => AddNodeWithContent(node, MarkdownElement.Blockquote, content);
    public static IMarkdownSyntaxNode AddBold(this IMarkdownSyntaxNode node, string? content = null) => AddNodeWithContent(node, MarkdownElement.Bold, content);
    public static IMarkdownSyntaxNode AddCodeInline(this IMarkdownSyntaxNode node, string? content = null) => AddNodeWithContent(node, MarkdownElement.CodeInline, content);
    public static IMarkdownSyntaxNode AddCodeBlock(this IMarkdownSyntaxNode node, string? content = null) => AddNodeWithContent(node, MarkdownElement.CodeBlock, content);
    public static IMarkdownSyntaxNode AddH1(this IMarkdownSyntaxNode node, string? content = null) => AddNodeWithContent(node, MarkdownElement.H1, content);
    public static IMarkdownSyntaxNode AddH2(this IMarkdownSyntaxNode node, string? content = null) => AddNodeWithContent(node, MarkdownElement.H2, content);
    public static IMarkdownSyntaxNode AddH3(this IMarkdownSyntaxNode node, string? content = null) => AddNodeWithContent(node, MarkdownElement.H3, content);
    public static IMarkdownSyntaxNode AddH4(this IMarkdownSyntaxNode node, string? content = null) => AddNodeWithContent(node, MarkdownElement.H4, content);
    public static IMarkdownSyntaxNode AddH5(this IMarkdownSyntaxNode node, string? content = null) => AddNodeWithContent(node, MarkdownElement.H5, content);
    public static IMarkdownSyntaxNode AddH6(this IMarkdownSyntaxNode node, string? content = null) => AddNodeWithContent(node, MarkdownElement.H6, content);
    public static IMarkdownSyntaxNode AddHorizontalRule(this IMarkdownSyntaxNode node) => AddNodeWithContent(node, MarkdownElement.HorizontalRule);
    public static IMarkdownSyntaxNode AddImage(this IMarkdownSyntaxNode node, string? content = null) => AddNodeWithContent(node, MarkdownElement.Image, content);
    public static IMarkdownSyntaxNode AddItalic(this IMarkdownSyntaxNode node, string? content = null) => AddNodeWithContent(node, MarkdownElement.Italic, content);
    public static IMarkdownSyntaxNode AddLink(this IMarkdownSyntaxNode node, string? content = null) => AddNodeWithContent(node, MarkdownElement.Link, content);
    public static IMarkdownSyntaxNode AddListItem(this IMarkdownSyntaxNode node, string? content = null) => AddNodeWithContent(node, MarkdownElement.ListItem, content);
    public static IMarkdownSyntaxNode AddListOrdered(this IMarkdownSyntaxNode node, string? content = null) => AddNodeWithContent(node, MarkdownElement.ListOrdered, content);
    public static IMarkdownSyntaxNode AddListUnordered(this IMarkdownSyntaxNode node, string? content = null) => AddNodeWithContent(node, MarkdownElement.ListUnordered, content);
    public static IMarkdownSyntaxNode AddParagraph(this IMarkdownSyntaxNode node, string? content = null) => AddNodeWithContent(node, MarkdownElement.Paragraph, content);
    public static IMarkdownSyntaxNode AddTag(this IMarkdownSyntaxNode node, string? content = null) => AddNodeWithContent(node, MarkdownElement.Tag, content);
    public static IMarkdownSyntaxNode AddStrikethrough(this IMarkdownSyntaxNode node, string? content = null) => AddNodeWithContent(node, MarkdownElement.Strikethrough, content);
    public static IMarkdownSyntaxNode AddSubscript(this IMarkdownSyntaxNode node, string? content = null) => AddNodeWithContent(node, MarkdownElement.Subscript, content);
    public static IMarkdownSyntaxNode AddSuperscript(this IMarkdownSyntaxNode node, string? content = null) => AddNodeWithContent(node, MarkdownElement.Superscript, content);
    public static IMarkdownSyntaxNode AddTable(this IMarkdownSyntaxNode node, string? content = null) => AddNodeWithContent(node, MarkdownElement.Table, content);
    public static IMarkdownSyntaxNode AddTableBody(this IMarkdownSyntaxNode node, string? content = null) => AddNodeWithContent(node, MarkdownElement.TableBody, content);
    public static IMarkdownSyntaxNode AddTableCell(this IMarkdownSyntaxNode node, string? content = null) => AddNodeWithContent(node, MarkdownElement.TableCell, content);
    public static IMarkdownSyntaxNode AddTableHead(this IMarkdownSyntaxNode node, string? content = null) => AddNodeWithContent(node, MarkdownElement.TableHead, content);
    public static IMarkdownSyntaxNode AddTableHeadCell(this IMarkdownSyntaxNode node, string? content = null) => AddNodeWithContent(node, MarkdownElement.TableHeadCell, content);
    public static IMarkdownSyntaxNode AddTableRow(this IMarkdownSyntaxNode node, string? content = null) => AddNodeWithContent(node, MarkdownElement.TableRow, content);
    public static IMarkdownSyntaxNode AddUnderline(this IMarkdownSyntaxNode node, string? content = null) => AddNodeWithContent(node, MarkdownElement.Underline, content);
    public static IMarkdownSyntaxNode AddCheckboxSelected(this IMarkdownSyntaxNode node, string? content = null) => AddNodeWithContent(node, MarkdownElement.CheckboxSelected, content);
    public static IMarkdownSyntaxNode AddCheckboxUnselected(this IMarkdownSyntaxNode node, string? content = null) => AddNodeWithContent(node, MarkdownElement.CheckboxUnselected, content);
}
