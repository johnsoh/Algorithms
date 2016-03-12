using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Hackerrank
{
    class PacManDFS
    {


        public class Grid
        {
            public bool isDirty;
            public int val;
            public int prev;
        }

        public static int[][] moves = new int[][] {
                new int[] {-1, 0 },
                new int[] {0, -1},
                new int[] {0, 1},
                new int[] {1, 0}
            };

        public static Grid[] init(int R, int C, string[] rep)
        {
            var N = R * C;
            var grid = new Grid[N];
            for (var i = 0; i < N; i++)
            {
                var newGrid = new Grid();
                // assign vals
                grid[i] = newGrid;
            }
            return grid;
        }

        public static void traverse(int R, int C, Grid[] grid)
        {
            var startR = 0; var startC = 0;
            var r = 0; var c = 0;
            
            var queue = new List<Grid>() { grid[startR * C + startC] };
            Grid startGrid = grid[r * C + C];
            Grid finalGrid = null;
            while (queue.Count != 0)
            {
                var currentGrid = queue[0];
                queue.RemoveAt(0);
                r = currentGrid.val / C;
                c = currentGrid.val % C;
                foreach (var move in moves)
                {
                    var rVal = r + move[0];
                    var cVal = c + move[1];
                    var cellPointer = rVal * C + cVal;
                    if (rVal < 0 || rVal >= R || cVal < 0 || cVal >= C || grid[cellPointer].isDirty) continue;
                    grid[cellPointer].prev = r * C + C;
                    // if we see this as a success... return;
                }
            }

            // no target
            if (finalGrid == null) return;

            // to find path: look backwards from target
            var path = new List<Grid>();
            Grid thisGrid = finalGrid;
            while (!finalGrid.Equals(startGrid))
            {
                path.Add(finalGrid);
                finalGrid = grid[finalGrid.prev];
            }
            path.Add(finalGrid);
            path.Reverse();
        }
    }
}
