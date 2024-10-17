using System.Data;
using FileProcessor.Logic.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
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

        var progressIndicator = new Progress<float>(percent => { Console.WriteLine($"Progress: {percent}%"); });

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

    public async Task<FileAggregatedInfo> GetAggregatedInfoAsync(CancellationToken ct)
    {
        await using var connection = _webContext.Database.GetDbConnection();
        await connection.OpenAsync(ct);

        await using var command = connection.CreateCommand();

        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "CalculateSumAndMedian";

        var sumParameter = new MySqlParameter("intSum", MySqlDbType.Int64)
            { Direction = ParameterDirection.Output };
        var medianParameter = new MySqlParameter("median", MySqlDbType.Decimal)
            { Direction = ParameterDirection.Output };

        command.Parameters.Add(sumParameter);
        command.Parameters.Add(medianParameter);

        await command.ExecuteNonQueryAsync(ct);

        var sum = (long)sumParameter.Value!;
        var median = (decimal)medianParameter.Value!;

        return new FileAggregatedInfo
        {
            Sum = sum,
            Median = median,
        };
    }
}