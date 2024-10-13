using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SqlModels.Entities;

namespace SqlModels;

public class WebContext : DbContext
{
    public DbSet<RandomFile> RandomFiles { get; set; }
    
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