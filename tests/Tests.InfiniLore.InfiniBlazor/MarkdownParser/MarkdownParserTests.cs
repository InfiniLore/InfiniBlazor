// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax;
using InfiniLore.InfiniBlazor.TextEditor;
using Tests.InfiniLore.InfiniBlazor.DataSources;

namespace Tests.InfiniLore.InfiniBlazor.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[DiDataSource]
public class MarkdownParserTests(IMdSyntaxParser syntaxParser, IMdSyntaxTreeConverter treeConverter) {
    
    [Test]
    [MethodDataSource(typeof(MarkdownDataSources), nameof(MarkdownDataSources.DataSources))]
    public async Task TryParse_String_ValidInputs(MdTestData data) {
        // Arrange
        MdSyntaxTree tree = MdSyntaxTree.Pool.Get();
        
        // Act
        syntaxParser.ParseToTree(data.Markdown, tree);
        string output = treeConverter.ConvertToString(tree);

        // Assert;
        await Assert.That(output)
            .IsNotNullOrWhitespace()
            .IsEqualTo(data.ExpectedStringOutput).IgnoringWhitespace();
        
        MdSyntaxTree.Pool.Return(tree);
    }
    
    [Test]
    [MethodDataSource(typeof(MarkdownDataSources), nameof(MarkdownDataSources.DataSources))]
    public async Task TryParse_TextSource_ValidInputs(MdTestData data) {
        // Arrange
        MdSyntaxTree tree = MdSyntaxTree.Pool.Get();
        var textSource = new TextSource {
            Text = data.Markdown
        };

        // Act
        syntaxParser.ParseToTree(textSource.Text, tree);
        string output = treeConverter.ConvertToString(tree);

        // Assert
        await Assert.That(output)
            .IsNotNullOrWhitespace()
            .IsEqualTo(data.ExpectedStringOutput).IgnoringWhitespace();
        
        MdSyntaxTree.Pool.Return(tree);
    }
}
