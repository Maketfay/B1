using SqlModels.Entities.Enums;

namespace ExcelProcessorWebApplication.Logic.Services.Contracts;

public interface IBankSheetProvider
{
    public IQueryable<BankSheetDto> GetBankSheet(int id);
}

public class BankSheetDto
{
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

    public required IEnumerable<BankAccountSimpleDataDto> Data { get; set; }
}

public class BankAccountSimpleDataDto
{
    public required BankAccountDataDto Data { get; set; }
    
    public required int AccountNumber { get; set; }
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