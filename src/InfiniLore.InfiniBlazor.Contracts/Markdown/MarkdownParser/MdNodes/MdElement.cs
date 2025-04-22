// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public enum MdElement : uint {
    Undefined = 0,
    Content = 1,
    HtmlContent = 2,

    Blockquote,
    Bold,
    CheckboxSelected,
    CheckboxUnselected,
    CodeBlock,
    CodeInline,
    H1,
    H2,
    H3,
    H4,
    H5,
    H6,
    HorizontalRule,
    Image,
    Italic,
    Link,
    ListItem,
    ListOrdered,
    ListUnordered,
    Paragraph,
    Strikethrough,
    Subscript,
    Superscript,
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableHeadCell,
    TableRow,
    Tag,
    Underline,
}
