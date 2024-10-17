using System.ComponentModel.DataAnnotations.Schema;

namespace SqlModels.Entities;

public class BankAccountData
{
    [System.ComponentModel.DataAnnotations.Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public required int AccountNumber { get; set; }
    
    public required BankSheetInfo Info { get; set; }
    
    public required int BankAccountBalanceGroupId { get; set; }

    public virtual BankAccountBalanceGroup BankAccountBalanceGroup { get; set; } = null!;
}