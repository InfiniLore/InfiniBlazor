// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.MdNodes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class MdNodeExtensions {
    public static IMdNode AddBlockquote(this IMdNode node) => node.AddChild(MdElement.Blockquote);
    public static IMdNode AddBold(this IMdNode node) => node.AddChild(MdElement.Bold);
    public static IMdNode AddCode(this IMdNode node) => node.AddChild(MdElement.Code);
    public static IMdNode AddH1(this IMdNode node) => node.AddChild(MdElement.H1);
    public static IMdNode AddH2(this IMdNode node) => node.AddChild(MdElement.H2);
    public static IMdNode AddH3(this IMdNode node) => node.AddChild(MdElement.H3);
    public static IMdNode AddH4(this IMdNode node) => node.AddChild(MdElement.H4);
    public static IMdNode AddH5(this IMdNode node) => node.AddChild(MdElement.H5);
    public static IMdNode AddH6(this IMdNode node) => node.AddChild(MdElement.H6);
    public static IMdNode AddHorizontalRule(this IMdNode node) => node.AddChild(MdElement.HorizontalRule);
    public static IMdNode AddImage(this IMdNode node) => node.AddChild(MdElement.Image);
    public static IMdNode AddInput(this IMdNode node) => node.AddChild(MdElement.Input);
    public static IMdNode AddItalic(this IMdNode node) => node.AddChild(MdElement.Italic);
    public static IMdNode AddLink(this IMdNode node) => node.AddChild(MdElement.Link);
    public static IMdNode AddListItem(this IMdNode node) => node.AddChild(MdElement.ListItem);
    public static IMdNode AddListOrdered(this IMdNode node) => node.AddChild(MdElement.ListOrdered);
    public static IMdNode AddListUnordered(this IMdNode node) => node.AddChild(MdElement.ListUnordered);
    public static IMdNode AddParagraph(this IMdNode node) => node.AddChild(MdElement.Paragraph);
    public static IMdNode AddPre(this IMdNode node) => node.AddChild(MdElement.Pre);
    public static IMdNode AddSpan(this IMdNode node) => node.AddChild(MdElement.Span);
    public static IMdNode AddStrikethrough(this IMdNode node) => node.AddChild(MdElement.Strikethrough);
    public static IMdNode AddSubscript(this IMdNode node) => node.AddChild(MdElement.Subscript);
    public static IMdNode AddSuperscript(this IMdNode node) => node.AddChild(MdElement.Superscript);
    public static IMdNode AddTable(this IMdNode node) => node.AddChild(MdElement.Table);
    public static IMdNode AddTableBody(this IMdNode node) => node.AddChild(MdElement.TableBody);
    public static IMdNode AddTableCell(this IMdNode node) => node.AddChild(MdElement.TableCell);
    public static IMdNode AddTableHead(this IMdNode node) => node.AddChild(MdElement.TableHead);
    public static IMdNode AddTableHeadCell(this IMdNode node) => node.AddChild(MdElement.TableHeadCell);
    public static IMdNode AddTableRow(this IMdNode node) => node.AddChild(MdElement.TableRow);
    public static IMdNode AddUnderline(this IMdNode node) => node.AddChild(MdElement.Underline);
}
