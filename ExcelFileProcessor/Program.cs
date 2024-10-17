
using ExelFileProcessor.Logic.Services.Contracts;
using ExelFileProcessor.Logic.Services.Implementations.BankSheetExcelParser;
using ExelFileProcessor.Logic.Services.Implementations.BankSheetSaver;
using ExelFileProcessor.Logic.Services.Implementations.ExcelFileValidator;
using SqlModels.Configuration;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddWebContext(configuration);

services.AddControllers();
services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy => { policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
});
services.AddHttpContextAccessor();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddSingleton<IExcelFileValidator, ExcelFileValidator>();
services.AddSingleton<IBankSheetExcelParser, BankSheetExcelParser>();
services.AddScoped<IBankSheetSaver, BankSheetSaver>();

var app = builder.Build();

app.UseRouting();
app.UseCors();
app.UseHttpsRedirection();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();