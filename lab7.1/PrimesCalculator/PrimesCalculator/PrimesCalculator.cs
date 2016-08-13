using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimesCalculator
{
    class PrimesCalculator : IPrimesCalculator

    {
        public IEnumerable<int> CalcPrimes(int from,int to)
        {
            return Enumerable.Range(from, to - from+1).Where((num) => IsPrime(num));
        }

        private bool IsPrime(int num)
        {
            if (num <= 1)
            {
                return false;
            }
            if (num == 2)
            {
                return true;
            }
            var list=Enumerable.Range(2, (int)(Math.Sqrt(num))).Where((number) => num % number == 0).ToList();
            return list.Count == 0 ? true : false;
        }
        
    }
}
