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
public sealed class MdStringMdSyntaxSerializer(ILogger<MdStringMdSyntaxSerializer> logger) : IMdStringMdSyntaxSerializer {
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

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
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
                        logger.LogError("Unhandled state in MdStringMdSyntaxSerializer.SerializeToTree with fragment '{Fragment}'.", fragment);
                        break;
                    }
                }
            }
        }
        catch (Exception ex) {
            logger.LogError(ex, "Error parsing Markdown during tree conversion.");
            throw;
        }
        finally {
            MdSyntaxFragmentStackPool.Shared.Return(fragmentStack);
        }
    }

    private void TryExtractFrontMatter(MdSyntaxFragmentStack fragmentStack, string markdown, IMdSyntaxTree nodeTree, out int newStartAtIndex) {
        newStartAtIndex = 0;
        if (FrontMatterSerializer is null) return;
        Match match = FrontMatterSerializer.Syntax.Match(markdown);
        if (!match.Success) return;
        
        newStartAtIndex = match.Index + match.Length;
        FrontMatterSerializer.Serialize(fragmentStack, nodeTree.RootNode, match);
    }
}
