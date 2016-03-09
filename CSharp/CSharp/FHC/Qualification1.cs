using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CSharp.FHC
{
    class Qualification1
    {
        public static void Parse()
        {
            var file = Properties.Resources.TextFile1.Split(new string[] { System.Environment.NewLine, "\n" }, StringSplitOptions.None);
            var ptr = 0;
            var totalCases = int.Parse(file[ptr++]);
            for (var caseNum = 1; caseNum <= totalCases; caseNum++)
            {
                var pairsCount = int.Parse(file[ptr++]);
                int[][] pairs = new int[pairsCount][];
                for(var pairsPos = 0; pairsPos < pairsCount; pairsPos++)
                {
                    pairs[pairsPos] = file[ptr++].Split(' ').Select(int.Parse).ToArray();
                }
                // var intArray = file[ptr++].Split(' ').Select(int.Parse).ToArray();
                var res = Solve(pairs);
                Debug.WriteLine("Case #{0}: {1}", caseNum, res);
            }
        }

        private static string Solve(int[][] pairs)
        {
            var N = pairs.Length;
            var linesCollection = new Dictionary<int, List<int[]>>();
            for(var i = 0; i < N; i++)
            {
                for (var j = i + 1; j < N; j++)
                {
                    // len^2 = (y_2 - y_1)^2 + (x_2 - x_1)^2
                    int lenSq = (int)(Math.Pow(pairs[i][0] - pairs[j][0], 2) + Math.Pow(pairs[i][0] - pairs[j][1], 2));

                    if (!linesCollection.ContainsKey(lenSq)) linesCollection.Add(lenSq, new List<int[]>());
                    linesCollection[lenSq].Add(new int[] { pairs[i][0], pairs[i][1], pairs[j][0], pairs[j][1] } );
                }
            }

            var res = 0;
            foreach(var list in linesCollection.Values)
            {
                var n = list.Count;
                for(var i = 0; i < n; i++)
                {
                    for(var j = 1; j < n; j++)
                    {
                        if (list[0][1] == list[1][0] || list[0][0] == list[1][1]) res++;
                    }
                }
            }
            return String.Empty + res;
        }
    }
}
