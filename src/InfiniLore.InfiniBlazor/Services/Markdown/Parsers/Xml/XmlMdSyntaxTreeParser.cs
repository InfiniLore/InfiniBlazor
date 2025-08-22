// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Parsers.Xml.NodeVisitors;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;
using System.Xml.Linq;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Xml;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IXmlMdSyntaxTreeParser>]
public class XmlMdSyntaxTreeParser : IXmlMdSyntaxTreeParser {
    private readonly Dictionary<Type, IXmlMdSyntaxNodeVisitor> _visitors = new();
    private readonly Dictionary<string, Type> _nodeTypes = new();
    
    public static IXmlMdSyntaxTreeParser Instance { get; } = new XmlMdSyntaxTreeParser();
    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public XmlMdSyntaxTreeParser() {
        RegisterVisitor<BlockQuoteMdSyntaxNode, BlockQuoteXmlMdSyntaxNodeVisitor>();
        RegisterVisitor<BoldMdSyntaxNode, BoldXmlMdSyntaxNodeVisitor>();
        RegisterVisitor<CalloutBodyMdSyntaxNode, CalloutBodyXmlMdSyntaxNodeVisitor>();
        RegisterVisitor<CalloutTitleMdSyntaxNode, CalloutTitleXmlMdSyntaxNodeVisitor>();
        RegisterVisitor<CalloutMdSyntaxNode, CalloutXmlMdSyntaxNodeVisitor>();
        RegisterVisitor<CodeBlockMdSyntaxNode, CodeBlockXmlMdSyntaxNodeVisitor>();
        RegisterVisitor<CodeInlineMdSyntaxNode, CodeInlineXmlMdSyntaxNodeVisitor>();
        RegisterVisitor<ContentHtmlMdSyntaxNode, ContentHtmlXmlMdSyntaxNodeVisitor>();
        RegisterVisitor<ContentMdSyntaxNode, ContentXmlMdSyntaxNodeVisitor>();
        RegisterVisitor<EmoteMdSyntaxNode, EmoteXmlMdSyntaxNodeVisitor>();
        RegisterVisitor<EscapedCharacterMdSyntaxNode, EscapedCharacterXmlMdSyntaxNodeVisitor>();
        RegisterVisitor<HeadingMdSyntaxNode, HeadingXmlMdSyntaxNodeVisitor>();
        RegisterVisitor<HorizontalRuleMdSyntaxNode, HorizontalRuleXmlMdSyntaxNodeVisitor>();
        RegisterVisitor<HtmlSpanMdSyntaxNode, HtmlSpanXmlMdSyntaxNodeVisitor>();
        RegisterVisitor<ImageMdSyntaxNode, ImageXmlMdSyntaxNodeVisitor>();
        RegisterVisitor<ItalicMdSyntaxNode, ItalicXmlMdSyntaxNodeVisitor>();
        RegisterVisitor<LinkMdSyntaxNode, LinkXmlMdSyntaxNodeVisitor>();
        RegisterVisitor<ListItemMdSyntaxNode, ListItemXmlMdSyntaxNodeVisitor>();
        RegisterVisitor<ListOrderedMdSyntaxNode, ListOrderedXmlMdSyntaxNodeVisitor>();
        RegisterVisitor<ListUnOrderedMdSyntaxNode, ListUnOrderedXmlMdSyntaxNodeVisitor>();
        RegisterVisitor<ParagraphMdSyntaxNode, ParagraphXmlMdSyntaxNodeVisitor>();
        // RegisterVisitor<RootMdSyntaxNode, RootXmlMdSyntaxNodeVisitor>();
        RegisterVisitor<StrikeMdSyntaxNode, StrikeXmlMdSyntaxNodeVisitor>();
        RegisterVisitor<SubScriptMdSyntaxNode, SubScriptXmlMdSyntaxNodeVisitor>();
        RegisterVisitor<SuperScriptMdSyntaxNode, SuperScriptXmlMdSyntaxNodeVisitor>();
        RegisterVisitor<TableCellMdSyntaxNode, TableCellXmlMdSyntaxNodeVisitor>();
        RegisterVisitor<TableRowMdSyntaxNode, TableRowXmlMdSyntaxNodeVisitor>();
        RegisterVisitor<TableMdSyntaxNode, TableXmlMdSyntaxNodeVisitor>();
        RegisterVisitor<TagMdSyntaxNode, TagXmlMdSyntaxNodeVisitor>();
        RegisterVisitor<UnderlineMdSyntaxNode, UnderlineXmlMdSyntaxNodeVisitor>();
    }

