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

namespace Tests.InfiniLore.InfiniBlazor.Markdown._DataSources;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdTestXmlData : IXmlSerializable {
    public string? MarkdownString { get; set; }
    public IMdSyntaxTree? SyntaxTree { get; set; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public XmlSchema? GetSchema() => null;

    public void ReadXml(XmlReader reader) {
        ArgumentNullException.ThrowIfNull(reader);

        reader.MoveToContent(); // <MdTestXmlData>
        reader.ReadStartElement(); // move into it

        while (reader.NodeType == XmlNodeType.Element) {
            switch (reader.Name) {
                case nameof(MarkdownString): {
                    MarkdownString = reader.ReadElementContentAsString();
                    break;
                }

                case "MdSyntaxTree": {
                    var syntaxTreeXml = (XElement)XNode.ReadFrom(reader);
                    SyntaxTree = MdSyntaxTreeXmlParser.Instance.DeserializeFromElement(syntaxTreeXml) as MdSyntaxTree;
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

        writer.WriteElementString(nameof(MarkdownString), MarkdownString ?? string.Empty);

        XElement syntaxTreeElement = MdSyntaxTreeXmlParser.Instance.SerializeToElement(SyntaxTree);
        syntaxTreeElement.WriteTo(writer);
    }
}
