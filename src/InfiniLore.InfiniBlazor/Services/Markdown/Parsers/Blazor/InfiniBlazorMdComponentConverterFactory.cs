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

        mapBuilder.Register<Infini_MdBlockQuote, BlockQuoteMdSyntaxNode>();
        mapBuilder.Register<Infini_MdBold, BoldMdSyntaxNode>();
        mapBuilder.Register<Infini_MdCallout, CalloutMdSyntaxNode>();
        mapBuilder.Register<Infini_MdCodeBlock, CodeBlockMdSyntaxNode>();
        mapBuilder.Register<Infini_MdCodeInline, CodeInlineMdSyntaxNode>();
        mapBuilder.Register<Infini_MdContent, ContentMdSyntaxNode>();
        mapBuilder.Register<Infini_MdEmote, EmoteMdSyntaxNode>();
        mapBuilder.Register<Infini_MdHeading, HeadingMdSyntaxNode>();
        mapBuilder.Register<Infini_MdHeadingSimple, HeadingSimpleMdSyntaxNode>();
        mapBuilder.Register<Infini_MdHorizontalRule, HorizontalRuleMdSyntaxNode>();
        mapBuilder.Register<Infini_MdHtmlSpan, HtmlSpanMdSyntaxNode>();
        mapBuilder.Register<Infini_MdImage, ImageMdSyntaxNode>();
        mapBuilder.Register<Infini_MdItalic, ItalicMdSyntaxNode>();
        mapBuilder.Register<Infini_MdLink, LinkMdSyntaxNode>();
        mapBuilder.Register<Infini_MdListOrdered, ListOrderedMdSyntaxNode>();
        mapBuilder.Register<Infini_MdListUnOrdered, ListUnOrderedMdSyntaxNode>();
        mapBuilder.Register<Infini_MdParagraph, ParagraphMdSyntaxNode>();
        mapBuilder.Register<Infini_MdStrike, StrikeMdSyntaxNode>();
        mapBuilder.Register<Infini_MdSubScript, SubScriptMdSyntaxNode>();
        mapBuilder.Register<Infini_MdSuperScript, SuperScriptMdSyntaxNode>();
        mapBuilder.Register<Infini_MdTable, TableMdSyntaxNode>();
        mapBuilder.Register<Infini_MdTag, TagMdSyntaxNode>();
        mapBuilder.Register<Infini_MdUnderline, UnderlineMdSyntaxNode>();
        
        mapBuilder.TrimExcess();
        return new BlazorMdComponentConverter {
            NodeToComponentMap =  mapBuilder.ToFrozenDictionary(),
            SkippedComponentTypes = [
                typeof(NewLineMdSyntaxNode)
            ]
        };
    }

    private static void Register<TComponent, TNode>(this Dictionary<Type, BlazorMdComponentRecord> dictionary) where TComponent : InfiniBlazorMdComponentBase<TNode> where TNode : class, IMdSyntaxNode {
        int count = dictionary.Count;
        if (dictionary.Capacity < count + 1) dictionary.EnsureCapacity(count * 2);
        
        dictionary.Add(typeof(TNode), BlazorMdComponentRecord.FromType<TComponent, TNode>());   
    }
}
