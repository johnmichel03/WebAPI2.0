Feature: CampaignFeature
	In order to avoid silly mistakes
	As a campaign api user
	I want to test all possible scenarios

Scenario Outline: Create New Campaign Using Api
	Given I have a Campaign <CampaignName>,<MaxNoOfWinner>,<PrimeCode>,<WinType>,<StartDate>,<EndDate>
	And I Save the Campaign
	When I Query the Campaign Repository
	Then the result should be an Campaign
	Examples:
	| CampaignName      | MaxNoOfWinner | PrimeCode | WinType | StartDate  | EndDate    |
	| Prize Contest     | 2             | 65353     | Prime   | 2018-07-01 | 2018-07-31 |
	| In Valid campaign | 100           | 65353     | Prime   | 2018-07-01 | 2018-07-10 |
	| No win No fee     | 5             | 123456    | Prime   | 2018-07-01 | 2018-07-31 |
	
Scenario Outline: Update a Campaign Using Api
	Given I have a Campaign <CampaignName>,<MaxNoOfWinner>,<PrimeCode>,<WinType>,<StartDate>,<EndDate>
	And I have a CampaignId <CampaignId> 
	And I Update the Campaign
	When I Query the Campaign Repository
	Then the result should be an Campaign
	Examples:
	| CampaignId | CampaignName      | MaxNoOfWinner | PrimeCode | WinType | StartDate  | EndDate    |
	| 2          | In Valid campaign | 100           | 65353     | Prime   | 2018-07-01 | 2018-07-11 |
	