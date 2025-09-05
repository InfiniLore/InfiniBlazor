// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Parsers.Blazor.Components;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Collections.Frozen;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Blazor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class InfiniBlazorMdComponentConverterFactory {
    public static IBlazorMdComponentConverter Create(IServiceProvider serviceProvider) {
        var mapBuilder = new Dictionary<Type, MdComponentRecord>(32);

        mapBuilder.Register<BlockQuoteMdSyntaxNode, MdInfiniBlockQuote>();
        mapBuilder.Register<BoldMdSyntaxNode, MdInfiniBold>();
        mapBuilder.Register<CalloutMdSyntaxNode, MdInfiniCallout>();
        mapBuilder.Register<CodeBlockMdSyntaxNode, MdInfiniCodeBlock>();
        mapBuilder.Register<CodeInlineMdSyntaxNode, MdInfiniCodeInline>();
        mapBuilder.Register<HtmlMdSyntaxNode, MdInfiniHtml>();
        mapBuilder.Register<TextMdSyntaxNode, MdInfiniText>();
        mapBuilder.Register<EmoteMdSyntaxNode, MdInfiniEmote>();
        mapBuilder.Register<EscapedCharacterMdSyntaxNode, MdInfiniEscapedCharacter>();
        mapBuilder.Register<HeadingMdSyntaxNode, MdInfiniHeading>();
        mapBuilder.Register<HeadingSimpleMdSyntaxNode, MdInfiniHeadingSimple>();
        mapBuilder.Register<HorizontalRuleMdSyntaxNode, MdInfiniHorizontalRule>();
        mapBuilder.Register<HtmlSpanMdSyntaxNode, MdInfiniHtmlSpan>();
        mapBuilder.Register<ImageMdSyntaxNode, MdInfiniImage>();
        mapBuilder.Register<ItalicMdSyntaxNode, MdInfiniItalic>();
        mapBuilder.Register<LinkMdSyntaxNode, MdInfiniLink>();
        mapBuilder.Register<ListItemMdSyntaxNode, MdInfiniListItem>();
        mapBuilder.Register<ListOrderedMdSyntaxNode, MdInfiniListOrdered>();
        mapBuilder.Register<ListUnOrderedMdSyntaxNode, MdInfiniListUnOrdered>();
        mapBuilder.Register<ParagraphMdSyntaxNode, MdInfiniParagraph>();
        mapBuilder.Register<StrikeMdSyntaxNode, MdInfiniStrike>();
        mapBuilder.Register<SubScriptMdSyntaxNode, MdInfiniSubScript>();
        mapBuilder.Register<SuperScriptMdSyntaxNode, MdInfiniSuperScript>();
        mapBuilder.Register<TableMdSyntaxNode, MdInfiniTable>();
        mapBuilder.Register<TagMdSyntaxNode, MdInfiniTag>();
        mapBuilder.Register<UnderlineMdSyntaxNode, MdInfiniUnderline>();
        mapBuilder.Register<UserMdSyntaxNode, MdInfiniUser>();
        mapBuilder.Register<WikiLinkMdSyntaxNode, MdInfiniWikiLink>();
        mapBuilder.Register<TemplateMdSyntaxNode, MdInfiniTemplate>();
        // mapBuilder.Register<NewLineMdSyntaxNode, MdInfiniNewLine>(); // Not implemented well yet, only as an example
        
        mapBuilder.TrimExcess();
        return new BlazorMdComponentConverter {
            NodeToComponentMap =  mapBuilder.ToFrozenDictionary(),
            SkippedComponentTypes = [
                typeof(NewLineMdSyntaxNode)
            ],
            RenderUnknownComponents = true
        };
    }

    private static void Register<TNode, TComponent>(this Dictionary<Type, MdComponentRecord> dictionary) where TComponent : InfiniBlazorMdComponentBase<TNode> where TNode : class, IMdSyntaxNode {
        
    }
}
