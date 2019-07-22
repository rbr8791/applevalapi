# applevalAPI .NET Core Technical Test - Applaudo Studios

This code tries to implement the following requirements for the Technical Evaluation test.

You must have [.NET Core 2.2](https://dotnet.microsoft.com/download/dotnet-core/2.2)

## Requirements
The API should allow:
* Adding/Removing products and set their stock quantity.
* Modify the price of the products
* Save a log of the price updates for a product.
* Buy a product
* Buying a product should reduce its stock.
* Keep a log of all the purchases (who bought it, how many, when).
* Liking a product
* Obtain a list of all the available products.
* The list should be sortable by name (default), and by popularity (likes) 
* The list should have pagination functionality
* Search through the products by name.
## Security requirements
* Add login functionality so: 
* Only admins can Add/remove products.
* Only admins can modify price of the products.
* Only logged in users can buy a product.
* Any logged in users could *like* a product.
* Everyone (logged in or not logged in) can get a list of all the products.
* Everyone (logged in or not logged in) can use the search feature.
### Extra credit
* Build a small front-end application that:
      * Connects to the API to show a list of the products
      * The app should allow searching products by name 
### Keep in mind
*  You are free to use any package, framework, library and weapons for the battle as long as you can justify their use.
* You may use any kind of database you like but you need to use Code-First approach.
* Provide a database dump so we can replicate the database locally.
* POSTMAN will be used to evaluate the API. It would be great if you can provide us with a collection to test your API
* Follow best Rest API practices.
* Use git and do small commits to facilitate the evaluation of progress.
* Include a *readme.md* file with instructions on how to setup your project locally to test it. (This is super important, if we cannot install it and run it easily we cannot evaluate it)
* Upload your solution to your GitHub account and share a link with your evaluator
* The test has been designed with enough time to do a good job, so don’t cut any corners, take your time and watch for quality


### How to test the project
## Description
* I create the project using a sample template
* The database used is SQlite (To avoid the need to get a running SQLServer instance installed)
* I've created a set of sample data to test the API
* The API currently is versioned (The current version is v1)
*