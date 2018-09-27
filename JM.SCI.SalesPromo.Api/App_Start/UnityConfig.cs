using JM.SCI.SalesPromo.Business;
using JM.SCI.SalesPromo.Business.Logic;
using JM.SCI.SalesPromo.Data.Core;
using JM.SCI.SalesPromo.Data.Repository;
using Unity;

namespace JM.SCI.SalesPromo.Api
{
    public static class UnityConfig
    {
        public static UnityContainer GetContainer()
        {
           
            var container = new UnityContainer();
            container.RegisterType<IRepository, Repository>();
            container.RegisterType<ICampaignManager, CampaignManager>();
            container.RegisterType<IWinnerManager, WinnerManager>();
            container.RegisterType<IWinFactory, WinFactory>();
          
            return container;
        }


    }
}