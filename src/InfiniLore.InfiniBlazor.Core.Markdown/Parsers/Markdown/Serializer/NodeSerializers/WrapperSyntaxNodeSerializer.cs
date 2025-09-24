// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown.Serializer.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class WrapperSyntaxNodeSerializer {
    private static readonly int WId = MdRegexLib.GetGroupId(MdRegexGroupNames.WrapperContent);
    private static readonly int WModsId = MdRegexLib.GetGroupId(MdRegexGroupNames.WrapperMods);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static void Serialize(IMdSyntaxFragmentStack stack, IMdSyntaxNode parentNode, Match match) {
        if (!match.Groups[WId].TryGetValue(out string? wrapperValue)) return;
        if (!match.Groups[WModsId].TryGetValue(out string? mods)) return;// Mods are required for this match

        WrapperMdSyntaxNode node = WrapperMdSyntaxNode.Pool.Get();
        node.WithModifier(MdSyntaxNodeModifier.FromString(mods));
        parentNode.AddChildNode(node);
        stack.PushSingleLineMatchesToStack(wrapperValue, node);
    }
}
