using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.TC.SRM688
{
    class ParenthesesDiv2Medium
    {
        public static int[] correct(string s)
        {
            var n = s.Length;
            var openCount = 0;
            var openPositions = new List<int>();
            var solution = new List<int>();
            for (var i = 0; i < n; i++)
            {
                char c = s[i];
                if (c == '(')
                {
                    openCount++;
                    openPositions.Add(i);
                }
                else if (openCount == 0)
                {
                    // close symbol without open 
                    solution.Add(i); // change 
                    openCount++;
                    openPositions.Add(i);
                }
                else
                {
                    openCount--;
                }
            }

            if (openCount > 0)
            {
                while (openCount > 0)
                {
                    var lastIndex = openPositions.Count - 1;
                    solution.Add(openPositions[lastIndex]);
                    openPositions.RemoveAt(lastIndex);
                    openCount--;
                }
            }

            return solution.ToArray();
        }

    }
}
