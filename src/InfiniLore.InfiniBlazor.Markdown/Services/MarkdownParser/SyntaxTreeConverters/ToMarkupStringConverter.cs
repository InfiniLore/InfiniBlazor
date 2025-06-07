// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.SyntaxTreeConverters;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownSyntaxTreeConverter<MarkupString>>]
public class ToStyledStringConverter(IPoolCache poolCache) : StyledSyntaxTreeConverter, IMarkdownSyntaxTreeConverter<MarkupString> {
    public async ValueTask<MarkupString> ConvertAsync(IMarkdownSyntaxTree tree, CancellationToken ct = default) {
        StringBuilder builder = poolCache.StringBuilderPool.Get();
        try {
            await ProcessNodeTree(tree, builder, static (sb, content) => sb.Append(content), ct);
            string str = builder.ToString();
            return new MarkupString(str);
        }
        finally {
            poolCache.StringBuilderPool.Return(builder);
        }
    }
}

