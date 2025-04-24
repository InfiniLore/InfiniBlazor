// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.SyntaxTreeConverters;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class ToTextWriterConverter<T> : SimpleSyntaxTreeConverter, IMarkdownSyntaxTreeToWriterConverter<T> where T : TextWriter {
    public void Convert(IMarkdownSyntaxTree tree, T writer) {
        ProcessNodeTree(tree, writer, static (w, content) => w.Write(content));
    }
}
