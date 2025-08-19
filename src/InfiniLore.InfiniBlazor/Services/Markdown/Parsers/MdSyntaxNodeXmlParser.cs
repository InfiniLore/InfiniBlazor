// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text;
using System.Xml.Linq;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;

namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Parsers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdSyntaxNodeXmlParser {
    private static readonly Dictionary<string, Type> NodeTypeMap = new() {
        { nameof(LinkMdSyntaxNode), typeof(LinkMdSyntaxNode) },
        { nameof(ContentMdSyntaxNode), typeof(ContentMdSyntaxNode) },
        { nameof(ImageMdSyntaxNode), typeof(ImageMdSyntaxNode) }
        // Add mappings for other syntax node types here
    };

    // -----------------------------------------------------------------------------------------------------------------
    // Method
    // -----------------------------------------------------------------------------------------------------------------
    public async Task ToXmlAsync(Stream stream, IMdSyntaxNode node, CancellationToken ct = default) {
        ArgumentNullException.ThrowIfNull(node);

        XElement xml = ToXmlInternal(node);
        await using StreamWriter writer = new(stream, Encoding.UTF8, leaveOpen: true);
        await writer.WriteAsync(xml.ToString().AsMemory(), ct);
        await writer.FlushAsync(ct);
    }

    public async Task ToXmlFileAsync(string filePath, IMdSyntaxNode node, CancellationToken ct = default) {
        if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentException("File path cannot be null or whitespace.", nameof(filePath));
        ArgumentNullException.ThrowIfNull(node);

        await using FileStream fileStream = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true);
        await ToXmlAsync(fileStream, node, ct);
    }

    private XElement ToXmlInternal(IMdSyntaxNode node) {
        var element = new XElement(
            node.GetType().Name, // Use the type name as the XML element name
            node is { ContainsModifiers: true, Modifiers: not null }
                ? new XElement("Modifiers", SerializeModifiers(node.Modifiers))
                : null
        );

        // Add type-specific attributes or elements
        if (node is LinkMdSyntaxNode linkNode) {
            element.Add(new XAttribute("Href", linkNode.Href));
        } else if (node is ContentMdSyntaxNode contentNode) {
            element.Add(new XElement("Content", contentNode.Content));
        }

        // Serialize children recursively
        foreach (var child in node.GetChildren()) {
            element.Add(ToXmlInternal(child));
        }

        return element;
    }

    private XElement SerializeModifiers(IMdSyntaxNodeModifier modifiers) {
        return new XElement("Attributes",
            modifiers.Attributes.Select(attr => new XElement(
                attr.Key,
                new XAttribute("Start", attr.Value.Start.Value),
                new XAttribute("End", attr.Value.End.Value)
            ))
        );
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Async Deserialization
    // -----------------------------------------------------------------------------------------------------------------
    public async Task<IMdSyntaxNode> FromXmlAsync(Stream stream, CancellationToken ct = default) {
        if (stream == null) throw new ArgumentNullException(nameof(stream));

        using StreamReader reader = new(stream, Encoding.UTF8, leaveOpen: true);
        string xmlContent = await reader.ReadToEndAsync(ct);
        XElement root = XElement.Parse(xmlContent);

        return DeserializeNode(root);
    }

    public async Task<IMdSyntaxNode> FromXmlFileAsync(string filePath, CancellationToken ct = default) {
        if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentException("File path cannot be null or whitespace.", nameof(filePath));

        using FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);
        return await FromXmlAsync(fileStream, ct);
    }

    private IMdSyntaxNode DeserializeNode(XElement element) {
        if (!NodeTypeMap.TryGetValue(element.Name.LocalName, out Type? nodeType)) {
            throw new InvalidOperationException($"Unknown node type: {element.Name.LocalName}");
        }

        // Create an instance of the node
        IMdSyntaxNode node = (IMdSyntaxNode)Activator.CreateInstance(nodeType)!;
        
        // Set specific properties
        if (node is LinkMdSyntaxNode linkNode && element.Attribute("Href")?.Value is not null) {
            linkNode.Href = element.Attribute("Href")!.Value;
        } else if (node is ContentMdSyntaxNode contentNode && element.Element("Content") is not null) {
            contentNode.Content = element.Element("Content")?.Value ?? string.Empty;
        }

        // Deserialize modifiers if present
        XElement? modifiersElement = element.Element("Modifiers");
        if (modifiersElement != null) {
            MdSyntaxNodeModifier modifiers = new();
            foreach (XElement attr in modifiersElement.Elements()) {
                string key = attr.Name.LocalName;
                int start = int.Parse(attr.Attribute("Start")?.Value ?? "0");
                int end = int.Parse(attr.Attribute("End")?.Value ?? "0");
                modifiers.Attributes[key] = new Range(start, end);
            }
            node.Modifiers = modifiers;
        }

        // Deserialize children
        foreach (XElement childElement in element.Elements().Where(e => NodeTypeMap.ContainsKey(e.Name.LocalName))) {
            node.AddChildNode(DeserializeNode(childElement));
        }

        return node;
    }
}