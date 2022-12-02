using CryptocurrencyQuote.Domain;
using CryptocurrencyQuote.Infrastructure;
using CryptocurrencyQuote.Infrastructure.ExchangeRates;
using CryptocurrencyQuote.WebAPI.Filters;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
var exchangeRatesApiKey = builder.Configuration["ExchangeRates:API_KEY"];
var coinMarketCapApiKey = builder.Configuration["CoinMarketCap:API_KEY"];
// Add services to the container.

builder.Services.AddControllers(options => { options.Filters.Add<RequireHttpsOrCloseAttribute>(); }) ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IConfigurationAPI, ExchangeRateConfiguration>();
builder.Services.AddScoped<ICryptocurrencyAPI, ExchangeRatesAPI>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseAuthorization();

app.MapControllers();

app.Run();
