// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.CliArgsParser;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using System.Xml;
using System.Xml.Serialization;
using Tests.Shared.Infinilore.InfiniBlazor;

namespace DevTools.InfiniLore.InfiniBlazor.Commands;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[CliData("xml-md-test-data")]
public partial class XmlMdTestDataCommand : ICliCommand<XmlMdTestDataParameters> {
    public async ValueTask ExecuteAsync(XmlMdTestDataParameters parameters, CancellationToken ct = new()) {
        Console.WriteLine("Do you want to add a new record or update an existing one? (add/update)");
        string? operation = Console.ReadLine()?.Trim().ToLower();

        if (operation == "add") {
            await HandleAddOperation(ct);
        }
        else if (operation == "update") {
            await HandleUpdateOperation(ct);
        }
        else {
            Console.WriteLine("Invalid input. Please type 'add' or 'update'.");
        }
    }

    private async Task HandleAddOperation(CancellationToken ct) {
        Console.Write("Enter filename to save the test data: ");
        string? fileName = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentException("Filename cannot be empty.");

        Console.Write("Enter the test ID: ");
        string? testId = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(testId)) throw new ArgumentException("Test ID cannot be empty.");

        Console.Write("Enter the Markdown string: ");
        string? markdownString = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(markdownString)) throw new ArgumentException("Markdown string cannot be empty.");

        // Auto-generate syntax tree (placeholder logic, replace with actual generation if necessary)
        var syntaxTree = new MdSyntaxTree();// Assuming this is implemented elsewhere

        // Create the new XML test data
        var newTestData = new MdTestData {
            Id = testId,
            MdString = markdownString,
            MdSyntaxTree = syntaxTree,
            FileName = string.Empty
        };

        // Append to file
        await AppendTestToFile(fileName, newTestData, ct);
    }

    private async Task HandleUpdateOperation(CancellationToken ct) {
        Console.Write("Enter filename containing the test data: ");
        string? fileName = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentException("Filename cannot be empty.");

        Console.Write("Enter the test ID to update: ");
        string? testId = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(testId)) throw new ArgumentException("Test ID cannot be empty.");

        // Load existing data
        List<MdTestData> testDataList = await LoadTestDataFromFile(fileName) ?? new List<MdTestData>();

        // Find the record to update
        MdTestData? existingTestData = testDataList.FirstOrDefault(data => data.Id == testId);
        if (existingTestData == null) {
            Console.WriteLine($"No test data found with ID {testId} in file {fileName}.");
            return;
        }

        // Ask for updated information
        Console.WriteLine($"Existing Markdown: {existingTestData.MdString}");
        Console.Write("Enter new Markdown string (leave empty to keep existing): ");
        string? newMarkdown = Console.ReadLine()?.Trim();
        if (!string.IsNullOrWhiteSpace(newMarkdown)) {
            existingTestData.MdString = newMarkdown;
        }

        // Save the updated list back to the file
        await SaveTestDataToFile(fileName, testDataList);
    }

    private async Task AppendTestToFile(string fileName, MdTestData newTestData, CancellationToken ct) {
        List<MdTestData> testDataList = await LoadTestDataFromFile(fileName) ?? new List<MdTestData>();
        testDataList.Add(newTestData);

        await SaveTestDataToFile(fileName, testDataList);
    }

    private static async Task<List<MdTestData>?> LoadTestDataFromFile(string fileName) {
        if (!File.Exists(fileName)) return new List<MdTestData>();

        await using var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
        using var reader = XmlReader.Create(stream);

        var serializer = new XmlSerializer(typeof(List<MdTestData>));
        return serializer.Deserialize(reader) as List<MdTestData>;
    }

    private async Task SaveTestDataToFile(string fileName, List<MdTestData> testDataList) {
        await using var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true);
        await using var writer = XmlWriter.Create(stream, new XmlWriterSettings { Indent = true });

        var serializer = new XmlSerializer(typeof(List<MdTestData>));
        serializer.Serialize(writer, testDataList);
    }
}
