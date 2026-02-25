// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniBlazor.Markdown.Syntax.Nodes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class ScriptingIfStatementSyntaxNode : MdSyntaxNode<ScriptingIfStatementSyntaxNode> {
    public int ElseConditionIndex { get; private set; } = -1;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public ScriptingIfStatementSyntaxNode WithIfCondition(ScriptingExpressionSyntaxNode expressionNode) {
        AddChildNode(expressionNode);
        return this;
    }
    
    public ScriptingIfStatementSyntaxNode WithElseCondition(ScriptingExpressionSyntaxNode expressionNode) {
        if (ElseConditionIndex != -1) {
            throw new InvalidOperationException("Else child already set.");
        }

        AddChildNode(expressionNode);
        ElseConditionIndex = ChildCount - 1;
        return this;
    }
    
    internal void WithElseConditionIndex(int parse) {
        ElseConditionIndex = parse;
    }
    
    public override bool TryReset() {
        ElseConditionIndex = -1;
        return base.TryReset();
    }

    protected override bool Equals(ScriptingIfStatementSyntaxNode? other)
        => base.Equals(other)
            && ElseConditionIndex == other.ElseConditionIndex;
}
