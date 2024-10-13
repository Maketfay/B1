using FileProcessor.Logic.Services.Contracts;
using FileProcessor.Logic.Services.Implementations.FileGenerator;
using FileProcessor.Logic.Services.Implementations.FileImporter;
using FileProcessor.Logic.Services.Implementations.FileMerger;
using FileProcessor.Logic.Services.Implementations.RandomFileProvider;
using FileProcessor.Workers;
using SqlModels.Configuration;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddWebContext(configuration);

services.AddScoped<IFileGenerator, FileGenerator>();
services.AddScoped<IFileImporter, FileImporter>();
services.AddScoped<IFileMerger, FileMerger>();
services.AddScoped<IRandomFileProvider, RandomFileProvider>();

services.AddControllers();
services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy => { policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
});
services.AddHttpContextAccessor();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddHostedService<MigrateWorker>();

var app = builder.Build();

app.UseRouting();
app.UseCors();
app.UseHttpsRedirection();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();