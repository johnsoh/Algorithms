using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.SRM685
{
    class MultiplicationTable2Easy
    {
        static string good = "Good";
        static string bad = "Not Good";

        public static string isGoodSet(int[] table, int[] t)
        {
            var set = new HashSet<int>();
            foreach (var v in t) set.Add(v);
            
            int N = table.Length;
            int n = (int) Math.Pow(table.Length, 0.5);
            for(var i = 0; i < t.Length; i++)
            {
                for(var j = 0; j < t.Length; j++)
                {
                    var res = table[t[i] * n + t[j]];
                    if(!set.Contains(res))
                    {
                        return bad;
                    }
                }
            }

            foreach(var v in t)
            {
                if (!set.Contains(v))
                {
                    return bad;
                }
            }

            return good;
        }
    }
}
