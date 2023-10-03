# CryptocurrencyQuoteAPI
Shahrzad Abedi
-----------------------------
Description:

1-I have implemented this project using .NET 7. and VS2022. I have applied Domain-Driver-Design project architecture for the src part. 
This four different parts of a DDD project have been put in three projects: CryptocurrencyQuote.Infrastructure, CryptocurrencyQuote.Domain, CryptocurrencyQuote.Application
and CryptocurrencyQuote.WebAPI. I used MediatR for CQRS and Automapper for mapping DTOs.

2-Unit tests are in path test/CryptocurrencyQuote.Infrastructure.Tests 

3-I have added the ability to use both 
  • https://exchangeratesapi.io
  • https://coinmarketcap.com/api (free version) 
  However I only have implemented consuming the first api to get the desired results.
  
4-I used Ion spec for Restful design

