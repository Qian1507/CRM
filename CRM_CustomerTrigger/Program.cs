using CRM_Function.Services;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);



builder.ConfigureFunctionsWebApplication();



// dependency injection
builder.Services.AddScoped<IEmailService, EmailService>();



builder.Build().Run();


// To run Azurite (Azure Storage Emulator):
//azurite --silent --location ./azurite-data --debug ./azurite-debug.log
