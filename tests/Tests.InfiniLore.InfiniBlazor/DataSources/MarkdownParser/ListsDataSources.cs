// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Syntax;

namespace Tests.InfiniLore.InfiniBlazor.DataSources.MarkdownParser;
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
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode list = rootNode.AddListOrdered();
                list.AddListItem("this");
                list.AddListItem("is");
                IMarkdownSyntaxNode item = list.AddListItem("a");
                IMarkdownSyntaxNode sublist = item.AddListUnordered();
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
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode list = rootNode.AddListUnordered();
                IMarkdownSyntaxNode item = list.AddListItem("Item 1");
                IMarkdownSyntaxNode sublist = item.AddListUnordered();
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
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode list = rootNode.AddListOrdered();
                list.AddListItem("Ordered item 1");
                IMarkdownSyntaxNode item = list.AddListItem("Ordered item 2");
                IMarkdownSyntaxNode sublist = item.AddListOrdered();
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
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode list = rootNode.AddListUnordered();
                list.AddListItem("Unordered item 1");
                IMarkdownSyntaxNode item = list.AddListItem("Unordered item 2");
                IMarkdownSyntaxNode sublist = item.AddListOrdered();
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
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode list = rootNode.AddListOrdered();
                IMarkdownSyntaxNode item = list.AddListItem("First ordered item");
                IMarkdownSyntaxNode sublist = item.AddListUnordered();
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
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode list = rootNode.AddListUnordered();
                IMarkdownSyntaxNode item = list.AddListItem("Top-level item");
                IMarkdownSyntaxNode sublist = item.AddListUnordered();
                IMarkdownSyntaxNode subitem1 = sublist.AddListItem("Subitem level 1");
                IMarkdownSyntaxNode subList1 = subitem1.AddListUnordered();
                IMarkdownSyntaxNode subitem2 = subList1.AddListItem("Subitem level 2");
                IMarkdownSyntaxNode subList2 = subitem2.AddListUnordered();
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
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode list = rootNode.AddListUnordered();
                list.AddListItem("Top item 1");
                list.AddListItem("Top item 2");

                rootNode.AddParagraph("Unrelated text block in the same list");

                IMarkdownSyntaxNode list2 = rootNode.AddListUnordered();
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
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode list = rootNode.AddListOrdered();
                IMarkdownSyntaxNode item = list.AddListItem("Extra spaces for alignment");
                IMarkdownSyntaxNode sublist = item.AddListUnordered();
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
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode list = rootNode.AddListUnordered();
                list.AddListItem("A list item with ").AddBold("bold text");
                list.AddListItem("A list item with ").AddItalic("italic text");
                list.AddListItem("A list item with ").AddCodeInline("inline code");
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
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode list = rootNode.AddListUnordered();
                IMarkdownSyntaxNode list2 = list.AddListItem("This list has").AddListUnordered();
                IMarkdownSyntaxNode list3 = list2.AddListItem("Uneven indentation").AddListUnordered();
                IMarkdownSyntaxNode list4 = list3.AddListItem("But still renders").AddListUnordered();
                list4.AddListItem("Correctly!");
            }
        );


        yield return static () => new MarkdownTestDto(SectionName,
            """
            1. this
            2. [ ] is
            3. [x] a
              - [X] nested
            4. todo list
            """,
            """
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
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode list = rootNode.AddListOrdered();
                list.AddListItem("this");
                IMarkdownSyntaxNode item1 = list.AddListItem();
                item1.AddCheckboxUnselected();
                item1.WithContent("is");

                IMarkdownSyntaxNode item2 = list.AddListItem();
                item2.AddCheckboxSelected();
                item2.WithContent("a");
                IMarkdownSyntaxNode sublist = item2.AddListUnordered();
                IMarkdownSyntaxNode item3 = sublist.AddListItem();
                item3.AddCheckboxSelected();
                item3.WithContent("nested");

                list.AddListItem("todo list");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            - [ ] Task with sublist
              - SubItem A
              - SubItem B
            - [x] Completed task with sublist
              - SubItem C
              - SubItem D
            """,
            """
            <ul>
                <li><input type="checkbox" disabled />Task with sublist
                    <ul>
                        <li>SubItem A</li>
                        <li>SubItem B</li>
                    </ul>
                </li>
                <li><input type="checkbox" disabled checked />Completed task with sublist
                    <ul>
                        <li>SubItem C</li>
                        <li>SubItem D</li>
                    </ul>
                </li>
            </ul>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode list = rootNode.AddListUnordered();
                IMarkdownSyntaxNode item0 = list.AddListItem();
                item0.AddCheckboxUnselected();
                item0.WithContent("Task with sublist");
                IMarkdownSyntaxNode nestedList0 = item0.AddListUnordered();
                nestedList0.AddListItem("SubItem A");
                nestedList0.AddListItem("SubItem B");

                IMarkdownSyntaxNode item1 = list.AddListItem();
                item1.AddCheckboxSelected();
                item1.WithContent("Completed task with sublist");
                IMarkdownSyntaxNode nestedList1 = item1.AddListUnordered();
                nestedList1.AddListItem("SubItem C");
                nestedList1.AddListItem("SubItem D");
            }
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
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode list = rootNode.AddListOrdered();
                list.AddListItem("Ordered task list");

                IMarkdownSyntaxNode item1 = list.AddListItem();
                item1.AddCheckboxUnselected();
                item1.WithContent("Incomplete task in ordered list");

                IMarkdownSyntaxNode item2 = list.AddListItem();
                item2.AddCheckboxSelected();
                item2.WithContent("Completed task in ordered list");

            }
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
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode list = rootNode.AddListUnordered();
                IMarkdownSyntaxNode item0 = list.AddListItem();
                item0.AddCheckboxSelected();
                item0.WithContent("Mixed list");

                IMarkdownSyntaxNode nestedList = item0.AddListOrdered();
                IMarkdownSyntaxNode item0A = nestedList.AddListItem();
                item0A.AddCheckboxUnselected();
                item0A.WithContent("Subtask 1");
                IMarkdownSyntaxNode item0B = nestedList.AddListItem();
                item0B.AddCheckboxSelected();
                item0B.WithContent("Subtask 2");

                list.AddListItem("Another item");
            }
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
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode list0 = rootNode.AddListUnordered();
                list0.AddListItem("Normal item");
                IMarkdownSyntaxNode item01 = list0.AddListItem();
                item01.AddCheckboxUnselected();
                item01.WithContent("Task item without sublist");

                rootNode.AddParagraph("Unrelated paragraph between list items.");

                IMarkdownSyntaxNode list1 = rootNode.AddListUnordered();
                IMarkdownSyntaxNode item10 = list1.AddListItem();
                item10.AddCheckboxSelected();
                item10.WithContent("Another completed task");
            }
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
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode list0 = rootNode.AddListUnordered();
                list0.AddListItem("Normal item");
                IMarkdownSyntaxNode item01 = list0.AddListItem();
                item01.AddCheckboxUnselected();
                item01.WithContent("Task item without sublist");
                item01.AddParagraph("related paragraph between list items.");

                IMarkdownSyntaxNode list1 = rootNode.AddListUnordered();
                IMarkdownSyntaxNode item10 = list1.AddListItem();
                item10.AddCheckboxSelected();
                item10.WithContent("Another completed task");
            }
        );

    }
}
