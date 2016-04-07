using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Utilities
{
    class GIntAlgorithms
    {
        #region merge sort with inversions
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

        #region quick sort
        public static void qs(int[] arr, int left = 0, int right = -1)
        {
            if (right == -1) right = arr.Length;

            if(left < right)
            {
                var pivot = 4;
                if (pivot > 1)
                    qs(arr, left, pivot - 1);

                if (pivot < right - 1)
                    qs(arr, pivot + 1, right);
            }
        }

        private static int partition(int[] arr, int left, int right )
        { 
            // pivotval, pivotPtr, nextPtr, done
            var pivotPtr = left;
            var nextPtr = left;
            var pivotValue = arr[right];
            while(nextPtr < right)
            {
                if(arr[nextPtr] < arr[pivotPtr])
                {
                    swap(arr, pivotPtr, nextPtr);
                    pivotPtr++;
                }
                nextPtr++;
            }
            swap(arr, pivotPtr, right);
            return pivotPtr;
        }

        private static void swap(int[] arr, int pivotPtr, int nextPtr)
        {
            var temp = arr[nextPtr];
            arr[nextPtr] = arr[pivotPtr];
            arr[pivotPtr] = arr[nextPtr];
        }
        #endregion

        #region binary search

        // ffftttttt
        public static void binarySearchDiscrete(int[] arr)
        {
            var left = 0;
            var right = arr.Length;
            while(left < right )
            {
                var mid = left / 2 + (right - left) / 2; //ERROR!!
                bool doesSatisfy = DoesSatisfy(arr[mid]);
                if(doesSatisfy)
                {
                    right = mid;
                }
                else
                {
                    left = mid + 1;
                }
            }
        }

        private static bool DoesSatisfy(int v)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    #region Heap-Max

    public class Heap
    {

        List<int> list = new List<int>();
        int ptr = 0;

        public void insert(int v)
        {
            // insert at back
            list.Add(v);
            var candidate = list.Count;
            while (candidate > 0)
            {
                var parent = getParent(candidate);

                // check if parent & child r/s holds
                if (list[parent] > list[candidate]) break;

                // otherwise we need to bubble up
                swap(parent, candidate);
                candidate = parent;
            }
        }

        public int extract()
        {
            // delete item 0;
            var result = peek();
            var n = list.Count;
            if (n == 1)
            {
                list.RemoveAt(0);
                return result;
            }

            // swap last element with previous top
            list[0] = list[n - 1];
            list.RemoveAt(n - 1);

            // bubble down: max heapify
            heapifyMax(0);

            // return value
            return result;
        }

        public int peek()
        {
            return list[0];
        }

        private void heapifyMax(int candidate)
        {
            var limit = list.Count;
            var leftChildIndex = 2 * candidate + 1;
            var rightChildIndex = 2 * candidate + 2;

            // find the index of the biggest avaliable value among candidate, leftptr, rightptr
            // so we are in teh order :  [candidate] .... [2*candidate] [leftChildIndex] [rightChildIndex]
            var largest = leftChildIndex < limit && list[candidate] < list[leftChildIndex] ? leftChildIndex : candidate;
            largest = rightChildIndex < limit && list[candidate] < list[rightChildIndex] ? rightChildIndex : largest;

            if (largest != candidate) // we are not done bubbling down
            {
                swap(largest, candidate);
                heapifyMax(largest);
            }
        }

        private void swap(int first, int second)
        {
            var tempFirst = list[first];
            list[first] = list[second];
            list[second] = tempFirst;
        }

        /*private int getchild(int parent)
        {
            return new List<int> { 2 * parent + 1, 2 * parent + 2 };
        }*/

        private int getParent(int child)
        {
            return (child + 1) / 2 - 1;
        }
    }

    #endregion Heap-Min

    #region Binary Search Tree

    public class BinarySearchNode
    {
        public BinarySearchNode left;
        public BinarySearchNode right;
        public int value;
        public int leftLimit;
        public int rightLimit; 

        public static void tesBinarySearchTree()
        {
            var root = new BinarySearchNode() { value = 5 }; // init root nodes values to neg and pos infinities 
            var inputs = new int[] { 1, 3, 8, 10, 12 };
            foreach(var input in inputs)
            {
                insert(root, input);
            }

            // other inputs: https://www.cs.rochester.edu/~gildea/csc282/slides/C12-bst.pdf
            var otherInputs = new int[] { 2, 3, 4, 5, 6, 7, 8, 9, 11, 12, 15, 19, 20};
            var root2 = new BinarySearchNode();
            createMinimumBinaryTree(otherInputs, 0, otherInputs.Length - 1, root2);
        }

        public static void delete(BinarySearchNode root, int value)
        {
            // look for the node. 
            var node = root;
            BinarySearchNode parent = null;
            while(node.value != value)
            {
                // check base case
                if (node == null || node.value == value)
                {
                    break;
                }

                parent = node;
                if(value < node.value)
                {
                    node = root.left;
                }
                else
                {
                    node = root.right;
                }
            }
            if (node == null) return;
            deleteHelper(node: node, parent: parent, value: value);
        }

        public static void deleteHelper(BinarySearchNode node, BinarySearchNode parent, int value)
        {
            // if it has no children delete
            // if it has one child, connect child to parent
            // else pick either l or r and recursively call deleteHelper
            if (node.right != null && node.left != null)
            {
                // recurse. lets pick right
                deleteHelper(node.right, node, value);
            }
            else
            {
                BinarySearchNode child = null;
                if (node.right != null || node.left != null)
                {
                    child = (node.right != null) ? node.right : node.left;
                }
                if (parent.right != null && parent.right.Equals(node))
                {
                    parent.right = child;
                }
                else if (parent.left != null && parent.left.Equals(node))
                {
                    parent.left = child;
                }
            }
        }

        public static void insert(BinarySearchNode root, int value)
        {
            var placeOnLeft = value < root.value;

            // if left, check for left null ? put init on left : recursively call insert on child node
            if(placeOnLeft)
            {
                if(root.left == null)
                {
                    var node = new BinarySearchNode();
                    node.rightLimit = Math.Min(root.rightLimit, value); // todo check if this is really true 
                    node.leftLimit = Math.Max(root.leftLimit, value);
                    node.value = value;
                    root.left = node; 
                }
                else
                {
                    insert(root.left, value);
                }
            }
            else
            {
                if(root.right == null)
                {
                    var node = new BinarySearchNode();
                    node.rightLimit = Math.Min(root.rightLimit, value); // todo check if this is really true 
                    node.leftLimit = Math.Max(root.leftLimit, value);
                    node.value = value; 
                    root.right = node;  
                }
                else
                {
                    insert(root.right, value);
                }
            }
        }

        // TODO: delete and find

        public static bool isValid(BinarySearchNode node, int leftLim = int.MinValue, int rightLim = int.MaxValue)
        {
            var leftEval = node.left == null ? true : isValid(node.left, leftLim, node.value);
            var rightEval = node.right == null ? true : isValid(node.right, node.value, rightLim);
            return leftEval && rightEval;
        }

        public static void createMinimumBinaryTree(int[] arr, int left, int right, BinarySearchNode node)
        {
            // base case
            if (left == right)
            {
                node.value = arr[left];
            }
            else {
                var mid = left + (right - left) / 2;
                node.value = arr[mid];

                // create children 
                // create left
                if (mid > left)
                {
                    node.left = new BinarySearchNode();
                    createMinimumBinaryTree(arr, left, mid - 1, node.left);
                }
                if (mid < right)
                {
                    node.right = new BinarySearchNode();
                    createMinimumBinaryTree(arr, mid + 1, right, node.right);
                }
            }
        }
    }

    #endregion

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
