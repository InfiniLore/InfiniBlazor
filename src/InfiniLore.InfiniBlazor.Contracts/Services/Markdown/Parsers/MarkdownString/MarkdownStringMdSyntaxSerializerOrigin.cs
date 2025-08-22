// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[Flags]
public enum MarkdownStringMdSyntaxSerializerOrigin {
    Undefined = 0,

    Bold = 1 << 0,
    Italic = 1 << 1,
    Strike = 1 << 2,
    Code = 1 << 3,
    Link = 1 << 4,
    Underline = 1 << 5,
    Emote = 1 << 6,
    SuperScript = 1 << 7,
    SubScript = 1 << 8,

    // Special cases
    PreserveHtml = 1 << 29,
    Html = 1 << 30,
    NotSkipped = 1 << 31
}

public static class MdSyntaxSerializerOriginExtensions {
    public static bool HasFlagFast(this MarkdownStringMdSyntaxSerializerOrigin value, MarkdownStringMdSyntaxSerializerOrigin flag) {
        return (value & flag) != 0;
    }
}
