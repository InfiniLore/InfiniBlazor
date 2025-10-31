// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Core.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Editors;
using InfiniLore.InfiniBlazor.Markdown.Parsers.Blazor;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Frozen;

namespace InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;

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
        serviceCollection.RegisterServicesFromInfiniLoreInfiniBlazorCoreMarkdown();
        serviceCollection.AddSingleton(TextEditorFactory.CreateTextEditor);
        serviceCollection.AddSingleton<IMarkdownConfig>(this);
        
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
        
        ComponentRecords.Add(typeof(TNode), MdComponentRecord.FromType<TComponent, TNode>());   
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
