Feature: ControlActions
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers
Background: Set Up scenario
Given I go to 'http://localhost:3000/'

@BVT
Scenario: Build simple burger
Given I click Burger Builder button
When I click More Salad button
When I click More Bacon button
When I click More Cheese button
When I click More Meat button
Then I should see '6.90' in Current Price

Scenario: Build2 simple burger
Given I click Burger Builder button
When I click More Salad button
When I click More Bacon button
When I click More Cheese button
When I click More Meat button
When I clickjask Order now button
