// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Components.DynamicMarkdownComponents;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;
using System.Collections.Frozen;

namespace InfiniLore.InfiniBlazor.DynamicMdComponents;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class DynamicMdComponentConverterFactory {
    public static IDynamicMdComponentConverter Create(IServiceProvider serviceProvider) {
        var mapBuilder = new Dictionary<Type, DynamicMdComponentRecord> {
            [typeof(BlockQuoteMdSyntaxNode)] = DynamicMdComponentRecord.FromType<BlockQuoteDynamicMdComponent, BlockQuoteMdSyntaxNode>(),
            [typeof(BoldMdSyntaxNode)] = DynamicMdComponentRecord.FromType<BoldDynamicMdComponent, BoldMdSyntaxNode>(),
            [typeof(CodeBlockMdSyntaxNode)] = DynamicMdComponentRecord.FromType<CodeBlockDynamicMdComponent, CodeBlockMdSyntaxNode>(),
            [typeof(CodeInlineMdSyntaxNode)] = DynamicMdComponentRecord.FromType<CodeInlineDynamicMdComponent, CodeInlineMdSyntaxNode>(),
            [typeof(HeadingMdSyntaxNode)] = DynamicMdComponentRecord.FromType<HeadingDynamicMdComponent, HeadingMdSyntaxNode>(),
            [typeof(HtmlSpanMdSyntaxNode)] = DynamicMdComponentRecord.FromType<HtmlSpanDynamicMdComponent, HtmlSpanMdSyntaxNode>(),
            [typeof(ContentMdSyntaxNode)] = DynamicMdComponentRecord.FromType<ContentDynamicMdComponent, ContentMdSyntaxNode>()
        };
        
        return new DynamicMdComponentConverter {
            NodeToComponentMap =  mapBuilder.ToFrozenDictionary()
        };
    }
}
