// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Components.DynamicMarkdownComponents;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Collections.Frozen;
using Infini_MdHtmlSpan = InfiniLore.InfiniBlazor.Markdown.Parsers.Blazor.Components.Infini_MdHtmlSpan;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Blazor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class InfiniBlazorMdComponentConverterFactory {
    public static IBlazorMdComponentConverter Create(IServiceProvider serviceProvider) {
        var mapBuilder = new Dictionary<Type, BlazorMdComponentRecord>(32);

        mapBuilder.Register<BlockQuoteMdSyntaxNode, Infini_MdBlockQuote>();
        mapBuilder.Register<BoldMdSyntaxNode, Infini_MdBold>();
        mapBuilder.Register<CalloutMdSyntaxNode, Infini_MdCallout>();
        mapBuilder.Register<CodeBlockMdSyntaxNode, Infini_MdCodeBlock>();
        mapBuilder.Register<CodeInlineMdSyntaxNode, Infini_MdCodeInline>();
        mapBuilder.Register<ContentHtmlMdSyntaxNode, Infini_MdContentHtml>();
        mapBuilder.Register<ContentMdSyntaxNode, Infini_MdContent>();
        mapBuilder.Register<EmoteMdSyntaxNode, Infini_MdEmote>();
        mapBuilder.Register<EscapedCharacterMdSyntaxNode, Infini_MdEscapedCharacter>();
        mapBuilder.Register<HeadingMdSyntaxNode, Infini_MdHeading>();
        mapBuilder.Register<HeadingSimpleMdSyntaxNode, Infini_MdHeadingSimple>();
        mapBuilder.Register<HorizontalRuleMdSyntaxNode, Infini_MdHorizontalRule>();
        mapBuilder.Register<HtmlSpanMdSyntaxNode, Infini_MdHtmlSpan>();
        mapBuilder.Register<ImageMdSyntaxNode, Infini_MdImage>();
        mapBuilder.Register<ItalicMdSyntaxNode, Infini_MdItalic>();
        mapBuilder.Register<LinkMdSyntaxNode, Infini_MdLink>();
        mapBuilder.Register<ListItemMdSyntaxNode, Infini_MdListItem>();
        mapBuilder.Register<ListOrderedMdSyntaxNode, Infini_MdListOrdered>();
        mapBuilder.Register<ListUnOrderedMdSyntaxNode, Infini_MdListUnOrdered>();
        mapBuilder.Register<ParagraphMdSyntaxNode, Infini_MdParagraph>();
        mapBuilder.Register<StrikeMdSyntaxNode, Infini_MdStrike>();
        mapBuilder.Register<SubScriptMdSyntaxNode, Infini_MdSubScript>();
        mapBuilder.Register<SuperScriptMdSyntaxNode, Infini_MdSuperScript>();
        mapBuilder.Register<TableMdSyntaxNode, Infini_MdTable>();
        mapBuilder.Register<TagMdSyntaxNode, Infini_MdTag>();
        mapBuilder.Register<UnderlineMdSyntaxNode, Infini_MdUnderline>();
        
        
        mapBuilder.TrimExcess();
        return new BlazorMdComponentConverter {
            NodeToComponentMap =  mapBuilder.ToFrozenDictionary(),
            SkippedComponentTypes = [
                typeof(NewLineMdSyntaxNode)
            ]
        };
    }

    private static void Register<TNode, TComponent>(this Dictionary<Type, BlazorMdComponentRecord> dictionary) where TComponent : InfiniBlazorMdComponentBase<TNode> where TNode : class, IMdSyntaxNode {
        int count = dictionary.Count;
        if (dictionary.Capacity < count + 1) dictionary.EnsureCapacity(count * 2);
        
        dictionary.Add(typeof(TNode), BlazorMdComponentRecord.FromType<TComponent, TNode>());   
    }
}
