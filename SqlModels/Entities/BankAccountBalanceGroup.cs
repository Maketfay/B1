using System.ComponentModel.DataAnnotations.Schema;

namespace SqlModels.Entities;

public class BankAccountBalanceGroup
{
    [System.ComponentModel.DataAnnotations.Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public required int GroupIndex { get; set; }
    
    public required BankSheetInfo AggregatedInfo { get; set; }
    
    public virtual List<BankAccountData> Data { get; set; } = null!;
}