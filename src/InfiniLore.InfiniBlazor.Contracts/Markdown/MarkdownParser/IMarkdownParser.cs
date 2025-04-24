// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMarkdownParser {
    void ParseToWriter<T>(string markdown, T writer) where T : TextWriter;
    bool TryParse<T>(string markdown, [NotNullWhen(true)] out T? output) where T : class;
}
