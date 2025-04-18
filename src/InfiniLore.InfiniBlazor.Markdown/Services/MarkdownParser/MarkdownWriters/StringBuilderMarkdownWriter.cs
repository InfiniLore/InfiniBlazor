// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.MarkdownWriters;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class StringBuilderMarkdownWriter(StringBuilder builder) : IMarkdownWriter {
    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public StringBuilderMarkdownWriter() : this(new StringBuilder()) {}
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public IMarkdownWriter Write(string value) {
        builder.Append(value);
        return this;
    }

    public IMarkdownWriter Write(char value) {
        builder.Append(value);
        return this;
    }

    public IMarkdownWriter Write(int value) {
        builder.Append(value);
        return this;
    }

    public IMarkdownWriter Write(ReadOnlySpan<char> value) {
        builder.Append(value);
        return this;
    }

    public override string ToString() => builder.ToString();

    public void Clear() {
        builder.Clear();
    }
}
