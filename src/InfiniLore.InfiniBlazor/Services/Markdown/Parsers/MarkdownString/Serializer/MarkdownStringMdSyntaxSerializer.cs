// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Frozen;
using System.Text.RegularExpressions;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.RegexLib;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownStringMdSyntaxSerializer>]
public sealed class MarkdownStringMdSyntaxSerializer(IServiceProvider serviceProvider, ILogger<MarkdownStringMdSyntaxSerializer> logger) : IMarkdownStringMdSyntaxSerializer {
    private readonly FrozenDictionary<string, IMarkdownStringMdSyntaxNodeSerializer> _elementHandlers = ToFrozenDictionary(logger, serviceProvider);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    private static FrozenDictionary<string, IMarkdownStringMdSyntaxNodeSerializer> ToFrozenDictionary(ILogger logger, IServiceProvider serviceProvider) {
        ReadOnlySpan<string> keyNames = MdRegexLib.MarkdownStructureGroupNames.AsSpan();
        var dictionaryBuilder = new Dictionary<string, IMarkdownStringMdSyntaxNodeSerializer>(keyNames.Length);

        for (int index = keyNames.Length - 1; index >= 0; index--) {
            string groupName = keyNames[index];
            if (serviceProvider.GetKeyedService<IMarkdownStringMdSyntaxNodeSerializer>(groupName) is not {} service) {
                logger.Warning("No MarkdownElementHandler service found for group name '{groupName}'", groupName);
                continue;
            }

            dictionaryBuilder[groupName] = service;
        }

        return dictionaryBuilder.ToFrozenDictionary();
    }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public IMdSyntaxTree SerializeToTree(string markdown) {
        MdSyntaxTree nodeTree = MdSyntaxTree.Pool.Get();
        SerializeToTree(markdown, nodeTree);
        return nodeTree;
    }

    public void SerializeToTree(string markdown, IMdSyntaxTree nodeTree) {
        MdSyntaxFragmentStack runningSerializer = MdSyntaxFragmentStack.Pool.Get();

        string normalized = markdown.ReplaceLineEndings("\n");

        try {
            runningSerializer.PushMultiLineMatchesToStack(normalized, nodeTree.RootNode);

            while (runningSerializer.TryPopDto(out MdSyntaxFragment fragment)) {
                switch (fragment) {
                    // Not yet processed
                    case { ParentNode: {} parentNode, Match: {} match }: {
                        ProcessMatch(match, parentNode, runningSerializer);
                        break;
                    }
                    // Already processed and simply needs to be added in correct location
                    case {ParentNode: {} parentNode, ChildNode: {} childNode} : {
                        parentNode.AddChildNode(childNode);
                        break;
                    }
                    
                    // Unhandled state which should never happen
                    default: {
                        logger.Error("Unhandled state in MarkdownStringMdSyntaxSerializer.SerializeToTree with fragment '{fragment}'.", fragment);
                        break;   
                    }
                }
            }
        }
        catch (Exception ex) {
            logger.Error(ex, "Error parsing Markdown, during tree conversion.");
            throw;
        }
        finally {
            MdSyntaxFragmentStack.Pool.Return(runningSerializer);
        }
    }

    private void ProcessMatch(Match match, IMdSyntaxNode parentNode, IMdSyntaxFragmentStack runningParser) {
        GroupCollection groups = match.Groups;
        int length = groups.Count;
        if (length == 0) return;

        for (int index = 0; index < length; index++) {
            Group group = groups[index];
            if (!group.Success) continue;
            if (!_elementHandlers.TryGetValue(group.Name, out IMarkdownStringMdSyntaxNodeSerializer? handler)) continue;

            handler.HandleMatch(runningParser, parentNode, match);
        }
    }
}
