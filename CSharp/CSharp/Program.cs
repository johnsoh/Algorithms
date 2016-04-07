using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSharp.SRM685;
using CSharp.Utilities;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //SRM189.Mortgage.test();
            //MyHt.testMyHt();
            //BinarySearchNode.tesBinarySearchTree();
            GIntCombinatorics.test();
            Console.Read();
        }

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
