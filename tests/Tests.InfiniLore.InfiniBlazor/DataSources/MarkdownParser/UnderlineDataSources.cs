// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.InfiniBlazor.DataSources.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class UnderlineDataSources {
    private static readonly string SectionName = nameof(UnderlineDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MdTestData>> DataSources() {
        yield return static () => new MdTestData(SectionName,
            "something _underlined_",
            """
            <p>something <u>underlined</u></p>
            """
        );

        yield return static () => new MdTestData(SectionName,
            "something _underlined with an \\_ escaped_",
            """
            <p>something <u>underlined with an _ escaped</u></p>
            """
        );

        yield return static () => new MdTestData(SectionName,
            "something _**bold and underlined**_",
            """
            <p>something <u><strong>bold and underlined</strong></u></p>
            """
        );

        yield return static () => new MdTestData(SectionName,
            "something _*italic and underlined*_",
            """
            <p>something <u><em>italic and underlined</em></u></p>
            """
        );

        yield return static () => new MdTestData(SectionName,
            "something _***bold, italic, and underlined***_",
            """
            <p>something <u><strong><em>bold, italic, and underlined</em></strong></u></p>
            """
        );

        yield return static () => new MdTestData(SectionName,
            "click _[here](https://example.com)_",
            """
            <p>click <u><a href="https://example.com">here</a></u></p>
            """
        );

        yield return static () => new MdTestData(SectionName,
            "something _**[bold link](https://example.com)**_",
            """
            <p>something <u><strong><a href="https://example.com">bold link</a></strong></u></p>
            """
        );

        yield return static () => new MdTestData(
            SectionName,
            "this has _**multiple** elements to *underline*_",
            """
            <p>this has <u><strong>multiple</strong> elements to<em> underline</em></u></p>
            """
        );

        yield return static () => new MdTestData(
            SectionName,
            "__",
            "<p>__</p>"
        );

        yield return static () => new MdTestData(
            SectionName,
            @"\_escaped_",
            "<p>_escaped_</p>"
        );

        yield return static () => new MdTestData(
            SectionName,
            @"_escaped\_",
            "<p>_escaped_</p>"// Escaped Char
        );
    }
}
