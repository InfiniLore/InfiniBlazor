// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using System.Text;

namespace InfiniLore.InfiniBlazor.SyntaxTreeConverters;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownSyntaxTreeConverter<string>>]
public class ToStringConverter(IPoolCache poolCache) : SimpleSyntaxTreeConverter, IMarkdownSyntaxTreeConverter<string> {
    public string Convert(IMarkdownSyntaxTree tree) {
        StringBuilder builder = poolCache.StringBuilderPool.Get();
        try {
            ProcessNodeTree(tree, builder, static (sb, content) => sb.Append(content));
            return builder.ToString();
        }
        finally {
            poolCache.StringBuilderPool.Return(builder);
        }
    }
}

