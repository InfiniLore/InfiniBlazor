// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.InfiniBlazor.DataSources.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class LinkDataSources {
    private static readonly string SectionName = nameof(LinkDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MdTestData>> DataSources() {
        yield return static () => new MdTestData(SectionName,
            "This is an [-->*example*<--](https://www.transgenderinfo.be) of a link.",
            """<p>This is an <a href="https://www.transgenderinfo.be">--><em>example</em><--</a> of a link.</p>"""
        );

        yield return static () => new MdTestData(SectionName,
            "This is an **[-->*example*<--](https://www.transgenderinfo.be)** of a link.",
            """<p>This is an <strong><a href="https://www.transgenderinfo.be">--><em>example</em><--</a></strong> of a link.</p>"""
        );

        yield return static () => new MdTestData(SectionName,
            "This is an ^[-->*example*<--](https://www.transgenderinfo.be)^ of a link.",
            """<p>This is an <sub><a href="https://www.transgenderinfo.be">--><em>example</em><--</a></sub> of a link.</p>"""
        );

        yield return static () => new MdTestData(SectionName,
            "This is an ^^[-->*example*<--](https://www.transgenderinfo.be)^^ of a link.",
            """<p>This is an <sup><a href="https://www.transgenderinfo.be">--><em>example</em><--</a></sup> of a link.</p>"""
        );

        yield return static () => new MdTestData(SectionName,
            "![Specs](https://i.imgur.com/aV8o3rE.png)",
            "<p><img class\"inline-block\" src=\"https://i.imgur.com/aV8o3rE.png\" alt=\"Specs\"/></p>"
        );

        yield return static () => new MdTestData(SectionName,
            "[![Specs](https://i.imgur.com/aV8o3rE.png)](https://imgur.com/)",
            """
            <p>
                <a href="https://imgur.com/">
                    <img class"inline-block" src="https://i.imgur.com/aV8o3rE.png" alt="Specs"/>
                </a>
            </p>
            """
        );

        yield return static () => new MdTestData(SectionName,
            "![](https://localhost:7210/icon.png)",
            """
            <p>
                <img class"inline-block" src="https://localhost:7210/icon.png"/>
            </p>
            """
        );
        
        yield return static () => new MdTestData(SectionName,
            @"![\]](https://localhost:7210/icon.png)",
            """
            <p>
                <img class"inline-block" src="https://localhost:7210/icon.png" alt="]"/>
            </p>
            """
        );

        yield return static () => new MdTestData(SectionName,
            "![](https://localhost:7210/icon.png|size=100x100)",
            """
            <p>
                <img class"inline-block" src="https://localhost:7210/icon.png" style="width: 100px; height: 100px;"/>
            </p>
            """
        );

        yield return static () => new MdTestData(SectionName,
            "![](https://localhost:7210/icon.png|title=something)",
            """
            <p>
                <img class"inline-block" src="https://localhost:7210/icon.png" title="something"/>
            </p>
            """
        );
        
        yield return static () => new MdTestData(SectionName,
            "![](https://localhost:7210/icon.png|fit)",
            """
            <p>
                <img class"inline-block" src="https://localhost:7210/icon.png" style="width:auto;height:2em;vertical-align:baseline;object-fit:contain;"/>
            </p>
            """
        );
    }
}
