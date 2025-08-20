// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.CliArgsParser;

namespace DevTools.InfiniLore.InfiniBlazor.Commands;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record XmlMdTestDataParameters : ICliParameters {
    [CliData("root", "r")] 
    // [CliArgsDescription("The root directory of the project to update")]
    public string Root { get; init; } = "../../";
}
