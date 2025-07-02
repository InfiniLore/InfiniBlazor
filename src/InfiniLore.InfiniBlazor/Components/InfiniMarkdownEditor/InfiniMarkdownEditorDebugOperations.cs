// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class InfiniMarkdownEditorDebugInserts {
    public const string LoremText = """
        Lorem ipsum dolor
        sit amet, consectetur
        adipiscing elit. Sed
        do eiusmod tempor
        incididunt ut labore
        et dolore magna aliqua.

        Ut enim ad minim veniam,
        quis nostrud exercitation
        ullamco laboris nisi ut
        aliquip ex ea commodo
        consequat.

        Duis aute irure dolor
        in reprehenderit in 
        voluptate velit esse
        cillum dolore eu fugiat
        nulla pariatur.

        Excepteur sint occaecat
        cupidatat non proident, 
        sunt in culpa qui officia
        deserunt mollit anim id
        est laborum.
        """;

    public const string TableText = """
        | test  | something |
        |  ---- | --------- |
        | alpha | beta      |
        """;

    public const string MarkdownText = """
        # Heading 1
        ## Heading 2
        ### Heading 3
        #### Heading 4
        ##### Heading 5
        ###### Heading 6
        
        Indirect Heading
        ---
        
        > This is a block quote
        
        > This is a quote.
        > Over multiple lines.
        
        ```csharp
        MdEditor editor = new();
        editor.DoSomething();
        ```
        
        Horizontal Rule
        
        ---
        
        - List item 1
        - List item 2
        - List item 3
        
        
        - [ ] Task 1
        - [x] Task 2
        - [ ] Task 3
        
        
        - list item
          - [ ] with a nested task
          
          
        - [ ] task item
          - with a nested list item
          
        1. Numbered list item 1
        2. Numbered list item 2
          - with a nested list item
          - [ ] with a nested task
          - [x] with a nested completed task
        
        
        A random paragraph
        
        
        | test  | something |
        |  ---- | --------- |
        | alpha | beta      |
        
        A paragragh with a **bold** word in it.
        A paragraph with an *italic* word in it.
        A paragraph with an _underline_ word in it.
        A paragraph with a ~strikethrough~ word in it.
        A paragraph with a `code` word in it.
        A paragraph with a [link](https://google.com) in it.
        A paragraph with a ![image](https://localhost:7006/icon.png) in it.
        A paragraph with an :flag-trans: emote in it.
        A paragraph with a ^subscript^ word in it.
        A paragraph with a ^^superscript^^ word in it.
        A paragraph with a #tag word in it.

        """;
}
