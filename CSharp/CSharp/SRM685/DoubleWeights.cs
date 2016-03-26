using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.SRM685
{
    class DoubleWeights
    {
        public static int minimalCost(string[] weight1, string[] weight2)
        {
            var n = weight1.Length;
            var graph = new int[n][];
            var w1 = new int[n][];
            var w2 = new int[n][];
            for (var i = 0; i < n; i++)
            {
                w1[i] = new int[n];
                w2[i] = new int[n];
                graph[i] = new int[n];
            }

            var list1 = new List<int>();
            var list2 = new List<int>();
            // fill graph 
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    if (weight1[i][j] == '.')
                    {
                        w1[i][j] = -1;
                        w2[i][j] = -1;
                        graph[i][j] = -1;
                    }
                    else
                    {
                        w1[i][j] = weight1[i][j] - '0';
                        w1[i][j] = weight2[i][j] - '0';
                        list1.Add(i);
                        list2.Add(j);
                        graph[i][j] = (weight1[i][j] - '0') + (weight2[i][j] - '0');
                    }
                }
            }

            var g = InitPoints(n, list1.ToArray(), list2.ToArray());

            // do recursion with stack. 
            var q = new List<Point>();
            q.Add(g[0]);
            while(q.Count != 0)
            {
                var node = q[0]; q.RemoveAt(0);
                if(node.pos == 1)
                {

                }
                node.isDirty = true; 
                
                foreach(var nxt in node.nextNods)
                {
                    if (!nxt.isDirty)
                    {
                        nxt.prev = node;
                        q.Add(nxt);
                    }
                }

            }



            return graph[0][1];
        }


        // no distance value 
        public static List<Point> InitPoints(int n, int[] s, int[] e)
        {
            var nodes = new List<Point>();
            for (var i = 0; i < n; i++) nodes.Add(new Point { pos = i });
            for (var i = 0; i < s.Length; i++)
            {
                var nodeOne = nodes[s[i]];
                var nodeTwo = nodes[e[i]];
                nodeOne.nextNods.Add(nodeOne);
                nodeTwo.nextNods.Add(nodeTwo);
            }
            return nodes;
        }

        public class Point
        {
            public int pos;
            public int value = int.MaxValue;
            public List<Point> nextNods = new List<Point>();
            public bool isDirty = false;
            public Point prev;
        }
    }
}


/*
for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    for (var k = 0; k < n; k++)
                    {
                        if (graph[j][i] == -1 || graph[i][k] == -1)
                        {
                            continue;
                        }
                        // j-k VS j-i-k
                        var jk = graph[j][k];
                        var jik = graph[j][i] * graph[i][k];
                        if (jik < jk || jk == -1)
                        {
                            graph[j][k] = jik;
                        }
                    }
                }
            }
*/