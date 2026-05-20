using CRM_Api.Endpoints;
using CRM_Api.Interfaces;
using CRM_Api.Repos;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Cosmos DB client
builder.Services.AddSingleton(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var conn = config.GetConnectionString("CosmosDb");

    return new CosmosClient(conn);
});

// Repo
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();




var app = builder.Build();

// Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



// endpoints
app.MapCustomerEndpoints();

app.Run();
