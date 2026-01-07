// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazor.Markdown.Syntax.Nodes;

namespace InfiniBlazorTests.Core.Markdown.Syntax;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdSyntaxNodeChildrenTests {

    [Test]
    public async Task GetChildrenSpan_ShouldBeEmpty_WhenNoChildren() {
        // Arrange
        var tree = new MdSyntaxTree();
        IMdSyntaxNode root = tree.RootNode;

        // Act
        ReadOnlySpan<IMdSyntaxNode> span = root.GetChildrenSpan();

        // Assert
        await Assert.That(span.Length).IsEqualTo(0);
    }

    [Test]
    public async Task GetChildrenSpan_ShouldExposeOrderedChildren() {
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
        IMdSyntaxNode[] span = root.GetChildrenSpan().ToArray(); // ReadOnlySpan<IMdSyntaxNode> is not preserved after the await context

        // Assert
        await Assert.That(span.Length).IsEqualTo(3);
        await Assert.That(span[0]).IsSameReferenceAs(p1);
        await Assert.That(span[1]).IsSameReferenceAs(p2);
        await Assert.That(span[2]).IsSameReferenceAs(p3);
    }

    [Test]
    public async Task GetChildren_ShouldEnumerateAllChildren() {
        // Arrange
        var tree = new MdSyntaxTree();
        IMdSyntaxNode root = tree.RootNode;

        var p1 = new ParagraphMdSyntaxNode();
        var p2 = new ParagraphMdSyntaxNode();

        root.AddChildNode(p1);
        root.AddChildNode(p2);
        
        // Act
        IMdSyntaxNode[] children = root.GetChildren().ToArray();

        // Assert
        await Assert.That(children.Length).IsEqualTo(2);
        await Assert.That(children[0]).IsSameReferenceAs(p1);
        await Assert.That(children[1]).IsSameReferenceAs(p2);

    }

    [Test]
    public async Task GetChildrenByType_ShouldFilterByType() {
        // Arrange
        var tree = new MdSyntaxTree();
        IMdSyntaxNode root = tree.RootNode;

        var p = new ParagraphMdSyntaxNode();
        var i = new ItalicMdSyntaxNode();
        var s = new StrikeMdSyntaxNode();

        root.AddChildNode(p);
        root.AddChildNode(i);
        root.AddChildNode(s);

        // Act
        ItalicMdSyntaxNode[] italics = root.GetChildrenByType<ItalicMdSyntaxNode>().ToArray();
        
        // Assert
        await Assert.That(italics.Length).IsEqualTo(1);
        await Assert.That(italics[0]).IsSameReferenceAs(i);
    }

    [Test]
    public async Task GetChildAt_ShouldReturnChildAtIndex() {
        // Arrange
        var tree = new MdSyntaxTree();
        IMdSyntaxNode root = tree.RootNode;

        var p1 = new ParagraphMdSyntaxNode();
        var p2 = new ParagraphMdSyntaxNode();

        root.AddChildNode(p1);
        root.AddChildNode(p2);

        // Act
        IMdSyntaxNode found = root.GetChildAt(1);

        // Assert
        await Assert.That(found).IsSameReferenceAs(p2);
    }

    [Test]
    public async Task TryGetChildAt_ShouldRespectBounds() {
        // Arrange
        var tree = new MdSyntaxTree();
        IMdSyntaxNode root = tree.RootNode;

        var p1 = new ParagraphMdSyntaxNode();
        root.AddChildNode(p1);
        
        // Act
        bool ok = root.TryGetChildAt(0, out IMdSyntaxNode? child);
        bool outOfRange = root.TryGetChildAt(2, out IMdSyntaxNode? none);

        // Assert
        await Assert.That(ok).IsTrue();
        await Assert.That(child).IsSameReferenceAs(p1);
        await Assert.That(outOfRange).IsFalse();
        await Assert.That(none).IsNull();
    }

    [Test]
    public async Task TryGetChildAt_Generic_ShouldReturnTypedChild() {
        // Arrange
        var tree = new MdSyntaxTree();
        IMdSyntaxNode root = tree.RootNode;

        var i = new ItalicMdSyntaxNode();
        root.AddChildNode(i);
        

        // Act
        bool okTyped = root.TryGetChildAt(0, out ItalicMdSyntaxNode? typed);
        bool wrongType = root.TryGetChildAt(0, out StrikeMdSyntaxNode? notTyped);

        // Assert
        await Assert.That(okTyped).IsTrue();
        await Assert.That(typed).IsSameReferenceAs(i);
        await Assert.That(wrongType).IsFalse();
        await Assert.That(notTyped).IsNull();
    }

