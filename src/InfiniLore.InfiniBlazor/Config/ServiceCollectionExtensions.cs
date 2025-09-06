// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor;
using InfiniLore.InfiniBlazor.Components;
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Core.Js;
using InfiniLore.InfiniBlazor.Markdown.Editors;
using InfiniLore.InfiniBlazor.Markdown.MdBlazorComponents;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ServiceCollectionExtensions {
    public static IServiceCollection AddInfiniBlazor(this IServiceCollection services, Action<InfiniBlazorConfig>? configure = null) {
        var config = new InfiniBlazorConfig(services); // Handles a lot of the boilerplate for each core package
        services.RegisterServicesFromInfiniLoreInfiniBlazor();
        services.RegisterServicesFromInfiniLoreInfiniBlazorCoreJs();
        services.AddLucideIcons();

        services.AddSingleton(CalloutStyleProviderFactory.Create);
        services.AddSingleton(MdStringMdSyntaxDeserializerFactory.Create);
        services.AddSingleton(TextEditorFactory.CreateTextEditor);

        RegisterMdBlazorComponents(config);

        services.AddHttpClient(HttpClientNames.InfiniBlazor, static (serviceProvider, client) => {
            var config = serviceProvider.GetService<IConfiguration>();
            string? baseUrl = null;

            // In WASM, use NavigationManager to get the browser's origin
            try {
                if (serviceProvider.GetService<NavigationManager>() is {} navManager) {
                    baseUrl = navManager.BaseUri;
                }
            }
            catch (Exception) {
                // Ignore
            }
            
            // In Server, use BaseUrl, or parse ASPNETCORE_URLS
            if (baseUrl.IsNullOrWhiteSpace()) {
                baseUrl = config?["BaseUrl"];

                if (baseUrl.IsNullOrWhiteSpace()) {
                    string? urls = config?["ASPNETCORE_URLS"];
                    if (urls.IsNotNullOrWhiteSpace()) {
                        string[] urlsSplit = urls.Split(';', StringSplitOptions.RemoveEmptyEntries)
                            .Select(u => u.Trim()).ToArray();

                        baseUrl = urlsSplit.FirstOrDefault(u => u.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                            ?? urlsSplit.FirstOrDefault();
                    }
                }
            }

            if (baseUrl.IsNotNullOrWhiteSpace()) client.BaseAddress = new Uri(baseUrl.TrimEnd('/'), UriKind.Absolute);
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        configure?.Invoke(config);

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

class BaseAddressHandler : DelegatingHandler {
    public BaseAddressHandler(string baseUri) {
        InnerHandler = new HttpClientHandler();
        BaseUri = baseUri;
    }

    public string BaseUri { get; }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct) {
        if (!request.RequestUri!.IsAbsoluteUri) {
            request.RequestUri = new Uri(new Uri(BaseUri), request.RequestUri);
        }

        return base.SendAsync(request, ct);
    }
}

class PassThroughHandler : DelegatingHandler {
    public PassThroughHandler() => InnerHandler = new HttpClientHandler();
}
