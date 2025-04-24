// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.NodeTreeConverters;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class NodeTreeToStringConverter : SimpleNodeTreeConverter, IMdNodeTreeConverter<string> {
    public string Convert(IMdNodeTree tree) {
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

