// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Tests.InfiniLore.InfiniBlazor.Markdown.DataSources.MarkdownParser;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.DataSources;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MarkdownDataSources {
    public static IEnumerable<Func<OldMdTestData>> DataSources() {
        List<IEnumerable<Func<OldMdTestData>>> sources = [
            AggregateDataSources.DataSources(),
            BlockQuoteDataSources.DataSources(),
            // BoldAndItalicDataSources.DataSources(),
            // BoldDataSources.DataSources(),
            // CalloutDataSources.DataSources(),
            // CodeDataSources.DataSources(),
            // CodeInlineDataSources.DataSources(),
            // EdgeCaseDataSources.DataSources(),
            // EmoteDataSources.DataSources(),
            // EscapedCharacterDataSources.DataSources(),
            // HeadingDataSources.DataSources(),
            // HorizontalLineDataSources.DataSources(),
            // HtmlDataSources.DataSources(),
            // ItalicDataSources.DataSources(),
            // LinkDataSources.DataSources(),
            // ListsDataSources.DataSources(),
            // SpecialCharacterDataSources.DataSources(),
            // StrikethroughDataSources.DataSources(),
            SubAndSuperScriptDataSources.DataSources(),
            SubScriptDataSources.DataSources(),
            SuperScriptDataSources.DataSources(),
            // TableDataSources.DataSources(),
            // UnderlineDataSources.DataSources(),
            XSSDataSources.DataSources()
        ];
        
        IEnumerable<Func<OldMdTestData>> sourcesCombined = sources.SelectMany(static source => source);
        return sourcesCombined;
    }
}

