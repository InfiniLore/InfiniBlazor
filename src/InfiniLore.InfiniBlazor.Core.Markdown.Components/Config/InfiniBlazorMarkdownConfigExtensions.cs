// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.MdBlazorComponents;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;

namespace InfiniLore.InfiniBlazor.Markdown.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class InfiniBlazorMarkdownConfigExtensions {
    public static InfiniBlazorMarkdownConfig WithDefaultEditorComponents(this InfiniBlazorMarkdownConfig config) {
        config.RegisterMdBlazorComponent<BlockQuoteMdSyntaxNode, MdInfiniBlockQuote>();
        config.RegisterMdBlazorComponent<BoldMdSyntaxNode, MdInfiniBold>();
        config.RegisterMdBlazorComponent<CalloutMdSyntaxNode, MdInfiniCallout>();
        config.RegisterMdBlazorComponent<CodeBlockMdSyntaxNode, MdInfiniCodeBlock>();
        config.RegisterMdBlazorComponent<CodeInlineMdSyntaxNode, MdInfiniCodeInline>();
        config.RegisterMdBlazorComponent<HtmlMdSyntaxNode, MdInfiniHtml>();
        config.RegisterMdBlazorComponent<TextMdSyntaxNode, MdInfiniText>();
        config.RegisterMdBlazorComponent<EmoteMdSyntaxNode, MdInfiniEmote>();
        config.RegisterMdBlazorComponent<EscapedCharacterMdSyntaxNode, MdInfiniEscapedCharacter>();
        config.RegisterMdBlazorComponent<HeadingMdSyntaxNode, MdInfiniHeading>();
        config.RegisterMdBlazorComponent<HeadingSimpleMdSyntaxNode, MdInfiniHeadingSimple>();
        config.RegisterMdBlazorComponent<HorizontalRuleMdSyntaxNode, MdInfiniHorizontalRule>();
        config.RegisterMdBlazorComponent<HtmlSpanMdSyntaxNode, MdInfiniHtmlSpan>();
        config.RegisterMdBlazorComponent<ImageMdSyntaxNode, MdInfiniImage>();
        config.RegisterMdBlazorComponent<ItalicMdSyntaxNode, MdInfiniItalic>();
        config.RegisterMdBlazorComponent<LinkMdSyntaxNode, MdInfiniLink>();
        config.RegisterMdBlazorComponent<ListItemMdSyntaxNode, MdInfiniListItem>();
        config.RegisterMdBlazorComponent<ListOrderedMdSyntaxNode, MdInfiniListOrdered>();
        config.RegisterMdBlazorComponent<ListUnOrderedMdSyntaxNode, MdInfiniListUnOrdered>();
        config.RegisterMdBlazorComponent<ParagraphMdSyntaxNode, MdInfiniParagraph>();
        config.RegisterMdBlazorComponent<StrikeMdSyntaxNode, MdInfiniStrike>();
        config.RegisterMdBlazorComponent<SubScriptMdSyntaxNode, MdInfiniSubScript>();
        config.RegisterMdBlazorComponent<SuperScriptMdSyntaxNode, MdInfiniSuperScript>();
        config.RegisterMdBlazorComponent<TableMdSyntaxNode, MdInfiniTable>();
        config.RegisterMdBlazorComponent<TagMdSyntaxNode, MdInfiniTag>();
        config.RegisterMdBlazorComponent<UnderlineMdSyntaxNode, MdInfiniUnderline>();
        config.RegisterMdBlazorComponent<UserMdSyntaxNode, MdInfiniUser>();
        config.RegisterMdBlazorComponent<WikiLinkMdSyntaxNode, MdInfiniWikiLink>();
        config.RegisterMdBlazorComponent<TemplateMdSyntaxNode, MdInfiniTemplate>();
        config.RegisterMdBlazorComponent<FootnoteReferenceMdSyntaxNode, MdInfiniFootnoteReference>();
        config.RegisterMdBlazorComponent<FootnoteDescriptionMdSyntaxNode, MdInfiniFootnoteDescription>();
        // config.RegisterBlazorComponent<NewLineMdSyntaxNode, MdInfiniNewLine>(); // Not implemented well yet, only as an example
        
        return config;
    }
}
