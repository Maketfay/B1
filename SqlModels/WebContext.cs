using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SqlModels.Entities;

namespace SqlModels;

public class WebContext : DbContext
{
    public DbSet<RandomFile> RandomFiles { get; set; }
    
    public DbSet<BankAccountData> BankAccountDatas { get; set; }
    
    public DbSet<BankAccountBalanceGroup> BankAccountBalanceGroups { get; set; }
    
    public DbSet<BankAccountGroup> BankAccountGroups { get; set; }
    
    public DbSet<BankBalanceSheet> BankBalanceSheets { get; set; }
    
    public WebContext(DbContextOptions<WebContext> options) 
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.MultipleCollectionIncludeWarning));
    }
}