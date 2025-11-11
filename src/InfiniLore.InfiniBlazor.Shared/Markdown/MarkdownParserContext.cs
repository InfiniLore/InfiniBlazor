// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using System.Text.Json;
using System.Xml.Linq;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public readonly record struct MarkdownParserContext(IMarkdownParser Parser, IMdSyntaxTree Tree) {
    public static MarkdownParserContext Create(IMarkdownParser parser,IMdSyntaxTree tree) => new(parser, tree);
    
    #region Markdown
    public string ToMarkdown() => Parser.Markdown.DeserializeToString(Tree);
    #endregion
    
    #region Html
    public Task<string> ToHtmlAsync() => Parser.Html.DeserializeToStringAsync(Tree);
    #endregion
    
    #region Json
    public string ToJsonString() => Parser.Json.DeserializeToString(Tree);
    public JsonElement ToJsonElement() => Parser.Json.DeserializeToJsonElement(Tree);
    public Task ToJsonStreamAsync(Stream stream, CancellationToken ct = default) => Parser.Json.DeserializeToJsonStreamAsync(stream, Tree, ct);
    public Task ToJsonFileAsync(string filePath, CancellationToken ct = default) => Parser.Json.DeserializeToJsonFileAsync(filePath, Tree, ct);
    #endregion
    
    #region Xml
    public string ToXmlString() => Parser.Xml.DeserializeToString(Tree);
    public XElement ToXmlElement() => Parser.Xml.DeserializeToXmlElement(Tree);
    public Task ToXmlStreamAsync(Stream stream) => Parser.Xml.DeserializeToXmlStreamAsync(stream, Tree);
    public Task ToXmlFileAsync(string filePath) => Parser.Xml.DeserializeToXmlFileAsync(filePath, Tree);
    #endregion
}
