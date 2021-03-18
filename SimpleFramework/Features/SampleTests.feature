Feature: SampleTests
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers


Scenario: GET AUTH
	When I get authentication token


Scenario: GET all BOOKING ID
	When I get all booking ID

Scenario: GET BOOKING by ID
	When I get a booking by ID

Scenario: POST BOOKING
	When I post booking

@Token
Scenario: UPDATE BOOKING
	When I post booking
	Then I update the booking