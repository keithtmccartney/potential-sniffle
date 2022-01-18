# potential-sniffle
[Kata] Web Scraper

* A web-scraper (accidental typo as "scrapper" through clipboard) for [Google UK](https://www.google.co.uk) to identify the listing of a wild-card keyword search:
	* Current search URL is defaulted to "www.gov.uk" (replace this with any Google result URL, e.g. "www.netflix.com", "amazon.co.uk", etc);
	* Current wild-card search is defaulted to "land+registry+searches" (the "+" will be catered-for, so space - " " - characters can equally be used);
	* Current result-set limit is defaulted to 100;

## Tips/Run

* (Angular 13) Load the project in an IDE, install the various packages - "npm install" - then run the project - "ng serve" - it'll load with [http://localhost:4200](localhost) but the back-end is catering for encapsulating the SPA through a Startup call, see below;
* (.NET 5.0) Load the project in an IDE, then run the project - press F5 - it'll load with a dedicated port and will encapsulate the above SPA project through a Startup call;

## Errors Experienced

* -;

## Notes:

* Abc;

## What's it all about?

* The solution is a (.NET 5.0) Web API and (Angular 13) SPA that is architected with a CQRS (Command and Query Responsibility Segregation) architecture, it makes use of Regex and internal .NET libraries - e.g. HttpClient - without the use of external/3rd-party libraries to scrape a website (Google UK) for keyword criteria;

## Thanks

-

* []() ...-...
