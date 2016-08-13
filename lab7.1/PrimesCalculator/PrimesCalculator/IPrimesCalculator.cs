using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrimesCalculator
{
    interface IPrimesCalculator
    {
        IEnumerable<int> CalcPrimes(int from, int to);
    }
}