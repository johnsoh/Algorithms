using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.SRM683
{
    class EqualSubstrings2
    {
        public static void Test()
        {
            var one = get("aa");
            var zero = get("abcd");
            var three = get("abab"); // cannot go backwards!
            var seven = get("aaaab");
        }

        public static int get(string s)
        {
            var n = s.Length;
            var sum = 0;
            for(var subStart = 0; subStart < n; subStart++)
            {
                for(var len = 1; len < n - subStart; len++)
                {
                    var subTail = subStart + len;
                    var sub = s.Substring(subStart, len);
                    var shadow = subStart; // To prevent double counts!
                    for ( ; shadow <= n - len; shadow++) { 
                        var shadowTail = shadow + len;
                        // Checking for overlap : TRICKY!
                        if (shadowTail > subStart && shadow <= subStart) continue;
                        if (shadowTail >= subTail && shadow < subTail) continue;  
                        // having <= or >= for start+start or end+end is ok . we need at least one equals or sign 
                        // this is to cover the case when they are the same 
                        var shadowSub = s.Substring(shadow, len);
                        if (sub.Equals(shadowSub)) sum++;
                    }
                }
            }
            return sum;
        }
    }
}
