namespace FileProcessor.Logic.Services.Contracts;

public interface IFileImporter
{
    public Task ImportFileAsync(string filePath, CancellationToken ct);
}