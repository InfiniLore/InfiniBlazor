// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor;
using InfiniLore.InfiniBlazor.Markdown;
using Tests.InfiniLore.InfiniBlazor.Markdown.MarkdownParser.DataSources;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[DIDataSource]
public class MarkdownParserTests(IMarkdownParser parser) {

    // see https://spec-md.com/
    [Test]
    [MethodDataSource(typeof(AggregateDataSources), nameof(AggregateDataSources.DataSources))]
    [MethodDataSource(typeof(BlockQuoteDataSources), nameof(BlockQuoteDataSources.DataSources))]
    [MethodDataSource(typeof(BoldAndItalicDataSources), nameof(BoldAndItalicDataSources.DataSources))]
    [MethodDataSource(typeof(BoldDataSources), nameof(BoldDataSources.DataSources))]
    [MethodDataSource(typeof(CodeDataSources), nameof(CodeDataSources.DataSources))]
    [MethodDataSource(typeof(CodeInlineDataSources), nameof(CodeInlineDataSources.DataSources))]
    [MethodDataSource(typeof(EdgeCaseDataSources), nameof(EdgeCaseDataSources.DataSources))]
    [MethodDataSource(typeof(EmoteDataSources), nameof(EmoteDataSources.DataSources))]
    [MethodDataSource(typeof(EscapedCharacterDataSources), nameof(EscapedCharacterDataSources.DataSources))]
    [MethodDataSource(typeof(HeadingDataSources), nameof(HeadingDataSources.DataSources))]
    [MethodDataSource(typeof(HorizontalLineDataSources), nameof(HorizontalLineDataSources.DataSources))]
    [MethodDataSource(typeof(HtmlDataSources), nameof(HtmlDataSources.DataSources))]
    [MethodDataSource(typeof(ItalicDataSources), nameof(ItalicDataSources.DataSources))]
    [MethodDataSource(typeof(LinkDataSources), nameof(LinkDataSources.DataSources))]
    [MethodDataSource(typeof(ListsDataSources), nameof(ListsDataSources.DataSources))]
    [MethodDataSource(typeof(SpecialCharacterDataSources), nameof(SpecialCharacterDataSources.DataSources))]
    [MethodDataSource(typeof(StrikethroughDataSources), nameof(StrikethroughDataSources.DataSources))]
    [MethodDataSource(typeof(SubAndSuperScriptDataSources), nameof(SubAndSuperScriptDataSources.DataSources))]
    [MethodDataSource(typeof(SubScriptDataSources), nameof(SubScriptDataSources.DataSources))]
    [MethodDataSource(typeof(SuperScriptDataSources), nameof(SuperScriptDataSources.DataSources))]
    [MethodDataSource(typeof(TableDataSources), nameof(TableDataSources.DataSources))]
    [MethodDataSource(typeof(TagDataSources), nameof(TagDataSources.DataSources))]
    [MethodDataSource(typeof(UnderlineDataSources), nameof(UnderlineDataSources.DataSources))]
    [MethodDataSource(typeof(XSSDataSources), nameof(XSSDataSources.DataSources))]
    public async Task ParseToString_ValidInputs(MarkdownTestDto dto) {
        // Arrange
        
        // Act
        string output = parser.ParseToString(dto.Markdown);

        // Assert
        await Assert.That(output).IsEqualTo(dto.ExpectedStringOutput).IgnoringWhitespace();
    }

