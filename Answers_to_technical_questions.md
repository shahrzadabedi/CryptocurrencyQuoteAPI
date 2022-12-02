Questions:

1. How long did you spend on the coding assignment? What would you add to your solution if you had
more time? If you didn't spend much time on the coding assignment then use this as an opportunity to
explain what you would add.

It took me 15 hours.
	1-If I wanted to spend more time I would implement either a user secret key mechanism or a token exchange mechanism so that the API_Key value 
	would be ommited from the source code since it is not secure to hard-code the secret keys in application source code. 
	2-I also would implement the integration test for the CryptocurrencyQuote.WebAPI project 

2. What was the most useful feature that was added to the latest version of your language of choice?
Please include a snippet of code that shows how you've used it.

.NET 7 was released in November 2022 and it has introduced different new features. One of the most commonly used features is:

	Use LinQ Order instead of OrderBy using lambda expression:You have the ability to simply call Order() instead of OrderBy(p=>p.ID) for a 
	simple entity for example:

	List<int> ages = new List<int>();
	var orderedAges = ages.Order();

	Or if you have an entity that implements IComparable interface you can also again use Order(). For example:

	public class Person:IComparable<Person> {
		public string Name{get;set;}
		public string Age {get;set;}
		public int CompareTo(Person other)
		 {
			 return this.Age== other.Age ? 0:(this.Age < other.Age? -1 : 1);
		 }
	}
	Now in your client code you can write:

	List<Person> poeple = new Lis<Person>();
	var newOrdered = poeple.Order();

	Instead of old one:
	var oldOrdered = poeple.OrderBy(p=> p.Age);

3. How would you track down a performance issue in production? Have you ever had to do this?
	
	Yes, I have experienced that. When you are experiencing performance issues you should investigate some internal or external dependecies 
	that might cause the problem:
	For example if you are calling an external api, it might be the problem with that external api performance. 
	What you should do generally is to track the log records to spot where the performance issue arises. After That you can go deeper and if 
	the performance issue is with an external api call, ask for support of the api providers wherever they are and if it is something internal
	you should go deeper to spot the performance problem for example it might be that you are performing a query on a table with lots of
	columns and returing all thos columns in your select part where you actually don't need those columns.
	So you may need to do some query optimizations to solve the performance issue.

4. What was the latest technical book you have read or tech conference you have been to? What did you
learn?

	The latest technical conference title was "Use Composition over inheritance". 
	I learned to minimize using implementaion inheritance and instead use more interface inheritance and
	to consider the future side effects of implementing hirarchical implementation inheritance and instead consider using 
	composition in design.

5. What do you think about this technical assessment?

	It was a great excercise especially I had the chance to apply my knowledge related to Restful api design in 
	a client library project.

6. Please, describe yourself using JSON

{
   "name":"Shahrzad",
   "family":"Abedi",
   "age":34,
   "gender":"female",
   "hobbies":[
      "Reading about new technology trends through youtube or other social media",
      "Going out and having fun with my friends"
   ],
   "sports":[
      "Taichi"
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
