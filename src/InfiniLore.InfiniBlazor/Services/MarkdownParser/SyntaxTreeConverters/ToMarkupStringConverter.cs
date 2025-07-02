// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using Microsoft.AspNetCore.Components;
using System.Text;

namespace InfiniLore.InfiniBlazor.MarkdownParser.SyntaxTreeConverters;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownSyntaxTreeConverter<MarkupString>>]
public class ToStyledStringConverter(IPoolCache poolCache) : StyledSyntaxTreeConverter, IMarkdownSyntaxTreeConverter<MarkupString> {
    public MarkupString Convert(IMarkdownSyntaxTree tree) {
        StringBuilder builder = poolCache.StringBuilderPool.Get();
        try {
            ProcessNodeTree(tree, builder, static (sb, content) => sb.Append(content));
            string str = builder.ToString();
            return new MarkupString(str);
        }
        finally {
            poolCache.StringBuilderPool.Return(builder);
        }
    }
}

