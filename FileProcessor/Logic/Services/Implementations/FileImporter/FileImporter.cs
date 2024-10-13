using System.Globalization;
using FileProcessor.Logic.Services.Contracts;

namespace FileProcessor.Logic.Services.Implementations.FileImporter;

public class FileImporter : IFileImporter
{
    private readonly IRandomFileProvider _randomFileProvider;

    public FileImporter(IRandomFileProvider randomFileProvider)
    {
        _randomFileProvider = randomFileProvider;
    }

    public async Task ImportFileAsync(string filePath, CancellationToken ct)
    {
        var parsedData = new List<RandomFileDataDto>();

        using var reader = new StreamReader(filePath);

        while (await reader.ReadLineAsync(ct) is { } line)
        {
            parsedData.Add(ParseLine(line));
        }

        await _randomFileProvider.SaveFileDataAsync(parsedData, ct);
    }
    
    private static RandomFileDataDto ParseLine(string line)
    {
        var values = line.Split("||");
        
        var date = DateTime.ParseExact(values[0], "dd.MM.yyyy", CultureInfo.InvariantCulture);
        var latin = values[1];
        var cyrillic = values[2];
        var integer = int.Parse(values[3]);
        var number = decimal.Parse(values[4], CultureInfo.InvariantCulture);

        return new RandomFileDataDto
        {
            Date = date,
            LatinWord = latin,
            CyrillicWord = cyrillic,
            RandomInteger = integer,
            RandomDouble = number,
        };
    }
}