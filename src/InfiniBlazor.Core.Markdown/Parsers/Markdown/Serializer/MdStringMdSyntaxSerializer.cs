// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniBlazor.Markdown.Parsers.Markdown.Serializer.NodeSerializers;
using InfiniBlazor.Markdown.Syntax;
using Microsoft.Extensions.Logging;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMdStringMdSyntaxSerializer>]
public sealed class MdStringMdSyntaxSerializer : IMdStringMdSyntaxSerializer {
    private readonly ILogger<MdStringMdSyntaxSerializer> _logger;
    public ImmutableArray<IMdSyntaxNodeSerializer> SingleLineSerializers { get; } = [
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
    ];
    public ImmutableArray<IMdSyntaxNodeSerializer> MultiLineSerializers { get; } = [
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
    ];

    public IMdSyntaxNodeSerializer? FrontMatterSerializer { get; } = new FrontmatterSyntaxNodeSerializer();

    private readonly ImmutableArray<IMdSyntaxNodeSerializer>[] _singleLineLookup = new ImmutableArray<IMdSyntaxNodeSerializer>[256];
    private readonly ImmutableArray<IMdSyntaxNodeSerializer>[] _multiLineLookup = new ImmutableArray<IMdSyntaxNodeSerializer>[256];

    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public MdStringMdSyntaxSerializer(ILogger<MdStringMdSyntaxSerializer> logger) {
        _logger = logger;
        
        // Initialize arrays with empty to avoid null checks later
        for (int i = 0; i < 256; i++) {
            _singleLineLookup[i] = ImmutableArray<IMdSyntaxNodeSerializer>.Empty;
            _multiLineLookup[i] = ImmutableArray<IMdSyntaxNodeSerializer>.Empty;
        }

        PopulateLookup(_singleLineLookup, SingleLineSerializers);
        PopulateLookup(_multiLineLookup, MultiLineSerializers);
    }

    private static void PopulateLookup(ImmutableArray<IMdSyntaxNodeSerializer>[] table, ImmutableArray<IMdSyntaxNodeSerializer> serializers) {
        // First, add serializers with specific triggers
        foreach (IMdSyntaxNodeSerializer s in serializers.Where(s => !s.TriggerCharacters.IsEmpty())) {
            foreach (char c in s.TriggerCharacters) {
                if (c < 256) table[c] = table[c].Add(s);
            }
        }

        // Second, add "Global" serializers (empty triggers) to EVERY slot
        ImmutableArray<IMdSyntaxNodeSerializer> globals = serializers.Where(s => s.TriggerCharacters.IsEmpty()).ToImmutableArray();
        if (globals.IsEmpty) return;

        for (int i = 0; i < 256; i++) {
            table[i] = table[i].AddRange(globals);
        }
    }
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public ImmutableArray<IMdSyntaxNodeSerializer> GetSingleLineSerializersForChar(char c) 
        => c < 256 ? _singleLineLookup[c] : ImmutableArray<IMdSyntaxNodeSerializer>.Empty;

    public ImmutableArray<IMdSyntaxNodeSerializer> GetMultiLineSerializersForChar(char c) 
        => c < 256 ? _multiLineLookup[c] : ImmutableArray<IMdSyntaxNodeSerializer>.Empty;

    public IMdSyntaxTree SerializeToTree(string markdown) {
        IMdSyntaxTree nodeTree = MdSyntaxTreePool.Shared.Get();
        SerializeToTree(markdown, nodeTree);
        return nodeTree;
    }

    public void SerializeToTree(string markdown, IMdSyntaxTree nodeTree) {
        MdSyntaxFragmentStack fragmentStack = MdSyntaxFragmentStackPool.Shared.Get();
        fragmentStack.TreeReference = nodeTree;
        fragmentStack.SerializerReference = this;

        string normalized = markdown.ReplaceLineEndings("\n");

        try {
            TryExtractFrontMatter(fragmentStack, normalized, nodeTree, out int newStartAtIndex);
            fragmentStack.PushMultiLineMatchesToStack(normalized, nodeTree.RootNode, newStartAtIndex);

            while (fragmentStack.TryPopDto(out MdSyntaxFragment fragment)) {
                switch (fragment) {
                    // Not yet processed
                    case { ParentNode: { } parentNode, Match: { } match, NodeSerializer: { } serializer }: {
                        serializer.Serialize(fragmentStack, parentNode, match);
                        break;
                    }

                    // Already processed and simply needs to be added in correct location
                    case { ParentNode: { } parentNode, ChildNode: { } childNode }: {
                        parentNode.AddChildNode(childNode);
                        break;
                    }

                    // Unhandled state which should never happen
                    default: {
                        _logger.LogError("Unhandled state in MdStringMdSyntaxSerializer.SerializeToTree with fragment '{Fragment}'.", fragment);
                        break;
                    }
                }
            }
        }
        catch (Exception ex) {
            _logger.LogError(ex, "Error parsing Markdown during tree conversion.");
            throw;
        }
        finally {
            MdSyntaxFragmentStackPool.Shared.Return(fragmentStack);
        }
    }

    private void TryExtractFrontMatter(MdSyntaxFragmentStack fragmentStack, string markdown, IMdSyntaxTree nodeTree, out int newStartAtIndex) {
        newStartAtIndex = 0;
        if (FrontMatterSerializer is null) return;
        Match match = FrontMatterSerializer.Match(markdown);
        if (!match.Success) return;
        
        newStartAtIndex = match.Index + match.Length;
        FrontMatterSerializer.Serialize(fragmentStack, nodeTree.RootNode, match);
    }
}
