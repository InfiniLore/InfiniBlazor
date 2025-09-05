// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor;
using InfiniLore.InfiniBlazor.Callouts;
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Markdown.Editors;
using InfiniLore.InfiniBlazor.Markdown.MdBlazorComponents;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using InfiniLore.InfiniBlazor.Toasting;
using System.Collections.Frozen;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ServiceCollectionExtensions {
    public static IServiceCollection AddInfiniBlazor(this IServiceCollection services, Action<InfiniBlazorConfig>? configure = null) {
        var config = new InfiniBlazorConfig(services);
        services.RegisterServicesFromInfiniLoreInfiniBlazor();
        services.AddLucideIcons();

        services.AddSingleton(CalloutStyleProviderFactory.Create);
        services.AddSingleton(MdStringMdSyntaxDeserializerFactory.Create);
        services.AddSingleton(TextEditorFactory.CreateTextEditor);
        
        RegisterMdBlazorComponents(config);
        
        configure?.Invoke(config);

        services.AddSingleton<IToastingConfig>(new FrozenToastingConfig {
            AutoRemoveDuration = config.ToastDefaultDuration,
            AppearanceComponentMapping = config.ToastAppearanceComponentMappings.ToFrozenDictionary(),
        });

        return services;
    }

    private static void RegisterMdBlazorComponents(InfiniBlazorConfig config) {
        config.Markdown.RegisterMdBlazorComponent<BlockQuoteMdSyntaxNode, MdInfiniBlockQuote>();
        config.Markdown.RegisterMdBlazorComponent<BoldMdSyntaxNode, MdInfiniBold>();
        config.Markdown.RegisterMdBlazorComponent<CalloutMdSyntaxNode, MdInfiniCallout>();
        config.Markdown.RegisterMdBlazorComponent<CodeBlockMdSyntaxNode, MdInfiniCodeBlock>();
        config.Markdown.RegisterMdBlazorComponent<CodeInlineMdSyntaxNode, MdInfiniCodeInline>();
        config.Markdown.RegisterMdBlazorComponent<HtmlMdSyntaxNode, MdInfiniHtml>();
        config.Markdown.RegisterMdBlazorComponent<TextMdSyntaxNode, MdInfiniText>();
        config.Markdown.RegisterMdBlazorComponent<EmoteMdSyntaxNode, MdInfiniEmote>();
        config.Markdown.RegisterMdBlazorComponent<EscapedCharacterMdSyntaxNode, MdInfiniEscapedCharacter>();
        config.Markdown.RegisterMdBlazorComponent<HeadingMdSyntaxNode, MdInfiniHeading>();
        config.Markdown.RegisterMdBlazorComponent<HeadingSimpleMdSyntaxNode, MdInfiniHeadingSimple>();
        config.Markdown.RegisterMdBlazorComponent<HorizontalRuleMdSyntaxNode, MdInfiniHorizontalRule>();
        config.Markdown.RegisterMdBlazorComponent<HtmlSpanMdSyntaxNode, MdInfiniHtmlSpan>();
        config.Markdown.RegisterMdBlazorComponent<ImageMdSyntaxNode, MdInfiniImage>();
        config.Markdown.RegisterMdBlazorComponent<ItalicMdSyntaxNode, MdInfiniItalic>();
        config.Markdown.RegisterMdBlazorComponent<LinkMdSyntaxNode, MdInfiniLink>();
        config.Markdown.RegisterMdBlazorComponent<ListItemMdSyntaxNode, MdInfiniListItem>();
        config.Markdown.RegisterMdBlazorComponent<ListOrderedMdSyntaxNode, MdInfiniListOrdered>();
        config.Markdown.RegisterMdBlazorComponent<ListUnOrderedMdSyntaxNode, MdInfiniListUnOrdered>();
        config.Markdown.RegisterMdBlazorComponent<ParagraphMdSyntaxNode, MdInfiniParagraph>();
        config.Markdown.RegisterMdBlazorComponent<StrikeMdSyntaxNode, MdInfiniStrike>();
        config.Markdown.RegisterMdBlazorComponent<SubScriptMdSyntaxNode, MdInfiniSubScript>();
        config.Markdown.RegisterMdBlazorComponent<SuperScriptMdSyntaxNode, MdInfiniSuperScript>();
        config.Markdown.RegisterMdBlazorComponent<TableMdSyntaxNode, MdInfiniTable>();
        config.Markdown.RegisterMdBlazorComponent<TagMdSyntaxNode, MdInfiniTag>();
        config.Markdown.RegisterMdBlazorComponent<UnderlineMdSyntaxNode, MdInfiniUnderline>();
        config.Markdown.RegisterMdBlazorComponent<UserMdSyntaxNode, MdInfiniUser>();
        config.Markdown.RegisterMdBlazorComponent<WikiLinkMdSyntaxNode, MdInfiniWikiLink>();
        config.Markdown.RegisterMdBlazorComponent<TemplateMdSyntaxNode, MdInfiniTemplate>();
        // config.Markdown.RegisterBlazorComponent<NewLineMdSyntaxNode, MdInfiniNewLine>(); // Not implemented well yet, only as an example
    }
}
