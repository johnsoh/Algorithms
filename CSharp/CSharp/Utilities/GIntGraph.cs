using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Utilities
{
    class GIntGraph
    {

        #region shortest path algorithms (bellman-ford)
        public static void bellmanFord(int[] starts, int[] ends, int[] values, int n)
        {
            var dist = new int[n];
            var prev = new int[n];
            var edges = new Dictionary<int, Dictionary<int, int>>();
            var alist = new List<int>[n];

            for (var i = 0; i < n; i++)
            {
                dist[i] = int.MaxValue;
                prev[i] = -1;
                alist[i] = new List<int>();
            }
            dist[0] = 0;

            for (var i = 0; i < starts.Length; i++)
            {
                var start = starts[i];
                var end = ends[i];
                var value = values[i];

                alist[start].Add(end);
                if (!edges.ContainsKey(start))
                    edges.Add(start, new Dictionary<int, int>();
                edges[start].Add(end, value);
            }

            // look at all edges (using our alist) and comapre
            for (var i = 0; i < n; i++)
            {
                // looks like n^3 but its only the total number of edges
                foreach (var start in edges.Keys)
                {
                    foreach (var pair in edges[start])
                    {
                        var end = pair.Key;
                        var value = pair.Value;
                        var computed = dist[start] + value;
                        if (computed < dist[end])
                        {
                            dist[end] = computed;
                            prev[end] = start;
                        }
                    }
                }
            }
            foreach (var val in dist) Console.Out.WriteLine(val);
        }
        #endregion

        #region  minimum spanning tree: Kruskal's Algorithm
        //TODO: use linked list instead of (array) list
        public List<Tuple<int, int, int>> kruskals(int[] starts, int[] ends, int[] values, int n)
        {
            // prepare forest
            var f2 = new LinkedList<HashSet<int>>();
            var forest = new List<HashSet<int>>();

            for (var i = 0; i < n; i++)
            {
                var set = new HashSet<int>();
                set.Add(i);
                forest[i] = set;
                f2.AddLast(set);
            }
            // prepare edges
            var edges = new List<Tuple<int, int, int>>();
            for (var i = 0; i < starts.Length; i++)
            {
                edges.Add(new Tuple<int, int, int>(starts[i], ends[i], values[i]));
            }
            // REVERSE SORT edges! so that we can do delete iterate through edges backwards
            edges.Sort((t1, t2) => t1.Item3 - t2.Item3); 

            // delete edges from the smallest
            var ptr = edges.Count - 1;
            while (ptr > 0)
            {
                var firstIndex = findSet(edges[ptr].Item1, forest);
                var secondIndex = findSet(edges[ptr].Item2, forest);
                if (firstIndex == secondIndex)
                {
                    edges.RemoveAt(ptr);
                    ptr--;
                }
                else {
                    int count = merge(firstIndex, secondIndex, forest);
                    ptr--;
                    if (count == n) break;
                }
            }
            // clear out the edges not used
            while (ptr > 0)
            {
                ptr--;
                edges.RemoveAt(0);
            }
            return edges;
        }

        private int findSetAndMerge(int edgeStart, int edgeEnd, LinkedList<HashSet<int>> forest)
        {
            var node = forest.First;
            LinkedListNode<HashSet<int>> nodeStart = null;
            LinkedListNode<HashSet<int>> nodeEnd = null;
            // find both nodes
            while (node != null)
            {
                if(node.Value.Contains(edgeStart))
                {
                    nodeStart = node;
                }
                if(node.Value.Contains(edgeEnd))
                {
                    nodeEnd = node;
                }
                // minor optimization. can break early
                node = node.Next;
            }

            // move 2nd node's value to first
            nodeStart.Value.UnionWith(nodeEnd.Value);

            // take 2nd node out of the linked list
            var prev = nodeEnd.Previous;
            if(prev != null)
            {
                var next = nodeEnd.Next;
                var prevNext = prev.Next;
                prevNext = next;
            }

            return nodeStart.Value.Count; 
        }

        public int findSet(int target, List<HashSet<int>> forest)
        {
            for (var i = 0; i < forest.Count; i++)
            {
                if (forest[i].Contains(target)) return i;
            }
            return -1; // impossible 
        }

        public int merge(int indexOneIn, int indexTwoIn, List<HashSet<int>> forest)
        {
            var indexOne = Math.Min(indexOneIn, indexTwoIn);
            var indexTwo = Math.Max(indexOneIn, indexTwoIn); // for efficient find set
            forest[indexOne].UnionWith(forest[indexTwo]);
            forest[indexTwo].Clear();
            return forest[indexOne].Count;
        }

        #endregion

        

        #region max-flow min-cut Edmond-Karp algorithm
        // max flow min cut
        public static void testMaxFlowMinCut()
        {
            // http://www.cs.princeton.edu/courses/archive/spr04/cos226/lectures/maxflow.4up.pdf
            var start = new int[]   { 0, 0, 0, 2, 3, 2, 2, 3, 7, 4, 5, 6, 5, 6, 7 };
            var end = new int[]     { 2, 3, 4, 3, 4, 5, 6, 6, 3, 7, 6, 7, 1, 1, 1 };
            var vals = new int[]    { 10, 5, 15, 4, 4, 9, 15, 8, 6, 30, 15, 15, 10, 10, 10 };
            var n = 8;
            var res =  maxFlow(start, end, vals, n);
        }

        // source = 0, sink = 1
        public static int maxFlow(int[] start, int[] end, int[] capacity, int n)
        {
            //var flow = new int[start.Length];
            //var prev = new int[start.Length];
            var edges = new Dictionary<int, List<FlowNode>>();

            for (var i = 0; i < n; i++) edges.Add(i, new List<FlowNode>());
            for(var i = 0; i < start.Length; i++)
            {
                edges[start[i]].Add(new FlowNode
                {
                    NextNode = end[i],
                    Flow = 0,
                    Capacity = capacity[i]
                });
            }

            var source = 0;
            var sink = 1;

            while(true)
            {
                // find bfs shortest path
                var queue = new List<int>() { 0 };
                var prev = new int[n];
                for (var i = 0; i < n; i++) prev[i] = -1;

                // find shortest path
                while (queue.Count != 0)
                {
                    var curr = queue[0];
                    queue.RemoveAt(0);
                    foreach(var edge in edges[curr])
                    {
                        // make sure the next node doesnt point back to source
                        // make sure the next node isnt already assigned
                        // make sure we have spare capacity!
                        if (prev[edge.NextNode] == -1 && prev[edge.NextNode] != source && edge.Flow < edge.Capacity)
                        {
                            prev[edge.NextNode] = curr;
                        }
                    }
                }

                // do we break yet? (are all paths to sink exhausted?)
                if (prev[sink] == -1) break;

                // find min
                var min = int.MaxValue;
                var ptr = sink;
                while(ptr != source)
                {
                    var startNode = prev[ptr];
                    var endNode = ptr;
                    var edge = edges[startNode].Where(x => x.NextNode == endNode).FirstOrDefault();
                    min = Math.Min(min, edge.Capacity - edge.Flow);
                }

                // update links
                ptr = sink;
                while (ptr != source)
                {
                    var startNode = prev[ptr];
                    var endNode = ptr;
                    var edge = edges[startNode].Where(x => x.NextNode == endNode).FirstOrDefault();
                    edge.Flow += min;
                }
            }
            
            // collect result
            var result = 0;
            foreach(var list in edges.Values)
            {
                foreach(var flowNode in list.Where(fN => fN.NextNode == sink))
                {
                    result += flowNode.Flow;
                }
            }

            return result;
        }
        #endregion

        // trie
    }

    public class FlowNode
    {
        public int Capacity;
        public int Flow;
        public int NextNode;
    }
}
