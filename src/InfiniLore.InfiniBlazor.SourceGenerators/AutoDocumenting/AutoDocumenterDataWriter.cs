// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.GeneratorTools;
using InfiniLore.InfiniBlazor.SourceGenerators.AutoDocumenting.RazorFiles;
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
    public static string GenerateFile(ImmutableArray<(AutoDocumentedData data, string assemblyName)> combination, string propertyName) {
        string assemblyName = combination.First().assemblyName.Replace(".", "");
        ImmutableArray<AutoDocumentedData> dataArray = combination
            .Select(data => data.data)
            .ToImmutableArray();
        
        GeneratorStringBuilder builder = new GeneratorStringBuilder()
            .AppendUsings(
                "System.Collections.Frozen"
            )
            .AppendNamespace(Namespace)
            .AppendLine($"public partial class AutoDocumenterData_{assemblyName} : {Namespace}.IAutoDocumenterData {{")
            .Indent(static (builder, box) 
                => WriteFileDataDictionary(builder, box.dataArray, box.propertyName),
                (dataArray, propertyName)
            )
            .AppendLine("}");

        return builder.ToStringAndClear();
    }

    private static void WriteFileDataDictionary(GeneratorStringBuilder builder, ImmutableArray<AutoDocumentedData> dataArray, string propertyName) {
        builder
            .AppendLine($"public static FrozenDictionary<string, string> {propertyName} {{ get; }} = new Dictionary<string, string>() {{")
            .ForEachAppendLineIndented(dataArray, static componentData 
                => $"[\"{componentData.Id}\"] = \"\"\"{componentData.Body}    \"\"\",")
            .AppendLine("}.ToFrozenDictionary();");
    }

}
