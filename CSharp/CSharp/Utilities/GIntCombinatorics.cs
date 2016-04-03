using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Utilities
{
    class GIntCombinatorics
    {
        public static void test()
        {
            var apple = new List<int>() { 0, 1, 1 };
            findPowerset(apple);
            findAllPermutations(apple);

            // testing next permutation
            var arr = new int[] { 4, 2, 2, 3 };
            var n = 12; //(int)Math.Pow(2, arr.Length);
            Console.Out.WriteLine("testing out next permutation");
            while (n > 0)
            {
                n--;
                Console.Out.WriteLine(string.Join("", arr));
                findNextPermutation(arr);
            }
        }

        #region Permutations: find all, find next
        public static void findAllPermutations(List<int> list)
        {
            // get freq table
            var freq = new Dictionary<int, int>();
            foreach(var val in list)
            {
                if (!freq.ContainsKey(val)) freq.Add(val, 0);
                freq[val]++;
            }

            // create permutations
            Console.Out.WriteLine("Finding all permuatations (considering duplications)");
            findPermutation(string.Empty, list.Count, freq);
        }

        private static void findPermutation(string result, int limit, Dictionary<int, int> freq)
        {
            if (result.Length == limit) Console.Out.WriteLine(result);
            foreach(var key in freq.Keys)
            {
                if(freq[key] > 0)
                {
                    var freqCopy = freq.ToDictionary(pair => pair.Key, pair => pair.Value);
                    freqCopy[key]--;
                    findPermutation(result + key, limit, freqCopy);
                }
            }
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

            if(lastPeak == 0)
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
        #endregion

        #region Combinations: powerset, find all, find next
        public static void findPowerset(List<int> list)
        {
            Console.Out.WriteLine("printing all combinations");
            var combinations = (int)Math.Pow(2, list.Count);
            for(var i = 0; i < combinations; i++)
            {
                Console.Out.WriteLine(Convert.ToString(i, 2));
            }
        }

        public static void findAllCombinations(List<int> list, int k)
        {
            var results = new List<int>();
            findAllCombinationsHelper(0, 0, k, results, list.Count);
        }

        private static void findAllCombinationsHelper(int state, int pos, int k, List<int> results, int n)
        {
            if (pos == n) return;
            if (k == 0) results.Add(state);
            // add
            findAllCombinationsHelper(state + (1 >> pos), pos + 1, k - 1, results, n);
            // not add
            findAllCombinationsHelper(state, pos + 1, k, results, n);
        }

        public static void findNextCombination(List<int> list, int k)
        {
            // fast bit twiddeling algorithms 
            // http://stackoverflow.com/questions/1851134/generate-all-binary-strings-of-length-n-with-k-bits-set
            //http://stackoverflow.com/questions/506807/creating-multiple-numbers-with-certain-number-of-bits-set
        }
        #endregion

    }
}
