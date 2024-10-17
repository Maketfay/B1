using System.Net;
using ExelFileProcessor.Logic.Services.Contracts;
using ExelFileProcessor.Models.Exception;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ExelFileProcessor.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BalanceSheetController : ControllerBase
{
    private readonly IExcelFileValidator _excelFileValidator;
    private readonly IBankSheetExcelParser _bankSheetExcelParser;
    private readonly IBankSheetSaver _bankSheetSaver;

    public BalanceSheetController(IExcelFileValidator excelFileValidator, IBankSheetExcelParser bankSheetExcelParser, IBankSheetSaver bankSheetSaver)
    {
        _excelFileValidator = excelFileValidator;
        _bankSheetExcelParser = bankSheetExcelParser;
        _bankSheetSaver = bankSheetSaver;
    }

    [HttpPost("upload")]
    [SwaggerResponse((int)HttpStatusCode.OK)]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, null, typeof(UploadFileBadRequest))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, null, typeof(ExcelFileValidationResult))]
    public async Task<IActionResult> UploadFile(IFormFile file, CancellationToken ct)
    {
        var validationResult = _excelFileValidator.Validate(file);

        if (validationResult != null)
            return BadRequest(validationResult);
        
        var result = await _bankSheetExcelParser.ParseAsync(file, ct);

        if (result.Result != BankSheetParseResultType.Ok)
            return BadRequest(new UploadFileBadRequest(result.Result));
        
        await _bankSheetSaver.SaveAsync(file.FileName, result.BankSheetDto!, ct);
        
        return Ok();
    }
}