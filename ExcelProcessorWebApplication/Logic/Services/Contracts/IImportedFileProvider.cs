namespace ExcelProcessorWebApplication.Logic.Services.Contracts;

public interface IImportedFileProvider
{
    public IQueryable<ImportedFileDto> GetImportedFiles();
}

public class ImportedFileDto
{
    public required int Id { get; set; }
    
    public required string? BankName { get; set; }
    
    public required string? Title { get; set; }
    
    public required DateTime? Date { get; set; }
    
    public required DateTime UpdateTime { get; set; }
    
    public required string FileName { get; set; }
}