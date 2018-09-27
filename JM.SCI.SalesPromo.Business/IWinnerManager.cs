using JM.SCI.SalesPromo.Model;
using System.Collections.Generic;

namespace JM.SCI.SalesPromo.Business
{
    public interface IWinnerManager
    {
        bool IsWinningCode(string code, int campaignId);
        CampaignWinner CreateWinner(CampaignWinner campainWinner);
        //Task<Winner> UpdateWinner(int id, Winner winner);
        CampaignWinner GetWinner(int campaignId, int winnerId);
        IEnumerable<Winner> GetAllWinnerByCampaignId(int campaignId);
    }
}
