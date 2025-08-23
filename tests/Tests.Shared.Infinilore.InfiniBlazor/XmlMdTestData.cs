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
    public string? MdString { get; set; }
    public IMdSyntaxTree? MdSyntaxTree { get; set; }
    public string? SimplifiedHtmlString { get; set; }
    public string? DeveloperNote { get; set; }

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
                case nameof(DeveloperNote): {
                    DeveloperNote = reader.ReadElementContentAsString();
                    break;
                }
                
                case nameof(MdString): {
                    MdString = reader.ReadElementContentAsString();
                    break;
                }

                case nameof(SimplifiedHtmlString): {
                    SimplifiedHtmlString = reader.ReadElementContentAsString();
                    break;
                }

                case nameof(MdSyntaxTree): {
                    var syntaxTreeXml = (XElement)XNode.ReadFrom(reader);
                    MdSyntaxTree = XmlMdSyntaxTreeParser.Instance.SerializeToSyntaxTree(syntaxTreeXml) as MdSyntaxTree;
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
        ArgumentNullException.ThrowIfNull(MdString);
        ArgumentNullException.ThrowIfNull(MdSyntaxTree);

        writer.WriteAttributeString(nameof(Id), Id);
        writer.WriteElementString(nameof(MdString), MdString ?? string.Empty);
        if (SimplifiedHtmlString.IsNotNullOrWhiteSpace()) writer.WriteElementString(nameof(SimplifiedHtmlString), SimplifiedHtmlString);
        if (DeveloperNote.IsNotNullOrWhiteSpace()) writer.WriteElementString(nameof(DeveloperNote), DeveloperNote);
        
        // Writes as "MdSyntaxTree"
        XElement syntaxTreeElement = XmlMdSyntaxTreeParser.Instance.DeserializeToXmlElement(MdSyntaxTree);
        syntaxTreeElement.WriteTo(writer);
    }
}
