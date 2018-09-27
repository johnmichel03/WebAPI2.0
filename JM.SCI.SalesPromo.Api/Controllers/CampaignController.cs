using JM.SCI.SalesPromo.Business;
using JM.SCI.SalesPromo.Model;
using System;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace JM.SCI.SalesPromo.Api.Controllers
{
    [Route("api/Campaigns")]
    public class CampaignController : ApiController
    {
        private readonly ICampaignManager _campaignManager;
        private readonly IWinnerManager _winnerManager;
        public CampaignController(ICampaignManager campaignManager, IWinnerManager winnerManager)
        {
            _campaignManager = campaignManager;
            _winnerManager = winnerManager;
        }

        [HttpGet]
        [Route("api/Campaigns/{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            var campaign = _campaignManager.GetCampaign(id);
            return Request.CreateResponse(HttpStatusCode.OK, campaign);
        }

        [HttpGet]
        [Route("api/Campaigns/{campaignId}/Winners")]
        public HttpResponseMessage GetByCampaign(int campaignId)
        {
            var winner = _winnerManager.GetAllWinnerByCampaignId(campaignId);
            return Request.CreateResponse(HttpStatusCode.OK, winner);
        }

        [HttpGet]
        [Route("api/Campaigns/{campaign:int}/Winners/{winnerId:int}")]
        public HttpResponseMessage Get(int campaign, int winnerId)
        {
            var winner = _winnerManager.GetWinner(campaign, winnerId);
            return Request.CreateResponse(HttpStatusCode.OK, winner);
        }

        [HttpPost]
        [Route("api/Campaigns")]
        public async Task<HttpResponseMessage> Post([FromBody]Campaign campaign)
        {
            try
            {
                var db = new
                {
                    campaign = await _campaignManager.CreateCampaign(campaign)
                };
                return Request.CreateResponse(HttpStatusCode.OK, db.campaign);
            }
            catch (Exception ex)
            {
                // log exception 
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("api/Campaigns/{id:int}")]
        public async Task<HttpResponseMessage> Post(int id, [FromBody]Campaign campaign)
        {
            var db = new
            {
                campaign = await _campaignManager.UpdateCampaign(id, campaign)
            };
            return Request.CreateResponse(HttpStatusCode.OK, db.campaign);
        }
    }
}
