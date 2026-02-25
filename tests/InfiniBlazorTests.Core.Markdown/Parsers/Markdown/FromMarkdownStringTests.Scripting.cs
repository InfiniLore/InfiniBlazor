// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;

namespace InfiniBlazorTests.Core.Markdown.Parsers.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class FromMarkdownTests {
    public static IEnumerable<Func<string>> ScriptingExampleTestCases() {
        yield return () => """
        @if(character.hp==stats.MinHp or character.hp<5) 
        **alive**
        
        @elseif(a==b)
        *maybe*
        
        @elseif(a<b)
        *maybe not*
        
        @else 
        **ded**
        
        @endif
        
        Not Part of the if statement
        """;
        
        yield return () => """
        @if(character.hp==stats.MinHp or character.hp<5) 
          @if(something) 
          **alive**
          @elseif(else)
          *maybe*
          @else 
          **ded**
          @endif

        @elseif(a==b)
        *maybe*

        @elseif(a<b)
        *maybe not*

        @else 
        **ded**

        @endif

        Not Part of the if statement
        """;
    }
    
    [Test]
    [MethodDataSource(nameof(ScriptingExampleTestCases))]
    public async Task ScriptingExample_FromMarkdown_ToSyntaxTree(string markdown) {
        // Arrange

        // Act
        IMdSyntaxTree foundTree = parser.Markdown.SerializeToSyntaxTree(markdown);

        // Assert
        await Assert.That(foundTree).IsNotNull();
        await Assert.That(foundTree.RootNode.ChildCount).IsEqualTo(4);
    }
}
