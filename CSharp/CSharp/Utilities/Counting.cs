using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.CSharp.Utilities
{
    class Counting
    {
        //TODO: one version for binary values 

        // for numbers
        public void NextPermutation(int[] nums)
        {
            var n = nums.Length;
            if (n <= 1) return;

            var lastPeak = n - 1;
            while (lastPeak > 0)
            {
                if (nums[lastPeak - 1] < nums[lastPeak]) break;
                else lastPeak--;
            }

            if (lastPeak == n - 1)
            {
                // quick case: last peak is n-1. swap last 2
                var temp = nums[lastPeak];
                nums[lastPeak] = nums[lastPeak - 1];
                nums[lastPeak - 1] = temp;
            }
            else if (lastPeak == 0)
            {
                // end case: last peak is zero (return the reverse)
                var stop = n / 2;
                for (var i = 0; i < stop; i++)
                {
                    var temp = nums[i];
                    nums[i] = nums[n - 1 - i];
                    nums[n - 1 - i] = temp;
                }
            }
            else {
                // with even less sorting. no extra list
                var nextBigThing = int.MaxValue;
                var prevPeakValue = nums[lastPeak - 1];
                var swapPos = -1;
                for (var i = lastPeak; i < n; i++)
                {
                    if (prevPeakValue < nums[i] && nums[i] < nextBigThing)
                    {
                        nextBigThing = nums[i];
                        swapPos = i;
                    }
                }

                nums[swapPos] = prevPeakValue;
                nums[lastPeak - 1] = nextBigThing;

                // need to sort from lastPeak to n
                Array.Sort(nums, lastPeak, n - lastPeak);
            }
        }
    }
}
