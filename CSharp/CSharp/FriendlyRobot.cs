using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp
{
    class FriendlyRobot
    {
        public static void Test()
        {
            var a = findMaximumReturns("ULDR", 2);
            var eight = findMaximumReturns("ULDRRLRUDUDLURLUDRUDL", 4);
        }

        public static int count(char[] arr)
        {
            
            if (arr.Length == 0) return 0;
            var claps = 0;
            var x = 0; var y = 0;
            foreach(char c in arr)
            {
                switch(c)
                {
                    case 'U':
                        y++;
                        break;
                    case 'D':
                        y--;
                        break;
                    case 'L':
                        x--;
                        break;
                    case 'R':
                        x++;
                        break;
                }
                if (x == 0 && y == 0)
                {
                    claps++;
                }
            }
            return claps;
        }

        public static int findMaximumReturns(String instructions, int changesAllowed)
        {
            var a = findMaximumReturnsSub(instructions, changesAllowed);
            var b = findMaximumReturnsSub(instructions, changesAllowed, 2);
            return Math.Max(a, b);
        }

        public static int findMaximumReturnsSub(String instructions, int changesAllowed, int START = 1)
        {
            // change
            var arr = instructions.ToCharArray();
            for (var i = START; i < arr.Length; )
            {
                char prev = arr[i - 1];
                char curr = arr[i];
                bool isMatch = false;
                switch(prev)
                {
                    case 'U':
                        isMatch = curr == 'D';
                        if (!isMatch && changesAllowed > 0)
                        {
                            changesAllowed--;
                            arr[i] = 'D';
                        }
                        break;
                    case 'D':
                        isMatch = curr == 'U';
                        if (!isMatch && changesAllowed > 0)
                        {
                            changesAllowed--;
                            arr[i] = 'U';
                        }
                        break;
                    case 'L':
                        isMatch = curr == 'R';
                        if (!isMatch && changesAllowed > 0)
                        {
                            changesAllowed--;
                            arr[i] = 'R';
                        }
                        break;
                    case 'R':
                        isMatch = curr == 'L';
                        if (!isMatch && changesAllowed > 0)
                        {
                            changesAllowed--;
                            arr[i] = 'L';
                        }
                        break;
                }
                i += 2;
            }

            var claps = count(arr);

            return claps;
        }
    }
}
