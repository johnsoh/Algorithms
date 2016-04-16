using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.TC.TCO16_1B
{
    class ExploringNumbers
    {
        public static void test()
        {
            var a1 = numberOfSteps(5);
            var a2 = numberOfSteps(57);
            var a3 = numberOfSteps(1);
        }

        public static int numberOfSteps(int n)
        {
            var num = n;
            for (var i = 1; i <= n; i++)
            {
                if (isPrime(num))
                {
                    return i;
                }
                else
                {
                    num = calcNew(num);
                }
            }
            return -1;
        }

        private static int calcNew(int num)
        {
            var sum = 0;
            while (num > 0)
            {
                sum += (int)Math.Pow(num % 10, 2);
                num = num / 10;
            }
            return sum;
        }



        public static bool isPrime(int number)
        {
            int boundary = (int)Math.Floor(Math.Sqrt(number));

            if (number == 1) return false;
            if (number == 2) return true;

            for (int i = 2; i <= boundary; ++i)
            {
                if (number % i == 0) return false;
            }

            return true;
        }


    }
}
