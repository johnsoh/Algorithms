using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp.ApacTest;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //CSharp.FHC.Qualification1.Parse();
            /*var one = new int[] {4, 3, 2, 1};
            var two = new int[] {1, 3, 2, 5, 4 };
            ms(one);
            ms(two);*/
            SRM342.TagalogDictionary.test();
            Console.Read();
        }

        /*
        Nodes + edges as objects
        Space: n+e
        Next Node: O(1)


        Matrix
        Space: n^2
        Next Node: O(N)
        */

        public static void ms(int[] arr)
        {
            split(0, arr.Length, arr);
        }

        // TOOD: baes case: 3, 5. midpt: 
        private static void split(int start, int end, int[] arr)
        {
            int midpoint = start / 2 + end / 2; // prevent overflow if we have(non-zero, integer.max)
            
            if ((start % 2 == 1) && (end % 2== 1)) midpoint++; // 3,5: 3/2+5/2 = 1+2 = 3. upgrade midpoint to 4. compensate for loss in decimal place. 
            if (end - start <= 1) return; 
            split(start, midpoint, arr);
            split(midpoint, end, arr);
            merge(start, midpoint, end, arr);
        }

        private static void merge(int start, int midpoint, int end, int[] arr)
        {
            // anchor a, compare it with b
            //insert these values into the actual array
            int aStart = start;
            int bStart = midpoint;
            int aEnd = midpoint;
            int bEnd = end;
            int tempPointer = 0;
            int[] temp = new int[end - start];
            while (aStart < aEnd && bStart < bEnd)
            {
                temp[tempPointer++] = (arr[bStart] < arr[aStart]) ? arr[bStart++] : arr[aStart++];
            }
            // at this point, either we have used up aStart or bStart. copy the rest from whichever into temp
            while (aStart < aEnd) temp[tempPointer++] = arr[aStart++];
            while (bStart < bEnd) temp[tempPointer++] = arr[bStart++];

            // finally, we copy everything from our temp to actual array. 
            for (int i = start; i < end; i++)
            {
                arr[i] = temp[i - start];
            }
        }
    }
}
