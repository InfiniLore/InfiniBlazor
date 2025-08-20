// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Parsers.Xml;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;
using System.Xml.Linq;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.Parsers.Xml;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdSyntaxTreeXmlParserTests {
    private readonly MdSyntaxTreeXmlParser _parser = new();
    private static IMdSyntaxTree TestTree => new MdSyntaxTree {
        RootNode = new RootMdSyntaxNode()
            .WithChild(
                new LinkMdSyntaxNode { Href = "https://example.com" }
                    .WithContent("Example Content")
                    .WithChild(new ImageMdSyntaxNode { Href = "https://example.com/image.png", AltText = "Example Image" })
            )
    };

    private const string Xml = """
        <MdSyntaxTree>
            <LinkMdSyntaxNode Href="https://example.com">
                <ContentMdSyntaxNode>Example Content</ContentMdSyntaxNode>
                <ImageMdSyntaxNode Href="https://example.com/image.png" AltText="Example Image" />
            </LinkMdSyntaxNode>
        </MdSyntaxTree>
        """;


    // -----------------------------------------------------------------------------------------------------------------
    // Test Methods
    // -----------------------------------------------------------------------------------------------------------------
    [Test]
    public async Task SerializeToStreamAsync_ShouldWriteXmlCorrectly() {
        // Arrange
        MemoryStream memoryStream = new();

        // Act
        await _parser.SerializeToStreamAsync(memoryStream, TestTree);

        // Assert
        memoryStream.Position = 0;
        string result = await new StreamReader(memoryStream).ReadToEndAsync();

        XElement expected = XElement.Parse(Xml);

        XElement resultXml = XElement.Parse(result);
        await Assert.That(resultXml.ToString(SaveOptions.DisableFormatting)).IsEqualTo(expected.ToString(SaveOptions.DisableFormatting));
    }

    [Test]
    public async Task SerializeToFileAsync_ShouldCreateFileWithCorrectXml() {
        // Arrange
        string filePath = "output.xml";

        // Act
        await _parser.SerializeToFileAsync(filePath, TestTree);

        // Assert
        await Assert.That(File.Exists(filePath)).IsTrue();
        string result = await File.ReadAllTextAsync(filePath);

        XElement expected = XElement.Parse(Xml);

        XElement resultXml = XElement.Parse(result);
        await Assert.That(resultXml.ToString(SaveOptions.DisableFormatting)).IsEqualTo(expected.ToString(SaveOptions.DisableFormatting));

        // Cleanup
        File.Delete(filePath);
    }

    [Test]
    public async Task DeserializeFromStreamAsync_ShouldBuildCorrectTree() {
        // Arrange
        using MemoryStream stream = new(Encoding.UTF8.GetBytes(Xml));

        // Act
        IMdSyntaxTree tree = await _parser.DeserializeFromStreamAsync(stream);

        // Assert
        await Assert.That(tree.RootNode).IsNotNull();
        await Assert.That(tree.RootNode.GetChildAt(0)).IsTypeOf<LinkMdSyntaxNode>();

        var listNode = (LinkMdSyntaxNode)tree.RootNode.GetChildAt(0);
        await Assert.That(listNode.Href).IsEqualTo("https://example.com");

        List<IMdSyntaxNode> children = listNode.GetChildren().ToList();
        await Assert.That(children).HasCount(2);

        IMdSyntaxNode firstChild = children[0];
        await Assert.That(firstChild).IsNotNull();
        await Assert.That(firstChild).IsTypeOf<ContentMdSyntaxNode>();
        await Assert.That(((ContentMdSyntaxNode)firstChild).Content).IsEqualTo("Example Content");

        IMdSyntaxNode secondChild = children[1];
        await Assert.That(secondChild).IsNotNull();
        await Assert.That(secondChild).IsTypeOf<ImageMdSyntaxNode>();
        var imageNode = (ImageMdSyntaxNode)secondChild;
        await Assert.That(imageNode.Href).IsEqualTo("https://example.com/image.png");
        await Assert.That(imageNode.AltText).IsEqualTo("Example Image");
    }

    [Test]
    public async Task DeserializeFromFileAsync_ShouldBuildCorrectTree() {
        // Arrange
        string filePath = "input.xml";
        await File.WriteAllTextAsync(filePath, Xml);

        // Act
        IMdSyntaxTree tree = await _parser.DeserializeFromFileAsync(filePath);

        // Assert
        await Assert.That(tree.RootNode).IsNotNull();
        await Assert.That(tree.RootNode.GetChildAt(0)).IsTypeOf<LinkMdSyntaxNode>();

        var listNode = (LinkMdSyntaxNode)tree.RootNode.GetChildAt(0);
        await Assert.That(listNode.Href).IsEqualTo("https://example.com");

        List<IMdSyntaxNode> children = listNode.GetChildren().ToList();
        await Assert.That(children).HasCount(2);

        IMdSyntaxNode firstChild = children[0];
        await Assert.That(firstChild).IsNotNull();
        await Assert.That(firstChild).IsTypeOf<ContentMdSyntaxNode>();
        await Assert.That(((ContentMdSyntaxNode)firstChild).Content).IsEqualTo("Example Content");

        IMdSyntaxNode secondChild = children[1];
        await Assert.That(secondChild).IsNotNull();
        await Assert.That(secondChild).IsTypeOf<ImageMdSyntaxNode>();
        var imageNode = (ImageMdSyntaxNode)secondChild;
        await Assert.That(imageNode.Href).IsEqualTo("https://example.com/image.png");
        await Assert.That(imageNode.AltText).IsEqualTo("Example Image");

        // Cleanup
        File.Delete(filePath);
    }

    [Test]
    public async Task SerializeDeserialize_ShouldPreserveTreeStructure() {
        // Arrange
        var parser = new MdSyntaxTreeXmlParser();

        // Create a sample syntax tree
        IMdSyntaxTree originalTree = TestTree;

        // Serialize to memory stream
        await using var memoryStream = new MemoryStream();
        await parser.SerializeToStreamAsync(memoryStream, originalTree);

        // Reset the memory stream for reading
        memoryStream.Position = 0;

        // Deserialize back into a syntax tree
        IMdSyntaxTree deserializedTree = await parser.DeserializeFromStreamAsync(memoryStream);

        // Assert: Ensure the structure and content remain identical
        await Assert.That(originalTree).IsEquivalentTo(deserializedTree);
    }

}
