using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.GCJ
{
    class CodeJam2016_Q
    {
        private static List<int> primes = new List<int> { 2, 3, 5 };
        private static bool isPrime(int num)
        {
            var max = 1 + (int)Math.Pow(num, 0.5);
            var prime = -1;
            foreach (var p in primes)
            {
                if (num % p == 0) return false;
                prime = p;
            }
            for (var i = prime + 1; i < num; i++)
            {
                var thisIsPrime = true;
                foreach (var p in primes)
                {
                    if (i % p == 0) thisIsPrime = false;
                    break;
                }
                if (thisIsPrime)
                {
                    primes.Add(i);
                    if (num % i == 0) return false;
                }
            }
            return true;
        }

        static void parseSmallInput()
        {
            var file = Properties.Resources.InputBig.Split(new string[] { System.Environment.NewLine, "\n" }, StringSplitOptions.None);
            var ptr = 0;
            var totalCases = int.Parse(file[ptr++]);
            var lines = string.Empty;
            for (var caseNum = 1; caseNum <= totalCases; caseNum++)
            {
                var arr = file[ptr++].Split(' ').Select(long.Parse).ToArray();
                //var res = solveSmall(arr[0], arr[1], arr[2]);
                var res = solveLarge(arr[0], arr[1], arr[2]);
                lines += string.Format("Case #{0}: {1}", caseNum, res) + "\n";
            }

            System.IO.File.WriteAllText(@"C:\git\CSharpTester\CSharpTester\OutputBig.txt", lines);
            Console.Read();
        }

        static string solveLarge(long k, long c, long s)
        {
            var fullLeafLength = (long)Math.Pow(k, c);
            var sectionLength = fullLeafLength / k;

            // initialize verticle sets
            var verticalSets = new HashSet<int>[fullLeafLength];
            for (var i = 0; i < fullLeafLength; i++) verticalSets[i] = new HashSet<int>();

            // we make horizontal sets and populate these vertical sets with each pattern instance
            for (var pos = 1; pos <= k; pos++)
            {
                makePatternInstanceAndPopulateVerticalSets(k, c, pos, verticalSets);
            }

            // use combinatorics to get the sets we need
            var listOfBinaryChoices = GetNCK(k, s);

            // try each set out 
            foreach (var solution in listOfBinaryChoices)
            {
                var accumulator = new HashSet<int>();
                for (var i = 0; i < k; i++)
                {
                    var indicator = solution[i];
                    if (indicator == 1)
                    {
                        accumulator.UnionWith(verticalSets[i]);
                    }
                }
                if (accumulator.Count == k)
                {
                    string res = string.Empty;
                    for (var i = 0; i < k; i++)
                    {
                        res += (i + 1) + " ";
                    }
                    return res;
                }
            }

            return "IMPOSSIBLE";
        }

        private static List<int[]> GetNCK(long k, long s)
        {
            var arr = new int[k];
            for (var i = 0; i < s; i++)
            {
                arr[i] = 1;
            }

            // get N new patterns
            var N = 100;
            var list = new List<int[]>();
            list.Add(arr.ToArray());
            while (N-- > 0)
            {
                findNextPermutation(arr);
                list.Add(arr.ToArray());
            }
            return list;
        }

        public static void findNextPermutation(int[] arr)
        {
            var n = arr.Length;

            /// find last peak
            var lastPeak = n - 1;
            while (lastPeak > 0)
            {
                if (arr[lastPeak - 1] < arr[lastPeak])
                {
                    break;
                }
                lastPeak--;
            }

            if (lastPeak == 0)
            {
                // then we reverse the input
                Array.Reverse(arr);
                return;
            }

            // find nextBiggest from lowerSet (if its not the last iteration)
            var targetIndex = lastPeak - 1;
            var targetValue = arr[targetIndex];
            var swapIndex = -1;
            var swapValue = int.MaxValue;
            for (var i = lastPeak; i < n; i++)
            {
                if (arr[i] > targetValue && arr[i] < swapValue)
                {
                    swapIndex = i;
                    swapValue = arr[swapIndex];
                }
            }

            // swap
            var temp = arr[targetIndex];
            arr[targetIndex] = swapValue;
            arr[swapIndex] = temp;

            // sort lowerSet
            Array.Sort(arr, lastPeak, length: n - lastPeak);
        }

        static char[] makePatternInstanceAndPopulateVerticalSets(long k, long c, long pos, HashSet<int>[] verticalSets)
        {
            var originalPattern = string.Empty;
            for (var i = 1; i <= k; i++)
            {
                originalPattern += (i == pos ? 'G' : 'L');
            }


            char[] prev = new char[] { 'O' };
            for (var i = 2; i < c; i++)
            {
                var patternLength = (long)Math.Pow(k, i);
                var next = new char[patternLength];
                var ptr = 0;
                foreach (var prevTop in prev)
                {
                    if (prevTop == 'O')
                        foreach (var s in originalPattern) next[ptr++] = (s == 'G' ? 'G' : 'O');
                    else
                        for (var j = 0; j < originalPattern.Length; j++) next[ptr++] = 'G';
                }
                prev = next;
            }

            // populate vertical set
            var pointer = 0;
            while (pointer < verticalSets.Length)
            {
                foreach (var ch in prev)
                {
                    if (ch == 'O')
                    {
                        foreach (var s in originalPattern)
                        {
                            if (s == 'G')
                            {
                                verticalSets[pointer].Add(pointer + 1);
                            }
                            pointer++;
                        }
                    }
                    else
                    {
                        for (var j = 0; j < originalPattern.Length; j++)
                        {
                            verticalSets[pointer].Add(pointer + 1);
                            pointer++;
                        }
                    }
                }
            }

            return prev;
        }

        static string solveSmall(long k, long c, long s)
        {
            if (k == 1) return "1";

            var fullLeafLength = (long)Math.Pow(k, c);
            var sectionLength = fullLeafLength / k;
            var results = new List<long>();

            for (var i = 0; i < s; i++)
            {
                var indexToPut = 1 + i * sectionLength;
                results.Add(indexToPut);
            }

            return string.Join(" ", results);
        }
    }
}

