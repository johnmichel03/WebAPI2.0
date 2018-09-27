using JM.SCI.SalesPromo.Business;
using JM.SCI.SalesPromo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace JM.SCI.SalesPromo.Api.Controllers
{
    [Route("api/Winners")]
    public class WinnerController : ApiController
    {
        private readonly IWinnerManager _winnerManager;
        public WinnerController(IWinnerManager winnerManager)
        {
            _winnerManager = winnerManager;
        }

        [HttpGet]
        [Route("api/Winners/{id:int}")]
        public HttpResponseMessage Get(int winnerId)
        {
            return Request.CreateResponse(HttpStatusCode.NotImplemented);
        }

        [HttpPost]
        [Route("api/Winners")]
        public HttpResponseMessage Post([FromBody]CampaignWinner winner)
        {
            var db = new
            {
                winner = _winnerManager.CreateWinner(winner)
            };
            return Request.CreateResponse(HttpStatusCode.OK, db.winner.Winner.WinnerId);

        }

        [HttpGet]
        [Route("api/Winners/IsWinningCode/{id:int}/{code}")]
        public async Task<HttpResponseMessage> Get(int id, string code)
        {
            bool winner = await Task<bool>.Run(() => { return _winnerManager.IsWinningCode(code, id); });
            return Request.CreateResponse(HttpStatusCode.OK, winner);
        }

        //[HttpPost]
        //[Route("api/Winners/{id:int}")]
        //public async Task<HttpResponseMessage> Post(int id, [FromBody]Winner winner)
        //{
        //    var db = new
        //    {
        //        winner = await _winnerManager.UpdateWinner(id, winner)
        //    };
        //    return Request.CreateResponse(HttpStatusCode.OK, db.winner);
        //}

    }
}
