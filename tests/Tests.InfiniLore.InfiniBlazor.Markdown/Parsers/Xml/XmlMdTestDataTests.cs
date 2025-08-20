// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Xml.Serialization;
using Tests.InfiniLore.InfiniBlazor.Markdown._DataSources;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.Parsers.Xml;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class XmlMdTestDataTests {
    private static readonly string FileName = $"{Guid.NewGuid():N}.xml";
    private static readonly string FileNameArray = $"{Guid.NewGuid():N}.xml";
    private static XmlMdTestData TestEntry => new() {
        Id = nameof(TestEntry),
        MarkdownString = "Sample **Markdown**",
        SyntaxTree = new MdSyntaxTree {
            RootNode = new RootMdSyntaxNode()
                .WithContent("Sample ")
                .WithChild(
                    new BoldMdSyntaxNode().WithContent("Markdown")
                )
        }
    };

    // -----------------------------------------------------------------------------------------------------------------
    // Test Methods
    // -----------------------------------------------------------------------------------------------------------------
    [Before(Class)]
    [After(Class)]
    public static void FileSetup() {
        if (File.Exists(FileName)) File.Delete(FileName);
        if (File.Exists(FileNameArray)) File.Delete(FileNameArray);
    }

    [Test]
    public async Task SerializeDeserializeTest() {
        // Arrange
        XmlMdTestData testEntry = TestEntry;

        // Act
        var serializer = new XmlSerializer(typeof(XmlMdTestData));
        await using (StreamWriter writer = new(FileName)) {
            serializer.Serialize(writer, testEntry);
        }

        XmlMdTestData? deserializedData;
        using (StreamReader reader = new(FileName)) {
            deserializedData = (XmlMdTestData?)serializer.Deserialize(reader);
        }
        
        // Assert
        await Assert.That(deserializedData)
            .IsNotNull()
            .IsEquivalentTo(testEntry);
        
        await Assert.That(deserializedData?.Id).IsEqualTo(nameof(TestEntry));
    }

    [Test]
    public async Task SerializeDeserializeTest_IntoArray() {
        // Arrange
        XmlMdTestData[] testEntry = [
            TestEntry,
            TestEntry,
            TestEntry
        ];

        // Act
        var serializer = new XmlSerializer(typeof(XmlMdTestData[]));
        await using (StreamWriter writer = new(FileNameArray)) {
            serializer.Serialize(writer, testEntry);
        }

        XmlMdTestData[]? deserializedData;
        using (StreamReader reader = new(FileNameArray)) {
            deserializedData = (XmlMdTestData[]?)serializer.Deserialize(reader);
        }
        
        // Assert
        await Assert.That(deserializedData)
            .IsNotNull()
            .IsNotEmpty()
            .HasCount(3)
            .IsEquivalentTo([
                TestEntry,
                TestEntry,
                TestEntry
            ]);
    }
}
