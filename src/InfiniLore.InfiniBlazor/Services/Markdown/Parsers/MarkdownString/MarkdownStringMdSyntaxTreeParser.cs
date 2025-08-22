// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Pooling;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MarkdownStringMdSyntaxTreeParser : IMarkdownStringMdSyntaxTreeParser {

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public string SerializeToString(IMdSyntaxTree tree) {
        StringBuilder builder = GlobalPools.StringBuilder.Get();
        try {
            foreach (IMdSyntaxNode child in tree.RootNode.GetChildren()) {
                SerializeNode(child, builder);
            }
            return builder.ToString();
        }
        finally {
            GlobalPools.StringBuilder.Return(builder);
        }
    }
    
    private void SerializeNode(IMdSyntaxNode child, StringBuilder builder) {
        throw new NotImplementedException();
    }
}
