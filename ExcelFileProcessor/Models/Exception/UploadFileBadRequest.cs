using ExelFileProcessor.Logic.Services.Contracts;

namespace ExelFileProcessor.Models.Exception;

public class UploadFileBadRequest
{
    public BankSheetParseResultType Result { get; }

    public UploadFileBadRequest(BankSheetParseResultType result)
    {
        Result = result;
    }
}