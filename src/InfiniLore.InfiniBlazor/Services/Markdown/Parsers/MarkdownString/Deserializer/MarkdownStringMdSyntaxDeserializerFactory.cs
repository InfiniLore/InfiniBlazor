// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Frozen;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class MarkdownStringMdSyntaxDeserializerFactory {
    public static IMarkdownStringMdSyntaxDeserializer Create(IServiceProvider provider) {
        var instance = ActivatorUtilities.CreateInstance<MarkdownStringMdSyntaxDeserializer>(provider);

        Dictionary<Type, IMarkdownStringMdSyntaxNodeDeserializer> deserializers = new Dictionary<Type, IMarkdownStringMdSyntaxNodeDeserializer>()
            .Register<BlockQuoteMdSyntaxNode, BlockQuoteSyntaxNodeDeserializer>(instance)
            .Register<BoldMdSyntaxNode, BoldSyntaxNodeDeserializer>(instance)
            // .Register<CalloutBodyMdSyntaxNode, CalloutBodySyntaxNodeDeserializer>(instance) // Not implemented due to the CalloutSyntaxNodeDeserializer handling them directly
            // .Register<CalloutTitleMdSyntaxNode, CalloutTitleSyntaxNodeDeserializer>(instance) // Not implemented due to the CalloutSyntaxNodeDeserializer handling them directly
            .Register<CalloutMdSyntaxNode, CalloutSyntaxNodeDeserializer>(instance)
            .Register<CodeBlockMdSyntaxNode, CodeBlockSyntaxNodeDeserializer>(instance)
            .Register<CodeInlineMdSyntaxNode, CodeInlineSyntaxNodeDeserializer>(instance)
            .Register<ContentHtmlMdSyntaxNode, ContentHtmlSyntaxNodeDeserializer>(instance)
            .Register<ContentMdSyntaxNode, ContentSyntaxNodeDeserializer>(instance)
            .Register<EmoteMdSyntaxNode, EmoteSyntaxNodeDeserializer>(instance)
            .Register<EmptyLineMdSyntaxNode, EmptyLineSyntaxNodeDeserializer>(instance)
            .Register<EscapedCharacterMdSyntaxNode, EscapedCharacterSyntaxNodeDeserializer>(instance)
            .Register<HeadingMdSyntaxNode, HeadingSyntaxNodeDeserializer>(instance)
            .Register<HeadingSimpleMdSyntaxNode, HeadingSimpleSyntaxNodeDeserializer>(instance)
            .Register<HorizontalRuleMdSyntaxNode, HorizontalRuleSyntaxNodeDeserializer>(instance)
            .Register<HtmlSpanMdSyntaxNode, HtmlSpanSyntaxNodeDeserializer>(instance)
            .Register<ImageMdSyntaxNode, ImageSyntaxNodeDeserializer>(instance)
            .Register<ItalicMdSyntaxNode, ItalicSyntaxNodeDeserializer>(instance)
            .Register<LinkMdSyntaxNode, LinkSyntaxNodeDeserializer>(instance)
            .Register<ListItemMdSyntaxNode, ListItemSyntaxNodeDeserializer>(instance)
            .Register<ListOrderedMdSyntaxNode, ListOrderedSyntaxNodeDeserializer>(instance)
            .Register<ListUnOrderedMdSyntaxNode, ListUnOrderedSyntaxNodeDeserializer>(instance)
            .Register<ParagraphMdSyntaxNode, ParagraphSyntaxNodeDeserializer>(instance)
            // Register<RootMdSyntaxNode, RootSyntaxNodeDeserializer>(instance) // Is a semantic node and cannot be processed
            .Register<StrikeMdSyntaxNode, StrikeSyntaxNodeDeserializer>(instance)
            .Register<SubScriptMdSyntaxNode, SubScriptSyntaxNodeDeserializer>(instance)
            .Register<SuperScriptMdSyntaxNode, SuperScriptSyntaxNodeDeserializer>(instance)
            // .Register<TableCellMdSyntaxNode, TableCellSyntaxNodeDeserializer>(instance) // Not implemented due to the TableSyntaxNodeDeserializer handling them directly
            // .Register<TableRowMdSyntaxNode, TableRowSyntaxNodeDeserializer>(instance) // Not implemented due to the TableSyntaxNodeDeserializer handling them directly
            .Register<TableMdSyntaxNode, TableSyntaxNodeDeserializer>(instance)
            .Register<TagMdSyntaxNode, TagSyntaxNodeDeserializer>(instance)
            .Register<UnderlineMdSyntaxNode, UnderlineSyntaxNodeDeserializer>(instance);
        
        instance.Deserializers = deserializers.ToFrozenDictionary();
        return instance;
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Helper Methods
    // -----------------------------------------------------------------------------------------------------------------
    private static Dictionary<Type, IMarkdownStringMdSyntaxNodeDeserializer> Register<TNode, TDeserializer>(
        this Dictionary<Type, IMarkdownStringMdSyntaxNodeDeserializer> deserializers,
        IMarkdownStringMdSyntaxDeserializer instance
    ) where TDeserializer : BaseMarkdownStringMdSyntaxNodeDeserializer<TNode>, new() where TNode : IMdSyntaxNode {
        deserializers.AddOrUpdate(typeof(TNode), new TDeserializer {
            Deserializer = instance
        });
        return deserializers;
    }
}
