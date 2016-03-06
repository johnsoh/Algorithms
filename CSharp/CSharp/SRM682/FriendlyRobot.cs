using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.SRM682
{
    class FriendlyRobot
    {
        public static void Test()
        {
            
            var two = findMaximumReturns("ULDR", 2);
            var eight = findMaximumReturns("ULDRRLRUDUDLURLUDRUDL", 4);
        }

        public static void solveRecursive()
        {

        }

        

        public static int helper(int pos, int movesLeft, int x, int y, int MAX_POS)
        {
            var res = (x == 0 && y == 0) ? 1 : 0;
            if (pos == MAX_POS) return res;

            var best = 0;
            if (movesLeft > 0)
            {
                var right = res + helper(pos + 1, movesLeft - 1, x + 1, y, MAX_POS);
                var left =  res + helper(pos + 1, movesLeft - 1, x - 1, y, MAX_POS);
                var up = res + helper(pos + 1, movesLeft - 1, x, y + 1, MAX_POS);
                var down = res + helper(pos + 1, movesLeft - 1, x, y - 1, MAX_POS);
                best = Math.Max(Math.Max(up, down), Math.Max(right, left));
            }
            return 0;

        }

        public static void solveDP(String instructions, int changesAllowed)
        {
            var STEPS = instructions.Length;
            var CHANGES = changesAllowed;
            //var x = 0; var y = 0;
            var dp = new int[STEPS][][][];

            for(var i = 0; i < STEPS; i++)
            {
                //dp[i] = new int[CHANGES][][];
            }

            for(var step = 0; step < STEPS; step++)
            {
                var possibleChangesSize = Math.Min(step, changesAllowed) + 1;
                dp[step] = new int[possibleChangesSize][][];
                for(var changes = 0; changes < possibleChangesSize; changes++)
                {
                    // init if empty
                    if (dp[step][changes] == null)
                    {
                        dp[step][changes] = new int[STEPS][];
                        for(var x = 0; x < STEPS; x++)
                        {
                            dp[step][changes][x] = new int[STEPS];
                            for (var y = 0; y < STEPS; y++) dp[step][changes][x][y] = -1;
                        }
                    }

                    // init first step
                    if (step == 0 && changes == CHANGES)
                    {
                        // fill first
                        int x, y;
                        //Find(instructions.charAt(step), out x, out y);
                        // dp[step][changes][x][y] = 0

                        //break
                    }

                    // check for all possibilities
  

                }
            }

        }

        //Dictionary<int[], int[]> frequency;

        public static int simulate(char[] arr)
        {

            var frequency = new Dictionary<int[], int[]>();
            var currentSteps = 0;
            var STEP_POINTER = 1;
            var COUNT_POINTER = 0;

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
                var key = new int[] { x, y };
                if (!frequency.ContainsKey(key)) frequency.Add(key, new int[] { 0, currentSteps });
                frequency[key][COUNT_POINTER]++;
                currentSteps++;
            }


            return claps;
        }


        // use dp?
        // dp[300][300][300][300]= 3 600 000 000
        // dp[300][300][100][100]=   900 000 000

        //dp[time][changesAllowed][x][y] = zeros hit
        // for each time time
            // for each changes allowed where changes allowed >= 0
                // process next
                // if changes allowed == 0 continue
                // for each possible x, y where hits != -1, do up down left right. if conflict, take greater

        // at time , search all x and y and return greatest

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

            var claps = 0;//count(arr);

            return claps;
        }
    }
}
