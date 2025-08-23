// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Xml.Serialization;

namespace Tests.Shared.Infinilore.InfiniBlazor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<MdTestDataProvider>]
public class MdTestDataProvider(ILogger<MdTestDataProvider> logger) {
    private const string RootFilePath = "../../";
    private const string TestFolderFromRootPath = "tests/Tests.InfiniLore.InfiniBlazor.Markdown/DataSources/Files";

    private string TestFolder { get; init; } = Path.GetFullPath(Path.Combine(RootFilePath, TestFolderFromRootPath));

    public static readonly MdTestDataProvider TestInstance = new(Substitute.For<ILogger<MdTestDataProvider>>()) {
        TestFolder = Path.GetFullPath(Path.Combine("../../../../../", TestFolderFromRootPath))
    };

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public string[]? TryGetFileNames() {
        if (!Directory.Exists(TestFolder)) {
            logger.Warning("Directory {Directory} does not exist", TestFolder);
            return null;
        }

        try {
            return Directory.GetFiles(TestFolder);
        }
        catch (Exception ex) {
            logger.Error(ex, "Error getting files from directory {Directory}", TestFolder);
            return null;
        }
    }

    public async Task<List<MdTestData>?> TryGetXmlMdTestDataAsync(string fileName, CancellationToken ct = default) {
        string fullFilePath = Path.Combine(TestFolder, SetCorrectExtension(fileName));
        if (!File.Exists(fullFilePath)) {
            logger.Warning("File {FilePath} does not exist", fullFilePath);
            return null;
        }

        try {
            var serializer = new XmlSerializer(typeof(List<MdTestData>));

            await using var fileStream = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);
            using var reader = new StreamReader(fileStream);
            string xmlContent = await reader.ReadToEndAsync(ct);
            using var stringReader = new StringReader(xmlContent);

            // ReSharper disable once InvertIf
            if (serializer.Deserialize(stringReader) is not List<MdTestData> deserializedData) {
                logger.Error("Could not deserialize file {FilePath}", fullFilePath);
                return null;
            }

            return ValidateTestData(SetCorrectExtension(fileName), deserializedData)
                ? deserializedData
                : null;

        }
        catch (Exception ex) {
            logger.Error(ex, "Error deserializing file {FilePath}", fullFilePath);
            return null;
        }
    }

    public async Task<bool> TryWriteXmlMdTestData(string fileName, List<MdTestData> data, CancellationToken ct = default) {
        if (!ValidateTestData(SetCorrectExtension(fileName), data)) return false;

        string fullFilePath = Path.Combine(TestFolder, SetCorrectExtension(fileName));

        try {
            // Ensure directory exists
            if (!Directory.Exists(TestFolder)) {
                Directory.CreateDirectory(TestFolder);
                logger.Information("Created directory {Directory}", TestFolder);
            }

            var serializer = new XmlSerializer(typeof(List<MdTestData>));

            // Write directly to FileStream with UTF-8 encoding
            await using var fileStream = new FileStream(fullFilePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true);
            await using var writer = new StreamWriter(fileStream, System.Text.Encoding.UTF8);

            // Serialize directly to the StreamWriter
            serializer.Serialize(writer, data);
            await writer.FlushAsync(ct);

            logger.Information("Successfully wrote {Count} items to file {FilePath}", data.Count, fullFilePath);
            return true;
        }
        catch (Exception ex) {
            logger.Error(ex, "Error writing data to file {FilePath}", fullFilePath);
            return false;
        }
    }

    private bool ValidateTestData(string fileName, List<MdTestData> data) {
        foreach (MdTestData testData in data) {
            if (testData.FileName != fileName) {
                logger.Information("Data item does not have the correct file name: {Data}, correcting", testData);
                testData.FileName = fileName;
            }

            if (testData.Id.IsNullOrWhiteSpace()) {
                logger.Warning("Data item does not have an ID: {Data}", testData);
                return false;
            }

            // ReSharper disable once InvertIf
            if (testData.MdString.IsNullOrWhiteSpace()) {
                logger.Warning("Data item does not have Markdown: {Data}", testData);
                return false;
            }
        }

        return true;
    }

    public async Task<bool> TryAddOrUpdateAsync(string fileName, MdTestData testData, CancellationToken ct = default) {
        List<MdTestData> dataArray = await TryGetXmlMdTestDataAsync(fileName, ct) ?? new List<MdTestData>();
        if (dataArray.FindIndex(data => data.Id == testData.Id) is var index and not -1) dataArray[index] = testData;
        else dataArray.Add(testData);

        return await TryWriteXmlMdTestData(fileName, dataArray, ct);
    }

    public async Task<bool> TryDeleteAsync(string fileName, string testId, CancellationToken ct = default) {
        List<MdTestData> dataArray = await TryGetXmlMdTestDataAsync(fileName, ct) ?? new List<MdTestData>();
        if (dataArray.FindIndex(data => data.Id == testId) is var index and not -1) dataArray.RemoveAt(index);
        else return false;
        return await TryWriteXmlMdTestData(fileName, dataArray, ct);
    }

    private static string SetCorrectExtension(string fileName) => Path.ChangeExtension(fileName, ".xml");

}
