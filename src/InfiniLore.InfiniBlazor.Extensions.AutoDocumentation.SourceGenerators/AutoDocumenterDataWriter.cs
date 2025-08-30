// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.GeneratorTools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InfiniLore.InfiniBlazor.Extensions.AutoDocumentation.SourceGenerators;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class AutoDocumenterDataWriter {
    public const string FileName = "AutoDocumentorData.g.cs";
    private const string Namespace = "InfiniLore.InfiniBlazor.AutoDocumentation";

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static string GenerateFile(IEnumerable<AutoDocumentedData> data, string assemblyName, string propertyName) {
        GeneratorStringBuilder builder = new GeneratorStringBuilder()
            .AppendUsings(
                "System",
                "System.Collections.Frozen",
                "System.Collections.Immutable"
            )
            .AppendNamespace(Namespace)
            .AppendLine($"public partial class AutoDocumenterData_{assemblyName.Replace(".", "")} : {Namespace}.IAutoDocumenterData {{")
            .Indent(static (builder, box) 
                => WriteFileDataDictionary(builder, box.data.GroupBy(data => data.Id), box.propertyName),
                (data, propertyName)
            )
            .AppendLine("}");

        return builder.ToStringAndClear();
    }

    /// <summary>
    /// Writes a dictionary representation of auto-documented data into a file structure using the provided builder.
    /// </summary>
    /// <param name="builder">The <see cref="GeneratorStringBuilder"/> instance used for constructing the output file content.</param>
    /// <param name="dataArray">A collection of grouped auto-documented data, grouped by an identifier.</param>
    /// <param name="propertyName">The name of the property that represents the dictionary in the generated file.</param>
    private static void WriteFileDataDictionary(GeneratorStringBuilder builder, IEnumerable<IGrouping<string, AutoDocumentedData>> dataArray, string propertyName) {
        builder.AppendLine($"public Lazy<FrozenDictionary<string, ImmutableArray<string>>> {propertyName} {{ get; }} = new Lazy<FrozenDictionary<string, ImmutableArray<string>>>(static () => new Dictionary<string, ImmutableArray<string>>() {{");

        foreach (IGrouping<string, AutoDocumentedData> grouping in dataArray) {
            builder.Indent(b => {
                b.AppendLine($"[\"{grouping.Key}\"] = [");

                foreach (AutoDocumentedData data in grouping) {
                    int leadingWhitespaceCount = 0;
                    string[] dataLines = data.Body.Split('\n');

                    for (int i = dataLines.Length - 1; i >= 0; i--) {
                        string line = dataLines[i];
                        if (line.Length > 0) continue;

                        // count leading whitespace
                        while (leadingWhitespaceCount < line.Length && char.IsWhiteSpace(line[leadingWhitespaceCount])) {
                            leadingWhitespaceCount++;
                        }
                        break;       
                    }
                    string body = string.Join("\n", dataLines);
                    
                    b.AppendLineIndented($"\"\"\"\"\"\"{body}{new string(' ', leadingWhitespaceCount)}\"\"\"\"\"\",");
                }
                
                b.AppendLine("],");
            });
        }
        builder.AppendLine("}.ToFrozenDictionary());");
    }

}
