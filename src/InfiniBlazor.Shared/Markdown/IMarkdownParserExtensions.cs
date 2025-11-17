// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text.Json;
using System.Xml.Linq;

namespace InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class IMarkdownParserExtensions {
    #region Markdown
    public static MarkdownParserContext FromMarkdown(this IMarkdownParser parser, string markdown) 
        => MarkdownParserContext.Create(parser, parser.Markdown.SerializeToSyntaxTree(markdown));
    #endregion
    
    #region Json
    public static MarkdownParserContext FromJson(this IMarkdownParser parser, JsonElement element)
        => MarkdownParserContext.Create(parser, parser.Json.SerializeToSyntaxTree(element));
    
    public static async Task<MarkdownParserContext> FromJsonAsync(this IMarkdownParser parser, Stream stream, CancellationToken ct = default)
        => MarkdownParserContext.Create(parser, await parser.Json.SerializeToSyntaxTreeAsync(stream, ct).ConfigureAwait(false));
    
    public static async Task<MarkdownParserContext> FromJsonAsync(this IMarkdownParser parser, string filePath, CancellationToken ct = default)
        => MarkdownParserContext.Create(parser, await parser.Json.SerializeToSyntaxTreeAsync(filePath, ct).ConfigureAwait(false));
    #endregion
    
    #region Xml
    public static MarkdownParserContext FromXml(this IMarkdownParser parser, XElement xml)
        => MarkdownParserContext.Create(parser, parser.Xml.SerializeToSyntaxTree(xml));
    
    public static async Task<MarkdownParserContext> FromXmlAsync(this IMarkdownParser parser, Stream stream, CancellationToken ct = default)
        => MarkdownParserContext.Create(parser, await parser.Xml.SerializeToSyntaxTreeAsync(stream, ct).ConfigureAwait(false));
    
    public static async Task<MarkdownParserContext> FromXmlAsync(this IMarkdownParser parser, string filePath, CancellationToken ct = default)
        => MarkdownParserContext.Create(parser, await parser.Xml.SerializeToSyntaxTreeAsync(filePath, ct).ConfigureAwait(false));
    #endregion
}
