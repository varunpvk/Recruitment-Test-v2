# JustGiving Test

####	Tech Stack
	* ASP.NET core 2.1
	* NUnit, NSubstitute
	* DI, Repository
	* DataLayer: SQLite

####	AuthZ
	* Authorization using JWT token
	
####	EndPoints
    * Donors
		* POST api/donors
		* GET api/token

  	* GiftAid
		* POST api/donor/add
		* GET api/giftaid

#### Run the solution
-------------------------------------------------------------------------------------------------------------------------------
##### Compile the solution
##### Run the solution in kestrel webserver / IISExpress
##### Run swagger for documentation - ex: https://localhost:44320/swagger
##### Steps
    	* Call api/donors endpoint to register the user.
       	* Capture the JWT from the response and authorize to get access to the remaining endpoints
       	* To calculate GiftAid call api/giftaid
       	* To Store UserDetails to DB call api/donor/add

![Swagger](../../../Users/varun.palavalasa.FARFETCH/Desktop/Swagger.png)
--------------------------------------------------------------------------------------------------------------------------------

