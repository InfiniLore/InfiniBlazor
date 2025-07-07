// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class ImageMdSyntaxNode : MdSyntaxNode<ImageMdSyntaxNode> {
    public string Href { get; set; } = string.Empty;
    public string AltText { get; set; } = string.Empty;
     
    public bool ContainsMods { get; set; }
    public string? ModTitle { get; set; }
    public (int width, int height)? ModSize { get; set; }
    public bool ModFit { get; set; }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override bool TryReset() {
        Href = string.Empty;
        AltText = string.Empty;
        
        ContainsMods = false;
        ModTitle = null;
        ModSize = null;
        ModFit = false;
        
        return base.TryReset();
    }
}
