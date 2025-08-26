// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Parsers.Xml;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Tests.Shared.InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdTestData : IXmlSerializable {
    public required string FileName { get; set; } = string.Empty;
    public required string Id { get; set; } = string.Empty;
    public string? DeveloperNote { get; set; }
    public required string MdString { get; set; } = string.Empty;
    public required IMdSyntaxTree MdSyntaxTree { get; set; } 
    public string? ExpectedHtmlStringSimplified { get; set; }
    public string? ExpectedMarkdownString { get; set; }
    public bool ExpectedMarkdownStringSkipOnWhitespaceMisMatch { get; set; }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override string ToString() => $"{FileName}/{Id}{(DeveloperNote.IsNotNullOrWhiteSpace() ? $" - '{DeveloperNote}'" : string.Empty)}"; 
    
    public XmlSchema? GetSchema() => null;

    public void ReadXml(XmlReader reader) {
        ArgumentNullException.ThrowIfNull(reader);

        reader.MoveToContent(); // <MdTestXmlData>
        
        Id = reader.GetAttribute(nameof(Id)) ?? string.Empty;
        FileName = reader.GetAttribute(nameof(FileName)) ?? string.Empty;
        
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

                case nameof(ExpectedHtmlStringSimplified): {
                    ExpectedHtmlStringSimplified = reader.ReadElementContentAsString();
                    break;
                }

                case nameof(MdSyntaxTree): {
                    var syntaxTreeXml = (XElement)XNode.ReadFrom(reader);
                    MdSyntaxTree = XmlMdSyntaxTreeParser.Instance.SerializeToSyntaxTree(syntaxTreeXml);
                    break;
                }
                
                case nameof(ExpectedMarkdownString): {
                    ExpectedMarkdownString = reader.ReadElementContentAsString();
                    break;
                }

                case nameof(ExpectedMarkdownStringSkipOnWhitespaceMisMatch): {
                    ExpectedMarkdownStringSkipOnWhitespaceMisMatch = bool.Parse(reader.ReadElementContentAsString());
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
        writer.WriteAttributeString(nameof(FileName), FileName);
        if (DeveloperNote.IsNotNullOrWhiteSpace()) writer.WriteElementString(nameof(DeveloperNote), DeveloperNote);
        writer.WriteElementString(nameof(MdString), MdString);
        
        // Writes as "MdSyntaxTree"
        XElement syntaxTreeElement = XmlMdSyntaxTreeParser.Instance.DeserializeToXmlElement(MdSyntaxTree);
        syntaxTreeElement.WriteTo(writer);
        
        if (ExpectedHtmlStringSimplified.IsNotNullOrWhiteSpace()) writer.WriteElementString(nameof(ExpectedHtmlStringSimplified), ExpectedHtmlStringSimplified);
        if (ExpectedMarkdownString.IsNotNullOrWhiteSpace()) writer.WriteElementString(nameof(ExpectedMarkdownString), ExpectedMarkdownString);
        if (ExpectedMarkdownStringSkipOnWhitespaceMisMatch) writer.WriteElementString(nameof(ExpectedMarkdownStringSkipOnWhitespaceMisMatch), ExpectedMarkdownStringSkipOnWhitespaceMisMatch.ToString());
    }
}
