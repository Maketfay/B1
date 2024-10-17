namespace ExelFileProcessor.Logic.Services.Contracts;

public interface IBankSheetSaver
{
    public Task SaveAsync(string fileName, BankSheetDto bankSheet, CancellationToken ct);
}