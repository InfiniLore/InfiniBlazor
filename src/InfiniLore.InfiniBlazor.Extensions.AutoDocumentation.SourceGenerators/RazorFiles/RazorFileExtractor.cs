// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Linq;

namespace InfiniLore.InfiniBlazor.Extensions.AutoDocumentation.SourceGenerators.RazorFiles;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class RazorFileExtractor {
    private const string TagStart = "<InfiniAutoDocument";
    private static readonly SourceText TagStartText = SourceText.From(TagStart);
    private static readonly int TagStartLength = TagStart.Length;

    private const string IdStart = "Id=\"";
    private static readonly SourceText IdStartText = SourceText.From(IdStart);
    private static readonly int IdStartLength = IdStart.Length;

    private const string EndTag = "</InfiniAutoDocument>";
    private static readonly SourceText EndTagText = SourceText.From(EndTag);
    private static readonly int EndTagLength = EndTag.Length;

    private const string CodeBlockStart = "@code";
    private static readonly SourceText CodeBlockStartText = SourceText.From(CodeBlockStart);
    private static readonly int CodeBlockStartLength = CodeBlockStart.Length;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<AutoDocumentedData> ExtractInfiniAutoDocumentComponents(SourceText source) {
        int sourceLength = source.Length;

        for (int index = 0; index < sourceLength;) {
            // Check bounds before creating TextSpan
            if (index + TagStartLength > sourceLength) {
                index++;
                continue;
            }

            if (source.GetSubText(TextSpan.FromBounds(index, index + TagStartLength)).ContentEquals(TagStartText)) {
                index += TagStartLength;

                string idContent = string.Empty;

                // Find the Id attribute
                for (int idIndexStart = index; idIndexStart < sourceLength; idIndexStart++) {
                    // Check bounds before creating TextSpan
                    if (idIndexStart + IdStartLength > sourceLength) break;

                    TextSpan possibleIdStart = TextSpan.FromBounds(idIndexStart, idIndexStart + IdStartLength);
                    if (!source.GetSubText(possibleIdStart).ContentEquals(IdStartText)) continue;

                    idIndexStart += IdStartLength;

                    // Extract ID content until closing quote
                    int idIndexEnd = FindNonEscapedCharacterIndex(source, '"', idIndexStart);
                    if (idIndexEnd < 0) break;

                    idContent = source.GetSubText(TextSpan.FromBounds(idIndexStart, idIndexEnd)).ToString().TrimStart('@');
                    index = idIndexEnd + 1;
                    break;
                }

                if (string.IsNullOrEmpty(idContent)) {
                    index++;
                    continue;
                }

                // Find the end of the opening tag
                int componentIndexEnd = FindNonEscapedCharacterIndex(source, '>', index);
                if (componentIndexEnd < 0) break;

                int bodyIndexStart = componentIndexEnd + 1;
                int bodyIndexEnd = -1;

                // Find the closing tag
                for (int bodyIndex = bodyIndexStart; bodyIndex < sourceLength; bodyIndex++) {
                    // Check bounds before creating TextSpan
                    if (bodyIndex + EndTagLength > sourceLength) break;

                    TextSpan possibleBodyEnd = TextSpan.FromBounds(bodyIndex, bodyIndex + EndTagLength);
                    if (!source.GetSubText(possibleBodyEnd).ContentEquals(EndTagText)) continue;

                    bodyIndexEnd = bodyIndex;
                    break;
                }

                if (bodyIndexEnd < 0) {
                    index++;
                    continue;
                }

                string bodyContent = source.GetSubText(TextSpan.FromBounds(bodyIndexStart, bodyIndexEnd)).ToString();
                yield return new AutoDocumentedData(idContent, bodyContent);

                // Skip past the end tag to avoid reprocessing
                index = bodyIndexEnd + EndTagLength;
                continue;
            }

            index++;
        }
    }

    public static IEnumerable<AutoDocumentedData> ExtractAutoDocumentMembers(SourceText source) {
        int sourceLength = source.Length;

        for (int index = 0; index < sourceLength; index++) {
            // Check bounds before creating TextSpan
            if (index + CodeBlockStartLength > sourceLength) continue;

            if (!source.GetSubText(TextSpan.FromBounds(index, index + CodeBlockStartLength)).ContentEquals(CodeBlockStartText)) continue;

            index += CodeBlockStartLength;

            int braceOpen = FindNonEscapedCharacterIndex(source, '{', index);
            if (braceOpen == -1) break;

            int braceClose = FindMatchingBrace(source.ToString(), braceOpen);
            if (braceClose == -1) break;

            SourceText blockContent = source.GetSubText(TextSpan.FromBounds(braceOpen + 1, braceClose));

            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(blockContent);
            CompilationUnitSyntax root = syntaxTree.GetCompilationUnitRoot();

            foreach (MemberDeclarationSyntax? member in root.DescendantNodes().OfType<MemberDeclarationSyntax>()) {
                switch (member) {
                    case StructDeclarationSyntax:
                    case RecordDeclarationSyntax:
                    case PropertyDeclarationSyntax:
                    case ClassDeclarationSyntax: {
                        if (!AutoDocumentedData.TryGetFromMember(member, out AutoDocumentedData? data)) yield break;

                        yield return data;

                        break;
                    }

                    case GlobalStatementSyntax { Statement: LocalDeclarationStatementSyntax local }: {
                        if (!AutoDocumentedData.TryGetFromStatement(local, out AutoDocumentedData? data)) yield break;

                        yield return data;

                        break;
                    }

                    case GlobalStatementSyntax { Statement: LocalFunctionStatementSyntax local }: {
                        if (!AutoDocumentedData.TryGetFromStatement(local, out AutoDocumentedData? data)) yield break;

                        yield return data;

                        break;
                    }
                }
            }

            // Skip past this @code block
            index = braceClose;
        }
    }

    private static int FindNonEscapedCharacterIndex(SourceText source, char character, int startIndex) {
        for (int i = startIndex; i < source.Length; i++) {
            if (source[i] != character) continue;

            int backslashCount = 0;
            for (int j = i - 1; j >= 0 && source[j] == '\\'; j--) {
                backslashCount++;
            }

            if (backslashCount % 2 != 0) continue;

            return i;
        }

        return -1;
    }

    private static int FindMatchingBrace(string text, int openIndex) {
        int depth = 0;
        for (int i = openIndex; i < text.Length; i++) {
            switch (text[i]) {
                case '{':
                    depth++;
                    break;
                case '}':
                    depth--;
                    if (depth == 0) return i;

                    break;
            }
        }

        return -1;
    }



}
