// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Components.DynamicMarkdownComponents;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Collections.Frozen;

namespace InfiniLore.InfiniBlazor.DynamicMdComponents;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class DynamicMdComponentConverterFactory {
    public static IDynamicMdComponentConverter Create(IServiceProvider serviceProvider) {
        var mapBuilder = new Dictionary<Type, DynamicMdComponentRecord>(32);

        mapBuilder.Register<BlockQuoteDynamicMdComponent, BlockQuoteMdSyntaxNode>();
        mapBuilder.Register<BoldDynamicMdComponent, BoldMdSyntaxNode>();
        mapBuilder.Register<CalloutDynamicMdComponent, CalloutMdSyntaxNode>();
        mapBuilder.Register<CodeBlockDynamicMdComponent, CodeBlockMdSyntaxNode>();
        mapBuilder.Register<CodeInlineDynamicMdComponent, CodeInlineMdSyntaxNode>();
        mapBuilder.Register<ContentDynamicMdComponent, ContentMdSyntaxNode>();
        mapBuilder.Register<EmoteDynamicMdComponent, EmoteMdSyntaxNode>();
        mapBuilder.Register<HeadingDynamicMdComponent, HeadingMdSyntaxNode>();
        mapBuilder.Register<HeadingSimpleDynamicMdComponent, HeadingSimpleMdSyntaxNode>();
        mapBuilder.Register<HorizontalRuleDynamicMdComponent, HorizontalRuleMdSyntaxNode>();
        mapBuilder.Register<HtmlSpanDynamicMdComponent, HtmlSpanMdSyntaxNode>();
        mapBuilder.Register<ImageDynamicMdComponent, ImageMdSyntaxNode>();
        mapBuilder.Register<ItalicDynamicMdComponent, ItalicMdSyntaxNode>();
        mapBuilder.Register<LinkDynamicMdComponent, LinkMdSyntaxNode>();
        mapBuilder.Register<ListOrderedDynamicMdComponent, ListOrderedMdSyntaxNode>();
        mapBuilder.Register<ListUnOrderedDynamicMdComponent, ListUnOrderedMdSyntaxNode>();
        mapBuilder.Register<ParagraphDynamicMdComponent, ParagraphMdSyntaxNode>();
        mapBuilder.Register<StrikeDynamicMdComponent, StrikeMdSyntaxNode>();
        mapBuilder.Register<SubScriptDynamicMdComponent, SubScriptMdSyntaxNode>();
        mapBuilder.Register<SuperScriptDynamicMdComponent, SuperScriptMdSyntaxNode>();
        mapBuilder.Register<TableDynamicMdComponent, TableMdSyntaxNode>();
        mapBuilder.Register<TagDynamicMdComponent, TagMdSyntaxNode>();
        mapBuilder.Register<UnderlineDynamicMdComponent, UnderlineMdSyntaxNode>();
        
        mapBuilder.TrimExcess();
        return new DynamicMdComponentConverter {
            NodeToComponentMap =  mapBuilder.ToFrozenDictionary(),
            SkippedComponentTypes = [
                typeof(EmptyLineMdSyntaxNode)
            ]
        };
    }

    private static void Register<TComponent, TNode>(this Dictionary<Type, DynamicMdComponentRecord> dictionary) where TComponent : DynamicMdComponentBase<TNode> where TNode : class, IMdSyntaxNode {
        int count = dictionary.Count;
        if (dictionary.Capacity < count + 1) dictionary.EnsureCapacity(count * 2);
        
        dictionary.Add(typeof(TNode), DynamicMdComponentRecord.FromType<TComponent, TNode>());   
    }
}
