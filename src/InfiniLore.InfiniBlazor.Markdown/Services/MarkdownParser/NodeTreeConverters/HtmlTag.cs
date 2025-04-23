// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.NodeTreeConverters;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public readonly record struct HtmlTag(string OpenTag, string CloseTag) {
    public ReadOnlySpan<char> OpenTagSpan => OpenTag.AsSpan();
    public ReadOnlySpan<char> CloseTagSpan => CloseTag.AsSpan();
    
    public bool HasClosingTag => CloseTag.Length > 0;

    public static HtmlTag Create(string tag)
        => new($"<{tag}", $"</{tag}>");
    
    public static HtmlTag CreateVoid(string tag) 
        => new($"<{tag}", string.Empty);
    
    public static HtmlTag CreateWithClass(string tag, string className)
        => new($"<{tag} class=\"{className}\"", $"</{tag}>");
    
    public static HtmlTag CreateWithStyle(string tag, string style) 
        => new($"<{tag} style=\"{style}\"", $"</{tag}>");
}
