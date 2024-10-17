using System.Text;
using ExelFileProcessor.Logic.Services.Contracts;
using OfficeOpenXml;
using SqlModels.Entities.Enums;

namespace ExelFileProcessor.Logic.Services.Implementations.BankSheetExcelParser;

public class BankSheetExcelParser : IBankSheetExcelParser
{
    private readonly ILogger<BankSheetExcelParser> _logger;

    public BankSheetExcelParser(ILogger<BankSheetExcelParser> logger)
    {
        _logger = logger;
    }

    public async Task<BankSheetParseResult> ParseAsync(IFormFile file, CancellationToken ct)
    {
        try
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var stream = new MemoryStream();

            await file.CopyToAsync(stream, ct);
            stream.Position = 0;

            using var package = new ExcelPackage(stream);

            var worksheet = package.Workbook.Worksheets.FirstOrDefault();

            if (worksheet == null)
                return new BankSheetParseResult
                {
                    Result = BankSheetParseResultType.WorksheetIsEmpty,
                    BankSheetDto = null,
                };

            var bankName = worksheet.Cells["A1"].Text;

            var titleBuilder = new StringBuilder();

            titleBuilder.Append(worksheet.Cells["A2"].Text);
            titleBuilder.Append(" ");
            titleBuilder.Append(worksheet.Cells["A3"].Text);
            titleBuilder.Append(" ");
            titleBuilder.Append(worksheet.Cells["A4"].Text);

            var title = titleBuilder.ToString();
            
            if(string.IsNullOrEmpty(title))
                return new BankSheetParseResult
                {
                    Result = BankSheetParseResultType.TitleIsEmpty,
                    BankSheetDto = null,
                };

            if (!DateTime.TryParse(worksheet.Cells["A6"].Text, out var date))
            {
                return new BankSheetParseResult
                {
                    Result = BankSheetParseResultType.DateIsEmpty,
                    BankSheetDto = null,
                };
            }

            var currency = worksheet.Cells["G6"].Text;

            var parsedCurrency = ParseCurrency(currency);
            
            if(parsedCurrency == null)
                return new BankSheetParseResult
                {
                    Result = BankSheetParseResultType.CurrencyIsEmptyOrNotSupported,
                    BankSheetDto = null,
                };

            int rowCount = worksheet.Dimension.Rows;

            var groups = new List<BankAccountGroupDto>();

            var currentClass = new BankAccountGroupDto
            {
                Title = "",
                AggregatedData = null,
            };

            var currentGroupIndex = new BankAccountIndexGroupDto
            {
                GroupIndex = 0,
                AggregatedData = null,
            };

            for (int row = 9; row <= rowCount - 2; row++)
            {
                if (worksheet.Cells[row, 1].Text.StartsWith("КЛАСС"))
                {
                    currentClass.Title = worksheet.Cells[row, 1].Text;

                    continue;
                }

                if (worksheet.Cells[row, 1].Text.StartsWith("ПО КЛАССУ") && worksheet.Cells[row, 1].Style.Font.Bold)
                {
                    var aggregatedInfo = new BankAccountDataDto
                    {
                        IncomingBalanceActive = Convert.ToDecimal(worksheet.Cells[row, 2].Value ?? 0),
                        IncomingBalancePassive = Convert.ToDecimal(worksheet.Cells[row, 3].Value ?? 0),
                        TurnoverDebit = Convert.ToDecimal(worksheet.Cells[row, 4].Value ?? 0),
                        TurnoverCredit = Convert.ToDecimal(worksheet.Cells[row, 5].Value ?? 0),
                        OutgoingBalanceActive = Convert.ToDecimal(worksheet.Cells[row, 6].Value ?? 0),
                        OutgoingBalancePassive = Convert.ToDecimal(worksheet.Cells[row, 7].Value ?? 0)
                    };

                    currentClass.AggregatedData = aggregatedInfo;

                    groups.Add(currentClass);

                    currentClass = new BankAccountGroupDto
                    {
                        Title = "",
                        AggregatedData = null,
                    };

                    continue;
                }

                if (worksheet.Cells[row, 1].Style.Font.Bold)
                {
                    currentGroupIndex.GroupIndex = Convert.ToInt32(worksheet.Cells[row, 1].Value ?? 0);

                    var aggregatedInfo = new BankAccountDataDto
                    {
                        IncomingBalanceActive = Convert.ToDecimal(worksheet.Cells[row, 2].Value ?? 0),
                        IncomingBalancePassive = Convert.ToDecimal(worksheet.Cells[row, 3].Value ?? 0),
                        TurnoverDebit = Convert.ToDecimal(worksheet.Cells[row, 4].Value ?? 0),
                        TurnoverCredit = Convert.ToDecimal(worksheet.Cells[row, 5].Value ?? 0),
                        OutgoingBalanceActive = Convert.ToDecimal(worksheet.Cells[row, 6].Value ?? 0),
                        OutgoingBalancePassive = Convert.ToDecimal(worksheet.Cells[row, 7].Value ?? 0)
                    };

                    currentGroupIndex.AggregatedData = aggregatedInfo;

                    currentClass.Groups.Add(currentGroupIndex);

                    currentGroupIndex = new BankAccountIndexGroupDto
                    {
                        GroupIndex = 0,
                        AggregatedData = null,
                    };

                    continue;
                }

                var accountNumber = Convert.ToInt32(worksheet.Cells[row, 1].Value ?? 0);

                var data = new BankAccountDataDto
                {
                    IncomingBalanceActive = Convert.ToDecimal(worksheet.Cells[row, 2].Value ?? 0),
                    IncomingBalancePassive = Convert.ToDecimal(worksheet.Cells[row, 3].Value ?? 0),
                    TurnoverDebit = Convert.ToDecimal(worksheet.Cells[row, 4].Value ?? 0),
                    TurnoverCredit = Convert.ToDecimal(worksheet.Cells[row, 5].Value ?? 0),
                    OutgoingBalanceActive = Convert.ToDecimal(worksheet.Cells[row, 6].Value ?? 0),
                    OutgoingBalancePassive = Convert.ToDecimal(worksheet.Cells[row, 7].Value ?? 0),
                };

                currentGroupIndex.Data[accountNumber] = data;
            }

            return new BankSheetParseResult
            {
                Result = BankSheetParseResultType.Ok,
                BankSheetDto = new BankSheetDto
                {
                    BankName = bankName,
                    Title = title,
                    Date = date,
                    Currency = parsedCurrency.Value,
                    Groups = groups,
                    AggregatedData = new BankAccountDataDto
                    {
                        IncomingBalanceActive = Convert.ToDecimal(worksheet.Cells[rowCount - 1, 2].Value ?? 0),
                        IncomingBalancePassive = Convert.ToDecimal(worksheet.Cells[rowCount - 1, 3].Value ?? 0),
                        TurnoverDebit = Convert.ToDecimal(worksheet.Cells[rowCount - 1, 4].Value ?? 0),
                        TurnoverCredit = Convert.ToDecimal(worksheet.Cells[rowCount - 1, 5].Value ?? 0),
                        OutgoingBalanceActive = Convert.ToDecimal(worksheet.Cells[rowCount - 1, 6].Value ?? 0),
                        OutgoingBalancePassive = Convert.ToDecimal(worksheet.Cells[rowCount - 1, 7].Value ?? 0)
                    },
                }
            };
        }
        catch (Exception ex)
        {
            _logger.LogWarning("Parse excel file error, see inner exception: {ex}", ex);

            return new BankSheetParseResult
            {
                Result = BankSheetParseResultType.UnknownError,
                BankSheetDto = null
            };
        }
    }

    private static BankSheetCurrency? ParseCurrency(string currency)
    {
        return currency switch
        {
            "в руб." => BankSheetCurrency.RUB,
            "в бел.руб." => BankSheetCurrency.BYN,
            "в дол." => BankSheetCurrency.USD,
            _ => null,
        };
    }
}