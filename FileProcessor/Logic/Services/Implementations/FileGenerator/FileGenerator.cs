using System.Text;
using FileProcessor.Logic.Helpers;
using FileProcessor.Logic.Services.Contracts;

namespace FileProcessor.Logic.Services.Implementations.FileGenerator;

public class FileGenerator : IFileGenerator
{
    private readonly Random _random;

    private static readonly string LatinChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    private static readonly string CyrillicChars = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";

    public FileGenerator()
    {
        _random = new Random((int)DateTime.UtcNow.ToTimeStamp());
    }

    public async Task GenerateFilesAsync(int fileCount, string outputPath, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        
        for (int i = 0; i < fileCount; i++)
        {
            var filePath = Path.Combine(outputPath, $"file_{i + 1}.txt");

            await using var writer = new StreamWriter(filePath);

            for (int j = 0; j < 100000; j++)
            {
                string line = GenerateRandomLine();

                await writer.WriteLineAsync(line);
            }
        }
    }

    private string GenerateRandomLine()
    {
        var randomDate = DateTime.Now.AddDays(-_random.Next(0, 5 * 365)).ToString("dd.MM.yyyy");
        var randomLatin = GenerateRandomString(10, LatinChars);
        var randomCyrillic = GenerateRandomString(10, CyrillicChars);
        
        var randomInteger = _random.Next(1, 50000000) * 2;
        var randomDouble = _random.NextDouble() * (20 - 1) + 1;

        return $"{randomDate}||{randomLatin}||{randomCyrillic}||{randomInteger}||{randomDouble:F8}";
    }

    private string GenerateRandomString(int length, string chars)
    {
        var stringBuilder = new StringBuilder(length);
        
        for (int i = 0; i < length; i++)
        {
            stringBuilder.Append(chars[_random.Next(chars.Length)]);
        }
        
        return stringBuilder.ToString();
    }
}