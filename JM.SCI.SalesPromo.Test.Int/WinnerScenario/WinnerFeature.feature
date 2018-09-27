Feature: WinnerFeature
	In order to avoid silly mistakes
	As a winner api user
	I want to test all possible scenarios

Scenario Outline: Create New Winner Using Api
	Given I have a Coupon <CouponCode> and Campaign <CampaignId>
	And I Check the Coupon Code By Campaign
	When the result should be a Valid Coupon
	And I have a Winner <FirstName>,<LastName>,<AddressLine>,<PostalCode>,<CouponCode>,<CampaignId>
	And I Save the Winner
	And I Query the Winner Repository
	Then the result should be an Winner
	
	Examples:
	| FirstName | LastName | AddressLine              | PostalCode | CouponCode | CampaignId |
	| John      | Michel   | 101 Clements Road Ilford | IG11BE     | ca6a6      | 1          |
	| Jessy     | Andriya  | 55 Ilford Lane Ilford    | IG12AT     | 12f9f9     | 1          |

	Scenario Outline: Check Invalid Coupon Using Api
	Given I have a Coupon <CouponCode> and Campaign <CampaignId>
	When I Check the Coupon Code By Campaign
	Then the result should be a InValid Coupon
	Examples:
	| CouponCode | CampaignId |
	| abcdef     | 1          |
	| 654321     | 1          |

Scenario Outline: Check Valid Coupon When The Campaign Is Reached Maximum No Of Winners Using Api
	Given I have a Coupon <CouponCode> and Campaign <CampaignId>
	When I Check the Coupon Code By Campaign
	Then the result should be a InValid Coupon
	Examples:
	| CouponCode | CampaignId |
	| 1fa09f     | 1          |

	Scenario Outline: Check Valid Coupon When The Campaign Is Closed Using Api
	Given I have a Coupon <CouponCode> and Campaign <CampaignId>
	When I Check the Coupon Code By Campaign
	Then the result should be a InValid Coupon
	Examples:
	| CouponCode | CampaignId |
	| 12f9f9     | 2          |
