// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Parsers.Xml;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Tests.Shared.Infinilore.InfiniBlazor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class XmlMdTestData : IXmlSerializable {
    public string Id { get; set; } = string.Empty;
    public string? MarkdownString { get; set; }
    public IMdSyntaxTree? SyntaxTree { get; set; }
    public string? SimplifiedHtmlString { get; set; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public XmlSchema? GetSchema() => null;

    public void ReadXml(XmlReader reader) {
        ArgumentNullException.ThrowIfNull(reader);

        reader.MoveToContent(); // <MdTestXmlData>
        
        Id = reader.GetAttribute(nameof(Id)) ?? string.Empty;
        
        reader.ReadStartElement(); // move into it

        while (reader.NodeType == XmlNodeType.Element) {
            switch (reader.Name) {
                case nameof(MarkdownString): {
                    MarkdownString = reader.ReadElementContentAsString();
                    break;
                }

                case nameof(SimplifiedHtmlString): {
                    SimplifiedHtmlString = reader.ReadElementContentAsString();
                    break;
                }

                case nameof(MdSyntaxTree): {
                    var syntaxTreeXml = (XElement)XNode.ReadFrom(reader);
                    SyntaxTree = XmlMdSyntaxTreeParser.Instance.DeserializeFromElement(syntaxTreeXml) as MdSyntaxTree;
                    break;
                }

                // unknown element, skip it
                default:{
                    reader.Skip(); 
                    break;
                }
            }
        }

        reader.ReadEndElement();
    }
    
    public void WriteXml(XmlWriter writer) {
        ArgumentNullException.ThrowIfNull(writer);
        ArgumentNullException.ThrowIfNull(MarkdownString);
        ArgumentNullException.ThrowIfNull(SyntaxTree);

        writer.WriteAttributeString(nameof(Id), Id);
        writer.WriteElementString(nameof(MarkdownString), MarkdownString ?? string.Empty);
        if (SimplifiedHtmlString.IsNotNullOrWhiteSpace()) writer.WriteElementString(nameof(SimplifiedHtmlString), SimplifiedHtmlString);

        XElement syntaxTreeElement = XmlMdSyntaxTreeParser.Instance.SerializeToElement(SyntaxTree);
        syntaxTreeElement.WriteTo(writer);
    }
}
