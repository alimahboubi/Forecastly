using Forecastly.Application;
using Forecastly.Infrastructure.Cache.InMemory;
using Forecastly.Infrastructure.OpenWeatherMap;
using Forecastly.Infrastructure.OpenWeatherMap.Models;


var builder = WebApplication.CreateBuilder(args);

var openWeatherMapConfiguration = new OpenWeatherMapConfiguration();
builder.Configuration.GetSection(nameof(OpenWeatherMapConfiguration)).Bind(openWeatherMapConfiguration);
builder.Services.AddSingleton(openWeatherMapConfiguration);

builder.Services.AddOpenWeatherServices()
    .AddInMemoryCache()
    .AddOpenWeatherMap(openWeatherMapConfiguration);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

await app.RunAsync();