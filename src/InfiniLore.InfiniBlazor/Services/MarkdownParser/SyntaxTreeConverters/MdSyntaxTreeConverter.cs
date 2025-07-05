// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.SyntaxTreeConverters.Converters;
using InfiniLore.InfiniBlazor.Pooling;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.ObjectPool;
using System.Text;

namespace InfiniLore.InfiniBlazor.MarkdownParser.SyntaxTreeConverters;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class BaseMdSyntaxTreeConverter<TConverter> : IMdSyntaxTreeConverter where TConverter : class, IMdSyntaxNodeConverter, IResettable, new() {
    private readonly Lock PoolLock = new();
    private static readonly ObjectPool<TConverter> ConverterPool = 
        PoolingHelpers.CreateResettablePool<TConverter>(PoolingHelpers.ParsersRetained);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public MarkupString ConvertToMarkupString(IMdSyntaxTree tree) 
        => new(ConvertToString(tree));
    
    public string ConvertToString(IMdSyntaxTree tree) {
        StringBuilder builder;
        TConverter converter;
    
        // WTF why is a lock needed here? This makes no sense!
        lock (PoolLock) {
            builder = GlobalPools.StringBuilder.Get();
            converter = ConverterPool.Get();
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
                ConverterPool.Return(converter);
            }
        }
    }
}

[InjectableSingleton<IMdSyntaxTreeConverter>]
public sealed class MdSyntaxTreeConverter : BaseMdSyntaxTreeConverter<SimpleMdSyntaxNodeConverter>;

[InjectableSingleton<IMdSyntaxTreeConverter>("styled")]
public sealed class StyledMdSyntaxTreeConverter : BaseMdSyntaxTreeConverter<StyledMdSyntaxNodeConverter>;