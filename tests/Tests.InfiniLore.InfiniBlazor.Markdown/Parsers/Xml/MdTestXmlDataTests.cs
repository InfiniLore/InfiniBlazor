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
public class MdTestXmlDataTests {
    private const string FileName = "testData.xml";
    private const string FileNameArray = "testDataArray.xml";
    private static MdTestXmlData TestEntry => new() {
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
    [Before(Test)]
    [After(Test)]
    public void FileSetup() {
        if (File.Exists(FileName)) File.Delete(FileName);
        if (File.Exists(FileNameArray)) File.Delete(FileNameArray);
    }

    [Test]
    public async Task SerializeDeserializeTest() {
        // Arrange
        MdTestXmlData testEntry = TestEntry;

        // Act
        var serializer = new XmlSerializer(typeof(MdTestXmlData));
        await using (StreamWriter writer = new(FileName)) {
            serializer.Serialize(writer, testEntry);
        }

        MdTestXmlData? deserializedData;
        using (StreamReader reader = new(FileName)) {
            deserializedData = (MdTestXmlData?)serializer.Deserialize(reader);
        }
        
        // Assert
        await Assert.That(deserializedData)
            .IsNotNull()
            .IsEquivalentTo(testEntry);
    }

    [Test]
    public async Task SerializeDeserializeTest_IntoArray() {
        // Arrange
        MdTestXmlData[] testEntry = [
            TestEntry,
            TestEntry,
            TestEntry
        ];

        // Act
        var serializer = new XmlSerializer(typeof(MdTestXmlData[]));
        await using (StreamWriter writer = new(FileNameArray)) {
            serializer.Serialize(writer, testEntry);
        }

        MdTestXmlData[]? deserializedData;
        using (StreamReader reader = new(FileNameArray)) {
            deserializedData = (MdTestXmlData[]?)serializer.Deserialize(reader);
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
