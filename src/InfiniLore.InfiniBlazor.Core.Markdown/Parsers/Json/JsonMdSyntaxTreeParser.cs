// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Parsers.Json.NodeVisitors;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.Json;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Json;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IJsonMdSyntaxTreeParser>]
public class JsonMdSyntaxTreeParser : IJsonMdSyntaxTreeParser {
    private readonly Dictionary<Type, IJsonMdSyntaxNodeVisitor> _visitors = new();
    private readonly Dictionary<string, Type> _nodeTypes = new();

    private static readonly JsonWriterOptions WriterOptions = new() {
        Indented = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    };

    private static readonly JsonSerializerOptions SerializerOptions = new() {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    };

    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public JsonMdSyntaxTreeParser() {
        RegisterVisitor<BlockQuoteMdSyntaxNode, BlockQuoteJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<BoldMdSyntaxNode, BoldJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<CalloutBodyMdSyntaxNode, CalloutBodyJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<CalloutTitleMdSyntaxNode, CalloutTitleJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<CalloutMdSyntaxNode, CalloutJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<CodeBlockMdSyntaxNode, CodeBlockJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<CodeInlineMdSyntaxNode, CodeInlineJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<HtmlMdSyntaxNode, HtmlJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<TextMdSyntaxNode, TextJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<EmoteMdSyntaxNode, EmoteJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<EscapedCharacterMdSyntaxNode, EscapedCharacterJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<HeadingMdSyntaxNode, HeadingJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<HeadingSimpleMdSyntaxNode, HeadingSimpleJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<HorizontalRuleMdSyntaxNode, HorizontalRuleJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<HtmlSpanMdSyntaxNode, HtmlSpanJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<ImageMdSyntaxNode, ImageJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<ItalicMdSyntaxNode, ItalicJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<LinkMdSyntaxNode, LinkJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<ListItemMdSyntaxNode, ListItemJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<ListOrderedMdSyntaxNode, ListOrderedJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<ListUnOrderedMdSyntaxNode, ListUnOrderedJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<ParagraphMdSyntaxNode, ParagraphJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<StrikeMdSyntaxNode, StrikeJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<SubScriptMdSyntaxNode, SubScriptJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<SuperScriptMdSyntaxNode, SuperScriptJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<TableCellMdSyntaxNode, TableCellJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<TableRowMdSyntaxNode, TableRowJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<TableMdSyntaxNode, TableJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<TagMdSyntaxNode, TagJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<UnderlineMdSyntaxNode, UnderlineJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<NewLineMdSyntaxNode, NewLineJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<UserMdSyntaxNode, UserJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<WikiLinkMdSyntaxNode, WikiLinkJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<TemplateMdSyntaxNode, TemplateJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<HighlightMdSyntaxNode, HighlightJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<WrapperMdSyntaxNode, WrapperJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<FrontMatterMdSyntaxNode, FrontMatterJsonMdSyntaxNodeVisitor>();
        RegisterVisitor<BreakMdSyntaxNode, BreakJsonMdSyntaxNodeVisitor>();
    }

    private void RegisterVisitor<TNode, TVisitor>() where TNode : MdSyntaxNode<TNode>, new() where TVisitor : JsonMdSyntaxNodeVisitor<TNode>, new() {
        _visitors[typeof(TNode)] = new TVisitor();
        _nodeTypes[typeof(TNode).Name] = typeof(TNode);
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region Deserialize
    public string DeserializeToString(IMdSyntaxTree input) {
        var element = DeserializeToJsonElement(input);
        return JsonSerializer.Serialize(element, SerializerOptions);
    }

    public JsonElement DeserializeToJsonElement(IMdSyntaxTree tree) {
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream, WriterOptions);

        writer.WriteStartObject();
        writer.WriteString("type", "MdSyntaxTree");
        writer.WriteStartArray("children");

        foreach (IMdSyntaxNode child in tree.RootNode.GetChildren()) {
            DeserializeNode(child, writer);
        }

        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Flush();

        stream.Position = 0;
        using var document = JsonDocument.Parse(stream);
        return document.RootElement.Clone();
    }

    public async Task DeserializeToJsonStreamAsync(Stream stream, IMdSyntaxTree tree, CancellationToken ct = default) {
        ArgumentNullException.ThrowIfNull(stream);
        ArgumentNullException.ThrowIfNull(tree);

        JsonElement rootElement = DeserializeToJsonElement(tree);
        await JsonSerializer.SerializeAsync(stream, rootElement, SerializerOptions, ct);
    }

    public async Task DeserializeToJsonFileAsync(string filePath, IMdSyntaxTree tree, CancellationToken ct = default) {
        if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentException("File path cannot be null or whitespace.", nameof(filePath));

        ArgumentNullException.ThrowIfNull(tree);

        await using FileStream fileStream = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true);
        await DeserializeToJsonStreamAsync(fileStream, tree, ct);
    }

    private void DeserializeNode(IMdSyntaxNode node, Utf8JsonWriter writer) {
        writer.WriteStartObject();
        writer.WriteString("type", node.GetType().Name);

        if (_visitors.TryGetValue(node.GetType(), out IJsonMdSyntaxNodeVisitor? visitor)) {
            visitor.DeserializeToJson(node, writer);
        }

        var children = node.GetChildren().ToList();
        if (children.Count > 0) {
            writer.WriteStartArray("children");
            foreach (IMdSyntaxNode child in children) {
                DeserializeNode(child, writer);
            }
            writer.WriteEndArray();
        }

        writer.WriteEndObject();
    }
    #endregion

    #region Serialize
    public IMdSyntaxTree SerializeToSyntaxTree(JsonElement element) {
        if (!element.TryGetProperty("type", out var typeProperty) || typeProperty.GetString() != "MdSyntaxTree") {
            throw new InvalidOperationException("Invalid JSON root element");
        }

        MdSyntaxTree tree = new();

        if (element.TryGetProperty("children", out var childrenProperty) && childrenProperty.ValueKind == JsonValueKind.Array) {
            foreach (JsonElement child in childrenProperty.EnumerateArray()) {
                SerializeNode(child, tree.RootNode);
            }
        }

        return tree;
    }

    public async Task<IMdSyntaxTree> SerializeToSyntaxTreeAsync(Stream stream, CancellationToken ct = default) {
        ArgumentNullException.ThrowIfNull(stream);

        using var document = await JsonDocument.ParseAsync(stream, cancellationToken: ct);
        return SerializeToSyntaxTree(document.RootElement);
    }

    public async Task<IMdSyntaxTree> SerializeToSyntaxTreeAsync(string filePath, CancellationToken ct = default) {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be null or whitespace.", nameof(filePath));

        await using FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
        return await SerializeToSyntaxTreeAsync(fileStream, ct);
    }

    private void SerializeNode(JsonElement element, IMdSyntaxNode parentNode) {
        if (element.TryGetProperty("type", out var typeProperty)) {
            string? typeName = typeProperty.GetString();
            if (!string.IsNullOrWhiteSpace(typeName)
                && _nodeTypes.TryGetValue(typeName, out Type? nodeType)
                && _visitors.TryGetValue(nodeType, out IJsonMdSyntaxNodeVisitor? visitor)) {

                var newNode = visitor.SerializeToNode(element, parentNode);

                if (element.TryGetProperty("children", out var childrenProperty) && childrenProperty.ValueKind == JsonValueKind.Array) {
                    foreach (JsonElement child in childrenProperty.EnumerateArray()) {
                        SerializeNode(child, newNode);
                    }
                }
            }
        }
    }
    #endregion
}
