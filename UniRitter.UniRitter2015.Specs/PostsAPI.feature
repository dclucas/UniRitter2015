Feature: Posts API
	In order to allow the easy management of posts in my blog
	As a blog owner
	I want to have an API that allows my apps to manage post information

Background: 
	Given an API populated with the following Post
	| id                                   | body        | title   | authorId                             | tags        |
	| 5e3e751a-490d-4c29-a93d-26dcfd2ce4a2 | Hello World | Hello   | 8d0d477f-1378-4fc1-bb47-29eb3ea959e1 | Hello,World |
	| cb4e2dae-b29d-4484-8001-9322912a6376 | Post 2      | a2      | 1a5fd0be-d654-40ff-8190-ca59e3b52e76 | Second,Post |
	| 4c134160-6575-4421-a7ab-1d75ca586774 | Yet another | Another | 8d0d477f-1378-4fc1-bb47-29eb3ea959e1 | Post        |
	| c2423529-b1bd-4dfb-8a0b-5541f04e2ce7 | Last post   | Last    | 58b024e9-57dc-49e4-8fc9-2d4d82bf1670 | Last,Post   |

	@integrated
	Scenario: Get all entries Post
	Given the populated API Post
	When I get from the /Posts API endpoint Post
	Then I get a list containing the populated resources Post

	@integrated
	Scenario Outline: Get a specific entry
	Given the populated API Post
	When I get from the /Posts/<id> API endpoint Post
	Then the data matches that id Post
	Examples:
	| id                                   |
	| 5e3e751a-490d-4c29-a93d-26dcfd2ce4a2 |
	| cb4e2dae-b29d-4484-8001-9322912a6376 |
	| 4c134160-6575-4421-a7ab-1d75ca586774 |
	| c2423529-b1bd-4dfb-8a0b-5541f04e2ce7 |
		
	@integrated
	Scenario: Add a Post
	Given a resource as described below Post:
	| body        | title   | authorId                             | tags		|
	| My new Post | New one	| 8d0d477f-1378-4fc1-bb47-29eb3ea959e1 | New,Post	|
	When I post it to the /Posts API endpoint
	Then I receive a success (code 200) return Post message
	And I receive the Post resource
	And the posted resource now has an Post ID
	And I can fetch /Post from the APIPost

	@integrated
	Scenario Outline: Invalid post data on insertion
	Given a <case> resource Post
	When I post the following data to the /Posts API endpoint: <data>
	Then I receive an error (code 400) return message Post
	And I receive a Post message that conforms <messageRegex>
	Examples:
	| case           | data																																																		| messageRegex	|
	| missing body	 | {"title":"tttt","autthorId":"8d0d477f-1378-4fc1-bb47-29eb3ea959e1","tags":["Body"]}																														| .*body.*		|
	| title too long | {"body":"bbbbbb","title":"tttt123456789.123456789.123456789.123456789.123456789.123456789.123456789.123456789.123456789.123456789.","autthorId":"4c134160-6575-4421-a7ab-1d75ca586774","tags":["Title"]}	| .*title.*		|
