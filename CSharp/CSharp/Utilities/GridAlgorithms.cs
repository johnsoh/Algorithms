using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Utilities
{
    class GridAlgorithms
    {
        static void dfs(int r, int c, int pacman_r, int pacman_c, int food_r, int food_c, String[] grid)
        {
            var g = init(r, c, grid);
            traverse(pacman_r, pacman_c, r, c, g, grid);
        }

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
            for(var r = 0; r < R; r++)
            {
                for(var c = 0; c < C; c++)
                {
                    var newGrid = new Grid();
                    newGrid.isDirty = rep[r][c] == '%';
                    newGrid.val = r * C + C;
                }
            }
            return grid;
        }

        public static void traverse(int r, int c, int R, int C, Grid[] grid, string[] rep)
        {   
            Grid startGrid = grid[r * C + c];
            Grid finalGrid = null;
            Grid prevGrid = null;
            var queue = new List<Grid>() { startGrid };

            while ( queue.Count != 0)
            {
                var currentGrid = queue[0]; queue.RemoveAt(0);
                currentGrid.isDirty = true;
                r = currentGrid.val / C;
                c = currentGrid.val % C;

                // add neighbours
                foreach (var move in moves)
                {
                    var rVal = r + move[0];
                    var cVal = c + move[1];
                    var nextGrid = grid[rVal * C + cVal];
                    if (rVal < 0 || rVal >= R || cVal < 0 || cVal >= C || nextGrid.isDirty) continue;
                    nextGrid.prev = r * C + c;
                    queue.Add(nextGrid);
                }

                // check if winning
                if (rep[r][c] == '.')
                {
                    break;
                }
                else if (currentGrid.Equals(startGrid))
                {
                    prevGrid = currentGrid;
                }
                else
                {
                    currentGrid.prev = prevGrid.val;
                }
                
                
            }

            // no target
            if (finalGrid == null) return;

            // to find path: look backwards from target
            var path = new List<Grid>();
            Grid thisGrid = finalGrid;
            while(!finalGrid.Equals(startGrid)) {
                path.Add(finalGrid);
                finalGrid = grid[finalGrid.prev];
            }
            path.Add(finalGrid);
            path.Reverse();
            foreach(Grid step in path)
            {
                r = step.val / C;
                c = step.val % C;
                Console.WriteLine(r + " " + c);
            }
        }
    }
}
