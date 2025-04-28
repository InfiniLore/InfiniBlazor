// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.SyntaxTreeConverters;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class ToStringConverter : SimpleSyntaxTreeConverter, IMarkdownSyntaxTreeConverter<string> {
    public async ValueTask<string> ConvertAsync(IMarkdownSyntaxTree tree, CancellationToken ct = default) {
        StringBuilder builder = PoolCache.StringBuilderPool.Get();
        try {
            await ProcessNodeTree(tree, builder, static (sb, content) => sb.Append(content), ct);
            return builder.ToString();
        }
        finally {
            PoolCache.StringBuilderPool.Return(builder);
        }
    }
}

