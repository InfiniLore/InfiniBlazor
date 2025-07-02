// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using CodeOfChaos.Extensions.ObjectPool;
using InfiniLore.InfiniBlazor.Markdown;
using Microsoft.Extensions.ObjectPool;
using System.Text;

namespace InfiniLore.InfiniBlazor.MarkdownParser.SyntaxTreeConverters;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownSyntaxTreeConverter<string>>]
public class ToStringConverter : IMarkdownSyntaxTreeConverter<string> {
    private static readonly ObjectPool<SimpleSyntaxNodeConverter> SimpleSyntaxNodeConverterPool = new DefaultObjectPool<SimpleSyntaxNodeConverter>(new ResettablePoolPolicy<SimpleSyntaxNodeConverter>());

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public string Convert(IMarkdownSyntaxTree tree) {
        StringBuilder builder = GlobalPools.StringBuilder.Get();
        SimpleSyntaxNodeConverter converter = SimpleSyntaxNodeConverterPool.Get();
        try {
            converter.Sb = builder;
            SyntaxTreeConverter.ProcessNodeTree(tree, converter);
            return builder.ToString();
        }
        finally {
            GlobalPools.StringBuilder.Return(builder);
            SimpleSyntaxNodeConverterPool.Return(converter);
        }
    }
}

