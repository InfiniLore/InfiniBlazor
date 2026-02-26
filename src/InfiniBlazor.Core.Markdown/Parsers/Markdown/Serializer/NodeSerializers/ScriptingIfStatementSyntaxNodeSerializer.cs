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
public sealed partial class ScriptingIfStatementSyntaxNodeSerializer : BaseMdSyntaxNodeSerializer {
    [GeneratedRegex("""
        \G
        ^@if\((?<ifExpression>.+)\)\ *$\s
        (?<ifBody>(?:(?!^@elseif|^@else|^@endif)^.*$\s*?)*)
        (?<elifSection>
          (?:
            ^@elseif\(.+\)\ *$\s
            (?:(?:(?!^@elseif|^@else|^@endif)^.*$\s*?)+)?
          )+
        )?
        (?<elseSection>
          ^@else\ *$\s
          (?<elseBody>(?:(?!^@endif)^.*$\s*?)*)
        )?
        ^@endif\ *$
        """, DefaultMultiLineRegexOptions)]
    private static partial Regex RegexRule { get; }
    protected override Regex Syntax => RegexRule;

    [GeneratedRegex("""
        ^@elseif\(.+\)\ *$
        (?:\s(?!^@elseif|^@else|^@endif)(?:^.*$)+)+
        """, DefaultMultiLineRegexOptions)]
    private static partial Regex ElifSectionRegexRule { get; }

    private static readonly char[] STriggerCharacters = ['@'];
    public override ReadOnlySpan<char> TriggerCharacters => STriggerCharacters;

    private static readonly int IfExpressionId = RegexRule.GroupNumberFromName("ifExpression");
    private static readonly int IfBodyId = RegexRule.GroupNumberFromName("ifBody");
    private static readonly int ElifSectionId = RegexRule.GroupNumberFromName("elifSection");
    private static readonly int ElseSectionId = RegexRule.GroupNumberFromName("elseSection");
    private static readonly int ElseBodyId = RegexRule.GroupNumberFromName("elseBody");

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override void Serialize(IMdSyntaxFragmentStack stack, IMdSyntaxNode parentNode, Match match) {
        ScriptingIfStatementSyntaxNode statementNode = MdSyntaxNodePool<ScriptingIfStatementSyntaxNode>.Shared.Get();
        parentNode.AddChildNode(statementNode);
        
        Group ifExpression = match.Groups[IfExpressionId];
        Group ifBody = match.Groups[IfBodyId];
        ScriptingExpressionSyntaxNode ifExpressionNode = MdSyntaxNodePool<ScriptingExpressionSyntaxNode>.Shared.Get();
        ifExpressionNode.WithExpression($"@if({ifExpression.Value})", ifExpression.Index, ifExpression.Length);
        statementNode.WithIfCondition(ifExpressionNode);
        
        string adjustedIfBody = LineNormalization.NormalizeBlockQuote(ifBody.ValueSpan, out int ifBodyLeadingSpaces);
        ScriptingBodySyntaxNode ifBodyNode = MdSyntaxNodePool<ScriptingBodySyntaxNode>.Shared.Get();
        ifBodyNode.WithLeadingSpaces(ifBodyLeadingSpaces);
        ifExpressionNode.AddChildNode(ifBodyNode);
        
        stack.PushMultiLineMatchesToStack(adjustedIfBody, ifBodyNode);
        
        // parse the elif section
        if (match.Groups[ElifSectionId] is { Success: true, ValueSpan: var span }) {
            foreach (ValueMatch valueMatch in ElifSectionRegexRule.EnumerateMatches(span)) {
                int index = valueMatch.Index;
                int length = valueMatch.Length;
                ReadOnlySpan<char> valueSpan = span[index..(index + length)];

                int endBracket = valueSpan[..valueSpan.IndexOf('\n')].LastIndexOf(')');
                ReadOnlySpan<char> elifExpression = valueSpan[8..endBracket];
                ReadOnlySpan<char> elifBody = valueSpan[(endBracket + 2)..];
                
                ScriptingExpressionSyntaxNode elifExpressionNode = MdSyntaxNodePool<ScriptingExpressionSyntaxNode>.Shared.Get();
                elifExpressionNode.WithExpression($"@elseif({elifExpression})", 8, endBracket-8);
                statementNode.WithIfCondition(elifExpressionNode);
                
                string adjustedElifBody = LineNormalization.NormalizeBlockQuote(elifBody, out int elifBodyLeadingSpaces);
                ScriptingBodySyntaxNode elifBodyNode = MdSyntaxNodePool<ScriptingBodySyntaxNode>.Shared.Get();
                elifBodyNode.WithLeadingSpaces(elifBodyLeadingSpaces);
                elifExpressionNode.AddChildNode(elifBodyNode);
                
                stack.PushMultiLineMatchesToStack(adjustedElifBody, elifBodyNode);
            }
        }
        
        // parse the else section
        if (match.Groups[ElseSectionId] is { Success: true }) {
            Group elseBody = match.Groups[ElseBodyId];
            
            ScriptingExpressionSyntaxNode elseExpressionNode = MdSyntaxNodePool<ScriptingExpressionSyntaxNode>.Shared.Get();
            elseExpressionNode.WithExpression("@else", 5, 0);
            statementNode.WithElseCondition(elseExpressionNode);
            
            string adjustedElseBody = LineNormalization.NormalizeBlockQuote(elseBody.ValueSpan, out int elseBodyLeadingSpaces);
            ScriptingBodySyntaxNode elseBodyNode = MdSyntaxNodePool<ScriptingBodySyntaxNode>.Shared.Get();
            elseBodyNode.WithLeadingSpaces(elseBodyLeadingSpaces);
            elseExpressionNode.AddChildNode(elseBodyNode);
            
            stack.PushMultiLineMatchesToStack(adjustedElseBody, elseBodyNode);
        }
    }
}
