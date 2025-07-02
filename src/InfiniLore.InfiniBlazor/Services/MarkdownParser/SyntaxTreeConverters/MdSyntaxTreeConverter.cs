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
    private static readonly Lock PoolLock = new();
    private static readonly ObjectPool<SimpleMdSyntaxNodeConverter> SimpleSyntaxNodeConverterPool = 
        Pooling.CreateResettablePool<SimpleMdSyntaxNodeConverter>(Pooling.ParsersRetained);
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public MarkupString ConvertToMarkupString(IMdSyntaxTree tree) 
        => new(ConvertToString(tree));
    

    public string ConvertToString(IMdSyntaxTree tree) {
        StringBuilder builder;
        SimpleMdSyntaxNodeConverter converter;
    
        lock (PoolLock) {
            builder = GlobalPools.StringBuilder.Get();
            converter = SimpleSyntaxNodeConverterPool.Get();
        }
    
        try {
            converter.Sb = builder;
            BaseMdSyntaxTreeConverter.ProcessNodeTree(tree, converter);
            return builder.ToString();
        }
        finally {
            lock (PoolLock) {
                builder.Clear();
                GlobalPools.StringBuilder.Return(builder);
                SimpleSyntaxNodeConverterPool.Return(converter);
            }
        }
    }

}