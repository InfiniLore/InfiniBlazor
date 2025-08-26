// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Xml.Serialization;

namespace Tests.Shared.InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<MdTestDataProvider>]
[SuppressMessage("ReSharper", "InvertIf")]
public class MdTestDataProvider(ILogger<MdTestDataProvider> logger) {
    private const string RootFilePath = "../../";
    private const string TestFolderFromRootPath = "tests/Tests.InfiniLore.InfiniBlazor.Markdown/DataSources/Files";

    internal string TestFolder { get; private init; } = Path.GetFullPath(Path.Combine(RootFilePath, TestFolderFromRootPath));

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
            string[] array = Directory.GetFiles(TestFolder);
            for (int i = 0; i < array.Length; i++) {
                array[i] = Path.GetFileName(array[i]);
            }

            return array;
        }
        catch (Exception ex) {
            logger.Error(ex, "Error getting files from directory {Directory}", TestFolder);
            return null;
        }
    }

    private static string SetCorrectExtension(string fileName) => Path.ChangeExtension(fileName, ".xml");

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
        
        // sorted by ID
        data.Sort((a, b) => string.Compare(a.Id, b.Id, StringComparison.InvariantCultureIgnoreCase));

        return true;
    }

    private string GetFullFilePath(string fileName) => Path.Combine(TestFolder, SetCorrectExtension(fileName));

    private bool CheckFileExists(string fullFilePath) {
        if (!File.Exists(fullFilePath)) {
            logger.Warning("File {FilePath} does not exist", fullFilePath);
            return false;
        }

        return true;
    }

    private void EnsureDirectoryExists() {
        if (!Directory.Exists(TestFolder)) {
            Directory.CreateDirectory(TestFolder);
            logger.Information("Created directory {Directory}", TestFolder);
        }
    }

    private List<MdTestData>? DeserializeXmlData(string xmlContent, string fullFilePath) {
        var serializer = new XmlSerializer(typeof(List<MdTestData>));
        using var stringReader = new StringReader(xmlContent);

        if (serializer.Deserialize(stringReader) is not List<MdTestData> deserializedData) {
            logger.Error("Could not deserialize file {FilePath}", fullFilePath);
            return null;
        }

        return deserializedData;
    }

    private static void AddOrUpdateTestData(List<MdTestData> dataArray, MdTestData testData) {
        if (dataArray.FindIndex(data => data.Id == testData.Id) is var index and not -1) dataArray[index] = testData;
        else dataArray.Add(testData);
    }

    private static bool RemoveTestData(List<MdTestData> dataArray, string testId) {
        if (dataArray.FindIndex(data => data.Id == testId) is not (var index and not -1)) return false;

        dataArray.RemoveAt(index);
        return true;
    }

    #region Async
    public async Task<List<MdTestData>?> TryGetXmlMdTestDataAsync(string fileName, CancellationToken ct = default) {
        string fullFilePath = GetFullFilePath(fileName);
        if (!CheckFileExists(fullFilePath)) return null;

        try {
            await using var fileStream = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);
            using var reader = new StreamReader(fileStream);
            string xmlContent = await reader.ReadToEndAsync(ct);

            if (DeserializeXmlData(xmlContent, fullFilePath) is not {} deserializedData) return null;

            return ValidateTestData(SetCorrectExtension(fileName), deserializedData)
                ? deserializedData
                : null;

        }
        catch (Exception ex) {
            logger.Error(ex, "Error deserializing file {FilePath}", fullFilePath);
            return null;
        }
    }

    public async Task<bool> TryWriteXmlMdTestDataAsync(string fileName, List<MdTestData> data, CancellationToken ct = default) {
        if (!ValidateTestData(SetCorrectExtension(fileName), data)) return false;

        string fullFilePath = GetFullFilePath(fileName);

        try {
            EnsureDirectoryExists();

            var serializer = new XmlSerializer(typeof(List<MdTestData>));

            await using var fileStream = new FileStream(fullFilePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true);
            await using var writer = new StreamWriter(fileStream, Encoding.UTF8);

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

    public async Task<bool> TryAddOrUpdateAsync(string fileName, MdTestData testData, CancellationToken ct = default) {
        List<MdTestData> dataArray = await TryGetXmlMdTestDataAsync(fileName, ct) ?? new List<MdTestData>();
        AddOrUpdateTestData(dataArray, testData);
        return await TryWriteXmlMdTestDataAsync(fileName, dataArray, ct);
    }
    
    public async Task<bool> TryRenameTest(string fileName, string oldTestId, string newTestId, CancellationToken ct = default) {
        List<MdTestData> dataArray = await TryGetXmlMdTestDataAsync(fileName, ct) ?? new List<MdTestData>();
        if (dataArray.FindIndex(data => data.Id == oldTestId) is not (var index and not -1)) return false;
        dataArray[index].Id = newTestId;
        return await TryWriteXmlMdTestDataAsync(fileName, dataArray, ct);
    }

    public async Task<bool> TryDeleteAsync(string fileName, string testId, CancellationToken ct = default) {
        List<MdTestData> dataArray = await TryGetXmlMdTestDataAsync(fileName, ct) ?? new List<MdTestData>();
        if (!RemoveTestData(dataArray, testId)) return false;

        return await TryWriteXmlMdTestDataAsync(fileName, dataArray, ct);
    }
    #endregion

    #region Sync
    public List<MdTestData>? TryGetXmlMdTestData(string fileName) {
        string fullFilePath = GetFullFilePath(fileName);
        if (!CheckFileExists(fullFilePath)) return null;

        try {
            using var fileStream = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var reader = new StreamReader(fileStream);
            string xmlContent = reader.ReadToEnd();

            if (DeserializeXmlData(xmlContent, fullFilePath) is not {} deserializedData) return null;

            return ValidateTestData(SetCorrectExtension(fileName), deserializedData)
                ? deserializedData
                : null;

        }
        catch (Exception ex) {
            logger.Error(ex, "Error deserializing file {FilePath}", fullFilePath);
            return null;
        }
    }

    public bool TryWriteXmlMdTestData(string fileName, List<MdTestData> data) {
        if (!ValidateTestData(SetCorrectExtension(fileName), data)) return false;

        string fullFilePath = GetFullFilePath(fileName);

        try {
            EnsureDirectoryExists();

            var serializer = new XmlSerializer(typeof(List<MdTestData>));

            using var fileStream = new FileStream(fullFilePath, FileMode.Create, FileAccess.Write, FileShare.None);
            using var writer = new StreamWriter(fileStream, Encoding.UTF8);

            serializer.Serialize(writer, data);
            writer.Flush();

            logger.Information("Successfully wrote {Count} items to file {FilePath}", data.Count, fullFilePath);
            return true;
        }
        catch (Exception ex) {
            logger.Error(ex, "Error writing data to file {FilePath}", fullFilePath);
            return false;
        }
    }

    public bool TryAddOrUpdate(string fileName, MdTestData testData) {
        List<MdTestData> dataArray = TryGetXmlMdTestData(fileName) ?? new List<MdTestData>();
        AddOrUpdateTestData(dataArray, testData);
        return TryWriteXmlMdTestData(fileName, dataArray);
    }

    public bool TryDelete(string fileName, string testId) {
        List<MdTestData> dataArray = TryGetXmlMdTestData(fileName) ?? new List<MdTestData>();
        return RemoveTestData(dataArray, testId) && TryWriteXmlMdTestData(fileName, dataArray);
    }
    #endregion
}
