// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.NodeTreeConverters;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class ToStringConverter : SimpleSyntaxTreeConverter, IMarkdownSyntaxTreeConverter<string> {
    public string Convert(IMarkdownSyntaxTree tree) {
        StringBuilder builder = PoolCache.StringBuilderPool.Get();
        try {
            ProcessNodeTree(tree, builder, static (sb, content) => sb.Append(content));
            return builder.ToString();
        }
        finally {
            PoolCache.StringBuilderPool.Return(builder);
        }
    }
}

