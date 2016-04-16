using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.TC.SRM688
{
    class ParenthesesDiv2Easy
    {
        public static int getDepth(string s)
        {
            var n = s.Length;
            var max = 0;
            var open = 0;
            foreach (char c in s)
            {
                if (c == '(') open++;
                if (c == ')') open--;
                max = Math.Max(max, open);
            }
            return max;
        }
    }
}
