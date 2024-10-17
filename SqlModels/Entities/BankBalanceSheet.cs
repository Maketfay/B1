using System.ComponentModel.DataAnnotations.Schema;
using SqlModels.Entities.Enums;

namespace SqlModels.Entities;

public class BankBalanceSheet
{
    [System.ComponentModel.DataAnnotations.Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public required string? BankName { get; set; }
    
    public required string? Title { get; set; }
    
    public required DateTime? Date { get; set; }
    
    public required DateTime UpdateTime { get; set; }
    
    public required string FileName { get; set; }
    
    public required BankSheetCurrency Currency { get; set; }
    
    public required BankSheetInfo AggregatedInfo { get; set; }

    public virtual List<BankAccountGroup> Groups { get; set; } = null!;
}