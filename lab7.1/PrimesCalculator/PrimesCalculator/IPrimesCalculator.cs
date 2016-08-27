using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrimesCalculator
{
    //Nice
    interface IPrimesCalculator
    {
        IEnumerable<int> CalcPrimes(int from, int to);
    }
}