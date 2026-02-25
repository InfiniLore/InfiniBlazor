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
public sealed partial class BreakSyntaxNodeSerializer : BaseMdSyntaxNodeSerializer {
    [GeneratedRegex(@"\G<[Bb][Rr]/?>", DefaultSingleLineRegexOptions)]
    private static partial Regex RegexRule { get; }
    protected override Regex Syntax { get; } = RegexRule;

    private static readonly char[] STriggerCharacters = ['<'];
    public override ReadOnlySpan<char> TriggerCharacters => STriggerCharacters;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override void Serialize(IMdSyntaxFragmentStack stack, IMdSyntaxNode parentNode, Match match) {
        BreakMdSyntaxNode node = MdSyntaxNodePool<BreakMdSyntaxNode>.Shared.Get();
        parentNode.AddChildNode(node);
    }
}
