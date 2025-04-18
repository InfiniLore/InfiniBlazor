// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.MarkdownWriters;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class TextWriterMarkdownWriter<T>(T writer) : IMarkdownWriter where T : TextWriter {
    public IMarkdownWriter Write(string value) {
        writer.Write(value);
        return this;
    }

    public IMarkdownWriter Write(char value) {
        writer.Write(value);
        return this;
    }

    public IMarkdownWriter Write(int value) {
        writer.Write(value);
        return this;
    }

    public IMarkdownWriter Write(ReadOnlySpan<char> value) {
        writer.Write(value);
        return this;
    }
}
