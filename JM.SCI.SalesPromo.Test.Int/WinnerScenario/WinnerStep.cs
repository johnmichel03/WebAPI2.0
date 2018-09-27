using JM.SCI.SalesPromo.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace JM.SCI.SalesPromo.Test.Int.WinnerScenario
{
    public class WinnerStep : TestBase
    {
        private const string _uri = "api/winners";
        [BeforeFeature]
        public static void BeforeFeature()
        {
            var coupons = "ca6a6,12f9f9";
            var codes = coupons.Split(',');
            foreach (var code in codes)
            {
                var winner = SalesPromoRepository.Query<CampaignWinner>(w => w.CouponCode == code, w => w.Winner).FirstOrDefault();
                if (winner != null)
                    SalesPromoRepository.Delete(winner.Winner);
            }
            SalesPromoRepository.Save();
        }


        [Given(@"I have a Coupon (.*) and Campaign (.*)")]
        public void GivenIHaveACoupon(string couponCode, int campaignId)
        {
            ScenarioContext.Current["CouponCode"] = couponCode;
            ScenarioContext.Current["CampaignId"] = campaignId;
        }

        [Given(@"I Check the Coupon Code By Campaign")]
        public void GivenICheckTheCouponCodeByCampaign()
        {
            var campaignId = ScenarioContext.Current["CampaignId"] as int? ?? 0;
            var couponCode = ScenarioContext.Current["CouponCode"] as string;
            var response = SalesPromoServer.HttpClient.GetAsync($"{_uri}/IsWinningCode/{campaignId}/{couponCode}").Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var isWinner = JsonConvert.DeserializeObject<bool>(json);
            ScenarioContext.Current["DbIsWinner"] = isWinner;
        }

        [When(@"the result should be a Valid Coupon")]
        public void WhenTheResultShouldBeAValidCoupon()
        {
            ScenarioContext.Current["IsWinner"] = true;
        }
        [When(@"I Check the Coupon Code By Campaign")]
        public void WhenICheckTheCouponCodeByCampaign()
        {
            GivenICheckTheCouponCodeByCampaign();
        }

        [Then(@"the result should be a InValid Coupon")]
        public void ThenTheResultShouldBeAInValidCoupon()
        {
            var expected = ScenarioContext.Current["DbIsWinner"] as bool? ?? null;
            Assert.AreEqual(expected, false, "Expected FALSE value..!");
        }

        [When(@"I have a Winner (.*),(.*),(.*),(.*),(.*),(.*)")]
        public void WhenIHaveAWinner(string firstName, string lastName, string addressLine, string postCode, string couponCode, int campaignId)
        {
            var winnerCampaign = new CampaignWinner()
            {
                CampaignId = campaignId,
                CouponCode = couponCode,
                Winner = new Winner()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    AddressLine = addressLine,
                    PostalCode = postCode
                }
            };
            ScenarioContext.Current["Winner"] = winnerCampaign;
        }

        [When(@"I Save the Winner")]
        public void WhenISaveTheWinner()
        {
            var campWinner = ScenarioContext.Current["Winner"] as CampaignWinner;
            var response = SalesPromoServer.HttpClient.PostAsJsonAsync(_uri, campWinner).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var winnerId = JsonConvert.DeserializeObject<int>(json);
            ScenarioContext.Current["WinnerId"] = winnerId;
        }

        [When(@"I Query the Winner Repository")]
        public void WhenIQueryTheWinnerRepository()
        {
            var winnerId = ScenarioContext.Current["WinnerId"] as int? ?? 0;
            var campaignId = ScenarioContext.Current["CampaignId"] as int? ?? 0;
            var response = SalesPromoServer.HttpClient.GetAsync($"api/Campaigns/{campaignId}/winners/{winnerId}").Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var winner = JsonConvert.DeserializeObject<CampaignWinner>(json);
            ScenarioContext.Current["DbWinner"] = winner;
        }

        [Then(@"the result should be an Winner")]
        public void ThenTheResultShouldBeAnWinner()
        {
            var winner = ScenarioContext.Current["Winner"] as CampaignWinner;
            var dbWinner = ScenarioContext.Current["DbWinner"] as CampaignWinner;

            Assert.AreEqual(winner.Winner.FirstName, dbWinner.Winner.FirstName, "Mismatch in Winner  First Name");
            Assert.AreEqual(winner.Winner.LastName, dbWinner.Winner.LastName, "Mismatch in Winner  Last Name");
            Assert.AreEqual(winner.Winner.AddressLine, dbWinner.Winner.AddressLine, "Mismatch in Winner  Address Line");
            Assert.AreEqual(winner.Winner.PostalCode, dbWinner.Winner.PostalCode, "Mismatch in Winner  Postal Code");
            Assert.AreEqual(winner.CampaignId, dbWinner.CampaignId, "Mismatch in Winner Campaign  CampaignId");
            Assert.AreEqual(winner.CouponCode, dbWinner.CouponCode, "Mismatch in Winner Campaign  CouponCode");
        }
    }
}
