using EnglishVoNo.Services;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<CosmosClient>(sp =>
    new CosmosClient(
        builder.Configuration["CosmosDb:ConnectionString"],
        new CosmosClientOptions
        {
            SerializerOptions = new CosmosSerializationOptions
            {
                PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
            }
        }
    )
);
builder.Services.AddScoped<IVocabularyService,VocabularyService>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapGet("/ping", () => Results.Ok("pong1"));
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
