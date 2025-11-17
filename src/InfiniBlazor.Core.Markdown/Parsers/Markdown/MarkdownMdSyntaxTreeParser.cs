// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniBlazor.Markdown.Parsers.Markdown.Deserializer;
using InfiniBlazor.Markdown.Parsers.Markdown.Serializer;
using InfiniBlazor.Markdown.Syntax;

namespace InfiniBlazor.Markdown.Parsers.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownMdSyntaxTreeParser>]
public class MarkdownMdSyntaxTreeParser(IMdStringMdSyntaxSerializer serializer, IMdStringMdSyntaxDeserializer deserializer) : IMarkdownMdSyntaxTreeParser {
    public IMdSyntaxTree SerializeToSyntaxTree(string input) => serializer.SerializeToTree(input);
    public string DeserializeToString(IMdSyntaxTree tree) => deserializer.DeserializeToString(tree);
}
