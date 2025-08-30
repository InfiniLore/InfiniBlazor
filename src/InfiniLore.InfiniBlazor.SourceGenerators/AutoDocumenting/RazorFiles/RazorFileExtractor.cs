// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
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

        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(text);
        CompilationUnitSyntax root = syntaxTree.GetCompilationUnitRoot();

        foreach (MemberDeclarationSyntax? member in root.DescendantNodes().OfType<MemberDeclarationSyntax>()) {
            switch (member) {
                case BaseFieldDeclarationSyntax fieldDecl:
                    foreach (VariableDeclaratorSyntax variable in fieldDecl.Declaration.Variables) {
                        string? attr = GetAutoDocumentId(fieldDecl.AttributeLists);
                        if (attr == null) continue;

                        string body = variable.Initializer?.Value.ToFullString() ?? string.Empty;
                        yield return new AutoDocumentedData(attr, $"field {variable.Identifier.Text} = {body}");
                    }

                    break;

                case PropertyDeclarationSyntax propDecl: {
                    string? propAttr = GetAutoDocumentId(propDecl.AttributeLists);
                    if (propAttr == null) break;

                    string body = propDecl.ExpressionBody?.Expression.ToFullString()
                        ?? propDecl.AccessorList?.ToFullString()
                        ?? string.Empty;

                    yield return new AutoDocumentedData(propAttr, $"property {propDecl.Identifier.Text} {body}");

                    break;
                }

                case MethodDeclarationSyntax methodDecl: {
                    string? methodAttr = GetAutoDocumentId(methodDecl.AttributeLists);
                    if (methodAttr == null) break;

                    string body = methodDecl.Body?.ToFullString()
                        ?? methodDecl.ExpressionBody?.Expression.ToFullString()
                        ?? string.Empty;

                    yield return new AutoDocumentedData(methodAttr, $"method {methodDecl.Identifier.Text} {body}");

                    break;
                }
            }
        }
    }
    
    private static string? GetAutoDocumentId(SyntaxList<AttributeListSyntax> attrLists)
        => attrLists.SelectMany(
            static attributeList => attributeList.Attributes
                .Select(static attribute => new { attribute, name = attribute.Name.ToString()})
                .Where(static t => t.name.EndsWith("AutoDocument") || t.name.EndsWith("AutoDocumentAttribute"))
                .Select(static t => t.attribute.ArgumentList?.Arguments.FirstOrDefault()?.Expression.ToString())
                .Where(static argument => !string.IsNullOrWhiteSpace(argument)),
            static (_, argument) => argument!.Trim('"')
            ).FirstOrDefault();
}
