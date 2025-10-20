// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Xml.Serialization;
using Tests.InfiniBlazor.Shared.Markdown;

namespace Tests.InfiniBlazor.Core.Markdown.Parsers.Xml;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class XmlMdTestDataTests {
    private static readonly string FileName = $"{Guid.NewGuid():N}.xml";
    private static readonly string FileNameArray = $"{Guid.NewGuid():N}.xml";
    private static MdTestData TestEntry => new() {
        Id = nameof(TestEntry),
        FileName = string.Empty,
        MdString = "Sample **Markdown**",
        MdSyntaxTree = new MdSyntaxTree {
            RootNode = new RootMdSyntaxNode()
                .WithText("Sample ")
                .WithChild(
                    new BoldMdSyntaxNode().WithText("Markdown")
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
        MdTestData testEntry = TestEntry;

        // Act
        var serializer = new XmlSerializer(typeof(MdTestData));
        await using (StreamWriter writer = new(FileName)) {
            serializer.Serialize(writer, testEntry);
        }

        MdTestData? deserializedData;
        using (StreamReader reader = new(FileName)) {
            deserializedData = (MdTestData?)serializer.Deserialize(reader);
        }
        
        // Assert
        await Assert.That(deserializedData)
            .IsNotNull()
            .And.IsEqualTo(testEntry);
        
        await Assert.That(deserializedData?.Id).IsEqualTo(nameof(TestEntry));
    }

    [Test]
    public async Task SerializeDeserializeTest_IntoArray() {
        // Arrange
        MdTestData[] testEntry = [
            TestEntry,
            TestEntry,
            TestEntry
        ];

        // Act
        var serializer = new XmlSerializer(typeof(MdTestData[]));
        await using (StreamWriter writer = new(FileNameArray)) {
            serializer.Serialize(writer, testEntry);
        }

        MdTestData[]? deserializedData;
        using (StreamReader reader = new(FileNameArray)) {
            deserializedData = (MdTestData[]?)serializer.Deserialize(reader);
        }
        
        // Assert
        await Assert.That(deserializedData)
            .IsNotNull()
            .And.IsNotEmpty()
            .And.HasCount(3);

        await Assert.That(deserializedData![0]).IsEqualTo(TestEntry);
        await Assert.That(deserializedData[1]).IsEqualTo(TestEntry);
        await Assert.That(deserializedData[2]).IsEqualTo(TestEntry);
    }
}
