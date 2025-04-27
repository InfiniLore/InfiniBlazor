// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.SyntaxTreeConverters;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record HtmlTag(string OpenTag, string CloseTag) {
    public ReadOnlySpan<char> OpenTagSpan => OpenTag.AsSpan();
    public ReadOnlySpan<char> CloseTagSpan => CloseTag.AsSpan();

    public bool HasClosingTag { get; private init; } = true;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static HtmlTag Create(string tag)
        => new($"<{tag}", $"</{tag}>");
    
    public static HtmlTag CreateVoid(string tag) 
        => new($"<{tag}", string.Empty) {
            HasClosingTag = false
        };
    
    public static HtmlTag CreateWithClass(string tag, string className)
        => new($"<{tag} class=\"{className}\"", $"</{tag}>");
    
    public static HtmlTag CreateWithStyle(string tag, string style) 
        => new($"<{tag} style=\"{style}\"", $"</{tag}>");
}
