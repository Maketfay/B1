namespace FileProcessor.Logic.Services.Contracts;

public interface IFileGenerator
{
    public Task GenerateFilesAsync(int fileCount, string outputPath, CancellationToken ct);
}