using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Utilities
{
    class GIntAlgorithms
    {
        #region merge sort
        public static int msCountInv(int[] arr, int left = 0, int right = -1)
        {
            if (right == -1) right = arr.Length;
            var inversions = 0;
            if (left < right)
            {
                var mid = left / 2 + (right - left) / 2;
                inversions += msCountInv(arr, left, mid);
                inversions += msCountInv(arr, mid + 1, right);
                inversions += mergeCountInv(arr, left, mid, right);
            }
            return inversions;
        }

        public static int mergeCountInv(int[] arr, int left, int mid, int right)
        {
            var inversions = 0;
            var copy = new int[right - left];
            var copyPtr = 0;
            var leftPtr = left;
            var rightPtr = mid;
            while (leftPtr < mid && rightPtr < right)
            {
                if (arr[rightPtr] < arr[leftPtr])
                {
                    copy[copyPtr++] = arr[rightPtr++];
                    inversions += mid - leftPtr;
                }
                else copy[copyPtr++] = arr[leftPtr++];
            }
            while (leftPtr < mid) copy[copyPtr++] = arr[leftPtr++]; // already counted
            while (rightPtr < right) copy[copyPtr++] = arr[rightPtr++]; // this is expected

            return inversions;
        }
        #endregion

        // quick sort

        // binary search
    }

    #region Hash table
    public class MyHt
    {

        // test method
        public static void testMyHt()
        {
            var inputs = new string[] {"cpp", "java", "python", "python2","sth-else", "sth-else", "vba?", "csharp"};
            var keys = new int[] { 10, 21, 32, 42, 5, 5, 4, 84 };
            // expected: insert, insert, insert , add to list
            // expected: rehash, same key, insert, add to list
            var ht = new MyHt();
            for (var i = 0; i < 4; i++)
            {
                ht.Insert(keys[i], inputs[i]);
            }

            string value = null;
            for (var i = 0; i < 4; i++)
            {
                value = ht.Get(keys[i]); //cpp , java, py, py2
                Console.Out.WriteLine(value);
            }
            value = ht.Get(7); // should be null
            Console.Out.WriteLine(value);

            for (var i = 4; i < 8; i++)
            {
                var b = ht.Insert(keys[i], inputs[i]); // t, f, t, t
                Console.Out.WriteLine(b);
            }
            Console.Out.WriteLine(ht.n); // should be 10!

            ht.Delete(84);
            value = ht.Get(4); // should have vba?
            Console.Out.WriteLine(value);
            value = ht.Get(84); // should be null
            Console.Out.WriteLine(value);
        }


        // properties
        List<Tuple<int, string>>[] chain;
        Tuple<int, string>[] linearProbe;
        public int n = 5;
        int itemCount = 0;
        
        // methods
        public MyHt()
        {
            chain = new List<Tuple<int, string>>[n];
        }

        private int getHash(int key)
        {
            return key % n;
        }

        private void rehash()
        {
            if (itemCount < n * 0.8) return;

            var oldChain = chain;
            n = n * 2;
            chain = new List<Tuple<int, string>>[n];

            // copy old to new
            itemCount = 0;
            foreach (var list in oldChain)
            {
                if (list == null) continue;
                foreach (var tuple in list)
                {
                    Insert(tuple.Item1, tuple.Item2);
                }
            }
        }

        public bool Insert(int key, string value)
        {
            rehash();
            // chaining
            int hashIndex = getHash(key);
            if (chain[hashIndex] == null)
                chain[hashIndex] = new List<Tuple<int, string>>();
            var list = chain[hashIndex];
            foreach (var tuple in list) if (tuple.Item1 == key) return false;
            list.Add(new Tuple<int, string>(key, value));
            itemCount++;
            return true;
        }

        public string Get(int key)
        {
            int hashIndex = getHash(key);
            var list = chain[hashIndex];
            if (list == null) return null;
            string value = null;
            foreach (var tuple in list)
            {
                if (tuple.Item1 == key)
                    value = tuple.Item2;
            }
            return value;
        }

        public bool Delete(int key)
        {
            int hashIndex = getHash(key);
            var list = chain[hashIndex];
            if (list == null) return false;
            var index = 0;

            foreach (var tuple in list)
            {
                if (tuple.Item1 == key) break;
                index++;
            }
            if (index == list.Count) return false;
            list.RemoveAt(index);
            return true;
        }
    }
    #endregion
}
