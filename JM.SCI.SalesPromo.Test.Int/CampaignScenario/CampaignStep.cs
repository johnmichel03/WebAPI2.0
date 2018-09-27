using JM.SCI.SalesPromo.Model;
using JM.SCI.SalesPromo.Model.Enum;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using System;
using System.Net.Http.Formatting;
using System.Net.Http;

namespace JM.SCI.SalesPromo.Test.Int.CampaignScenario
{
    [Binding]
    public sealed class CampaignStep : TestBase
    {
        private const string _uri = "api/campaigns";

        [Given(@"I have a Campaign (.*),(.*),(.*),(.*),(.*),(.*)")]
        public void GivenIHaveACampaign(string campaignName, int maxNoOfWinner, string primeCode, WinType winType, DateTime startDate, DateTime endDate)
        {
            var campaign = new Campaign()
            {
                CampaignName = campaignName,
                MaxNoOfWinner = maxNoOfWinner,
                PrimeCode = primeCode,
                WinType = winType,
                StartDate = startDate,
                EndDate = endDate
            };
            ScenarioContext.Current["Campaign"] = campaign;
        }

        [Given(@"I Save the Campaign")]
        public void GivenISaveTheCampaign()
        {
            var campaign = ScenarioContext.Current["Campaign"] as Campaign;
            var response = SalesPromoServer.HttpClient.PostAsJsonAsync<Campaign>(_uri, campaign).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            campaign = JsonConvert.DeserializeObject<Campaign>(json);
            ScenarioContext.Current["CampaignId"] = campaign.CampaignId;

        }

        [When(@"I Query the Campaign Repository")]
        public void WhenIQueryTheCampaignRepository()
        {
            var campaignId = ScenarioContext.Current["CampaignId"] as int? ?? 0;
            var response = SalesPromoServer.HttpClient.GetAsync($"{_uri}/{campaignId}").Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var campaign = JsonConvert.DeserializeObject<Campaign>(json);
            ScenarioContext.Current["DbCampaign"] = campaign;
        }

        [Then(@"the result should be an Campaign")]
        public void ThenTheResultShouldBeAnCampaign()
        {
            var campaign = ScenarioContext.Current["Campaign"] as Campaign;
            var dbCampaign = ScenarioContext.Current["DbCampaign"] as Campaign;

            Assert.AreEqual(campaign.CampaignName, dbCampaign.CampaignName, "Mismatch in Campaign Name");
            Assert.AreEqual(campaign.MaxNoOfWinner, dbCampaign.MaxNoOfWinner, "Mismatch in Campaign Max No Of Winner");
            Assert.AreEqual(campaign.PrimeCode, dbCampaign.PrimeCode, "Mismatch in Campaign Prime Code");
            Assert.AreEqual(campaign.WinType, dbCampaign.WinType, "Mismatch in Campaign Win Type");
            Assert.AreEqual(campaign.StartDate, dbCampaign.StartDate, "Mismatch in Campaign Start Date");
            Assert.AreEqual(campaign.EndDate, dbCampaign.EndDate, "Mismatch in Campaign End Date");
        }

        [Given(@"I have a CampaignId (.*)")]
        public void GivenIHaveACampaignId(int campaignId)
        {
            ScenarioContext.Current["CampaignId"] = campaignId;
        }

        [Given(@"I Update the Campaign")]
        public void GivenIUpdateTheCampaign()
        {
            var campaign = ScenarioContext.Current["Campaign"] as Campaign;
            var campaignId = ScenarioContext.Current["CampaignId"] as int? ?? 0;

            var response = SalesPromoServer.HttpClient.PostAsJsonAsync($"{_uri}/{campaignId}", campaign).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            campaign = JsonConvert.DeserializeObject<Campaign>(json);
            ScenarioContext.Current["DbCampaign"] = campaign;
        }

    }
}
