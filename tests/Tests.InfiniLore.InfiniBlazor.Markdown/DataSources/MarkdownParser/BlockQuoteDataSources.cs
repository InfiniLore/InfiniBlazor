// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Syntax;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.DataSources.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class BlockQuoteDataSources {
    private static readonly string SectionName = nameof(BlockQuoteDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MarkdownTestDto>> DataSources() {

        yield return static () => new MarkdownTestDto(SectionName,
            """
            > # test
            > > test
            >
            > - test
            > - test
            """,
            """
            <blockquote>
                <h1>test</h1>
                <blockquote>
                    <p>test</p>
                </blockquote>
                <ul>
                    <li>test</li>
                    <li>test</li>
                </ul>
            </blockquote>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode blockquote = rootNode.AddBlockquote();
                blockquote.AddH1("test");

                IMarkdownSyntaxNode nestedBlockquote = blockquote.AddBlockquote();
                nestedBlockquote.AddParagraph("test");

                IMarkdownSyntaxNode list = blockquote.AddListUnordered();
                list.AddListItem("test");
                list.AddListItem("test");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            > blockQuote 1
            >> ...blockQuote 2
            >>> ...blockQuote 3
            """,
            """
            <blockquote>
                <p>blockQuote 1</p>
                <blockquote>
                    <p>...blockQuote 2</p>
                    <blockquote>
                        <p>...blockQuote 3</p>
                    </blockquote>
                </blockquote>
            </blockquote>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode blockquote = rootNode.AddBlockquote();
                blockquote.AddParagraph("blockQuote 1");

                IMarkdownSyntaxNode nestedBlockquote = blockquote.AddBlockquote();
                nestedBlockquote.AddParagraph("...blockQuote 2");

                IMarkdownSyntaxNode nestedNestedBlockquote = nestedBlockquote.AddBlockquote();
                nestedNestedBlockquote.AddParagraph("...blockQuote 3");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            > This is a blockquote with **bold text** and *italic text*.
            >
            > 1. First item
            > 2. Second item
            >    - Sub-item
            """,
            """
            <blockquote>
                <p>This is a blockquote with <strong>bold text</strong> and <em>italic text</em>.</p>
                <ol>
                    <li>First item</li>
                    <li>Second item
                        <ul>
                            <li>Sub-item</li>
                        </ul>
                    </li>
                </ol>
            </blockquote>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode blockquote = rootNode.AddBlockquote();
                IMarkdownSyntaxNode paragraph = blockquote.AddParagraph();
                paragraph.WithContent("This is a blockquote with ");
                paragraph.AddBold("bold text");
                paragraph.WithContent(" and ");
                paragraph.AddItalic("italic text");
                paragraph.WithContent(".");

                IMarkdownSyntaxNode list = blockquote.AddListOrdered();
                list.AddListItem("First item");

                IMarkdownSyntaxNode secondListItem = list.AddListItem("Second item");
                secondListItem.AddListUnordered().AddListItem("Sub-item");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            > Nested quotes:
            > > Level 2
            > > > Level 3
            > > > > Level 4
            """,
            """
            <blockquote>
                <p>Nested quotes:</p>
                <blockquote>
                    <p>Level 2</p>
                    <blockquote>
                        <p>Level 3</p>
                        <blockquote>
                            <p>Level 4</p>
                        </blockquote>
                    </blockquote>
                </blockquote>
            </blockquote>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode blockquote = rootNode.AddBlockquote();
                blockquote.AddParagraph("Nested quotes:");

                IMarkdownSyntaxNode nestedBlockquote = blockquote.AddBlockquote();
                nestedBlockquote.AddParagraph("Level 2");

                IMarkdownSyntaxNode nestedNestedBlockquote = nestedBlockquote.AddBlockquote();
                nestedNestedBlockquote.AddParagraph("Level 3");

                IMarkdownSyntaxNode nestedNestedNestedBlockquote = nestedNestedBlockquote.AddBlockquote();
                nestedNestedNestedBlockquote.AddParagraph("Level 4");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            > Combining elements:
            > ### Heading inside blockquote
            > - List item
            > - Another item
            > > Sub blockquote
            """,
            """
            <blockquote>
                <p>Combining elements:</p>
                <h3>Heading inside blockquote</h3>
                <ul>
                    <li>List item</li>
                    <li>Another item</li>
                </ul>
                <blockquote>
                    <p>Sub blockquote</p>
                </blockquote>
            </blockquote>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode blockquote = rootNode.AddBlockquote();
                blockquote.AddParagraph("Combining elements:");

                blockquote.AddH3("Heading inside blockquote");

                IMarkdownSyntaxNode list = blockquote.AddListUnordered();
                list.AddListItem("List item");
                list.AddListItem("Another item");

                IMarkdownSyntaxNode nestedBlockquote = blockquote.AddBlockquote();
                nestedBlockquote.AddParagraph("Sub blockquote");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            > Inline formatting:
            > **Bold**, *italic*, `code`, and [link](https://example.com).
            """,
            """
            <blockquote>
                <p>Inline formatting:</p>
                <p><strong>Bold</strong>, <em>italic</em>, <code>code</code>, and <a href="https://example.com">link</a>.</p>
            </blockquote>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode blockquote = rootNode.AddBlockquote();
                blockquote.AddParagraph("Inline formatting:");

                IMarkdownSyntaxNode paragraph2 = blockquote.AddParagraph();

                paragraph2.AddBold("Bold");
                paragraph2.WithContent(", ");

                paragraph2.AddItalic("italic");
                paragraph2.WithContent(", ");

                paragraph2.AddCodeInline("code");
                paragraph2.WithContent(", and ");

                IMarkdownSyntaxNode link = paragraph2.AddLink("link");
                link.WithAttribute(MarkdownAttribute.LinkHref, "https://example.com");

                paragraph2.WithContent(".");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            > Mixed levels and lists:
            > 1. First
            >    > Nested blockquote with text.
            > 2. Second
            >    - Sub-item
            >       > Another nested quote.
            """,
            """
            <blockquote>
                <p>Mixed levels and lists:</p>
                <ol>
                    <li>
                        First
                        <blockquote>
                            <p>Nested blockquote with text.</p>
                        </blockquote>
                    </li>
                    <li>
                        Second
                        <ul>
                            <li>
                                Sub-item
                                <blockquote>
                                    <p>Another nested quote.</p>
                                </blockquote>
                            </li>
                        </ul>
                    </li>
                </ol>
            </blockquote>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode blockquote = rootNode.AddBlockquote();
                blockquote.AddParagraph("Mixed levels and lists:");

                IMarkdownSyntaxNode list = blockquote.AddListOrdered();
                IMarkdownSyntaxNode firstListItem = list.AddListItem("First");
                IMarkdownSyntaxNode nestedBlockquote = firstListItem.AddBlockquote();
                nestedBlockquote.AddParagraph("Nested blockquote with text.");

                IMarkdownSyntaxNode secondListItem = list.AddListItem("Second");
                IMarkdownSyntaxNode nestedList = secondListItem.AddListUnordered();
                IMarkdownSyntaxNode subItem = nestedList.AddListItem("Sub-item");
                IMarkdownSyntaxNode nestedNestedBlockquote = subItem.AddBlockquote();
                nestedNestedBlockquote.AddParagraph("Another nested quote.");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            ">",
            "<p>></p>",// an empty blockquote is not parsed as a blockquote
            ConfigureExpectedNode: static rootNode => {
                rootNode.AddParagraph(">");
            });


        yield return static () => new MarkdownTestDto(SectionName,
            "> Plain text blockquote.",
            """
            <blockquote>
                <p>Plain text blockquote.</p>
            </blockquote>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode blockquote = rootNode.AddBlockquote();
                blockquote.AddParagraph("Plain text blockquote.");
            });

        yield return static () => new MarkdownTestDto(SectionName,
            """
            > This is a blockquote with escaped characters: \*, \>, `\``.
            """,
            """
            <blockquote>
                <p>This is a blockquote with escaped characters: *, >, <code>`</code>.</p>
            </blockquote>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode blockquote = rootNode.AddBlockquote();
                IMarkdownSyntaxNode paragraph = blockquote.AddParagraph("This is a blockquote with escaped characters: *, >, ");
                paragraph.AddCodeInline("`");
                paragraph.WithContent(".");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            > Level 1
            > > Level 2
            > Back to Level 1
            > > > Level 3
            """,
            """
            <blockquote>
                <p>Level 1</p>
                <blockquote>
                    <p>Level 2</p>
                </blockquote>
                <p>Back to Level 1</p>
                <blockquote>
                    <blockquote>
                        <p>Level 3</p>
                    </blockquote>
                </blockquote>
            </blockquote>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode blockquote = rootNode.AddBlockquote();
                blockquote.AddParagraph("Level 1");

                IMarkdownSyntaxNode nestedBlockquote = blockquote.AddBlockquote();
                nestedBlockquote.AddParagraph("Level 2");

                blockquote.AddParagraph("Back to Level 1");

                IMarkdownSyntaxNode nestedNestedBlockquote = blockquote.AddBlockquote().AddBlockquote();
                nestedNestedBlockquote.AddParagraph("Level 3");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            > **Bold text**, *italic text*, `inline code`, ~strikethrough~, and [link](https://example.com).
            """,
            """
            <blockquote>
                <p><strong>Bold text</strong>, <em>italic text</em>, <code>inline code</code>, <s>strikethrough</s>, and <a href="https://example.com">link</a>.</p>
            </blockquote>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode blockquote = rootNode.AddBlockquote();
                IMarkdownSyntaxNode paragraph = blockquote.AddParagraph();
                paragraph.AddBold("Bold text");
                paragraph.WithContent(", ");

                paragraph.AddItalic("italic text");
                paragraph.WithContent(", ");

                paragraph.AddCodeInline("inline code");
                paragraph.WithContent(", ");

                paragraph.AddStrikethrough("strikethrough");
                paragraph.WithContent(", and ");

                IMarkdownSyntaxNode link = paragraph.AddLink("link");
                link.WithAttribute(MarkdownAttribute.LinkHref, "https://example.com");
                paragraph.WithContent(".");

            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            > Line 1
            >
            >
            > Line 2
            """,
            """
            <blockquote>
                <p>Line 1</p>
                <p>Line 2</p>
            </blockquote>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode blockquote = rootNode.AddBlockquote();
                blockquote.AddParagraph("Line 1");
                blockquote.AddParagraph("Line 2");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            > This is a paragraph.
            >
            > ```
            > Code block inside a blockquote.
            > ```
            """,
            """
            <blockquote>
                <p>This is a paragraph.</p>
                <pre><code>Code block inside a blockquote.</code></pre>
            </blockquote>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode blockquote = rootNode.AddBlockquote();
                blockquote.AddParagraph("This is a paragraph.");
                blockquote.AddCodeBlock("Code block inside a blockquote.\n");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            > This is the first paragraph.
            >
            > This is the second paragraph, separated by an empty line.
            """,
            """
            <blockquote>
                <p>This is the first paragraph.</p>
                <p>This is the second paragraph, separated by an empty line.</p>
            </blockquote>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode blockquote = rootNode.AddBlockquote();
                blockquote.AddParagraph("This is the first paragraph.");
                blockquote.AddParagraph("This is the second paragraph, separated by an empty line.");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            > <div>This is a div inside a blockquote.</div>
            """,
            """
            <blockquote>
                <div>This is a div inside a blockquote.</div>
            </blockquote>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode blockquote = rootNode.AddBlockquote();
                blockquote.WithHtmlContent("<div>This is a div inside a blockquote.</div>");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            > Level 1
            >>>>>>>>>>>>>>>>>>>> Level 20
            """,
            """
            <blockquote>
                <p>Level 1</p>
                <p>>>>>>>>>>>>>>>>>>>>Level 20</p>
            </blockquote>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode blockQuote = rootNode.AddBlockquote();
                blockQuote.AddParagraph("Level 1");
                blockQuote.AddParagraph(">>>>>>>>>>>>>>>>>>> Level 20");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            > - First item
            >    - Nested item with **bold text**.
            > - Second item
            """,
            """
            <blockquote>
                <ul>
                    <li>First item
                        <ul>
                            <li>Nested item with <strong>bold text</strong>.</li>
                        </ul>
                    </li>
                    <li>Second item</li>
                </ul>
            </blockquote>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode blockQuote = rootNode.AddBlockquote();
                IMarkdownSyntaxNode list = blockQuote.AddListUnordered();
                IMarkdownSyntaxNode li1 = list.AddListItem("First item");
                IMarkdownSyntaxNode li2 = li1.AddListUnordered().AddListItem();
                li2.WithContent("Nested item with ");
                li2.AddBold("bold text");
                li2.WithContent(".");

                list.AddListItem("Second item");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            > Line 1
            >> Line 2
            >>> Line 3
            > Line 4
            """,
            """
            <blockquote>
                <p>Line 1</p>
                <blockquote>
                    <p>Line 2</p>
                    <blockquote>
                        <p>Line 3</p>
                    </blockquote>
                </blockquote>
                <p>Line 4</p>
            </blockquote>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode blockQuote = rootNode.AddBlockquote();
                blockQuote.AddParagraph("Line 1");
                IMarkdownSyntaxNode blockQuote2 = blockQuote.AddBlockquote();
                blockQuote2.AddParagraph("Line 2");
                IMarkdownSyntaxNode blockQuote3 = blockQuote2.AddBlockquote();
                blockQuote3.AddParagraph("Line 3");
                blockQuote.AddParagraph("Line 4");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            > This is a blockquote
            >
            > - List item 1
            > - List item 2
            """,
            """
            <blockquote>
                <p>This is a blockquote</p>
                <ul>
                    <li>List item 1</li>
                    <li>List item 2</li>
                </ul>
            </blockquote>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode blockquote = rootNode.AddBlockquote();
                IMarkdownSyntaxNode paragraph = blockquote.AddParagraph();
                paragraph.WithContent("This is a blockquote");

                IMarkdownSyntaxNode list = blockquote.AddListUnordered();
                list.AddListItem(content: "List item 1");
                list.AddListItem(content: "List item 2");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            - List item
                > This is a blockquote inside a list item.
            """,
            """
            <ul>
                <li>
                    List item
                    <blockquote>
                        <p>This is a blockquote inside a list item.</p>
                    </blockquote>
                </li>
            </ul>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode list = rootNode.AddListUnordered();
                IMarkdownSyntaxNode listItem = list.AddListItem(content: "List item");

                IMarkdownSyntaxNode blockquote = listItem.AddBlockquote();
                blockquote.AddParagraph("This is a blockquote inside a list item.");
            }
        );
    }
}
