using System.ComponentModel.DataAnnotations.Schema;

namespace SqlModels.Entities;

public class BankAccountGroup
{
    [System.ComponentModel.DataAnnotations.Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public required string Title { get; set; }
    
    public required BankSheetInfo AggregatedInfo { get; set; }
    
    public required int BankBalanceSheetId { get; set; }

    public virtual BankBalanceSheet BankBalanceSheet { get; set; } = null!;

    public virtual List<BankAccountBalanceGroup> BalanceGroups { get; set; } = null!;
}

[ComplexType]
public class BankSheetInfo
{
    public required decimal IncomingBalanceActive { get; set; }
    
    public required decimal IncomingBalancePassive { get; set; }
    
    public required decimal TurnoverDebit { get; set; }
    
    public required decimal TurnoverCredit { get; set; }
    
    public required decimal OutgoingBalanceActive { get; set; }
    
    public required decimal OutgoingBalancePassive { get; set; }
}