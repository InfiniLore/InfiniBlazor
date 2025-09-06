// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.CliArgsParser;
using CodeOfChaos.CliArgsParser.Library;

namespace DevTools;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class Program {
    #pragma warning disable TUnit0034
    public static async Task Main(string[] args) {
    #pragma warning restore TUnit0034
        // Register & Build the parser
        //      Don't forget to add the current assembly if you built more tools for the current project
        ICliParser parser = CliParser.CreateBuilder()
            .AddFromAssembly<IAssemblyEntry>()
            .Build();

        // We are doing this here because else the launchSettings.json file becomes a humongous issue to deal with.
        //      Sometimes CLI params are not the answer.
        //      Code is the true savior
        string projects = string.Join(";", 
            "InfiniLore.InfiniBlazor.Core.Components",
            "InfiniLore.InfiniBlazor.Core.Js",
            "InfiniLore.InfiniBlazor.Core.Markdown",
            "InfiniLore.InfiniBlazor.Core.Theming",
            
            "InfiniLore.InfiniBlazor.Extensions.AutoDocumentation",
            "InfiniLore.InfiniBlazor.Extensions.AutoDocumentation.SourceGenerators",
            "InfiniLore.InfiniBlazor.Extensions.Maui",
            "InfiniLore.InfiniBlazor.Extensions.ExtraComponents",
            
            "InfiniLore.InfiniBlazor",
            "InfiniLore.InfiniBlazor.Contracts",
            "InfiniLore.InfiniBlazor.Shared",
            "InfiniLore.InfiniBlazor.SourceGenerators"
        );

        string oneLineArgs = ArgsInputHelper.ToOneLine(args).Replace("%PROJECTS%", projects);
        await parser.ExecuteAsync(oneLineArgs);
    }
}
