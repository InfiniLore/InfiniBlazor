// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.InfiniBlazor.DataSources.MarkdownParser;
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
            !>[note|icon=liSignature] **Do Something**
            !> Body Content
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
            !>[note|icon=liSignature] **Do Something**
            !>!>[note|icon=liSignature] **Do Something**
            !>!> Body Content
            """,
            """
            <div class="md-callout md-callout-note">
                <div class="md-callout-title"><strong>Do Something</strong></div>
                <div class="md-callout-body">
                    <div class="md-callout md-callout-note">
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
            !> callout without a type
            !> Body Content
            """,
            """
            <div class="md-callout">
                <div class="md-callout-title">callout without a type</div>
                <div class="md-callout-body">
                    <p>Body Content</p>
                </div>
            </div>
            """
        );
        
        yield return static () => new MdTestData(SectionName,
            """
            !> callout with only a title
            """,
            """
            <div class="md-callout">
                <div class="md-callout-title">callout with only a title</div>
            </div>
            """
        );
        
        yield return static () => new MdTestData(SectionName,
            """
            !> callout with only a title
            !>!> callout with only a title
            !>!>!> callout with only a title
            """,
            """
            <div class="md-callout">
                <div class="md-callout-title">callout with only a title</div>
                <div class="md-callout-body">
                    <div class="md-callout">
                    <div class="md-callout-title">callout with only a title</div>
                        <div class="md-callout-body">
                            <div class="md-callout">
                                <div class="md-callout-title">callout with only a title</div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            """
        );
    }
}
