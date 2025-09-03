// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Parsers.Xml;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;
using System.Xml.Linq;

namespace Tests.InfiniBlazor.Markdown.Parsers.Xml;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdSyntaxTreeXmlParserTests {
    private readonly XmlMdSyntaxTreeParser _parser = new();
    private static IMdSyntaxTree TestTree => new MdSyntaxTree {
        RootNode = new RootMdSyntaxNode()
            .WithChild(
                new LinkMdSyntaxNode()
                    .WithHref("https://example.com")
                    .WithText("Example Content")
                    .WithChild(new ImageMdSyntaxNode()
                        .WithHref("https://example.com/image.png")
                        .WithAltText("Example Image"))
            )
    };

    private const string Xml = """
        <MdSyntaxTree>
            <LinkMdSyntaxNode Href="https://example.com">
                <TextMdSyntaxNode xml:space="preserve">Example Content</TextMdSyntaxNode>
                <ImageMdSyntaxNode Href="https://example.com/image.png" AltText="Example Image" />
            </LinkMdSyntaxNode>
        </MdSyntaxTree>
        """;
    
    private static readonly string FilePathOutput = $"{Guid.NewGuid():N}.xml";
    private static readonly string FilePathInput = $"{Guid.NewGuid():N}.xml";


    // -----------------------------------------------------------------------------------------------------------------
    // Test Methods
    // -----------------------------------------------------------------------------------------------------------------
    [Before(Class)]
    [After(Class)]
    public static void FileSetup() {
        if (File.Exists(FilePathOutput)) File.Delete(FilePathOutput);
        if (File.Exists(FilePathInput)) File.Delete(FilePathInput);   
    }
    
    [Test]
    public async Task DeserializeToStreamAsync_ShouldWriteXmlCorrectly() {
        // Arrange
        XElement expected = XElement.Parse(Xml);

        // Act
        MemoryStream memoryStream = new();
        await _parser.DeserializeToXmlStreamAsync(memoryStream, TestTree);
        memoryStream.Position = 0;
        string result = await new StreamReader(memoryStream).ReadToEndAsync();
        XElement resultXml = XElement.Parse(result);
        // Assert


        await Assert.That(resultXml.ToString(SaveOptions.DisableFormatting)).IsEqualTo(expected.ToString(SaveOptions.DisableFormatting));
    }

    [Test]
    public async Task DeserializeToFileAsync_ShouldCreateFileWithCorrectXml() {
        // Arrange

        // Act
        await _parser.DeserializeToXmlFileAsync(FilePathOutput, TestTree);

        // Assert
        await Assert.That(File.Exists(FilePathOutput)).IsTrue();
        string result = await File.ReadAllTextAsync(FilePathOutput);

        XElement expected = XElement.Parse(Xml);

        XElement resultXml = XElement.Parse(result);
        await Assert.That(resultXml.ToString(SaveOptions.DisableFormatting)).IsEqualTo(expected.ToString(SaveOptions.DisableFormatting));

        // Cleanup
        File.Delete(FilePathOutput);
    }

    [Test]
    public async Task DeserializeFromStreamAsync_ShouldBuildCorrectTree() {
        // Arrange
        using MemoryStream stream = new(Encoding.UTF8.GetBytes(Xml));

        // Act
        IMdSyntaxTree tree = await _parser.SerializeToSyntaxTreeAsync(stream);

        // Assert
        await Assert.That(tree.RootNode).IsNotNull();
        await Assert.That(tree.RootNode.GetChildAt(0)).IsTypeOf<LinkMdSyntaxNode>();

        var listNode = (LinkMdSyntaxNode)tree.RootNode.GetChildAt(0);
        await Assert.That(listNode.Href).IsEqualTo("https://example.com");

        List<IMdSyntaxNode> children = listNode.GetChildren().ToList();
        await Assert.That(children).HasCount(2);

        IMdSyntaxNode firstChild = children[0];
        await Assert.That(firstChild).IsNotNull();
        await Assert.That(firstChild).IsTypeOf<TextMdSyntaxNode>();
        await Assert.That(((TextMdSyntaxNode)firstChild).Content).IsEqualTo("Example Content");

        IMdSyntaxNode secondChild = children[1];
        await Assert.That(secondChild).IsNotNull();
        await Assert.That(secondChild).IsTypeOf<ImageMdSyntaxNode>();
        var imageNode = (ImageMdSyntaxNode)secondChild;
        await Assert.That(imageNode.Href).IsEqualTo("https://example.com/image.png");
        await Assert.That(imageNode.NormalizedAltText).IsEqualTo("Example Image");
    }

    [Test]
    public async Task DeserializeFromFileAsync_ShouldBuildCorrectTree() {
        // Arrange
        await File.WriteAllTextAsync(FilePathInput, Xml);

        // Act
        IMdSyntaxTree tree = await _parser.SerializeToSyntaxTreeAsync(FilePathInput);

        // Assert
        await Assert.That(tree.RootNode).IsNotNull();
        await Assert.That(tree.RootNode.GetChildAt(0)).IsTypeOf<LinkMdSyntaxNode>();

        var listNode = (LinkMdSyntaxNode)tree.RootNode.GetChildAt(0);
        await Assert.That(listNode.Href).IsEqualTo("https://example.com");

        List<IMdSyntaxNode> children = listNode.GetChildren().ToList();
        await Assert.That(children).HasCount(2);

        IMdSyntaxNode firstChild = children[0];
        await Assert.That(firstChild).IsNotNull();
        await Assert.That(firstChild).IsTypeOf<TextMdSyntaxNode>();
        await Assert.That(((TextMdSyntaxNode)firstChild).Content).IsEqualTo("Example Content");

        IMdSyntaxNode secondChild = children[1];
        await Assert.That(secondChild).IsNotNull();
        await Assert.That(secondChild).IsTypeOf<ImageMdSyntaxNode>();
        var imageNode = (ImageMdSyntaxNode)secondChild;
        await Assert.That(imageNode.Href).IsEqualTo("https://example.com/image.png");
        await Assert.That(imageNode.NormalizedAltText).IsEqualTo("Example Image");

        // Cleanup
        File.Delete(FilePathInput);
    }

    [Test]
    public async Task DeserializeSerialize_ShouldPreserveTreeStructure() {
        // Arrange
        var parser = new XmlMdSyntaxTreeParser();

        // Create a sample syntax tree
        IMdSyntaxTree originalTree = TestTree;

        // Serialize to memory stream
        await using var memoryStream = new MemoryStream();
        await parser.DeserializeToXmlStreamAsync(memoryStream, originalTree);

        // Reset the memory stream for reading
        memoryStream.Position = 0;

        // Deserialize back into a syntax tree
        IMdSyntaxTree deserializedTree = await parser.SerializeToSyntaxTreeAsync(memoryStream);

        // Assert: Ensure the structure and content remain identical
        await Assert.That(originalTree).IsEquivalentTo(deserializedTree);
    }

}
