// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Components.DynamicMarkdownComponents;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;
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
        mapBuilder.Register<CodeBlockDynamicMdComponent, CodeBlockMdSyntaxNode>();
        mapBuilder.Register<CodeInlineDynamicMdComponent, CodeInlineMdSyntaxNode>();
        mapBuilder.Register<EmoteDynamicMdComponent, EmoteMdSyntaxNode>();
        mapBuilder.Register<HeadingDynamicMdComponent, HeadingMdSyntaxNode>();
        mapBuilder.Register<HorizontalRuleDynamicMdComponent, HorizontalRuleMdSyntaxNode>();
        mapBuilder.Register<HtmlSpanDynamicMdComponent, HtmlSpanMdSyntaxNode>();
        mapBuilder.Register<ContentDynamicMdComponent, ContentMdSyntaxNode>();
        mapBuilder.Register<ImageDynamicMdComponent, ImageMdSyntaxNode>();
        mapBuilder.Register<ItalicDynamicMdComponent, ItalicMdSyntaxNode>();
        mapBuilder.Register<LinkDynamicMdComponent, LinkMdSyntaxNode>();
        mapBuilder.Register<ListOrderedDynamicMdComponent, ListOrderedMdSyntaxNode>();
        mapBuilder.Register<ListUnOrderedDynamicMdComponent, ListUnOrderedMdSyntaxNode>();
        mapBuilder.Register<ParagraphDynamicMdComponent, ParagraphMdSyntaxNode>();
        mapBuilder.Register<StrikeDynamicMdComponent, StrikeMdSyntaxNode>();
        mapBuilder.Register<SubScriptDynamicMdComponent, SubScriptMdSyntaxNode>();
        mapBuilder.Register<SuperScriptDynamicMdComponent, SuperScriptMdSyntaxNode>();
        mapBuilder.Register<TagDynamicMdComponent, TagMdSyntaxNode>();
        mapBuilder.Register<UnderlineDynamicMdComponent, UnderlineMdSyntaxNode>();
        
        mapBuilder.TrimExcess();
        return new DynamicMdComponentConverter {
            NodeToComponentMap =  mapBuilder.ToFrozenDictionary()
        };
    }

    private static void Register<TComponent, TNode>(this Dictionary<Type, DynamicMdComponentRecord> dictionary) where TComponent : DynamicMdComponentBase<TNode> where TNode : class, IMdSyntaxNode {
        int count = dictionary.Count;
        if (dictionary.Capacity < count + 1) dictionary.EnsureCapacity(count * 2);
        
        dictionary.Add(typeof(TNode), DynamicMdComponentRecord.FromType<TComponent, TNode>());   
    }
}
