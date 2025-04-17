// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMarkdownWriter {
    IMarkdownWriter Write(string value);
    IMarkdownWriter Write(char value);
    IMarkdownWriter Write(int value);
    IMarkdownWriter Write(ReadOnlySpan<char> value);
}
