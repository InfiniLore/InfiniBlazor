// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.SyntaxTreeConverters.Converters;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.ObjectPool;
using System.Text;

namespace InfiniLore.InfiniBlazor.MarkdownParser.SyntaxTreeConverters;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMdSyntaxTreeConverter>]
public class MdSyntaxTreeConverter : IMdSyntaxTreeConverter {
    private static readonly ObjectPool<SimpleMdSyntaxNodeConverter> SimpleSyntaxNodeConverterPool = Pooling.CreateResettablePool<SimpleMdSyntaxNodeConverter>(4);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public MarkupString ConvertToMarkupString(IMdSyntaxTree tree) 
        => new(ConvertToString(tree));
    
    public string ConvertToString(IMdSyntaxTree tree) {
        StringBuilder builder = GlobalPools.StringBuilder.Get();
        SimpleMdSyntaxNodeConverter converter = SimpleSyntaxNodeConverterPool.Get();
        try {
            converter.Sb = builder;
            BaseMdSyntaxTreeConverter.ProcessNodeTree(tree, converter);
            return builder.ToString();
        }
        finally {
            GlobalPools.StringBuilder.Return(builder);
            SimpleSyntaxNodeConverterPool.Return(converter);
        }
    }
}