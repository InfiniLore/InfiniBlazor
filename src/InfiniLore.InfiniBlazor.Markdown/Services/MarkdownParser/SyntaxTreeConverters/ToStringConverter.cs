// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.SyntaxTreeConverters;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownSyntaxTreeConverter<string>>]
public class ToStringConverter(IPoolCache poolCache) : SimpleSyntaxTreeConverter, IMarkdownSyntaxTreeConverter<string> {
    public async ValueTask<string> ConvertAsync(IMarkdownSyntaxTree tree, CancellationToken ct = default) {
        StringBuilder builder = poolCache.StringBuilderPool.Get();
        try {
            await ProcessNodeTree(tree, builder, static (sb, content) => sb.Append(content), ct);
            return builder.ToString();
        }
        finally {
            poolCache.StringBuilderPool.Return(builder);
        }
    }
}

