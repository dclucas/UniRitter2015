﻿Feature: People API
	In order to know who places posts and comments on my blog
	As a blog owner
	I want to have an API that allows my apps to manage user information

@integrated
Scenario: Add a person
	Given a valid person resource
	When I post it to the /people API endpoint
	Then I receive a success (code 200) return message
	And I receive the posted resource
	And the posted resource now has an ID
	And the person is added to the database

@integrated
Scenario: Invalid person data on insertion
	Given an invalid person resource
	When I post it to the /people API endpoint
	Then I receive an error (code 400) return message
	And I receive a message listing all validation errors
