Feature: Posts API
	In order to allow the easy management of posts in my blog
	As a blog owner
	I want to have an API that allows my apps to manage post information

Background: 
	Given an API populated with the following posts
	| id                                   | body        | title   | authorId                             | tags        |
	| 5e3e751a-490d-4c29-a93d-26dcfd2ce4a2 | Hello World | Hello   | 8d0d477f-1378-4fc1-bb47-29eb3ea959e1 | Hello,World |
	| cb4e2dae-b29d-4484-8001-9322912a6376 | Post 2      | #2      | 1a5fd0be-d654-40ff-8190-ca59e3b52e76 | Second,Post |
	| 4c134160-6575-4421-a7ab-1d75ca586774 | Yet another | Another | 8d0d477f-1378-4fc1-bb47-29eb3ea959e1 | Post        |
	| c2423529-b1bd-4dfb-8a0b-5541f04e2ce7 | Last post   | Last    | 58b024e9-57dc-49e4-8fc9-2d4d82bf1670 | Last,Post   |


	@integrated
	Scenario: Get all post entries
	Given the populated API
	When I GET from the /posts API endpoint
	Then I get a list containing the populated resources

	@integrated
	Scenario Outline: Get a specific post entry
	Given the populated API
	When I GET from the /posts/<id> API endpoint
	Then the data matches that id
	Examples:
	| id                                   |
	| 5e3e751a-490d-4c29-a93d-26dcfd2ce4a2 |
	| cb4e2dae-b29d-4484-8001-9322912a6376 |
	| 4c134160-6575-4421-a7ab-1d75ca586774 |
	| c2423529-b1bd-4dfb-8a0b-5541f04e2ce7 |

	@integrated
	Scenario: Add a post
	Given a post resource as described below:
	| body        | title   | authorId                             | tags     |
	| My new Post | New one	| 8d0d477f-1378-4fc1-bb47-29eb3ea959e1 | New,Post |
	When I post it to the /posts API endpoint
	Then I receive a success (code 200) return message
	And I receive the posted resource
	And the posted resource now has an ID
	And I can fetch it from the API

	@integrated
	Scenario Outline: Invalid post data on insertion
	Given a <case> resource
	When I post the following data to the /posts API endpoint: <data>
	Then I receive an error (code 400) return message
	And I receive a message that conforms <messageRegex>
	Examples:
	| case           | data | messageRegex |
	| missing body	 | {}   | .*body.*	   |
	| title too long | {}   | .*title.*    |
