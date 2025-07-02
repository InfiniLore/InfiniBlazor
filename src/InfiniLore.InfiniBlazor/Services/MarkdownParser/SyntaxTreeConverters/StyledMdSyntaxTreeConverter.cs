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
[InjectableSingleton<IMdSyntaxTreeConverter>("styled")]
public class StyledMdSyntaxTreeConverter : IMdSyntaxTreeConverter {
    private static readonly ObjectPool<StyledMdSyntaxNodeConverter> StyledSyntaxNodeConverterPool = Pooling.CreateResettablePool<StyledMdSyntaxNodeConverter>(4);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public MarkupString ConvertToMarkupString(IMdSyntaxTree tree) 
        => new(ConvertToString(tree));
    
    public string ConvertToString(IMdSyntaxTree tree) {
        StringBuilder builder = GlobalPools.StringBuilder.Get();
        StyledMdSyntaxNodeConverter converter = StyledSyntaxNodeConverterPool.Get();
        try {
            converter.Sb = builder;
            BaseMdSyntaxTreeConverter.ProcessNodeTree(tree, converter);
            return builder.ToString();
        }
        finally {
            GlobalPools.StringBuilder.Return(builder);
            StyledSyntaxNodeConverterPool.Return(converter);
        }
    }
}