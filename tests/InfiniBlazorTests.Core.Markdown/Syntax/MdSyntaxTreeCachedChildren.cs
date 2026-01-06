// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazor.Markdown.Syntax.Nodes;

namespace InfiniBlazorTests.Core.Markdown.Syntax;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdSyntaxTreeCachedChildrenTests {

    public static IEnumerable<Func<(MdSyntaxTree, Type, IMdSyntaxNode[])>> GetCachedChildrenByTypeTestCases() {
        yield return () => {
            var tree = new MdSyntaxTree();

            var p1 = new ParagraphMdSyntaxNode();
            var p2 = new ParagraphMdSyntaxNode();

            p1.AddChildNode(p2);
            tree.RootNode.AddChildNode(p1);

            tree.StoreChildAtCache(p2);

            return (tree, typeof(ParagraphMdSyntaxNode), [
                p2
            ]);
        };

        yield return () => {
            var tree = new MdSyntaxTree();

            var p2 = new ParagraphMdSyntaxNode();

            tree.RootNode.AddChildNode(new ParagraphMdSyntaxNode());
            tree.RootNode.AddChildNode(p2);

            tree.StoreChildAtCache(p2);

            return (tree, typeof(ParagraphMdSyntaxNode), [
                p2
            ]);
        };

        yield return () => {
            var tree = new MdSyntaxTree();

            var p2 = new ParagraphMdSyntaxNode();
            var i1 = new ItalicMdSyntaxNode();

            p2.AddChildNode(i1);

            tree.RootNode.AddChildNode(new ParagraphMdSyntaxNode());
            tree.RootNode.AddChildNode(p2);

            tree.StoreChildAtCache(i1);

            return (tree, typeof(ItalicMdSyntaxNode), [
                i1
            ]);
        };
    }

    [Test]
    [MethodDataSource(nameof(GetCachedChildrenByTypeTestCases))]
    public async Task StoreCachedChildrenByType_ShouldCreateCorrectCache(MdSyntaxTree tree, Type cachedType, IMdSyntaxNode[] expectedChildren) {
        // Arrange & Act 
        bool result = tree.TryGetCachedChildrenByType(cachedType, out IEnumerable<IMdSyntaxNode>? nodes);
        IMdSyntaxNode[] cachedChildren = nodes?.ToArray() ?? [];
        
        // Assert
        await Assert.That(result).IsTrue();
        await Assert.That(cachedChildren).Count().IsEqualTo(expectedChildren.Length);

        for (int i = 0; i < expectedChildren.Length; i++) {
            IMdSyntaxNode cached = cachedChildren[i];
            IMdSyntaxNode expected = expectedChildren[i];

            await Assert.That(cached).IsSameReferenceAs(expected);
        }
    }
}
