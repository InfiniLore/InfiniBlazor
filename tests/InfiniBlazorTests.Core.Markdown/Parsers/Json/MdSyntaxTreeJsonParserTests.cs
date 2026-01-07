// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Parsers.Json;
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;
using System.Text.Json;

namespace InfiniBlazorTests.Core.Markdown.Parsers.Json;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdSyntaxTreeJsonParserTests {
    private readonly JsonMdSyntaxTreeParser _parser = new();
    private static IMdSyntaxTree TestTree {
        get {
            var tree = new MdSyntaxTree();
            tree.RootNode.WithChild(
                new LinkMdSyntaxNode()
                    .WithHref("https://example.com")
                    .WithText("Example Content")
                    .WithChild(new ImageMdSyntaxNode()
                        .WithHref("https://example.com/image.png")
                        .WithAltText("Example Image"))
            );
            return tree;
        }
    }

    private const string Json = """
        {
          "type": "MdSyntaxTree",
          "children": [
            {
              "type": "LinkMdSyntaxNode",
              "href": "https://example.com",
              "children": [
                {
                  "type": "TextMdSyntaxNode",
                  "content": "Example Content"
                },
                {
                  "type": "ImageMdSyntaxNode",
                  "href": "https://example.com/image.png",
                  "altText": "Example Image"
                }
              ]
            }
          ]
        }
        """;

    private static readonly string FilePathOutput = $"{Guid.NewGuid():N}.json";
    private static readonly string FilePathInput = $"{Guid.NewGuid():N}.json";

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
    public async Task DeserializeToStreamAsync_ShouldWriteJsonCorrectly() {
        // Arrange
        JsonDocument expected = JsonDocument.Parse(Json);

        // Act
        MemoryStream memoryStream = new();
        await _parser.DeserializeToJsonStreamAsync(memoryStream, TestTree);
        memoryStream.Position = 0;
        string result = await new StreamReader(memoryStream).ReadToEndAsync();
        JsonDocument resultJson = JsonDocument.Parse(result);

        // Assert
        string expectedJson = JsonSerializer.Serialize(expected.RootElement, new JsonSerializerOptions { WriteIndented = false });
        string actualJson = JsonSerializer.Serialize(resultJson.RootElement, new JsonSerializerOptions { WriteIndented = false });

        await Assert.That(actualJson).IsEqualTo(expectedJson);
    }

    [Test]
    public async Task DeserializeToFileAsync_ShouldCreateFileWithCorrectJson() {
        // Arrange
        JsonDocument expected = JsonDocument.Parse(Json);

        // Act
        await _parser.DeserializeToJsonFileAsync(FilePathOutput, TestTree);

        // Assert
        await Assert.That(File.Exists(FilePathOutput)).IsTrue();
        string result = await File.ReadAllTextAsync(FilePathOutput);

        JsonDocument resultJson = JsonDocument.Parse(result);
        string expectedJson = JsonSerializer.Serialize(expected.RootElement, new JsonSerializerOptions { WriteIndented = false });
        string actualJson = JsonSerializer.Serialize(resultJson.RootElement, new JsonSerializerOptions { WriteIndented = false });

        await Assert.That(actualJson).IsEqualTo(expectedJson);

        // Cleanup
        File.Delete(FilePathOutput);
    }

    [Test]
    public async Task DeserializeFromStreamAsync_ShouldBuildCorrectTree() {
        // Arrange
        using MemoryStream stream = new(Encoding.UTF8.GetBytes(Json));

        // Act
        IMdSyntaxTree tree = await _parser.SerializeToSyntaxTreeAsync(stream);

        // Assert
        await Assert.That(tree.RootNode).IsNotNull();
        await Assert.That(tree.RootNode.GetChildAt(0)).IsTypeOf<LinkMdSyntaxNode>();

        var linkNode = (LinkMdSyntaxNode)tree.RootNode.GetChildAt(0);
        await Assert.That(linkNode.Href).IsEqualTo("https://example.com");

        List<IMdSyntaxNode> children = linkNode.GetChildren().ToList();
        await Assert.That(children).Count().IsEqualTo(2);

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
        await File.WriteAllTextAsync(FilePathInput, Json);

        // Act
        IMdSyntaxTree tree = await _parser.SerializeToSyntaxTreeAsync(FilePathInput);

        // Assert
        await Assert.That(tree.RootNode).IsNotNull();
        await Assert.That(tree.RootNode.GetChildAt(0)).IsTypeOf<LinkMdSyntaxNode>();

        var linkNode = (LinkMdSyntaxNode)tree.RootNode.GetChildAt(0);
        await Assert.That(linkNode.Href).IsEqualTo("https://example.com");

        List<IMdSyntaxNode> children = linkNode.GetChildren().ToList();
        await Assert.That(children).Count().IsEqualTo(2);

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
        var parser = new JsonMdSyntaxTreeParser();

        // Create a sample syntax tree
        IMdSyntaxTree originalTree = TestTree;

        // Serialize to memory stream
        await using var memoryStream = new MemoryStream();
        await parser.DeserializeToJsonStreamAsync(memoryStream, originalTree);

        // Reset the memory stream for reading
        memoryStream.Position = 0;

        // Deserialize back into a syntax tree
        IMdSyntaxTree deserializedTree = await parser.SerializeToSyntaxTreeAsync(memoryStream);

        // Assert: Ensure the structure and content remain identical
        IMdSyntaxNode originalRootNode = originalTree.RootNode;
        IMdSyntaxNode deserializedRootNode = deserializedTree.RootNode;
        await Assert.That(originalRootNode).IsEqualTo(deserializedRootNode);
    }

    [Test]
    public async Task DeserializeToJsonElement_ShouldReturnCorrectStructure() {
        // Act
        JsonElement result = _parser.DeserializeToJsonElement(TestTree);

        // Assert
        await Assert.That(result.GetProperty("type").GetString()).IsEqualTo("MdSyntaxTree");
        await Assert.That(result.GetProperty("children").GetArrayLength()).IsEqualTo(1);

        JsonElement linkElement = result.GetProperty("children")[0];
        await Assert.That(linkElement.GetProperty("type").GetString()).IsEqualTo("LinkMdSyntaxNode");
        await Assert.That(linkElement.GetProperty("href").GetString()).IsEqualTo("https://example.com");

        await Assert.That(linkElement.GetProperty("children").GetArrayLength()).IsEqualTo(2);

        JsonElement textElement = linkElement.GetProperty("children")[0];
        await Assert.That(textElement.GetProperty("type").GetString()).IsEqualTo("TextMdSyntaxNode");
        await Assert.That(textElement.GetProperty("content").GetString()).IsEqualTo("Example Content");

        JsonElement imageElement = linkElement.GetProperty("children")[1];
        await Assert.That(imageElement.GetProperty("type").GetString()).IsEqualTo("ImageMdSyntaxNode");
        await Assert.That(imageElement.GetProperty("href").GetString()).IsEqualTo("https://example.com/image.png");
        await Assert.That(imageElement.GetProperty("altText").GetString()).IsEqualTo("Example Image");
    }
}
