// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Xml.Serialization;
using Tests.Shared.Infinilore.InfiniBlazor;

namespace DevTools.MdTestHelper.Services;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<MdTestDataProvider>]
public class MdTestDataProvider(ILogger<MdTestDataProvider> logger) {
    private const string RootFilePath = "../../";
    private const string TestFolderFromRootPath = "tests/Tests.InfiniLore.InfiniBlazor.Markdown/_DataSources/Files";

    private readonly string _testFolder = Path.GetFullPath(Path.Combine(RootFilePath, TestFolderFromRootPath));

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public string[]? TryGetFileNames() {
        if (!Directory.Exists(_testFolder)) {
            logger.Warning("Directory {Directory} does not exist", _testFolder);
            return null;
        }

        try {
            return Directory.GetFiles(_testFolder);
        }
        catch (Exception ex) {
            logger.Error(ex, "Error getting files from directory {Directory}", _testFolder);
            return null;
        }
    }

    public async Task<List<XmlMdTestData>?> TryGetXmlMdTestDataAsync(string fileName, CancellationToken ct = default) {
        string fullFilePath = Path.Combine(_testFolder, SetCorrectExtension(fileName));
        if (!File.Exists(fullFilePath)) {
            logger.Warning("File {FilePath} does not exist", fullFilePath);
            return null;
        }

        try {
            var serializer = new XmlSerializer(typeof(List<XmlMdTestData>));

            await using var fileStream = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);
            using var reader = new StreamReader(fileStream);
            string xmlContent = await reader.ReadToEndAsync(ct);
            using var stringReader = new StringReader(xmlContent);

            // ReSharper disable once InvertIf
            if (serializer.Deserialize(stringReader) is not List<XmlMdTestData> deserializedData) {
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

    public async Task<bool> TryWriteXmlMdTestData(string fileName, List<XmlMdTestData> data, CancellationToken ct = default) {
        if (!ValidateTestData(SetCorrectExtension(fileName), data)) return false;

        string fullFilePath = Path.Combine(_testFolder, SetCorrectExtension(fileName));

        try {
            // Ensure directory exists
            if (!Directory.Exists(_testFolder)) {
                Directory.CreateDirectory(_testFolder);
                logger.Information("Created directory {Directory}", _testFolder);
            }

            var serializer = new XmlSerializer(typeof(List<XmlMdTestData>));

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

    private bool ValidateTestData(string fileName, List<XmlMdTestData> data) {
        foreach (XmlMdTestData testData in data) {
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

    public async Task<bool> TryAddOrUpdateAsync(string fileName, XmlMdTestData testData, CancellationToken ct = default) {
        List<XmlMdTestData> dataArray = await TryGetXmlMdTestDataAsync(fileName, ct) ?? new List<XmlMdTestData>();
        if (dataArray.FindIndex(data => data.Id == testData.Id) is var index and not -1) dataArray[index] = testData;
        else dataArray.Add(testData);

        return await TryWriteXmlMdTestData(fileName, dataArray, ct);
    }

    public async Task<bool> TryDeleteAsync(string fileName, string testId, CancellationToken ct = default) {
        List<XmlMdTestData> dataArray = await TryGetXmlMdTestDataAsync(fileName, ct) ?? new List<XmlMdTestData>();
        if (dataArray.FindIndex(data => data.Id == testId) is var index and not -1) dataArray.RemoveAt(index);
        else return false;
        return await TryWriteXmlMdTestData(fileName, dataArray, ct);
    }

    private static string SetCorrectExtension(string fileName) => Path.ChangeExtension(fileName, ".xml");

}
