// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InfiniLore.InfiniBlazor.SourceGenerators.AutoDocumenting.RazorFiles;
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

                    idContent = source.GetSubText(TextSpan.FromBounds(idIndexStart, idIndexEnd)).ToString();
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

    private static int FindNonEscapedCharacterIndex(SourceText source, char character, int startIndex) {
        for (int i = startIndex; i < source.Length; i++) {
            if (source[i] != character) continue;

            // Count preceding backslashes
            int backslashCount = 0;
            for (int j = i - 1; j >= 0 && source[j] == '\\'; j--) {
                backslashCount++;
            }

            // If even number of backslashes (including 0), the character is not escaped
            if (backslashCount % 2 != 0) continue;

            return i;
        }

        return -1;

    }

    public static IEnumerable<AutoDocumentedData> ExtractAutoDocumentMembers(SourceText source) {
        string text = source.ToString();

        // Locate all @code {...} blocks in the Razor file using basic pattern searching
        const string codeBlockStart = "@code";
        int startIndex = 0;

        while (true) {
            int codeStart = text.IndexOf(codeBlockStart, startIndex, StringComparison.Ordinal);
            if (codeStart == -1) break;// Exit the loop when no more @code blocks are found

            int braceOpen = text.IndexOf('{', codeStart);
            if (braceOpen == -1) break;// Malformed block, skip the remaining content

            int braceClose = FindMatchingBrace(text, braceOpen);
            if (braceClose == -1) break;// Unmatched braces, stop processing

            string blockContent = text.Substring(braceOpen + 1, braceClose - braceOpen - 1).Trim();

            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(blockContent);
            CompilationUnitSyntax root = syntaxTree.GetCompilationUnitRoot();

            // Look for global statements (this is because they arent wrapped in a class)
            foreach (GlobalStatementSyntax? globalStatement in root.Members.OfType<GlobalStatementSyntax>()) {
                StatementSyntax statement = globalStatement.Statement;

                switch (statement) {
                    case LocalFunctionStatementSyntax localFunction: {
                        string? attr = GetAutoDocumentId(localFunction.AttributeLists);
                        if (attr == null) continue;

                        // Remove the AutoDocument attribute
                        LocalFunctionStatementSyntax cleanedFunction = localFunction.WithAttributeLists(
                            RemoveAutoDocumentAttribute(localFunction.AttributeLists)
                        );

                        // Format the cleaned function and return it, not perfect but will work
                        yield return new AutoDocumentedData(attr, $"\n    {cleanedFunction.ToFullString()}\n");

                        break;
                    }
                }
            }

            // Continue searching for the next @code block
            startIndex = braceClose + 1;
        }
    }

    private static SyntaxList<AttributeListSyntax> RemoveAutoDocumentAttribute(SyntaxList<AttributeListSyntax> attributeLists) {
        return SyntaxFactory.List(attributeLists
            .Select(attrList => SyntaxFactory.AttributeList(
                SyntaxFactory.SeparatedList(attrList.Attributes.Where(attr =>
                    !attr.Name.ToString().EndsWith("AutoDocument") &&
                    !attr.Name.ToString().EndsWith("AutoDocumentAttribute")
                ))
            ))
            .Where(list => list.Attributes.Any())
        );
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

    private static string? GetAutoDocumentId(SyntaxList<AttributeListSyntax> attrLists) {
        return attrLists.SelectMany(attrList => attrList.Attributes)
            .Where(attr => attr.Name.ToString().EndsWith("AutoDocument") || attr.Name.ToString().EndsWith("AutoDocumentAttribute"))
            .Select(attr => attr.ArgumentList?.Arguments.FirstOrDefault()?.Expression.ToString().Trim('"'))
            .FirstOrDefault();
    }
}
