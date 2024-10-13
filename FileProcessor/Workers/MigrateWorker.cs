using Microsoft.EntityFrameworkCore;
using SqlModels;

namespace FileProcessor.Workers;

public class MigrateWorker : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<MigrateWorker> _logger;

    public MigrateWorker(IServiceScopeFactory serviceScopeFactory, ILogger<MigrateWorker> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var serviceScope = _serviceScopeFactory.CreateAsyncScope();
        
        var context = serviceScope.ServiceProvider.GetRequiredService<WebContext>();

        context.Database.SetCommandTimeout(TimeSpan.FromMinutes(5));
        
        await using var transaction = await context.Database.BeginTransactionAsync(stoppingToken);

        await context.Database.MigrateAsync(cancellationToken: stoppingToken);

        await transaction.CommitAsync(stoppingToken);

        _logger.LogInformation("Migrations applied");
    }
}