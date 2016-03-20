using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.SRM189
{
    class Mortgage
    {
        public static void test()
        {
            var one = monthlyPayment(1000, 50, 1);
        }

        public static int monthlyPayment(int loan, int interest, int term)
        {
            double pmt = loan;
            int max = loan * 2; int min = 0;

            // FFFFTTTTTT type. so min=mid+1. for TTTTTFFFF type instead do max=mid-1
            while(min < max)
            {
                int mid = (max + min) / 2;
                double afterPaying = simulate(mid, loan, interest, term);
                if (afterPaying <= 0) //if satisfies
                {
                    max = mid;
                }
                else
                {
                    min = mid+1;
                }
            }
            return min;
        }

        private static int continousBinarySearch()
        {
            double max = double.MaxValue;
            double currVal = 4;
            double prevValue = simulate(max);
            double threshold = 0.001;
            while(Math.Abs(prevValue - currVal) > threshold)
            {

            }
            return 0;
        }

        private static double simulate(double d) { return 0.01; }

        //10 = 0.10%  
        public static double simulate(double pmt, int loan, int interest, int term)
        {
            double monthlyInterest = (double)interest / 12000;
            int months = term * 12;
            double loanAmt = (double) loan;
            while(months-- > 0)
            {
                loanAmt -= pmt;
                loanAmt += Math.Ceiling(loanAmt * monthlyInterest);
            }
            Console.WriteLine(string.Format("{0}  {1}", pmt, loanAmt));
            return loanAmt;
        }
    }
}
