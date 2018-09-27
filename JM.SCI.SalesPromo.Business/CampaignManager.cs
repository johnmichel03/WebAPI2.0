using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JM.SCI.SalesPromo.Model;
using JM.SCI.SalesPromo.Data.Core;

namespace JM.SCI.SalesPromo.Business
{
    public class CampaignManager : ICampaignManager
    {
        private readonly IRepository _repository;
        public CampaignManager(IRepository repository)
        {
            this._repository = repository;
        }

        public async Task<Campaign> CreateCampaign(Campaign campaign)
        {
            campaign.CreatedOn = DateTime.Now;
            _repository.Add<Campaign>(campaign);
            await _repository.SaveAsync();
            return campaign;
        }

        public async Task<Campaign> UpdateCampaign(int id, Campaign campaign)
        {
            var db = new { campaign=_repository.Query<Campaign>(q => q.CampaignId == id).Single() };
            db.campaign.UpdatedOn = DateTime.Now;
            db.campaign.CampaignName = campaign.CampaignName;
            db.campaign.MaxNoOfWinner = campaign.MaxNoOfWinner;
            db.campaign.PrimeCode = campaign.PrimeCode;
            db.campaign.WinType = campaign.WinType;
            db.campaign.StartDate = campaign.StartDate;
            db.campaign.EndDate = campaign.EndDate;
            await _repository.SaveAsync();
            return db.campaign;
        }

        public Campaign GetCampaign(int campaignId)
        {
            return _repository.Query<Campaign>(q => q.CampaignId == campaignId).Single();
        }
    } 
}
