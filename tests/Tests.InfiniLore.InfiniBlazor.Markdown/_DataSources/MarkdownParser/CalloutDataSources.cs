// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.InfiniBlazor.Markdown._DataSources.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class CalloutDataSources {
    private static readonly string SectionName = nameof(CalloutDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MdTestData>> DataSources() {
        yield return static () => new MdTestData(SectionName,
            """
            > [!note] **Do Something**
            > Body Content
            """,
            """
            <div class="md-callout md-callout-note">
                <div class="md-callout-title"><strong>Do Something</strong></div>
                <div class="md-callout-body">
                    <p>Body Content</p>
                </div>
            </div>
            """
        );
        
        yield return static () => new MdTestData(SectionName,
            """
            > [!note|icon=signature] **Do Something**
            >> [!note|icon=signature] **Do Something**
            >> Body Content
            """,
            """
            <div class="md-callout md-callout-note icon-signature">
                <div class="md-callout-title"><strong>Do Something</strong></div>
                <div class="md-callout-body">
                    <div class="md-callout md-callout-note icon-signature">
                        <div class="md-callout-title"><strong>Do Something</strong></div>
                        <div class="md-callout-body">
                            <p>Body Content</p>
                        </div>
                    </div>
                </div>
            </div>
            """
        );

        yield return static () => new MdTestData(SectionName,
            """
            > [!note] Title
            > Body

            [a]
            """,
            """
            <div class="md-callout md-callout-note">
                <div class="md-callout-title">Title</div>
                <div class="md-callout-body">
                    <p>Body</p>
                </div>
            </div>
            <p>[a]</p>
            """
        );
        
        yield return static () => new MdTestData(SectionName,
            """
            > [!note|icon=signature] Title
            > Body
            """,
            """
            <div class="md-callout md-callout-note icon-signature">
                <div class="md-callout-title">Title</div>
                <div class="md-callout-body">
                    <p>Body</p>
                </div>
            </div>
            """
        );
    }
}













