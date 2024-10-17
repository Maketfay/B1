using ExcelProcessorWebApplication.Logic.Services.Contracts;
using SqlModels;

namespace ExcelProcessorWebApplication.Logic.Services.Implementations.BankSheetProvider;

public class BankSheetProvider : IBankSheetProvider
{
    private readonly WebContext _webContext;

    public BankSheetProvider(WebContext webContext)
    {
        _webContext = webContext;
    }

    public IQueryable<BankSheetDto> GetBankSheet(int id)
    {
        return _webContext.BankBalanceSheets.Where(t => t.Id == id).Select(t => new BankSheetDto
        {
            Currency = t.Currency,
            Groups = t.Groups.Select(g => new BankAccountGroupDto
            {
                Title = g.Title,
                AggregatedData = new BankAccountDataDto
                {
                    IncomingBalanceActive = g.AggregatedInfo.IncomingBalanceActive,
                    IncomingBalancePassive = g.AggregatedInfo.IncomingBalancePassive,
                    TurnoverDebit = g.AggregatedInfo.TurnoverDebit,
                    TurnoverCredit = g.AggregatedInfo.TurnoverCredit,
                    OutgoingBalanceActive = g.AggregatedInfo.OutgoingBalanceActive,
                    OutgoingBalancePassive = g.AggregatedInfo.OutgoingBalancePassive,
                },
                Groups = g.BalanceGroups.Select(p => new BankAccountIndexGroupDto
                {
                    GroupIndex = p.GroupIndex,
                    AggregatedData = new BankAccountDataDto
                    {
                        IncomingBalanceActive = p.AggregatedInfo.IncomingBalanceActive,
                        IncomingBalancePassive = p.AggregatedInfo.IncomingBalancePassive,
                        TurnoverDebit = p.AggregatedInfo.TurnoverDebit,
                        TurnoverCredit = p.AggregatedInfo.TurnoverCredit,
                        OutgoingBalanceActive = p.AggregatedInfo.OutgoingBalanceActive,
                        OutgoingBalancePassive = p.AggregatedInfo.OutgoingBalancePassive,
                    },
                    Data = p.Data.Select(r => new BankAccountSimpleDataDto
                    {
                        Data = new BankAccountDataDto
                        {
                            IncomingBalanceActive = r.Info.IncomingBalanceActive,
                            IncomingBalancePassive = r.Info.IncomingBalancePassive,
                            TurnoverDebit = r.Info.TurnoverDebit,
                            TurnoverCredit = r.Info.TurnoverCredit,
                            OutgoingBalanceActive = r.Info.OutgoingBalanceActive,
                            OutgoingBalancePassive = r.Info.OutgoingBalancePassive,
                        },
                        AccountNumber = r.AccountNumber,
                    })
                }).ToList()
            }),
            AggregatedData = new BankAccountDataDto
            {
                IncomingBalanceActive = t.AggregatedInfo.IncomingBalanceActive,
                IncomingBalancePassive = t.AggregatedInfo.IncomingBalancePassive,
                TurnoverDebit = t.AggregatedInfo.TurnoverDebit,
                TurnoverCredit = t.AggregatedInfo.TurnoverCredit,
                OutgoingBalanceActive = t.AggregatedInfo.OutgoingBalanceActive,
                OutgoingBalancePassive = t.AggregatedInfo.OutgoingBalancePassive,
            }
        });
    }
}