    private void RegisterVisitor<TNode, TVisitor>() where TNode : IMdSyntaxNode where TVisitor : IXmlMdSyntaxNodeVisitor, new() {
        _visitors[typeof(TNode)] = new TVisitor();
        _nodeTypes[typeof(TNode).Name] = typeof(TNode);
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region Deserialize
    public XElement DeserializeToElement(IMdSyntaxTree tree) {
        var rootElement = new XElement("MdSyntaxTree");

        foreach (IMdSyntaxNode child in tree.RootNode.GetChildren()) {
            DeserializeNode(child, rootElement);
        }

        return rootElement;
    }

    public async Task DeserializeToStreamAsync(Stream stream, IMdSyntaxTree tree, CancellationToken ct = default) {
        ArgumentNullException.ThrowIfNull(stream);
        ArgumentNullException.ThrowIfNull(tree);

        XElement rootElement = DeserializeToElement(tree);

        await using var writer = new StreamWriter(stream, Encoding.UTF8, leaveOpen: true);

        // Convert the string to ReadOnlyMemory<char> using AsMemory().
        await writer.WriteAsync(rootElement.ToString().AsMemory(), ct);
        await writer.FlushAsync(ct);
    }

    public async Task DeserializeToFileAsync(string filePath, IMdSyntaxTree tree, CancellationToken ct = default) {
        if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentException("File path cannot be null or whitespace.", nameof(filePath));

        ArgumentNullException.ThrowIfNull(tree);

        await using FileStream fileStream = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true);
        await DeserializeToStreamAsync(fileStream, tree, ct);
    }

    private void DeserializeNode(IMdSyntaxNode node, XElement parentElement) {
        if (_visitors.TryGetValue(node.GetType(), out IXmlMdSyntaxNodeVisitor? visitor)) {
            parentElement = visitor.DeserializeFromNode(node, parentElement);
        }

        foreach (IMdSyntaxNode child in node.GetChildren()) {
            DeserializeNode(child, parentElement);
        }
    }
    #endregion

    #region Serialize
    public IMdSyntaxTree SerializeFromElement(XElement element) {
        if (element.Name != "MdSyntaxTree") throw new InvalidOperationException("Invalid XML root element");

        MdSyntaxTree tree = new();

        foreach (XElement child in element.Elements()) {
            SerializeNode(child, tree.RootNode);
        }

        return tree;
    }

    public async Task<IMdSyntaxTree> SerializeFromStreamAsync(Stream stream, CancellationToken ct = default) {
        ArgumentNullException.ThrowIfNull(stream);

        using var reader = new StreamReader(stream, Encoding.UTF8, leaveOpen: true);
        string xmlContent = await reader.ReadToEndAsync(ct);
        XElement rootElement = XElement.Parse(xmlContent);

        return SerializeFromElement(rootElement);
    }

    public async Task<IMdSyntaxTree> SerializeFromFileAsync(string filePath, CancellationToken ct = default) {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be null or whitespace.", nameof(filePath));

        await using FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
        return await SerializeFromStreamAsync(fileStream, ct);
    }

    private void SerializeNode(XElement element, IMdSyntaxNode parentNode) {
        if (element.Name.LocalName.IsNotNullOrWhiteSpace()
            && _nodeTypes.TryGetValue(element.Name.LocalName, out Type? nodeType)
            && _visitors.TryGetValue(nodeType, out IXmlMdSyntaxNodeVisitor? visitor)) {
            parentNode = visitor.SerializeToNode(element, parentNode);
        }

        foreach (XElement child in element.Elements()) {
            SerializeNode(child, parentNode);
        }
    }
    #endregion
}
