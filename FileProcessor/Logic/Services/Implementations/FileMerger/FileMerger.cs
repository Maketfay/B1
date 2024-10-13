using FileProcessor.Logic.Services.Contracts;

namespace FileProcessor.Logic.Services.Implementations.FileMerger;

public class FileMerger : IFileMerger
{
    public async Task<int> MergeFilesAsync(string directoryPath, string outputFilePath, string substringToRemove, CancellationToken ct)
    {
        var files = Directory.GetFiles(directoryPath, "*.txt");
        int removedLinesCount = 0;

        await using var outputFile = new StreamWriter(outputFilePath);
        
        foreach (var file in files)
        {
            using var reader = new StreamReader(file);

            while (await reader.ReadLineAsync(ct) is { } line)
            {
                if (!line.Contains(substringToRemove, StringComparison.OrdinalIgnoreCase))
                {
                    await outputFile.WriteLineAsync(line);
                }
                else
                {
                    removedLinesCount++;
                }
            }
        }

        return removedLinesCount;
    }
}