using ExelFileProcessor.Logic.Services.Contracts;
using SqlModels;
using SqlModels.Entities;

namespace ExelFileProcessor.Logic.Services.Implementations.BankSheetSaver;

public class BankSheetSaver : IBankSheetSaver
{
    private readonly WebContext _webContext;

    public BankSheetSaver(WebContext webContext)
    {
        _webContext = webContext;
    }

    public async Task SaveAsync(string fileName, BankSheetDto bankSheet, CancellationToken ct)
    {
        var bankBalanceSheet = new BankBalanceSheet
        {
            BankName = bankSheet.BankName,
            Title = bankSheet.Title,
            Date = bankSheet.Date,
            UpdateTime = DateTime.UtcNow,
            FileName = fileName,
            Currency = bankSheet.Currency,
            Groups = bankSheet.Groups.Select(g => new BankAccountGroup
            {
                Title = g.Title,
                AggregatedInfo = new BankSheetInfo
                {
                    IncomingBalanceActive = g.AggregatedData?.IncomingBalanceActive ?? 0,
                    IncomingBalancePassive = g.AggregatedData?.IncomingBalancePassive ?? 0,
                    TurnoverDebit = g.AggregatedData?.TurnoverDebit ?? 0,
                    TurnoverCredit = g.AggregatedData?.TurnoverCredit ?? 0,
                    OutgoingBalanceActive = g.AggregatedData?.OutgoingBalanceActive ?? 0,
                    OutgoingBalancePassive = g.AggregatedData?.OutgoingBalancePassive ?? 0,
                },
                BankBalanceSheetId = 0,
                BalanceGroups = g.Groups.Select(t => new BankAccountBalanceGroup
                {
                    GroupIndex = t.GroupIndex,
                    AggregatedInfo = new BankSheetInfo
                    {
                        IncomingBalanceActive = t.AggregatedData?.IncomingBalanceActive ?? 0,
                        IncomingBalancePassive = t.AggregatedData?.IncomingBalancePassive ?? 0,
                        TurnoverDebit = t.AggregatedData?.TurnoverDebit ?? 0,
                        TurnoverCredit = t.AggregatedData?.TurnoverCredit ?? 0,
                        OutgoingBalanceActive = t.AggregatedData?.OutgoingBalanceActive ?? 0,
                        OutgoingBalancePassive = t.AggregatedData?.OutgoingBalancePassive ?? 0,
                    },
                    Data = t.Data.Select(p => new BankAccountData
                    {
                        AccountNumber = p.Key,
                        Info = new BankSheetInfo
                        {
                            IncomingBalanceActive = p.Value.IncomingBalanceActive,
                            IncomingBalancePassive = p.Value.IncomingBalancePassive,
                            TurnoverDebit = p.Value.TurnoverDebit,
                            TurnoverCredit = p.Value.TurnoverCredit,
                            OutgoingBalanceActive = p.Value.OutgoingBalanceActive,
                            OutgoingBalancePassive = p.Value.OutgoingBalancePassive,
                        },
                        BankAccountBalanceGroupId = 0
                    }).ToList(),
                }).ToList(),
            }).ToList(),
            AggregatedInfo = new BankSheetInfo
            {
                IncomingBalanceActive = bankSheet.AggregatedData.IncomingBalanceActive,
                IncomingBalancePassive = bankSheet.AggregatedData.IncomingBalancePassive,
                TurnoverDebit = bankSheet.AggregatedData.TurnoverDebit,
                TurnoverCredit = bankSheet.AggregatedData.TurnoverCredit,
                OutgoingBalanceActive = bankSheet.AggregatedData.OutgoingBalanceActive,
                OutgoingBalancePassive = bankSheet.AggregatedData.OutgoingBalancePassive,
            }
        };

        await _webContext.BankBalanceSheets.AddAsync(bankBalanceSheet, ct);
        await _webContext.SaveChangesAsync(ct);
    }
}