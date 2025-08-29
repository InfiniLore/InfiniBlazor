// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.GeneratorTools;
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
    public static string GenerateFile(ImmutableArray<RazorFileData> razorFileDatas) {
        GeneratorStringBuilder builder = new GeneratorStringBuilder()
            .AppendUsings(
                "System.Collections.Frozen"    
            )
            .AppendNamespace(Namespace)
            .AppendLine($"public partial class AutoDocumenterData : {Namespace}.IAutoDocumenterData {{")
            .AppendLineIndented($"// Count Components: {razorFileDatas.Sum(data => data.Components.Length)}")
            .AppendLineIndented($"// Count Files: {razorFileDatas.Length}")
            .Indent(WriteFileDataDictionary, razorFileDatas)
            .AppendLine("}");
        
        return builder.ToStringAndClear();
    }
    
    private static void WriteFileDataDictionary(GeneratorStringBuilder builder, ImmutableArray<RazorFileData> razorFileDatas) {
        builder.AppendLine("public static FrozenDictionary<string, string> RazorFileDatas { get; } = new Dictionary<string, string>() {")
            .ForEachAppendLineIndented(razorFileDatas.SelectMany(data => data.Components), static componentData => 
                $"[\"{componentData.Id}\"] = \"\"\"{componentData.Body}\"\"\","
            )
            .AppendLine("}.ToFrozenDictionary();");
    }
}
