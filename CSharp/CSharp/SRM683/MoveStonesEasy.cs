using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.SRM683
{
    class MoveStonesEasy
    {
        public static void Test()
        {
            var one = get(new int[] { 1, 2 }, new int[]{ 2, 1});
        }

        public static int get(int[] a, int[] b)
        {
            if (a.ToList().Sum() != b.ToList().Sum()) return -1;

            // prepare
            var N = a.Length;
            var diff = new int[N];
            for(var i = 0; i < N; i++)
            {
                diff[i] = a[i] - b[i];
            }
            a = diff;
            // execute
            var steps = 0;
            for(var i = 0; i < N; i++)
            {
                if (a[i] == 0) continue;
                var isAiPos = a[i] > 0;
                for (var j = i+1; j < N; j++)
                {
                    if (a[j] == 0) continue;
                    var isAjPos = a[j] > 0;
                    if (isAjPos != isAiPos)
                    {
                        var change = Math.Min(Math.Abs(a[i]), Math.Abs(a[j]));
                        a[i] += isAiPos ? -change : +change;
                        a[j] += isAjPos ? -change : +change;
                        steps += change * (j - i);
                    }
                }
                if (a[i] != 0) return -1;
            }
            return steps;
        }
    }
}
