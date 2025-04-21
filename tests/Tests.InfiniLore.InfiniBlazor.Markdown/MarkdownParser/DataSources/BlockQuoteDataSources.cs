// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.MdNodes;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.MarkdownParser.DataSources;
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
            static rootNode => {
                IMdNode blockquote = rootNode.AddBlockquote();
                IMdNode h1 = blockquote.AddH1();
                h1.WithContent("test");
                
                IMdNode nestedBlockquote = blockquote.AddBlockquote();
                IMdNode nestedParagraph = nestedBlockquote.AddParagraph();
                nestedParagraph.WithContent("test");

                IMdNode list = blockquote.AddListUnordered();
                list.AddListItem().WithContent("test");
                list.AddListItem().WithContent("test");
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
            static rootNode => {
                IMdNode blockquote = rootNode.AddBlockquote();
                IMdNode paragraph = blockquote.AddParagraph();
                paragraph.WithContent("blockQuote 1");
                
                IMdNode nestedBlockquote = blockquote.AddBlockquote();
                IMdNode nestedParagraph = nestedBlockquote.AddParagraph();
                nestedParagraph.WithContent("...blockQuote 2");
                
                IMdNode nestedNestedBlockquote = nestedBlockquote.AddBlockquote();
                IMdNode nestedNestedParagraph = nestedNestedBlockquote.AddParagraph();
                nestedNestedParagraph.WithContent("...blockQuote 3");
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
            static rootNode => {
                IMdNode blockquote = rootNode.AddBlockquote();
                IMdNode paragraph = blockquote.AddParagraph();
                paragraph.WithContent("This is a blockquote with ");
                paragraph.AddBold().WithContent("bold text");
                paragraph.WithContent(" and ");
                paragraph.AddItalic().WithContent("italic text");
                paragraph.WithContent(".");
                
                IMdNode list = blockquote.AddListOrdered();
                list.AddListItem().WithContent("First item");
                IMdNode secondListItem = list.AddListItem();
                secondListItem.WithContent("Second item");
                
                IMdNode nestedList = secondListItem.AddListUnordered();
                nestedList.AddListItem().WithContent("Sub-item");
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
            static rootNode => {
                IMdNode blockquote = rootNode.AddBlockquote();
                IMdNode paragraph = blockquote.AddParagraph();
                paragraph.WithContent("Nested quotes:");
                
                IMdNode nestedBlockquote = blockquote.AddBlockquote();
                IMdNode nestedParagraph = nestedBlockquote.AddParagraph();
                nestedParagraph.WithContent("Level 2");
                
                IMdNode nestedNestedBlockquote = nestedBlockquote.AddBlockquote();
                IMdNode nestedNestedParagraph = nestedNestedBlockquote.AddParagraph();
                nestedNestedParagraph.WithContent("Level 3");
                
                IMdNode nestedNestedNestedBlockquote = nestedNestedBlockquote.AddBlockquote();
                IMdNode nestedNestedNestedParagraph = nestedNestedNestedBlockquote.AddParagraph();
                nestedNestedNestedParagraph.WithContent("Level 4");
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
            static rootNode => {
                IMdNode blockquote = rootNode.AddBlockquote();
                IMdNode paragraph = blockquote.AddParagraph();
                paragraph.WithContent("Combining elements:");
                
                IMdNode heading = blockquote.AddH3();
                heading.WithContent("Heading inside blockquote");
                
                IMdNode list = blockquote.AddListUnordered();
                list.AddListItem().WithContent("List item");
                list.AddListItem().WithContent("Another item");
                
                IMdNode nestedBlockquote = blockquote.AddBlockquote();
                IMdNode nestedParagraph = nestedBlockquote.AddParagraph();
                nestedParagraph.WithContent("Sub blockquote");
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
            static rootNode => {
                IMdNode blockquote = rootNode.AddBlockquote();
                IMdNode paragraph = blockquote.AddParagraph();
                paragraph.WithContent("Inline formatting:");
                
                IMdNode paragraph2 = blockquote.AddParagraph();
                
                IMdNode bold = paragraph2.AddBold();
                bold.WithContent("Bold");
                paragraph2.WithContent(", ");
                
                IMdNode italic = paragraph2.AddItalic();
                italic.WithContent("italic");
                paragraph2.WithContent(", ");

                IMdNode code = paragraph2.AddCode();
                code.WithContent("code");
                paragraph2.WithContent(", and ");

                IMdNode link = paragraph2.AddLink();
                link.WithAttribute("href", "https://example.com");
                link.WithContent("link");
                
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
            static rootNode => {
                IMdNode blockquote = rootNode.AddBlockquote();
                IMdNode paragraph = blockquote.AddParagraph();
                paragraph.WithContent("Mixed levels and lists:");
                
                IMdNode list = blockquote.AddListOrdered();
                IMdNode firstListItem = list.AddListItem();
                firstListItem.WithContent("First");
                IMdNode nestedBlockquote = firstListItem.AddBlockquote();
                nestedBlockquote.AddParagraph().WithContent("Nested blockquote with text.");
                
                IMdNode secondListItem = list.AddListItem();
                secondListItem.WithContent("Second");
                IMdNode nestedList = secondListItem.AddListUnordered();
                IMdNode subItem = nestedList.AddListItem();
                subItem.WithContent("Sub-item");
                IMdNode nestedNestedBlockquote = subItem.AddBlockquote();
                nestedNestedBlockquote.AddParagraph().WithContent("Another nested quote.");
            }
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            ">", 
            "<p>&gt;</p>", // an empty blockquote is not parsed as a blockquote
            static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent(">");
            });
        
        
        yield return static () => new MarkdownTestDto(SectionName,
            "> Plain text blockquote.",
            """
            <blockquote>
                <p>Plain text blockquote.</p>
            </blockquote>
            """,
            static rootNode => {
                IMdNode blockquote = rootNode.AddBlockquote();
                IMdNode paragraph = blockquote.AddParagraph();
                paragraph.WithContent("Plain text blockquote.");
            });
        
        yield return static () => new MarkdownTestDto(SectionName,
            """
            > This is a blockquote with escaped characters: \*, \>, `\``.
            """,
            """
            <blockquote>
                <p>This is a blockquote with escaped characters: *, &gt;, <code>`</code>.</p>
            </blockquote>
            """,
            static rootNode => {
                IMdNode blockquote = rootNode.AddBlockquote();
                IMdNode paragraph = blockquote.AddParagraph();
                paragraph.WithContent("This is a blockquote with escaped characters: ");
                paragraph.WithContent("*");
                paragraph.WithContent(", ");
                paragraph.WithContent("&gt;");
                paragraph.WithContent(", ");
                
                IMdNode code = paragraph.AddCode();
                code.WithContent("`");
                
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
            static rootNode => {
                IMdNode blockquote = rootNode.AddBlockquote();
                IMdNode paragraph = blockquote.AddParagraph();
                paragraph.WithContent("Level 1");
                
                IMdNode nestedBlockquote = blockquote.AddBlockquote();
                IMdNode nestedParagraph = nestedBlockquote.AddParagraph();
                nestedParagraph.WithContent("Level 2");
                
                IMdNode paragraph2 = blockquote.AddParagraph();
                paragraph2.WithContent("Back to Level 1");
                
                IMdNode nestedNestedBlockquote = blockquote.AddBlockquote().AddBlockquote();
                IMdNode nestedNestedParagraph = nestedNestedBlockquote.AddParagraph();
                nestedNestedParagraph.WithContent("Level 3");
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
            """
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
            static rootNode => {
                IMdNode blockquote = rootNode.AddBlockquote();
                IMdNode paragraph = blockquote.AddParagraph();
                paragraph.WithContent("Line 1");
                
                IMdNode paragraph2 = blockquote.AddParagraph();
                paragraph2.WithContent("Line 2");
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
            static rootNode => {
                IMdNode blockquote = rootNode.AddBlockquote();
                IMdNode paragraph = blockquote.AddParagraph();
                paragraph.WithContent("This is a paragraph.");
                
                IMdNode code = blockquote.AddPre().AddCode();
                code.WithContent("Code block inside a blockquote.\n");
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
            static rootNode => {
                IMdNode blockquote = rootNode.AddBlockquote();
                IMdNode paragraph = blockquote.AddParagraph();
                paragraph.WithContent("This is the first paragraph.");
                
                IMdNode paragraph2 = blockquote.AddParagraph();
                paragraph2.WithContent("This is the second paragraph, separated by an empty line.");
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
            static rootNode => {
                IMdNode blockquote = rootNode.AddBlockquote();
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
                <p>&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;Level 20</p>
            </blockquote>
            """
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
            """
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
            """
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
            """
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
            """
        );
    }
}
