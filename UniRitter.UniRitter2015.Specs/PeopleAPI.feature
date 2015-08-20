Feature: People API
	In order to know who places posts and comments on my blog
	As a blog owner
	I want to have an API that allows my apps to manage user information

Background: 
	Given an API populated with the following people
	| id									| firstName | lastName	| email				| url					|
	| 8d0d477f-1378-4fc1-bb47-29eb3ea959e1	| John		| Doe		| john@email.com	| http://john.doe.com	|
	| 58b024e9-57dc-49e4-8fc9-2d4d82bf1670	| Jane		| Doe		| jane@email.com	| http://jane.doe.com	|
	| 1a5fd0be-d654-40ff-8190-ca59e3b52e76	| Jack		| Doe		| jack@email.com	| http://jack.doe.com	|


	@integrated
	Scenario: Get all people entries
	Given the populated API
	When I GET from the /people API endpoint
	Then I get a list containing the populated resources

	@integrated
	Scenario Outline: Get a specific person entry
	Given the populated API
	When I GET from the /people/<id> API endpoint
	Then I receive a success (code 200) return message
	And the data matches that id
	Examples:
	| id									|
	| 8d0d477f-1378-4fc1-bb47-29eb3ea959e1	|
	| 58b024e9-57dc-49e4-8fc9-2d4d82bf1670	|
	| 1a5fd0be-d654-40ff-8190-ca59e3b52e76	|	

	@integrated
	Scenario: Add a person
	Given a person resource as described below:
	| firstName | lastName	| email				| url					|
	| Josh		| Doe		| josh@email.com	| http://josh.doe.com	|
	When I post it to the /people API endpoint
	Then I receive a success (code 200) return message
	And I receive the posted resource
	And the posted resource now has an ID
	And I can fetch it from the API

	@integrated
	Scenario Outline: Invalid person data on insertion
	Given a <case> resource
	When I post the following data to the /people API endpoint: <data>
	Then I receive an error (code 400) return message
	And I receive a message that conforms <messageRegex>
	Examples:
	| case              | data																						| messageRegex	|
	| missing firstName	| {"LastName":"de Tal","Email":"fulano@email.com","Url":"http://fulano.com.br"}				| .*firstName.*	|
	| invalid email		| {"LastName":"de Tal","FirstName":"fulano", "Email":"fulano","Url":"http://fulano.com.br"} | .*email.*		|
