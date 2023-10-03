using CryptocurrencyQuote.Application.CryptoQuote.MappingProfile;
using CryptocurrencyQuote.Application.Models;
using CryptocurrencyQuote.Domain;
using CryptocurrencyQuote.Infrastructure;
using CryptocurrencyQuote.Infrastructure.ExchangeRates;
using CryptocurrencyQuote.WebAPI.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => { options.Filters.Add<RequireHttpsOrCloseAttribute>(); });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IConfigurationAPI, ExchangeRateConfiguration>();
builder.Services.AddAutoMapper(typeof(CryptoQuoteMappingProfile));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Resource).Assembly));
builder.Services.AddScoped<CryptocurrencyQuote.Infrastructure.ICryptocurrencyAPI, ExchangeRatesAPI>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();