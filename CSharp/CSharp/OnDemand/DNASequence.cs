using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// done At 1hr.05mins  left
namespace CSharp.OnDemand
{
    class DNASequence
    {
        public static int longestDNASequence(String sequence)
        {
            var dna = new List<char>() { 'A', 'T', 'G', 'C' };
            var arr = sequence.ToCharArray();
            var max = 0;
            var sum = 0;
            foreach(char c in arr)
            {
                if (dna.Contains(c))
                {
                    sum++;
                }
                else
                {
                    max = Math.Max(max, sum);
                    sum = 0;
                }
            }
            max = Math.Max(max, sum); // if all are ATGC

            return max;
        }

        // top coder
        public static void coder()
        {
            var a = longestDNASequence("GOODLUCK");
            var b = longestDNASequence("TOPBOATER");

            Test t = new Test();
            Test.Init(1);
            while (Test.hasNext())
            {
                var res = longestDNASequence(Test.nextLine());
                Test.nextAnswer();
                var ans = Test.nextInt();
                Test.Check(ans, res);
            }
        }
    }
}
