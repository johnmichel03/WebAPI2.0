
using JM.SCI.SalesPromo.Model.Enum;

namespace JM.SCI.SalesPromo.Business.Logic
{
    public interface IWinFactory
    {
        IWinLogic GetWinLogic(WinType wintype);
    }
}
