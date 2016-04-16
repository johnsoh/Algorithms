using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.TC.SRM688
{
    class ParenthesesDiv2Hard
    {
        public static int minSwaps(string s, int[] L, int[] R)
        {
            var imbalanceList = new List<int[]>();
            var n = L.Length;
            var leftovers = s.ToCharArray();

            for (var i = 0; i < n; i++)
            {
                var substring = s.Substring(L[i], R[i] - L[i] + 1);
                if (substring.Length % 2 == 1) return -1; //odd strings are never balanced. 

                //var needed = findNeeded(substring);
                //if (needed[0] != needed[1]) return -1;
                var opensNeeded = 0;
                var opens = 0;
                foreach (var c in substring)
                {
                    if (c == ')' && opens == 0)
                    {
                        opensNeeded++;
                        opens++;
                    }
                    else if (c == ')')
                    {
                        opens--;
                    }
                    else if (c == '(')
                    {
                        opens++;
                    }
                }
                var needed = new int[] {  opensNeeded,
                                Math.Max(0, opens/2) // close brackets needed. neg means dont need
                             };


                imbalanceList.Add(needed);

                // calc leftovers
                for (var j = L[i]; j <= R[i]; j++)
                {
                    leftovers[j] = ' ';
                }
            }

            // sum both lists up. if equal, return open.sum else return -1
            //if (imbalanceList.Count % 2 != 0) return -1; // need pairing

            var neededOpens = imbalanceList.Select(x => x[0]).Sum();
            var neededCloses = imbalanceList.Select(x => x[1]).Sum();



            if (neededOpens != neededCloses)
            {
                // add left overs n check
                var changes = 0;
                // MISTAKE!! keep plus and minus changes seprately !!!  max, absDiff. return max
                foreach (var leftover in leftovers)
                {
                    if (leftover == '(')
                    {
                        neededOpens--; changes++;
                    }
                    if (leftover == ')') { neededCloses--; changes++; }
                }

                if (neededCloses == neededOpens) return changes;
                // otherwise really cannot 
                return -1;
            }
            return neededOpens;
        }

        public static int[] findNeeded(string substring)
        {
            var opensNeeded = 0;
            var opens = 0;
            foreach (var c in substring)
            {
                if (c == ')' && opens == 0)
                {
                    opensNeeded++;
                    opens++;
                }
                else if (c == ')')
                {
                    opens--;
                }
                else if (c == '(')
                {
                    opens++;
                }
            }
            return new int[] {  opensNeeded,
                                Math.Max(0, opens) // close brackets needed. neg means dont need
                             };
        }

    }
}
