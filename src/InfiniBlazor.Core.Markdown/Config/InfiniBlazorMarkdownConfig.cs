// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Config;
using InfiniBlazor.Markdown.Editors;
using InfiniBlazor.Markdown.Parsers.Blazor;
using InfiniBlazor.Markdown.Parsers.Markdown.Serializer;
using InfiniBlazor.Markdown.Parsers.Markdown.Serializer.NodeSerializers;
using InfiniBlazor.Markdown.Syntax;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Frozen;

namespace InfiniBlazor.Markdown;
using InfiniBlazor.Markdown.Syntax.Nodes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class InfiniBlazorMarkdownConfig : IMarkdownConfig {
    private Dictionary<Type, MdComponentRecord> ComponentRecords { get; } = new(32);
    private HashSet<Type> SkippedBlazorComponentTypes { get; } = [typeof(FootnoteDescriptionMdSyntaxNode)]; 
    public bool RenderUnknownBlazorComponents { get; set; }
    public Type? HtmlRendererFootnoteWrapperType { get; set; }

    private Lazy<FrozenDictionary<Type, IMdComponentRecord>> ComponentRecordsLazy { get; }
    private Lazy<FrozenSet<Type>> SkippedBlazorComponentTypesLazy { get; }
    
    
    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public InfiniBlazorMarkdownConfig(IServiceCollection serviceCollection) {
        serviceCollection.RegisterServicesFromInfiniBlazorCoreMarkdown();
        serviceCollection.AddSingleton(TextEditorFactory.CreateTextEditor);
        serviceCollection.AddSingleton<IMarkdownConfig>(this);
        
        serviceCollection.AddSingleton<IMdStringMdSyntaxSerializer>(static sp => {
            var fullOptions = new MarkdownSerializerOptions {
                SingleLine = [
                    new EscapedCharacterSyntaxNodeSerializer(),
                    new BoldSyntaxNodeSerializer(),
                    new ItalicSyntaxNodeSerializer(),
                    new SuperScriptSyntaxNodeSerializer(),
                    new SubScriptSyntaxNodeSerializer(),
                    new CodeInlineSyntaxNodeSerializer(),
                    new StrikeSyntaxNodeSerializer(),
                    new UnderlineSyntaxNodeSerializer(),
                    new HighlightSyntaxNodeSerializer(),
                    new EmoteSyntaxNodeSerializer(),
                    new WikiLinkSyntaxNodeSerializer(),
                    new TemplateSyntaxNodeSerializer(),
                    new LinkSyntaxNodeSerializer(),
                    new TagSyntaxNodeSerializer(),
                    new UserSyntaxNodeSerializer(),
                    new FootnoteReferenceSyntaxNodeSerializer(),
                    new WrapperSyntaxNodeSerializer(),
                    new BreakSyntaxNodeSerializer(),
                ],
                MultiLine = [
                    new HeadingSyntaxNodeSerializer(),
                    new CodeBlockSyntaxNodeSerializer(),
                    new HeadingSimpleSyntaxNodeSerializer(),
                    new ListSyntaxNodeSerializer(),
                    new TableSyntaxNodeSerializer(),
                    new CalloutSyntaxNodeSerializer(),
                    new BlockQuoteSyntaxNodeSerializer(),
                    new FootnoteDescriptionSyntaxNodeSerializer(),
                    new HtmlBlockSyntaxNodeSerializer(),
                    new HorizontalRuleSyntaxNodeSerializer(),
                    new ParagraphSyntaxNodeSerializer(),
                    new NewLineSyntaxNodeSerializer()
                ],
                FrontMatter = new FrontmatterSyntaxNodeSerializer()
            };

            var factory = sp.GetRequiredService<IMdSerializerFactory>();
            return factory.Create(fullOptions);
        });
        
        ComponentRecordsLazy = new Lazy<FrozenDictionary<Type, IMdComponentRecord>>(() => {
            ComponentRecords.TrimExcess();
            return ComponentRecords.ToFrozenDictionary(
                pair => pair.Key, 
                IMdComponentRecord (pair) => pair.Value);
        });
        
        SkippedBlazorComponentTypesLazy = new Lazy<FrozenSet<Type>>(() => {
            SkippedBlazorComponentTypes.TrimExcess();
            return SkippedBlazorComponentTypes.ToFrozenSet();
        });
    }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public InfiniBlazorMarkdownConfig RegisterMdBlazorComponent<TNode, TComponent>() where TComponent : InfiniBlazorMdComponentBase<TNode> where TNode : class, IMdSyntaxNode {
        int count = ComponentRecords.Count;
        if (ComponentRecords.Capacity < count + 1) ComponentRecords.EnsureCapacity(count * 2);
        
        ComponentRecords.AddOrUpdate(typeof(TNode), MdComponentRecord.FromType<TComponent, TNode>());   
        return this;
    }
    
    public InfiniBlazorMarkdownConfig SkipBlazorRenderingOnComponent<TNode>() where TNode : class, IMdSyntaxNode {
        SkippedBlazorComponentTypes.Add(typeof(TNode));
        return this;   
    }

    public FrozenDictionary<Type, IMdComponentRecord> GetComponentRecords()
        => ComponentRecordsLazy.Value;
    
    public FrozenSet<Type> GetSkippedBlazorComponentTypes()
        => SkippedBlazorComponentTypesLazy.Value;
    
}
