
using JM.SCI.SalesPromo.Model;
using System.Threading.Tasks;

namespace JM.SCI.SalesPromo.Business
{
    public interface ICampaignManager
    {
        Task<Campaign> CreateCampaign(Campaign campaign);
        Campaign GetCampaign(int campaignId);
        Task<Campaign> UpdateCampaign(int id, Campaign campaign);
    }
}
