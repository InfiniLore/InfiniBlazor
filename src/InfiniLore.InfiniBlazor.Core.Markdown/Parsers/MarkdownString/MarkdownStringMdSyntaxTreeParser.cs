// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMsStringMdSyntaxTreeParser>]
public class MarkdownStringMdSyntaxTreeParser(IMdStringMdSyntaxSerializer serializer, IMdStringMdSyntaxDeserializer deserializer) : IMsStringMdSyntaxTreeParser {

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public IMdSyntaxTree SerializeToSyntaxTree(string input) => serializer.SerializeToTree(input);
    
    public string DeserializeToString(IMdSyntaxTree tree) => deserializer.DeserializeToString(tree);
}
