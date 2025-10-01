// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Parsers.Xml;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Tests.InfiniBlazor.Shared.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdTestData : IXmlSerializable, IEquatable<MdTestData> {
    public required string FileName { get; set; } = string.Empty;
    public required string Id { get; set; } = string.Empty;
    public string? DeveloperNote { get; set; }
    public required string MdString { get; set; } = string.Empty;
    public required IMdSyntaxTree MdSyntaxTree { get; set; } 
    public string? ExpectedMarkdown { get; set; }
    public string? ExpectedJsonString { get; set; }
    public bool ExpectedMarkdownSkipOnWhitespaceMisMatch { get; set; }
    
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

                case nameof(MdSyntaxTree): {
                    var syntaxTreeXml = (XElement)XNode.ReadFrom(reader);
                    MdSyntaxTree = XmlMdSyntaxTreeParser.Instance.SerializeToSyntaxTree(syntaxTreeXml);
                    break;
                }

                case nameof(ExpectedMarkdown): {
                    ExpectedMarkdown = reader.ReadElementContentAsString();
                    break;
                }

                case nameof(ExpectedMarkdownSkipOnWhitespaceMisMatch): {
                    ExpectedMarkdownSkipOnWhitespaceMisMatch = bool.Parse(reader.ReadElementContentAsString());
                    break;
                }

                case nameof(ExpectedJsonString): {
                    ExpectedJsonString = reader.ReadElementContentAsString();
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
        
        if (ExpectedMarkdown.IsNotNullOrWhiteSpace()) writer.WriteElementString(nameof(ExpectedMarkdown), ExpectedMarkdown);
        if (ExpectedMarkdownSkipOnWhitespaceMisMatch) writer.WriteElementString(nameof(ExpectedMarkdownSkipOnWhitespaceMisMatch), ExpectedMarkdownSkipOnWhitespaceMisMatch.ToString());
        if (ExpectedJsonString.IsNotNullOrWhiteSpace()) writer.WriteElementString(nameof(ExpectedJsonString), ExpectedJsonString);
    }
    
    public bool Equals(MdTestData? other) {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return FileName == other.FileName 
            && Id == other.Id 
            && DeveloperNote == other.DeveloperNote 
            && MdString == other.MdString 
            && MdSyntaxTree.Equals(other.MdSyntaxTree) 
            && ExpectedMarkdown == other.ExpectedMarkdown 
            && ExpectedMarkdownSkipOnWhitespaceMisMatch == other.ExpectedMarkdownSkipOnWhitespaceMisMatch;
    }
    
    public override bool Equals(object? obj) {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((MdTestData)obj);
    }
    
    // ReSharper disable NonReadonlyMemberInGetHashCode
    public override int GetHashCode() => HashCode.Combine(FileName, Id, DeveloperNote, MdString, MdSyntaxTree, ExpectedMarkdown, ExpectedMarkdownSkipOnWhitespaceMisMatch);
    // ReSharper restore NonReadonlyMemberInGetHashCode
}