    [Test]
    public async Task NextSiblingHelpers_ShouldWorkAcrossSiblings() {
        // Arrange
        var tree = new MdSyntaxTree();
        IMdSyntaxNode root = tree.RootNode;

        var a = new ParagraphMdSyntaxNode();
        var b = new ParagraphMdSyntaxNode();
        var c = new ParagraphMdSyntaxNode();

        root.AddChildNode(a);
        root.AddChildNode(b);
        root.AddChildNode(c);
        
        // Act
        bool hasNextA = a.HasNextSibling();
        bool gotNextA = a.TryGetNextSibling(out IMdSyntaxNode? nextOfA);

        bool hasNextC = c.HasNextSibling();
        bool gotNextC = c.TryGetNextSibling(out IMdSyntaxNode? nextOfC);

        // Assert
        await Assert.That(hasNextA).IsTrue();
        await Assert.That(gotNextA).IsTrue();
        await Assert.That(nextOfA).IsSameReferenceAs(b);

        await Assert.That(hasNextC).IsFalse();
        await Assert.That(gotNextC).IsFalse();
        await Assert.That(nextOfC).IsNull();
    }

    [Test]
    public async Task PreviousSiblingHelpers_ShouldWorkAcrossSiblings() {
        // Arrange
        var tree = new MdSyntaxTree();
        IMdSyntaxNode root = tree.RootNode;

        var a = new ParagraphMdSyntaxNode();
        var b = new ParagraphMdSyntaxNode();
        var c = new ParagraphMdSyntaxNode();

        root.AddChildNode(a);
        root.AddChildNode(b);
        root.AddChildNode(c);

        // Act
        bool hasPrevB = b.HasPreviousSibling();
        bool gotPrevB = b.TryGetPreviousSibling(out IMdSyntaxNode? prevOfB);

        bool hasPrevA = a.HasPreviousSibling();
        bool gotPrevA = a.TryGetPreviousSibling(out IMdSyntaxNode? prevOfA);

        // Assert
        await Assert.That(hasPrevB).IsTrue();
        await Assert.That(gotPrevB).IsTrue();
        await Assert.That(prevOfB).IsSameReferenceAs(a);

        await Assert.That(hasPrevA).IsFalse();
        await Assert.That(gotPrevA).IsFalse();
        await Assert.That(prevOfA).IsNull();
    }

    [Test]
    public async Task GetIndexAtParent_ShouldReturnCorrectIndices() {
        // Arrange
        var tree = new MdSyntaxTree();
        IMdSyntaxNode root = tree.RootNode;

        var a = new ParagraphMdSyntaxNode();
        var b = new ParagraphMdSyntaxNode();
        root.AddChildNode(a);
        root.AddChildNode(b);
        

        // Act & Assert
        await Assert.That(a.GetIndexAtParent()).IsEqualTo(0);
        await Assert.That(b.GetIndexAtParent()).IsEqualTo(1);
    }

    [Test]
    public async Task AddChildNode_ShouldSetParentDepthAndReturnChild() {
        // Arrange
        var tree = new MdSyntaxTree();
        IMdSyntaxNode root = tree.RootNode;

        var child = new ParagraphMdSyntaxNode();
        
        // Act
        IMdSyntaxNode returned = root.AddChildNode(child);

        // Assert
        await Assert.That(returned).IsSameReferenceAs(child);
        await Assert.That(child.Parent).IsSameReferenceAs(root);
        await Assert.That(child.Depth).IsEqualTo(root.Depth + 1);
        await Assert.That(root.ChildCount).IsEqualTo(1);
    }

    [Test]
    public async Task AddChildNode_Generic_ShouldReturnSameInstance() {
        // Arrange
        var tree = new MdSyntaxTree();
        IMdSyntaxNode root = tree.RootNode;

        var italic = new ItalicMdSyntaxNode();
        
        // Act
        ItalicMdSyntaxNode returned = root.AddChildNode(italic);

        // Assert
        await Assert.That(returned).IsSameReferenceAs(italic);
        await Assert.That(root.ChildCount).IsEqualTo(1);
    }

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
