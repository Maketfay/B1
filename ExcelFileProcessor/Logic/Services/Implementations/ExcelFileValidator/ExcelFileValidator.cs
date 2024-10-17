using ExelFileProcessor.Logic.Services.Contracts;

namespace ExelFileProcessor.Logic.Services.Implementations.ExcelFileValidator;

public class ExcelFileValidator : IExcelFileValidator
{
    public ExcelFileValidationResult? Validate(IFormFile file)
    {
        if (file.Length == 0)
            return new ExcelFileValidationResult(ExcelFileValidationResultType.FileIsEmpty, "File is empty");
        
        if (!file.FileName.EndsWith(".xlsx"))
            return new ExcelFileValidationResult(ExcelFileValidationResultType.WrongFormat, "Invalid file format. Please upload an Excel file (.xlsx).");

        var contentType = file.ContentType;
        if (contentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            return new ExcelFileValidationResult(ExcelFileValidationResultType.WrongContentType, "Invalid content type. Please upload a valid Excel file.");

        return null;
    }
}