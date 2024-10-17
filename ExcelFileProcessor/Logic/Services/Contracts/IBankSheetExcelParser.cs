using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SqlModels.Entities.Enums;

namespace ExelFileProcessor.Logic.Services.Contracts;

public interface IBankSheetExcelParser
{
    public Task<BankSheetParseResult> ParseAsync(IFormFile file, CancellationToken ct);
}

public class BankSheetParseResult
{
    public required BankSheetParseResultType Result { get; set; }

    public required BankSheetDto? BankSheetDto { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BankSheetParseResultType
{
    [EnumMember]
    Ok,
    
    [EnumMember]
    WorksheetIsEmpty,
    
    [EnumMember]
    DateIsEmpty,
    
    [EnumMember]
    CurrencyIsEmptyOrNotSupported,
    
    [EnumMember]
    TitleIsEmpty,
    
    [EnumMember]
    UnknownError,
}

public class BankSheetDto
{
    public required string? BankName { get; set; }

    public required string? Title { get; set; }

    public required DateTime? Date { get; set; }

    public required BankSheetCurrency Currency { get; set; }

    public required IEnumerable<BankAccountGroupDto> Groups { get; set; }
    
    public required BankAccountDataDto AggregatedData { get; set; }
}

public class BankAccountGroupDto
{
    public required string Title { get; set; }

    public required BankAccountDataDto? AggregatedData { get; set; }

    public List<BankAccountIndexGroupDto> Groups { get; set; } = new();
}

public class BankAccountIndexGroupDto
{
    public required int GroupIndex { get; set; }

    public required BankAccountDataDto? AggregatedData { get; set; }

    public Dictionary<int, BankAccountDataDto> Data { get; set; } = new();
}

public class BankAccountDataDto
{
    public required decimal IncomingBalanceActive { get; set; }

    public required decimal IncomingBalancePassive { get; set; }

    public required decimal TurnoverDebit { get; set; }

    public required decimal TurnoverCredit { get; set; }

    public required decimal OutgoingBalanceActive { get; set; }

    public required decimal OutgoingBalancePassive { get; set; }
}