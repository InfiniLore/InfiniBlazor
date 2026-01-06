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
    public ImmutableArray<IMdSyntaxNodeSerializer> SingleLineSerializers { get; init; } = [
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
    public ImmutableArray<IMdSyntaxNodeSerializer> MultiLineSerializers { get; init; } = [
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

    private readonly ImmutableArray<IMdSyntaxNodeSerializer>[] _singleLineLookup;
    private readonly ImmutableArray<IMdSyntaxNodeSerializer>[] _multiLineLookup;

    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public MdStringMdSyntaxSerializer(ILogger<MdStringMdSyntaxSerializer> logger) {
        _logger = logger;
        
        _singleLineLookup = BuildLookup(SingleLineSerializers);
        _multiLineLookup = BuildLookup(MultiLineSerializers);
    }

    private static ImmutableArray<IMdSyntaxNodeSerializer>[] BuildLookup(ImmutableArray<IMdSyntaxNodeSerializer> serializers) {
        var table = new ImmutableArray<IMdSyntaxNodeSerializer>[256];
        
        // Identify "Global" serializers (those with no triggers)
        IMdSyntaxNodeSerializer[] globals = serializers.Where(s => s.TriggerCharacters.IsEmpty()).ToArray();

        for (int i = 0; i < 256; i++) {
            char c = (char)i;
            
            // Get serializers specifically triggered by this character
            IEnumerable<IMdSyntaxNodeSerializer> triggers = serializers.Where(s => s.TriggerCharacters.Contains(c));
            
            // Result is Triggers + Globals
            table[i] = triggers.Concat(globals).ToImmutableArray();
        }

        return table;
    }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public ImmutableArray<IMdSyntaxNodeSerializer> GetSingleLineSerializersForChar(char c) 
        => c < 256 ? _singleLineLookup[c] : SingleLineSerializers;

    public ImmutableArray<IMdSyntaxNodeSerializer> GetMultiLineSerializersForChar(char c) 
        => c < 256 ? _multiLineLookup[c] : MultiLineSerializers;

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
