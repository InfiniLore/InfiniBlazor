// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Tests.InfiniLore.InfiniBlazor.DataSources.MarkdownParser;

namespace Tests.InfiniLore.InfiniBlazor.DataSources;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MarkdownDataSources {
    public static IEnumerable<Func<MdTestData>> DataSources() {
        List<IEnumerable<Func<MdTestData>>> sources = [
            AggregateDataSources.DataSources(),
            BlockQuoteDataSources.DataSources(),
            BoldAndItalicDataSources.DataSources(),
            BoldDataSources.DataSources(),
            CodeDataSources.DataSources(),
            CodeInlineDataSources.DataSources(),
            EdgeCaseDataSources.DataSources(),
            EmoteDataSources.DataSources(),
            EscapedCharacterDataSources.DataSources(),
            HeadingDataSources.DataSources(),
            HorizontalLineDataSources.DataSources(),
            HtmlDataSources.DataSources(),
            ItalicDataSources.DataSources(),
            LinkDataSources.DataSources(),
            ListsDataSources.DataSources(),
            SpecialCharacterDataSources.DataSources(),
            StrikethroughDataSources.DataSources(),
            SubAndSuperScriptDataSources.DataSources(),
            SubScriptDataSources.DataSources(),
            SuperScriptDataSources.DataSources(),
            TableDataSources.DataSources(),
            TagDataSources.DataSources(),
            UnderlineDataSources.DataSources(),
            XSSDataSources.DataSources()
        ];
        
        IEnumerable<Func<MdTestData>> sourcesCombined = sources.SelectMany(static source => source);
        return sourcesCombined;
    }
}
