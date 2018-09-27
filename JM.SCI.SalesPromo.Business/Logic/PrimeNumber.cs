using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace JM.SCI.SalesPromo.Business.Logic
{
    // we can use Azure function to do the calculation quickly for more number of concurrent users and for better perfromance.
    internal class PrimeNumber : IWinLogic
    {
        private bool IsPrimeNumber(double number)
        {
            if (number < 2) return false;
            if (number % 2 == 0) return (number == 2);
            int root = (int)Math.Sqrt((double)number);
            for (int i = 3; i <= root; i += 2)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

        public bool IsWon(string code, string CodeComparer)
        {
            try
            {
        //TODO : find another way to handle the fraction value.Right now the values are round off to nearest  number.It could be wrong when we decide the prime number logic
                double winCode = long.Parse(code, System.Globalization.NumberStyles.HexNumber) /
                long.Parse(CodeComparer, System.Globalization.NumberStyles.HexNumber);
                return IsPrimeNumber(winCode);
            }
            catch (Exception ex)
            {
                //log exception  with details
                return false;
            }
            
        }
    }
}

