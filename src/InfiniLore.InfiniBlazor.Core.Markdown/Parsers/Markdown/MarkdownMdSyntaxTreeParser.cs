// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown.Deserializer;
using InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown.Serializer;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownMdSyntaxTreeParser>]
public class MarkdownMdSyntaxTreeParser(IMdStringMdSyntaxSerializer serializer, IMdStringMdSyntaxDeserializer deserializer) : IMarkdownMdSyntaxTreeParser {

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public IMdSyntaxTree SerializeToSyntaxTree(string input) => serializer.SerializeToTree(input);
    
    public string DeserializeToString(IMdSyntaxTree tree) => deserializer.DeserializeToString(tree);
}
