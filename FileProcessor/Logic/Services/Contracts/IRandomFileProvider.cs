namespace FileProcessor.Logic.Services.Contracts;

public interface IRandomFileProvider
{
    public Task SaveFileDataAsync(IEnumerable<RandomFileDataDto> fileData, CancellationToken ct);
}

public class RandomFileDataDto
{
    public required DateTime Date { get; set; }

    public required string LatinWord { get; set; }

    public required string CyrillicWord { get; set; }

    public required int RandomInteger { get; set; }

    public required decimal RandomDouble { get; set; }
}