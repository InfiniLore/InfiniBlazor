// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownStringMdSyntaxTreeParser>]
public class MarkdownStringMdSyntaxTreeParser(IMarkdownStringMdSyntaxSerializer serializer) : IMarkdownStringMdSyntaxTreeParser {

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public IMdSyntaxTree SerializeToSyntaxTree(string input) => serializer.SerializeToTree(input);
    
    public string DeserializeToString(IMdSyntaxTree tree) => throw new NotImplementedException();
}
