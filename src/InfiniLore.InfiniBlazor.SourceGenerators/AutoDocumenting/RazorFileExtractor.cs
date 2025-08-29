// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;

namespace InfiniLore.InfiniBlazor.SourceGenerators.AutoDocumenting;

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
    
    
    public static IEnumerable<InfiniAutoDocumentComponent> ExtractInfiniAutoDocumentComponents(SourceText source) {
        int sourceLength = source.Length;
        for (int index = 0; index < sourceLength;) {

            if (source.GetSubText(TextSpan.FromBounds(index, index + TagStartLength)).ContentEquals(TagStartText)) {
                index += TagStartLength;

                string idContent = string.Empty;
                for (int idIndexStart = index; idIndexStart < sourceLength; idIndexStart++) {
                    TextSpan possibleIdStart = TextSpan.FromBounds(idIndexStart, idIndexStart + IdStartLength);
                    if (!source.GetSubText(possibleIdStart).ContentEquals(IdStartText)) continue;
                    idIndexStart += IdStartLength;
                    
                    char previousChar = char.MinValue;
                    int idIndexEnd = -1;
                    for (int idIndex = idIndexStart; idIndex < sourceLength; idIndex++) {
                        char currentChar = source[idIndexStart + idIndex];
                        if (currentChar != '\"' || previousChar == '\\') {
                            previousChar = currentChar;
                            continue;
                        }

                        idIndexEnd = idIndex-1;
                        break;
                    }
                    if (idIndexEnd < 0) break;
                    idContent = source.GetSubText(TextSpan.FromBounds(idIndexStart, idIndexEnd)).ToString();
                    
                    index = idIndexEnd + 1;
                    break;
                }
                if (string.IsNullOrEmpty(idContent)) continue;

                for (; index < sourceLength; index++) {
                    if (source[index] == '>') break;
                }
                
                int bodyIndexStart = index + 1;
                int bodyIndexEnd = -1;
                for (int bodyIndex = bodyIndexStart; bodyIndex < sourceLength; bodyIndex++) {
                    TextSpan possibleBodyEnd = TextSpan.FromBounds(bodyIndex, bodyIndex + EndTagLength);
                    if (!source.GetSubText(possibleBodyEnd).ContentEquals(EndTagText)) continue;
                    bodyIndexEnd = bodyIndex;
                    break;
                }
                if (bodyIndexEnd < 0) continue;
                string bodyContent = source.GetSubText(TextSpan.FromBounds(bodyIndexStart, bodyIndexEnd)).ToString();
                yield return new InfiniAutoDocumentComponent(idContent, bodyContent);
            }
            
            index++;
        }
    }
}
