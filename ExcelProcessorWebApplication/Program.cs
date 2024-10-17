using ExcelProcessorWebApplication.Components;
using ExcelProcessorWebApplication.Logic.Services.Contracts;
using ExcelProcessorWebApplication.Logic.Services.Implementations.BankSheetProvider;
using ExcelProcessorWebApplication.Logic.Services.Implementations.ImportedFileProvider;
using Radzen;
using SqlModels.Configuration;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddWebContext(configuration);

services.AddRadzenComponents();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

services.AddScoped<IImportedFileProvider, ImportedFileProvider>();
services.AddScoped<IBankSheetProvider, BankSheetProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();