    [Test]
    [MethodDataSource(typeof(AggregateDataSources), nameof(AggregateDataSources.DataSources))]
    [MethodDataSource(typeof(BlockQuoteDataSources), nameof(BlockQuoteDataSources.DataSources))]
    [MethodDataSource(typeof(BoldAndItalicDataSources), nameof(BoldAndItalicDataSources.DataSources))]
    [MethodDataSource(typeof(BoldDataSources), nameof(BoldDataSources.DataSources))]
    [MethodDataSource(typeof(CodeDataSources), nameof(CodeDataSources.DataSources))]
    [MethodDataSource(typeof(CodeInlineDataSources), nameof(CodeInlineDataSources.DataSources))]
    [MethodDataSource(typeof(EdgeCaseDataSources), nameof(EdgeCaseDataSources.DataSources))]
    [MethodDataSource(typeof(EmoteDataSources), nameof(EmoteDataSources.DataSources))]
    [MethodDataSource(typeof(EscapedCharacterDataSources), nameof(EscapedCharacterDataSources.DataSources))]
    [MethodDataSource(typeof(HeadingDataSources), nameof(HeadingDataSources.DataSources))]
    [MethodDataSource(typeof(HorizontalLineDataSources), nameof(HorizontalLineDataSources.DataSources))]
    [MethodDataSource(typeof(HtmlDataSources), nameof(HtmlDataSources.DataSources))]
    [MethodDataSource(typeof(ItalicDataSources), nameof(ItalicDataSources.DataSources))]
    [MethodDataSource(typeof(LinkDataSources), nameof(LinkDataSources.DataSources))]
    [MethodDataSource(typeof(ListsDataSources), nameof(ListsDataSources.DataSources))]
    [MethodDataSource(typeof(SpecialCharacterDataSources), nameof(SpecialCharacterDataSources.DataSources))]
    [MethodDataSource(typeof(StrikethroughDataSources), nameof(StrikethroughDataSources.DataSources))]
    [MethodDataSource(typeof(SubAndSuperScriptDataSources), nameof(SubAndSuperScriptDataSources.DataSources))]
    [MethodDataSource(typeof(SubScriptDataSources), nameof(SubScriptDataSources.DataSources))]
    [MethodDataSource(typeof(SuperScriptDataSources), nameof(SuperScriptDataSources.DataSources))]
    [MethodDataSource(typeof(TableDataSources), nameof(TableDataSources.DataSources))]
    [MethodDataSource(typeof(TagDataSources), nameof(TagDataSources.DataSources))]
    [MethodDataSource(typeof(UnderlineDataSources), nameof(UnderlineDataSources.DataSources))]
    [MethodDataSource(typeof(XSSDataSources), nameof(XSSDataSources.DataSources))]
    public async Task Parse_ValidInputs_WithTextWriter(MarkdownTestDto dto) {
        // Arrange

        // Act
        await using var writer = new StringWriter();
        parser.ParseToWriter(dto.Markdown, writer);
        string output = writer.ToString();

        // Assert
        await Assert.That(output).IsEqualTo(dto.ExpectedStringOutput).IgnoringWhitespace();
    }

    [Test]
    [MethodDataSource(typeof(AggregateDataSources), nameof(AggregateDataSources.DataSources))]
    [MethodDataSource(typeof(BlockQuoteDataSources), nameof(BlockQuoteDataSources.DataSources))]
    [MethodDataSource(typeof(BoldAndItalicDataSources), nameof(BoldAndItalicDataSources.DataSources))]
    [MethodDataSource(typeof(BoldDataSources), nameof(BoldDataSources.DataSources))]
    [MethodDataSource(typeof(CodeDataSources), nameof(CodeDataSources.DataSources))]
    [MethodDataSource(typeof(CodeInlineDataSources), nameof(CodeInlineDataSources.DataSources))]
    [MethodDataSource(typeof(EdgeCaseDataSources), nameof(EdgeCaseDataSources.DataSources))]
    [MethodDataSource(typeof(EmoteDataSources), nameof(EmoteDataSources.DataSources))]
    [MethodDataSource(typeof(EscapedCharacterDataSources), nameof(EscapedCharacterDataSources.DataSources))]
    [MethodDataSource(typeof(HeadingDataSources), nameof(HeadingDataSources.DataSources))]
    [MethodDataSource(typeof(HorizontalLineDataSources), nameof(HorizontalLineDataSources.DataSources))]
    [MethodDataSource(typeof(HtmlDataSources), nameof(HtmlDataSources.DataSources))]
    [MethodDataSource(typeof(ItalicDataSources), nameof(ItalicDataSources.DataSources))]
    [MethodDataSource(typeof(LinkDataSources), nameof(LinkDataSources.DataSources))]
    [MethodDataSource(typeof(ListsDataSources), nameof(ListsDataSources.DataSources))]
    [MethodDataSource(typeof(SpecialCharacterDataSources), nameof(SpecialCharacterDataSources.DataSources))]
    [MethodDataSource(typeof(StrikethroughDataSources), nameof(StrikethroughDataSources.DataSources))]
    [MethodDataSource(typeof(SubAndSuperScriptDataSources), nameof(SubAndSuperScriptDataSources.DataSources))]
    [MethodDataSource(typeof(SubScriptDataSources), nameof(SubScriptDataSources.DataSources))]
    [MethodDataSource(typeof(SuperScriptDataSources), nameof(SuperScriptDataSources.DataSources))]
    [MethodDataSource(typeof(TableDataSources), nameof(TableDataSources.DataSources))]
    [MethodDataSource(typeof(TagDataSources), nameof(TagDataSources.DataSources))]
    [MethodDataSource(typeof(UnderlineDataSources), nameof(UnderlineDataSources.DataSources))]
    [MethodDataSource(typeof(XSSDataSources), nameof(XSSDataSources.DataSources))]
    public async Task ParseToNodeTree_ValidInputs(MarkdownTestDto dto) {
        // Arrange
        
        // Act
        IMdNode output = parser.ParseToNodeTree(dto.Markdown);
        Skip.When(dto.ExpectedNode == null, "The node tree is null and thus cannot be compared.");

        // Assert
        await Assert.That(output).IsEquivalentTo(dto.ExpectedNode);
    }
}