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
public sealed partial class WrapperSyntaxNodeSerializer : BaseMdSyntaxNodeSerializer {

    [GeneratedRegex(@"\G<(?<mods>\|.*?)>(?<w>.*)</>", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex RegexRule { get; }
    protected override Regex Syntax { get; } = RegexRule;

    private static readonly int WId = RegexRule.GroupNumberFromName("w");
    private static readonly int WModsId = RegexRule.GroupNumberFromName("mods");

    private static readonly char[] STriggerCharacters = ['<'];
    public override ReadOnlySpan<char> TriggerCharacters => STriggerCharacters;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override void Serialize(IMdSyntaxFragmentStack stack, IMdSyntaxNode parentNode, Match match) {
        string wrapperValue = match.Groups[WId].Value;
        string mods = match.Groups[WModsId].Value;// Mods are required for this match

        WrapperMdSyntaxNode node = MdSyntaxNodePool<WrapperMdSyntaxNode>.Shared.Get();
        node.WithModifier(MdSyntaxNodeModifier.FromString(mods));
        parentNode.AddChildNode(node);
        stack.PushSingleLineMatchesToStack(wrapperValue, node);
    }
}
