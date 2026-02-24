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
public sealed partial class SuperScriptSyntaxNodeSerializer : BaseMdSyntaxNodeSerializer {
    [GeneratedRegex(@"\G\^(?<sp>(?>[^\\\^\n]+|\\\^|\^\^|(?<open>\^)|(?<-open>\^))+)(?(open)(?!))\^", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex RegexRule { get; }
    protected override Regex Syntax { get; } = RegexRule;

    private static readonly char[] STriggerCharacters = ['^'];
    public override char[] TriggerCharacters => STriggerCharacters;

    private static readonly int SpId = RegexRule.GroupNumberFromName("sp");

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        string superValue = match.Groups[SpId].Value;

        SuperScriptMdSyntaxNode node = MdSyntaxNodePool<SuperScriptMdSyntaxNode>.Shared.Get();
        parentNode.AddChildNode(node);
        stack.PushSingleLineMatchesToStack(superValue, node);
    }
}
