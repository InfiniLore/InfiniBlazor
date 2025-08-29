// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.GeneratorTools;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace InfiniLore.InfiniBlazor.SourceGenerators.AutoDocumenting;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class AutoDocumenterDataWriter {
    public const string FileName = "AutoDocumentorData.g.cs";
    private const string Namespace = "InfiniLore.InfiniBlazor.AutoDocumenting";

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static string GenerateFile(ImmutableArray<AutoDocumentedData> data, string assemblyName, string propertyName) {
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

    private static void WriteFileDataDictionary(GeneratorStringBuilder builder, IEnumerable<IGrouping<string, AutoDocumentedData>> dataArray, string propertyName) {
        builder.AppendLine($"public Lazy<FrozenDictionary<string, ImmutableArray<string>>> {propertyName} {{ get; }} = new Lazy<FrozenDictionary<string, ImmutableArray<string>>>(static () => new Dictionary<string, ImmutableArray<string>>() {{");

        foreach (IGrouping<string, AutoDocumentedData> grouping in dataArray) {
            builder.Indent(b => {
                b.AppendLine($"[\"{grouping.Key}\"] = [");

                foreach (AutoDocumentedData data in grouping) {
                    b.AppendLineIndented($"\"\"\"\"\"\"{data.Body}    \"\"\"\"\"\",");
                }
                
                b.AppendLine("],");
            });
        }
        builder.AppendLine("}.ToFrozenDictionary());");
    }

}
