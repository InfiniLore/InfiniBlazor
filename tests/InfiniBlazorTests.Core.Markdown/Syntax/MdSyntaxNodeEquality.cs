// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazor.Markdown.Syntax.Nodes;

namespace InfiniBlazorTests.Core.Markdown.Syntax;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdSyntaxNodeEqualityTests {
    public static IEnumerable<Func<(IMdSyntaxNode, IMdSyntaxNode, bool)>> TestCases() {
        // Positive cases (expected = true)
        yield return () => (
            new ParagraphMdSyntaxNode(),
            new ParagraphMdSyntaxNode(),
            true
        );

        yield return () => (
            new TextMdSyntaxNode().WithText("Hello world!"),
            new TextMdSyntaxNode().WithText("Hello world!"),
            true
        );

        yield return () => (
            new LinkMdSyntaxNode().WithHref("https://example.com"),
            new LinkMdSyntaxNode().WithHref("https://example.com"),
            true
        );

        yield return () => (
            new ImageMdSyntaxNode()
                .WithHref("https://example.com/image.jpg")
                .WithAltText("An Image"),
            new ImageMdSyntaxNode()
                .WithHref("https://example.com/image.jpg")
                .WithAltText("An Image"),
            true
        );

        yield return () => (
            new HtmlSpanMdSyntaxNode()
                .WithAttributes("class='test'"),
            new HtmlSpanMdSyntaxNode()
                .WithAttributes("class='test'"),
            true
        );

        yield return () => (
            new EmoteMdSyntaxNode()
                .WithEmoteKey(":smile:")
                .WithOriginalEmote("😊"),
            new EmoteMdSyntaxNode()
                .WithEmoteKey(":smile:")
                .WithOriginalEmote("😊"),
            true
        );

        yield return () => (
            new ListItemMdSyntaxNode()
                .WithIndex("1")
                .WithCheckMarker("x"),
            new ListItemMdSyntaxNode()
                .WithIndex("1")
                .WithCheckMarker("x"),
            true
        );

        yield return () => {
            var parentNode1 = new ParagraphMdSyntaxNode();
            parentNode1.AddChildNode(new TextMdSyntaxNode().WithText("Child Node"));
            var parentNode2 = new ParagraphMdSyntaxNode();
            parentNode2.AddChildNode(new TextMdSyntaxNode().WithText("Child Node"));

            return (parentNode1, parentNode2, true);
        };

        // Negative cases (expected = false)
        yield return () => (
            new ParagraphMdSyntaxNode(),
            new TextMdSyntaxNode(),
            false
        );

        yield return () => (
            new TextMdSyntaxNode().WithText("Hello"),
            new TextMdSyntaxNode().WithText("Hello world!"),
            false
        );

        yield return () => (
            new LinkMdSyntaxNode().WithHref("https://example.com"),
            new LinkMdSyntaxNode().WithHref("https://different.com"),
            false
        );

        yield return () => (
            new ImageMdSyntaxNode()
                .WithHref("https://example.com/image1.jpg")
                .WithAltText("An Image"),
            new ImageMdSyntaxNode()
                .WithHref("https://example.com/image2.jpg")
                .WithAltText("Another Image"),
            false
        );

        yield return () => (
            new HtmlSpanMdSyntaxNode()
                .WithAttributes("class='test'"),
            new HtmlSpanMdSyntaxNode()
                .WithAttributes("class='test'"),
            true
        );

        yield return () => (
            new EmoteMdSyntaxNode()
                .WithEmoteKey(":smile:")
                .WithOriginalEmote("😊"),
            new EmoteMdSyntaxNode()
                .WithEmoteKey(":laugh:")
                .WithOriginalEmote("😂"),
            false
        );

        yield return () => (
            new ListItemMdSyntaxNode()
                .WithIndex("1")
                .WithCheckMarker("x"),
            new ListItemMdSyntaxNode()
                .WithIndex("2")
                .WithCheckMarker("x"),
            false
        );

        yield return () => {
            var nodeWithChild1 = new ParagraphMdSyntaxNode();
            nodeWithChild1.AddChildNode(new TextMdSyntaxNode().WithText("First Child"));

            var nodeWithChild2 = new ParagraphMdSyntaxNode();
            nodeWithChild2.AddChildNode(new TextMdSyntaxNode().WithText("Second Child"));

            return (nodeWithChild1, nodeWithChild2, false);
        };

        // Mixed complexity
        yield return () => {
            var complexNode1 = new ListItemMdSyntaxNode()
                .WithIndex("1")
                .WithCheckMarker("x");

            complexNode1.AddChildNode(new EmoteMdSyntaxNode()
                .WithEmoteKey(":smile:"));

            var complexNode2 = new ListItemMdSyntaxNode()
                .WithIndex("1")
                .WithCheckMarker("x");

            complexNode2.AddChildNode(new EmoteMdSyntaxNode()
                .WithEmoteKey(":laugh:"));

            return (complexNode1, complexNode2, false);
        };

        yield return () => {
            var parentNode1 = new HeadingMdSyntaxNode().WithLevel(1);
            parentNode1.AddChildNode(new LinkMdSyntaxNode().WithHref("https://example.com"));
            var parentNode2 = new HeadingMdSyntaxNode().WithLevel(1);
            parentNode2.AddChildNode(new LinkMdSyntaxNode().WithHref("https://different.com"));

            return (parentNode1, parentNode2, false);
        };

        yield return () => {
            var listNode1 = new ListItemMdSyntaxNode()
                .WithIndex("1")
                .WithCheckMarker("o");

            listNode1.AddChildNode(new HtmlSpanMdSyntaxNode()
                .WithAttributes("style='color:red;'"));

            var listNode2 = new ListItemMdSyntaxNode()
                .WithIndex("1")
                .WithCheckMarker("o");

            listNode2.AddChildNode(new HtmlSpanMdSyntaxNode()
                .WithAttributes("style='color:blue;'"));

            return (listNode1, listNode2, false);
        };
    }

    [Test]
    [MethodDataSource(nameof(TestCases))]
    public async Task Equals_ShouldBeLikeExpected(IMdSyntaxNode node1, IMdSyntaxNode node2, bool expected) {
        // Arrange

        // Act
        bool result = node1.Equals(node2);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }
}