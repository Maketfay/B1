using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SqlModels.Configuration;

public static class WebContextConfiguration
{
    public static void AddWebContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("WebContext");

        services.AddDbContext<WebContext>(options =>
        {
            options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36)), op => op.UseNewtonsoftJson());
        });
    }
}