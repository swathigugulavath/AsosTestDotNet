Feature: StudentDiscountTests
	Tests to verify student discount functionality

Scenario Outline: Verify the student discount completion with valid user details
	Given I have asos application running
	When I enter customer '<FirstName>', '<LastNmae>', '<Country>', '<RegesterdEmailAddress>', '<StudentEmailAddress>', '<YearOfGraduation>' ,'<InterestedCollection>' details
	And check the terms and conditions before submitting the details
	And I click on submit button
	Then user should be see successfull message for student discount enrolement
	Examples: 
	| FirstName | LastNmae | Country        | RegesterdEmailAddress | StudentEmailAddress      | YearOfGraduation | InterestedCollection |
	| Tester    | One      | United Kingdom | TesterOne@test.com    | StudentTestrOne@test.com | 2021             | Womenswear           |
	| Tester    | Two      | France         | TesterTwo@test.com    | StudentTestrTwo@test.com | 2022             | Womenswear           |

Scenario: Verify error messages for all fields when no data is enetered
	Given I have asos application running
	When I click on submit button
	Then user should see error messages for empty fields

Scenario: Verify that firstname and lastname fields allow hundred characters only
	Given I have asos application running
	When I enter more than hundred characters in firstname and last name fields
	Then user should see only first hundred characters allowed in first name and last name fields




