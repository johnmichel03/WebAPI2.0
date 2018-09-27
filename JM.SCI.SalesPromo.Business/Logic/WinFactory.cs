using JM.SCI.SalesPromo.Model.Enum;
using System;

namespace JM.SCI.SalesPromo.Business.Logic
{
    public class WinFactory : IWinFactory
    {
        public IWinLogic GetWinLogic(WinType winType)
        {
            switch (winType)
            {
                case WinType.Prime:
                    return new PrimeNumber();
                case WinType.Random:
                    throw new NotImplementedException();
                case WinType.Custom:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
