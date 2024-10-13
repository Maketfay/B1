using FileProcessor.Logic.Services.Contracts;
using SqlModels;
using SqlModels.Entities;

namespace FileProcessor.Logic.Services.Implementations.RandomFileProvider;

public class RandomFileProvider : IRandomFileProvider
{
    private readonly WebContext _webContext;
    
    public RandomFileProvider(WebContext webContext)
    {
        _webContext = webContext;
    }

    public async Task SaveFileDataAsync(IEnumerable<RandomFileDataDto> fileData, CancellationToken ct)
    {
        var chunks = fileData.Chunk(500).ToArray();
        int totalChunks = chunks.Length;
        int currentChunk = 0;
        
        var progressIndicator = new Progress<float>(percent =>
        {
            Console.WriteLine($"Progress: {percent}%");
        });

        foreach (var chunk in chunks)
        {
            var data = chunk.Select(t => new RandomFile
            {
                Date = t.Date,
                LatinWord = t.LatinWord,
                CyrillicWord = t.CyrillicWord,
                RandomInteger = t.RandomInteger,
                RandomDouble = t.RandomDouble,
            });

            await _webContext.RandomFiles.AddRangeAsync(data, ct);
            await _webContext.SaveChangesAsync(ct);
            
            currentChunk++;
            var percentComplete = currentChunk / (float)totalChunks * 100;
            ((IProgress<float>)progressIndicator).Report(percentComplete);
        }
        
        Console.WriteLine();
    }
}