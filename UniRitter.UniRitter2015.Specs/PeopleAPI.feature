Feature: People API
	In order to know who places posts and comments on my blog
	As a blog owner
	I want to have an API that allows my apps to manage user information

@integrated
Scenario: Add two numbers
	Given a valid person resource
	When I post it to the /people API endpoint
	Then I receive a success (code 201) return message
	And I receive the posted resource
	And the posted resource now has an ID
