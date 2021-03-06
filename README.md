# CDList


#### _CD List web app, 2.23.17_

#### By _**Katy Daviscourt and Tammy Dang**_

## Description

_Use database, xunit testing and c# to make webpage listing restaurants in different cuisines_

## Setup/Installation Requirements

* Requires DNU, DNX, and Mono
* Clone to local machine
* Use command "dnu restore" in command prompt/shell
* Use command "dnx kestrel" to start server
* Navigate to http://localhost:5004 in web browser of choice

## Support and contact details

_For questions contact Katy at katy.daviscourt@gmail.com_

## Technologies Used

_C#, Nancy_



Specs:

* The get all method will return an empty list if the list of cuisines is empty in the beginning
    * input: nothing/null
    * output: empty string

* The equals method will return true if there are two cuisines that are the same
    * input: Thai, Thai
    * output: true

* The get all method will return the cuisine if the cuisine was saved in the database
    * input: American
    * output: American

* The save method will assign a new id to a new instance of the cuisine class
    * Input: Asian Cuisine, 0
    * Output: Asian Cuisine, non zero

* The get all method will return a list of all cuisines
    * input: Mexican, American, Thai, Indian
    * output: Mexican, American, Thai, Indian

* The find method will return the cuisine in the database.
    * input: Mexican
    * output: Mexican

* When the user updates the name of a cuisine, the update method will return the updated name
    * input: Thai Fusion replacing Thai
    * output: Thai Fusion

* When the user deletes a cuisine, the delete method will return an updated list without the deleted cuisine
    * input: DELETE Mexican
    * output: American, Thai Fusion, Indian

* The get all method will return an empty list if the list of restaurants is empty in the beginning
    * input: nothing/null
    * output: empty string

* The equals method will return true if there are two restaurants that are the same
    * input: Bill's, Bill's
    * output: true

* The save and get all methods will return true if the restaurant was saved in the database
    * input: Bob's
    * output: true

* The get all method will return true if the id for the first restaurant has an id of 1.
    input: Bob's
    output: 1

* The get all method will return a list of all restaurants
    * input: Bob's, Bill's, Sally's, Beth's
    * output: Bob's, Bill's, Sally's, Beth's

* When the user updates the name or other property of a restaurant, the update method will return the updated info
    * input: Bob's burgers replacing Bob's, $$ replacing $
    * output: Bob's burgers, $$

* When the user deletes a restaurant, the delete method will return an updated list without the deleted restaurant
    * input: DELETE Bill's
    * output: Bob's burgers, Sally's, Beth's

* The get all method can sort restaurants by cuisine
    * input: Mexican
    * output: Beth's, Aqua Verde

* The get all method can search restaurants by cuisine
    * input: Mexican
    * output: Beth's, Aqua Verde
    
    
### License

*Under MIT License*

Copyright (c) 2017 **_Katy Daviscourt and Tammy Dang_**
