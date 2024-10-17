using ExcelProcessorWebApplication.Logic.Services.Contracts;
using SqlModels;

namespace ExcelProcessorWebApplication.Logic.Services.Implementations.ImportedFileProvider;

public class ImportedFileProvider : IImportedFileProvider
{
    private readonly WebContext _webContext;

    public ImportedFileProvider(WebContext webContext)
    {
        _webContext = webContext;
    }

    public IQueryable<ImportedFileDto> GetImportedFiles()
    {
        return _webContext.BankBalanceSheets.Select(t => new ImportedFileDto
        {
            BankName = t.BankName,
            Title = t.Title,
            Date = t.Date,
            UpdateTime = t.UpdateTime,
            FileName = t.FileName,
            Id = t.Id
        });
    }
}