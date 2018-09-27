using JM.SCI.SalesPromo.Business.Logic;
using JM.SCI.SalesPromo.Data.Core;
using JM.SCI.SalesPromo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JM.SCI.SalesPromo.Business
{
    public class WinnerManager : IWinnerManager
    {
        private readonly IRepository _repository;
        public readonly IWinFactory _winFactory;

        public WinnerManager(IRepository repository, IWinFactory winFactory)
        {
            _repository = repository;
            _winFactory = winFactory;
        }

        public bool IsWinningCode(string code, int campaignId)
        {
            /*ToDo: Use mem cache / temp table to avoid more no of winners than the max number of winner.
            There could be a little chance for exceeding the man no of winners when there is more no of concurrent users.
            Since coupon checking and winner details are separate task and hence there will be a little space in the system to have more number of winners than the specified. 
            we can apply online booking concept to avoid this issue but which is not implemented in this code.
            */
            var campaign = _repository.Query<Campaign>(c => c.CampaignId == campaignId).Single();
            if (campaign.EndDate != null && DateTime.Now.CompareTo(campaign.EndDate) > 0)
                return false;
            var isWon = _winFactory.GetWinLogic(campaign.WinType)
                                    .IsWon(code, campaign.PrimeCode);
            if (isWon)
            {
                var totalWinners = _repository.Query<CampaignWinner>(w => w.CampaignId == campaignId).Count();
                var isCodeExists = _repository.Query<CampaignWinner>(w => w.CouponCode.ToUpper() == code.ToUpper()).Any();
                if (totalWinners < campaign.MaxNoOfWinner && !isCodeExists)
                    return true;
            }
            return false;
        }

        public CampaignWinner CreateWinner(CampaignWinner campainWinner)
        {
            if (!IsWinningCode(campainWinner.CouponCode, campainWinner.CampaignId))
                return campainWinner;
            campainWinner.Winner.CreatedOn = DateTime.Now;
            campainWinner.CreatedOn = DateTime.Now;
            _repository.Add<CampaignWinner>(campainWinner);
            _repository.Save();
            return campainWinner;
        }

        public CampaignWinner GetWinner(int campaignId, int winnerId)
        {
            return _repository.Query<CampaignWinner>(q => q.CampaignId == campaignId && q.WinnerId == winnerId,
                                                             w => w.Winner).Single();
        }

        public IEnumerable<Winner> GetAllWinnerByCampaignId(int campaignId)
        {
            return _repository.Query<CampaignWinner>(w => w.CampaignId == campaignId, ccc => ccc.Winner)
                              .Select(s => s.Winner)
                              .ToList();
        }

        //public async Task<Winner> UpdateWinner(int id, Winner winner)
        //{
        //    var db = new { winner = _repository.Query<Winner>(q => q.WinnerId == id).Single() };
        //    db.winner.UpdatedOn = DateTime.Now;
        //    db.winner.FirstName = winner.FirstName;
        //    db.winner.LastName = winner.LastName;
        //    db.winner.AddressLine = winner.AddressLine;
        //    db.winner.PostalCode = winner.PostalCode;
        //    await _repository.SaveAsync();
        //    return db.winner;
        //}

    }
}
