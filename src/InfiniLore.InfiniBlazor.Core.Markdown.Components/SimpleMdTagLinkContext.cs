// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Components;

namespace InfiniLore.InfiniBlazor.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class SimpleMdTagLinkContext : ITagLinkContext {
    public bool IsRenderedMarkdownComponent => true;
    
    public static SimpleMdTagLinkContext Shared { get; }= new();
}
