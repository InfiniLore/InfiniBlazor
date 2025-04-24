// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMarkdownSyntaxTreeConverter<out T> {
    T Convert(IMarkdownSyntaxTree tree);
}

public interface IMarkdownSyntaxTreeToWriterConverter<in T> where T : TextWriter {
    void Convert(IMarkdownSyntaxTree tree, T writer);
}
