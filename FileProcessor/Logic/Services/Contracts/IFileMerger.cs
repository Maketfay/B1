namespace FileProcessor.Logic.Services.Contracts;

public interface IFileMerger
{
    public Task<int> MergeFilesAsync(string directoryPath, string outputFilePath, string substringToRemove,
        CancellationToken ct);
}