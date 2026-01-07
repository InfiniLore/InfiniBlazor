// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazor.Markdown.Syntax.Nodes;

namespace InfiniBlazorTests.Core.Markdown.Syntax;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdSyntaxNodeRemoveChildTests {

    [Test]
    public async Task RemoveChildAt_ShouldRemoveAndPreserveOrder() {
        // Arrange
        var tree = new MdSyntaxTree();
        IMdSyntaxNode root = tree.RootNode;

        var p1 = new ParagraphMdSyntaxNode();
        var p2 = new ParagraphMdSyntaxNode();
        var p3 = new ParagraphMdSyntaxNode();

        root.AddChildNode(p1);
        root.AddChildNode(p2);
        root.AddChildNode(p3);

        // Act
        bool removed = root.RemoveChildAt(1);

        // Assert
        await Assert.That(removed).IsTrue();
        await Assert.That(root.ChildCount).IsEqualTo(2);

        IMdSyntaxNode[] children = root.GetChildren().ToArray();
        await Assert.That(children.Length).IsEqualTo(2);
        await Assert.That(children[0]).IsSameReferenceAs(p1);
        await Assert.That(children[1]).IsSameReferenceAs(p3);

        // Removed node should be reset/pool-ready
        await Assert.That(p2.Parent).IsNull();
        await Assert.That(p2.Depth).IsEqualTo(0);
    }

    [Test]
    public async Task RemoveChildAt_OutOfRange_ShouldReturnFalse_AndNotChangeChildren() {
        // Arrange
        var tree = new MdSyntaxTree();
        IMdSyntaxNode root = tree.RootNode;

        var p1 = new ParagraphMdSyntaxNode();
        root.AddChildNode(p1);

        // Act
        bool removedNegative = root.RemoveChildAt(-1);
        bool removedTooHigh = root.RemoveChildAt(2);

        // Assert
        await Assert.That(removedNegative).IsFalse();
        await Assert.That(removedTooHigh).IsFalse();
        await Assert.That(root.ChildCount).IsEqualTo(1);
        await Assert.That(root.GetChildAt(0)).IsSameReferenceAs(p1);
    }
}