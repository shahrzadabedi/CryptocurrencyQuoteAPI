Shahrzad Abedi
----------------
Questions:
1. How long did you spend on the coding assignment? What would you add to your solution if you had
more time? If you didn't spend much time on the coding assignment then use this as an opportunity to
explain what you would add.

It took me 7 hours:
	1-1 If I wanted to spend more time I would implement either a user secret key mechanism or a token exchange mechanism so that the API_Key value 
	would be ommited from the source code since it is not secure to hard-code the secret keys in application source code. 
	1-2 I also would implement the integration test for the CryptocurrencyQuote.WebAPI project 
	1-3 I would implement Ion spec more thoroughly

2. What was the most useful feature that was added to the latest version of your language of choice?
Please include a snippet of code that shows how you've used it.

.NET 7 was released in November 2022 and it has introduced different new features. One of the features introduced in .NET 7 was build-in rate-limiting middleware. We can install System.Threading.RateLimiting to have access to it.
Before that we had to install third-party libraries to be able to rate-limit our endpoints. To rate-limit an existing endpoint we need to first add RateLimiter to the IServiceCollection and add the RateLimiter middleware to the pipleline. For example here we use basic Fixed-window rate-limiting algorithm and configure that in our Program.cs file:
	
 	builder.Services.AddRateLimiter(_ => _
	    .AddFixedWindowLimiter("fixed", options =>
	    {
	        options.PermitLimit = 2;
	        options.Window = TimeSpan.FromSeconds(12);
	        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
	        options.QueueLimit = 2;
	    }));
	var app = builder.Build();
	
	if (app.Environment.IsDevelopment())
	{
	
	    app.UseSwagger();
	    app.UseSwaggerUI();
	}
	app.UseRateLimiter();
Then, we decorate our controller class with [EnableRateLimiting("fixed")] attribute to indicate that we want all the endpoints in this Controller to be 	rate-limited with policy name "fixed" that we already defined:

	[EnableRateLimiting("fixed")]
	public class CryptoQuotesController : BaseController
	{
	    [HttpGet]
	
	    public async Task<ActionResult<List<GetCryptoQuotesListResponse>>> Get([FromQuery] string fromCurrency,
	        string toCurrencies)
	    {
	        try
	        {
	            var toCurrenciesList = toCurrencies?.Split(',').ToList();
	
	            var query = new GetCryotoQuotesQuery(fromCurrency, toCurrenciesList);
	
	            var result = await Mediator.Send(query);
	
	            if (result.Data != null)
	                MakeUrlLink(result.Data, fromCurrency);
	
	            return result.ToActionResult();
	        }
	        catch (Exception ex)
	        {
	            return StatusCode((int)((StatusCodeHelper)ex).statusCode, ex.Message);                 
	        }            
	    } 
	}
3. How would you track down a performance issue in production? Have you ever had to do this?
	
	Yes, I have experienced that. When you are experiencing performance issues you should investigate some internal or external dependecies 
	that might cause the problem:
	For example if you are calling an external api, it might be the problem with that external api performance. 
	What you should do generally is to track the log records to spot where the performance issue arises. After That you can go deeper and if 
	the performance issue is with an external api call, ask for support of the api providers wherever they are and if it is something internal
	you should go deeper to spot the performance problem for example it might be that you are performing a query on a table with lots of
	columns and returing all those columns in your select part where you actually don't need those columns.
	So you may need to do some query optimizations to solve the performance issue.

4. What was the latest technical book you have read or tech conference you have been to? What did you
learn?

It was a Live .NET community Standup about Visual Studio Dev Tunnel. They introduced a new feature in Visual Studio 17.6 that enables ad-hoc connection between machines that cannot directly connect to each other. It creates a URL that allows any device with an internet connection to connect to an ASP.NET Core project while it runs on localhost. This is a useful feature especially when you don't want to host your application on a specific server but you want to be able to connect two or more consumer-supplier developers machines together.

5. What do you think about this technical assessment?
	It was my second time to do this excercise. I did a lot of refactoring to make the code cleaner and enhance its readability and maintainability.
	I also added CryptocurrencyQuote.Application layer to it and implemented CQRS using MediatR. I moved ICryptocurrencyAPI to infrastructure layer and also 	moved the Dtos in its method to infrastructure as well because these Dtos and the method deals with external api and it shouldn't be placed in Domain 		layer.
	It was a great excercise especially refactoring my code and refining the project architecture was good.

7. Please, describe yourself using JSON

{
   "name":"Shahrzad",
   "family":"Abedi",
   "age":35,
   "interests":[
   	"Learning",
    	"Building",
     	"Exploring",
        "Thinking",
   	"Technology",
    	"Science",
      	"Psychology",
        "Personal Development",
	"Nature",
	"Cats",
 	"Dogs"
   ],
   "gender":"female",
   "sports":[
      "Taichi",
      "Swimming"
   ],
   "programmingLanguage":{
      "backend":[
         "C#",
         "Java",
         "Golang"
      ],
      "frontend":"Angular"
   }
}
