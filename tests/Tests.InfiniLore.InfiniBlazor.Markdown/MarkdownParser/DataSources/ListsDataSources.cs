// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.MdNodes;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.MarkdownParser.DataSources;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ListsDataSources {
    private static readonly string SectionName = nameof(ListsDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MarkdownTestDto>> DataSources() {

        yield return static () => new MarkdownTestDto(SectionName,
            """
            1. this
            2. is
            3. a
              - nested
            4. list
            """,
            """
            <ol>
                <li>this</li>
                <li>is</li>
                <li>a
                    <ul>
                    <li>nested</li>
                    </ul>
                </li>
                <li>list</li>
            </ol>
            """,
            static rootNode => {
                IMdNode list = rootNode.AddListOrdered();
                list.AddListItem("this");
                list.AddListItem("is");
                IMdNode item = list.AddListItem("a");
                IMdNode sublist = item.AddListUnordered();
                sublist.AddListItem("nested");
                list.AddListItem("list");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            - Item 1
              - Subitem 1.1
              - Subitem 1.2
            - Item 2
            """,
            """
            <ul>
                <li>Item 1
                    <ul>
                        <li>Subitem 1.1</li>
                        <li>Subitem 1.2</li>
                    </ul>
                </li>
                <li>Item 2</li>
            </ul>
            """,
            static rootNode => {
                IMdNode list = rootNode.AddListUnordered();
                IMdNode item = list.AddListItem("Item 1");
                IMdNode sublist = item.AddListUnordered();
                sublist.AddListItem("Subitem 1.1");
                sublist.AddListItem("Subitem 1.2");
                list.AddListItem("Item 2");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            1. Ordered item 1
            2. Ordered item 2
               1. Nested ordered item 2.1
               2. Nested ordered item 2.2
            3. Ordered item 3
            """,
            """
            <ol>
                <li>Ordered item 1</li>
                <li>Ordered item 2
                    <ol>
                        <li>Nested ordered item 2.1</li>
                        <li>Nested ordered item 2.2</li>
                    </ol>
                </li>
                <li>Ordered item 3</li>
            </ol>
            """,
            static rootNode => {
                IMdNode list = rootNode.AddListOrdered();
                list.AddListItem("Ordered item 1");
                IMdNode item = list.AddListItem("Ordered item 2");
                IMdNode sublist = item.AddListOrdered();
                sublist.AddListItem("Nested ordered item 2.1");
                sublist.AddListItem("Nested ordered item 2.2");
                list.AddListItem("Ordered item 3");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            - Unordered item 1
            - Unordered item 2
              1. Mixed nested ordered item 2.1
              2. Mixed nested ordered item 2.2
            - Unordered item 3
            """,
            """
            <ul>
                <li>Unordered item 1</li>
                <li>Unordered item 2
                    <ol>
                        <li>Mixed nested ordered item 2.1</li>
                        <li>Mixed nested ordered item 2.2</li>
                    </ol>
                </li>
                <li>Unordered item 3</li>
            </ul>
            """,
            static rootNode => {
                IMdNode list = rootNode.AddListUnordered();
                list.AddListItem("Unordered item 1");
                IMdNode item = list.AddListItem("Unordered item 2");
                IMdNode sublist = item.AddListOrdered();
                sublist.AddListItem("Mixed nested ordered item 2.1");
                sublist.AddListItem("Mixed nested ordered item 2.2");
                list.AddListItem("Unordered item 3");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            1. First ordered item
               - Subitem A
               - Subitem B
            2. Second ordered item
            """,
            """
            <ol>
                <li>First ordered item
                    <ul>
                        <li>Subitem A</li>
                        <li>Subitem B</li>
                    </ul>
                </li>
                <li>Second ordered item</li>
            </ol>
            """,
            static rootNode => {
                IMdNode list = rootNode.AddListOrdered();
                IMdNode item = list.AddListItem("First ordered item");
                IMdNode sublist = item.AddListUnordered();
                sublist.AddListItem("Subitem A");
                sublist.AddListItem("Subitem B");
                list.AddListItem("Second ordered item");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            - Top-level item
                - Subitem level 1
                    - Subitem level 2
                        - Subitem level 3
            """,
            """
            <ul>
                <li>Top-level item
                    <ul>
                        <li>Subitem level 1
                            <ul>
                                <li>Subitem level 2
                                    <ul>
                                        <li>Subitem level 3</li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>
            </ul>
            """,
            static rootNode => {
                IMdNode list = rootNode.AddListUnordered();
                IMdNode item = list.AddListItem("Top-level item");
                IMdNode sublist = item.AddListUnordered();
                IMdNode subitem1 = sublist.AddListItem("Subitem level 1");
                IMdNode subList1 = subitem1.AddListUnordered();
                IMdNode subitem2 = subList1.AddListItem("Subitem level 2");
                IMdNode subList2 = subitem2.AddListUnordered();
                subList2.AddListItem("Subitem level 3");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            - Top item 1
            - Top item 2
            
                Unrelated text block in the same list

            - Top item 3
            """,
            """
            <ul>
                <li>Top item 1</li>
                <li>Top item 2</li>
            </ul>
            <p>Unrelated text block in the same list</p>
            <ul>
                <li>Top item 3</li>
            </ul>
            """,
            static rootNode => {
                IMdNode list = rootNode.AddListUnordered();
                list.AddListItem("Top item 1");
                list.AddListItem("Top item 2");
                
                rootNode.AddParagraph("Unrelated text block in the same list");
                
                IMdNode list2 = rootNode.AddListUnordered();
                list2.AddListItem("Top item 3");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            1.    Extra spaces for alignment
                  - This is a sublist
            2.    Another item
            """,
            """
            <ol>
                <li>Extra spaces for alignment
                    <ul>
                        <li>This is a sublist</li>
                    </ul>
                </li>
                <li>Another item</li>
            </ol>
            """,
            static rootNode => {
                IMdNode list = rootNode.AddListOrdered();
                IMdNode item = list.AddListItem("Extra spaces for alignment");
                IMdNode sublist = item.AddListUnordered();
                sublist.AddListItem("This is a sublist");
                
                list.AddListItem("Another item");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            - A list item with **bold text**
            - A list item with *italic text*
            - A list item with `inline code`
            """,
            """
            <ul>
                <li>A list item with <strong>bold text</strong></li>
                <li>A list item with <em>italic text</em></li>
                <li>A list item with <code>inline code</code></li>
            </ul>
            """,
            static rootNode => {
                IMdNode list = rootNode.AddListUnordered();
                list.AddListItem("A list item with ").AddBold("bold text");
                list.AddListItem("A list item with ").AddItalic("italic text");
                list.AddListItem("A list item with ").AddCode("inline code");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            - This list has
              - Uneven indentation
                - But still renders
                  - Correctly!
            """,
            """
            <ul>
                <li>This list has
                    <ul>
                        <li>Uneven indentation
                            <ul>
                                <li>But still renders
                                    <ul>
                                        <li>Correctly!</li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>
            </ul>
            """,
            static rootNode => {
                IMdNode list = rootNode.AddListUnordered();
                IMdNode list2 = list.AddListItem("This list has").AddListUnordered();
                IMdNode list3 = list2.AddListItem("Uneven indentation").AddListUnordered();
                IMdNode list4 = list3.AddListItem("But still renders").AddListUnordered();
                list4.AddListItem("Correctly!");
            }
        );


        yield return static () => new MarkdownTestDto( SectionName,
            Markdown: """
            1. this
            2. [ ] is
            3. [x] a
              - [X] nested
            4. todo list
            """,
            ExpectedStringOutput: """
            <ol>
                <li>this</li>
                <li><input type="checkbox" disabled/>is</li>
                <li><input type="checkbox" disabled checked/>a
                    <ul>
                        <li><input type="checkbox" disabled checked/>nested</li>
                    </ul>
                </li>
                <li>todo list</li>
            </ol>
            """,
            static rootNode => {
                var list = rootNode.AddListOrdered();
                list.AddListItem("this");
                var item1 = list.AddListItem();
                item1.AddCheckboxUnselected();
                item1.WithContent(" is");

                var item2 = list.AddListItem();
                item2.AddCheckboxSelected();
                item2.WithContent(" a");
                var sublist = item2.AddListUnordered();
                var item3 = sublist.AddListItem();
                item3.AddCheckboxSelected();
                item3.WithContent(" nested");

                list.AddListItem("todo list");
            }
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            """
            - [ ] Task with sublist
              - Subitem A
              - Subitem B
            - [x] Completed task with sublist
              - Subitem C
              - Subitem D
            """,
            """
            <ul>
                <li><input type="checkbox" disabled />Task with sublist
                    <ul>
                        <li>Subitem A</li>
                        <li>Subitem B</li>
                    </ul>
                </li>
                <li><input type="checkbox" disabled checked />Completed task with sublist
                    <ul>
                        <li>Subitem C</li>
                        <li>Subitem D</li>
                    </ul>
                </li>
            </ul>
            """
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            """
            1. Ordered task list
            2. [ ] Incomplete task in ordered list
            3. [x] Completed task in ordered list
            """,
            """
            <ol>
                <li>Ordered task list</li>
                <li><input type="checkbox" disabled />Incomplete task in ordered list</li>
                <li><input type="checkbox" disabled checked />Completed task in ordered list</li>
            </ol>
            """
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            """
            - [x] Mixed list
              1. [ ] Subtask 1
              2. [x] Subtask 2
            - Another item
            """,
            """
            <ul>
                <li><input type="checkbox" disabled checked />Mixed list
                    <ol>
                        <li><input type="checkbox" disabled />Subtask 1</li>
                        <li><input type="checkbox" disabled checked />Subtask 2</li>
                    </ol>
                </li>
                <li>Another item</li>
            </ul>
            """
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            """
            - Normal item
            - [ ] Task item without sublist
            
                Unrelated paragraph between list items.

            - [x] Another completed task
            """,
            """
            <ul>
                <li>Normal item</li>
                <li><input type="checkbox" disabled />Task item without sublist</li>
            </ul>
            <p>Unrelated paragraph between list items.</p>
            <ul>
                <li><input type="checkbox" disabled checked />Another completed task</li>
            </ul>
            """
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            """
            - Normal item
            - [ ] Task item without sublist
                related paragraph between list items.

            - [x] Another completed task
            """,
            """
            <ul>
                <li>Normal item</li>
                <li><input type="checkbox" disabled />Task item without sublist
                    <p>related paragraph between list items.</p>
                </li>
            </ul>
            
            <ul>
                <li><input type="checkbox" disabled checked />Another completed task</li>
            </ul>
            """
        );
        
    }
}
