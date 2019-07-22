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


### Description
* I've create the project using a sample template
* The database used is SQlite (To avoid the need to get a running SQLServer instance installed)
* I've created a set of sample data to test the API
* The API currently is versioned (The current version is v1) using:
	- Microsoft.AspNetCore.Mvc.Versioning 3.1.4
	- Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer 3.2.0
* I'm using AutoMapper 8.1.1 to map EF Entities to Classes
* I'm using SwashBuckle.AspNetCore 4.0.1 for the Swagger UI interface

### Test the project
* clone this repository: git clone git@github.com:rbr8791/applevalapi.git
* After the clone repository is completed, change to the folder where you clone the repository run the following commands using the windows command prompt (Make sure you're using the Netcore 2.2 Framework)
```console
cd applevalapi.Persistence
```

```console
createInitialMigration.cmd
```

* The above commands will create the initial migration and update
* Compile all the projects
* Seed the database with the initial test data
* Run the API service

### Test the API using Swagger:
* Open an internet Browser and point the URl bar to http://localhost:63010/swagger

### Users to test the API
* admin user -> 
  user name: admin
  password : admin
* operator user ->
  user name: operator
  password : operator
* customer user ->
  user name: customer
  password : customer

* The Above block covers the following evaluation points:
### Security requirements
* Add login functionality so: 
* Only admins can Add/remove products.
* Only admins can modify price of the products.
* Only logged in users can buy a product.
* Any logged in users could *like* a product.
* Everyone (logged in or not logged in) can get a list of all the products.
* Everyone (logged in or not logged in) can use the search feature.


### Login to the API using swagger:
* You can login using the following curl command
API URL: http://localhost:63010/api/v1/User/authenticate
```console
curl -X POST "http://localhost:63010/api/v1/User/authenticate" -H "accept: application/json" -H "Content-Type: application/json-patch+json" -d "{ \"username\":\"admin\", \"password\":\"admin\"}"
```
You will get a Bearer token that you can use to test the API (The basic API security is using JWT Token Authentication)






