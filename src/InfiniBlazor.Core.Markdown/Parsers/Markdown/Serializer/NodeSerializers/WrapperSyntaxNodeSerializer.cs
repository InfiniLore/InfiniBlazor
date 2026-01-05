// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class WrapperSyntaxNodeSerializer : IMdSyntaxNodeSerializer{

    [GeneratedRegex(@"<(?<mods>\|.*?)>(?<w>.*)</>", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex Syntax { get; }
    
    private static readonly int WId = Syntax.GroupNumberFromName("w");
    private static readonly int WModsId = Syntax.GroupNumberFromName("mods");
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public Match Match(string input, int startPosition = 0) 
        => Syntax.Match(input, startPosition);
    
    public void Serialize(IMdSyntaxFragmentStack stack, IMdSyntaxNode parentNode, Match match) {
        if (!match.Groups[WId].TryGetValue(out string? wrapperValue)) return;
        if (!match.Groups[WModsId].TryGetValue(out string? mods)) return;// Mods are required for this match

        WrapperMdSyntaxNode node = MdSyntaxNodePool<WrapperMdSyntaxNode>.Shared.Get();
        node.WithModifier(MdSyntaxNodeModifier.FromString(mods));
        parentNode.AddChildNode(node);
        stack.PushSingleLineMatchesToStack(wrapperValue, node);
    }
}
