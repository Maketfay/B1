namespace ExelFileProcessor.Logic.Services.Contracts;

public interface IExcelFileValidator
{
    public ExcelFileValidationResult? Validate(IFormFile file);
}

public class ExcelFileValidationResult
{
    public ExcelFileValidationResultType ValidationResultType { get; }
    
    public string Message { get; }

    public ExcelFileValidationResult(ExcelFileValidationResultType validationResultType, string message)
    {
        ValidationResultType = validationResultType;
        Message = message;
    }
}

public enum ExcelFileValidationResultType
{
    FileIsEmpty,
    WrongFormat,
    WrongContentType,
}