// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.MdNodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class MdNodeExtensions {
    private static IMdNode AddNodeWithContent(IMdNode node, MdElement element, string? content = null) {
        IMdNode newNode = node.AddChildNode(element);
        if (content != null) newNode.WithContent(content);
        return newNode;
    }

    public static IMdNode AddBlockquote(this IMdNode node, string? content = null) => AddNodeWithContent(node, MdElement.Blockquote, content);
    public static IMdNode AddBold(this IMdNode node, string? content = null) => AddNodeWithContent(node, MdElement.Bold, content);
    public static IMdNode AddCodeInline(this IMdNode node, string? content = null) => AddNodeWithContent(node, MdElement.CodeInline, content);
    public static IMdNode AddCodeBlock(this IMdNode node, string? content = null) => AddNodeWithContent(node, MdElement.CodeBlock, content);
    public static IMdNode AddH1(this IMdNode node, string? content = null) => AddNodeWithContent(node, MdElement.H1, content);
    public static IMdNode AddH2(this IMdNode node, string? content = null) => AddNodeWithContent(node, MdElement.H2, content);
    public static IMdNode AddH3(this IMdNode node, string? content = null) => AddNodeWithContent(node, MdElement.H3, content);
    public static IMdNode AddH4(this IMdNode node, string? content = null) => AddNodeWithContent(node, MdElement.H4, content);
    public static IMdNode AddH5(this IMdNode node, string? content = null) => AddNodeWithContent(node, MdElement.H5, content);
    public static IMdNode AddH6(this IMdNode node, string? content = null) => AddNodeWithContent(node, MdElement.H6, content);
    public static IMdNode AddHorizontalRule(this IMdNode node) => AddNodeWithContent(node, MdElement.HorizontalRule);
    public static IMdNode AddImage(this IMdNode node, string? content = null) => AddNodeWithContent(node, MdElement.Image, content);
    public static IMdNode AddItalic(this IMdNode node, string? content = null) => AddNodeWithContent(node, MdElement.Italic, content);
    public static IMdNode AddLink(this IMdNode node, string? content = null) => AddNodeWithContent(node, MdElement.Link, content);
    public static IMdNode AddListItem(this IMdNode node, string? content = null) => AddNodeWithContent(node, MdElement.ListItem, content);
    public static IMdNode AddListOrdered(this IMdNode node, string? content = null) => AddNodeWithContent(node, MdElement.ListOrdered, content);
    public static IMdNode AddListUnordered(this IMdNode node, string? content = null) => AddNodeWithContent(node, MdElement.ListUnordered, content);
    public static IMdNode AddParagraph(this IMdNode node, string? content = null) => AddNodeWithContent(node, MdElement.Paragraph, content);
    public static IMdNode AddTag(this IMdNode node, string? content = null) => AddNodeWithContent(node, MdElement.Tag, content);
    public static IMdNode AddStrikethrough(this IMdNode node, string? content = null) => AddNodeWithContent(node, MdElement.Strikethrough, content);
    public static IMdNode AddSubscript(this IMdNode node, string? content = null) => AddNodeWithContent(node, MdElement.Subscript, content);
    public static IMdNode AddSuperscript(this IMdNode node, string? content = null) => AddNodeWithContent(node, MdElement.Superscript, content);
    public static IMdNode AddTable(this IMdNode node, string? content = null) => AddNodeWithContent(node, MdElement.Table, content);
    public static IMdNode AddTableBody(this IMdNode node, string? content = null) => AddNodeWithContent(node, MdElement.TableBody, content);
    public static IMdNode AddTableCell(this IMdNode node, string? content = null) => AddNodeWithContent(node, MdElement.TableCell, content);
    public static IMdNode AddTableHead(this IMdNode node, string? content = null) => AddNodeWithContent(node, MdElement.TableHead, content);
    public static IMdNode AddTableHeadCell(this IMdNode node, string? content = null) => AddNodeWithContent(node, MdElement.TableHeadCell, content);
    public static IMdNode AddTableRow(this IMdNode node, string? content = null) => AddNodeWithContent(node, MdElement.TableRow, content);
    public static IMdNode AddUnderline(this IMdNode node, string? content = null) => AddNodeWithContent(node, MdElement.Underline, content);
    public static IMdNode AddCheckboxSelected(this IMdNode node, string? content = null) => AddNodeWithContent(node, MdElement.CheckboxSelected, content);
    public static IMdNode AddCheckboxUnselected(this IMdNode node, string? content = null) => AddNodeWithContent(node, MdElement.CheckboxUnselected, content);
}
