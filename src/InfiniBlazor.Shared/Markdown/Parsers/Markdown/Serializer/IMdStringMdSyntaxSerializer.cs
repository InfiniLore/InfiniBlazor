// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;
using System.Buffers;
using System.Collections.Immutable;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdStringMdSyntaxSerializer {
    SearchValues<char> SingleLineTriggerSearchValues { get; }
    
    ImmutableArray<IMdSyntaxNodeSerializer>[] SingleLineLookup { get; init; }
    ImmutableDictionary<char, ImmutableArray<IMdSyntaxNodeSerializer>> SingleLineNonAsciiLookup { get; init; }

    ImmutableArray<IMdSyntaxNodeSerializer>[] MultiLineLookup { get; init; }
    ImmutableDictionary<char, ImmutableArray<IMdSyntaxNodeSerializer>> MultiLineNonAsciiLookup { get; init; }
    
    IMdSyntaxNodeSerializer? FrontMatterSerializer { get; }

    ImmutableArray<IMdSyntaxNodeSerializer> GetSingleLineSerializersForChar(char c);
    ImmutableArray<IMdSyntaxNodeSerializer> GetMultiLineSerializersForChar(char c);
    IMdSyntaxTree SerializeToTree(string markdown);
    void SerializeToTree(string markdown, IMdSyntaxTree nodeTree);
}
