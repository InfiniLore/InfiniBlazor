// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown.Deserializer.NodeDeserializers;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Frozen;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown.Deserializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class MdStringMdSyntaxDeserializerFactory {
    public static IMdStringMdSyntaxDeserializer Create(IServiceProvider provider) {
        var instance = ActivatorUtilities.CreateInstance<MdStringMdSyntaxDeserializer>(provider);

        Dictionary<Type, IMdStringMdSyntaxNodeDeserializer> deserializers = new Dictionary<Type, IMdStringMdSyntaxNodeDeserializer>()
            .Register<BlockQuoteMdSyntaxNode, BlockQuoteSyntaxNodeDeserializer>(instance)
            .Register<BoldMdSyntaxNode, BoldSyntaxNodeDeserializer>(instance)
            // .Register<CalloutBodyMdSyntaxNode, CalloutBodySyntaxNodeDeserializer>(instance) // Not implemented due to the CalloutSyntaxNodeDeserializer handling them directly
            // .Register<CalloutTitleMdSyntaxNode, CalloutTitleSyntaxNodeDeserializer>(instance) // Not implemented due to the CalloutSyntaxNodeDeserializer handling them directly
            .Register<CalloutMdSyntaxNode, CalloutSyntaxNodeDeserializer>(instance)
            .Register<CodeBlockMdSyntaxNode, CodeBlockSyntaxNodeDeserializer>(instance)
            .Register<CodeInlineMdSyntaxNode, CodeInlineSyntaxNodeDeserializer>(instance)
            .Register<HtmlMdSyntaxNode, ContentHtmlSyntaxNodeDeserializer>(instance)
            .Register<TextMdSyntaxNode, ContentSyntaxNodeDeserializer>(instance)
            .Register<EmoteMdSyntaxNode, EmoteSyntaxNodeDeserializer>(instance)
            .Register<NewLineMdSyntaxNode, NewLineSyntaxNodeDeserializer>(instance)
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
            .Register<UnderlineMdSyntaxNode, UnderlineSyntaxNodeDeserializer>(instance)
            .Register<UserMdSyntaxNode, UserSyntaxNodeDeserializer>(instance)
            .Register<WikiLinkMdSyntaxNode, WikiLinkSyntaxNodeDeserializer>(instance)
            .Register<TemplateMdSyntaxNode, TemplateSyntaxNodeDeserializer>(instance)
            .Register<FootnoteReferenceMdSyntaxNode, FootnoteReferenceSyntaxNodeDeserializer>(instance)
            .Register<FootnoteDescriptionMdSyntaxNode, FootnoteDescriptionSyntaxNodeDeserializer>(instance)
            .Register<HighlightMdSyntaxNode, HighlightSyntaxNodeDeserializer>(instance)
            .Register<WrapperMdSyntaxNode, WrapperSyntaxNodeDeserializer>(instance);

        instance.Deserializers = deserializers.ToFrozenDictionary();
        return instance;
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Helper Methods
    // -----------------------------------------------------------------------------------------------------------------
    private static Dictionary<Type, IMdStringMdSyntaxNodeDeserializer> Register<TNode, TDeserializer>(
        this Dictionary<Type, IMdStringMdSyntaxNodeDeserializer> deserializers,
        IMdStringMdSyntaxDeserializer instance
    ) where TDeserializer : MdStringMdSyntaxNodeDeserializerBase<TNode>, new() where TNode : IMdSyntaxNode {
        deserializers.AddOrUpdate(typeof(TNode), new TDeserializer {
            Deserializer = instance
        });
        return deserializers;
    }
}
