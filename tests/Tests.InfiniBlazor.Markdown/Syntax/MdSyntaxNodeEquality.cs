// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;

namespace Tests.InfiniBlazor.Markdown.Syntax;
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
            new ContentMdSyntaxNode().WithContent("Hello world!"),
            new ContentMdSyntaxNode().WithContent("Hello world!"),
            true
        );

        yield return () => (
            new LinkMdSyntaxNode { Href = "https://example.com" },
            new LinkMdSyntaxNode { Href = "https://example.com" },
            true
        );

        yield return () => (
            new ImageMdSyntaxNode {
                Href = "https://example.com/image.jpg",
                OriginalAltText = "An Image"
            },
            new ImageMdSyntaxNode {
                Href = "https://example.com/image.jpg",
                OriginalAltText = "An Image"
            },
            true
        );

        yield return () => (
            new HtmlSpanMdSyntaxNode {
                TagValue = "span",
                Attributes = "class='test'"
            },
            new HtmlSpanMdSyntaxNode {
                TagValue = "span",
                Attributes = "class='test'"
            },
            true
        );

        yield return () => (
            new EmoteMdSyntaxNode {
                EmoteKey = ":smile:",
                OriginalEmote = "😊"
            },
            new EmoteMdSyntaxNode {
                EmoteKey = ":smile:",
                OriginalEmote = "😊"
            },
            true
        );

        yield return () => (
            new ListItemMdSyntaxNode {
                IsCheckable = true,
                Index = "1",
                OriginalCheckMarker = "x"
            },
            new ListItemMdSyntaxNode {
                IsCheckable = true,
                Index = "1",
                OriginalCheckMarker = "x"
            },
            true
        );

        yield return () => {
            var parentNode1 = new ParagraphMdSyntaxNode();
            parentNode1.AddChildNode(new ContentMdSyntaxNode().WithContent("Child Node"));
            var parentNode2 = new ParagraphMdSyntaxNode();
            parentNode2.AddChildNode(new ContentMdSyntaxNode().WithContent("Child Node"));

            return (parentNode1, parentNode2, true);
        };

        // Negative cases (expected = false)
        yield return () => (
            new ParagraphMdSyntaxNode(),
            new ContentMdSyntaxNode(),
            false
        );

        yield return () => (
            new ContentMdSyntaxNode().WithContent("Hello"),
            new ContentMdSyntaxNode().WithContent("Hello world!"),
            false
        );

        yield return () => (
            new LinkMdSyntaxNode { Href = "https://example.com" },
            new LinkMdSyntaxNode { Href = "https://different.com" },
            false
        );

        yield return () => (
            new ImageMdSyntaxNode {
                Href = "https://example.com/image1.jpg",
                OriginalAltText = "An Image"
            },
            new ImageMdSyntaxNode {
                Href = "https://example.com/image2.jpg",
                OriginalAltText = "Another Image"
            },
            false
        );

        yield return () => (
            new HtmlSpanMdSyntaxNode {
                TagValue = "div",
                Attributes = "class='test'"
            },
            new HtmlSpanMdSyntaxNode {
                TagValue = "span",
                Attributes = "class='test'"
            },
            false
        );

        yield return () => (
            new EmoteMdSyntaxNode {
                EmoteKey = ":smile:",
                OriginalEmote = "😊"
            },
            new EmoteMdSyntaxNode {
                EmoteKey = ":laugh:",
                OriginalEmote = "😂"
            },
            false
        );

        yield return () => (
            new ListItemMdSyntaxNode {
                IsCheckable = true,
                Index = "1",
                OriginalCheckMarker = "x"
            },
            new ListItemMdSyntaxNode {
                IsCheckable = false,
                Index = "2",
                OriginalCheckMarker = "o"
            },
            false
        );

        yield return () => {
            var nodeWithChild1 = new ParagraphMdSyntaxNode();
            nodeWithChild1.AddChildNode(new ContentMdSyntaxNode().WithContent("First Child"));

            var nodeWithChild2 = new ParagraphMdSyntaxNode();
            nodeWithChild2.AddChildNode(new ContentMdSyntaxNode().WithContent("Second Child"));

            return (nodeWithChild1, nodeWithChild2, false);
        };

        // Mixed complexity
        yield return () => {
            var complexNode1 = new ListItemMdSyntaxNode {
                IsCheckable = true,
                Index = "1",
                OriginalCheckMarker = "x"
            };

            complexNode1.AddChildNode(new EmoteMdSyntaxNode {
                EmoteKey = ":smile:"
            });

            var complexNode2 = new ListItemMdSyntaxNode {
                IsCheckable = true,
                Index = "1",
                OriginalCheckMarker = "x"
            };

            complexNode2.AddChildNode(new EmoteMdSyntaxNode {
                EmoteKey = ":laugh:"
            });

            return (complexNode1, complexNode2, false);
        };

        yield return () => {
            var parentNode1 = new HeadingMdSyntaxNode { Level = 1 };
            parentNode1.AddChildNode(new LinkMdSyntaxNode { Href = "https://example.com" });
            var parentNode2 = new HeadingMdSyntaxNode { Level = 1 };
            parentNode2.AddChildNode(new LinkMdSyntaxNode { Href = "https://different.com" });

            return (parentNode1, parentNode2, false);
        };

        yield return () => {
            var listNode1 = new ListItemMdSyntaxNode {
                IsCheckable = false,
                Index = "1",
                OriginalCheckMarker = "o"
            };

            listNode1.AddChildNode(new HtmlSpanMdSyntaxNode { TagValue = "span", Attributes = "style='color:red;'" });

            var listNode2 = new ListItemMdSyntaxNode {
                IsCheckable = false,
                Index = "1",
                OriginalCheckMarker = "o"
            };

            listNode2.AddChildNode(new HtmlSpanMdSyntaxNode { TagValue = "span", Attributes = "style='color:blue;'" });

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
