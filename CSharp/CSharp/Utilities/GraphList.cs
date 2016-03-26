using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Utilities
{
    class GraphList
    {
        // graph with distance
        public static List<Node> InitNodes(int n, int[] s, int[] e, int[] v)
        {
            var nodes = new List<Node>();
            for (var i = 0; i < n; i++) nodes.Add(new Node());
            for (var i = 0; i < s.Length; i++)
            {
                var nodeOne = nodes[s[i]];
                var nodeTwo = nodes[e[i]];
                var path1 = new Path { value = v[i], nextNode = nodeTwo };
                nodeOne.nextPaths.Add(path1);
                var path2 = new Path { value = v[i], nextNode = nodeOne };
                nodeTwo.nextPaths.Add(path2);
            }
            return nodes;
        }

        public static void dijkstra(int start, List<Node> nodes)
        {
            var queue = new List<Node>();
            nodes[start].value = 0;
            queue.Add(nodes[start]);
            while(queue.Count != 0)
            {
                Node node = queue[0];
                queue.RemoveAt(0);
                foreach(var path in node.nextPaths)
                {
                    if (path.nextNode.isDirty) continue;
                    path.nextNode.isDirty = true;
                    var val = node.value + path.value;
                    if (val < path.nextNode.value) path.nextNode.value = val;
                    path.nextNode.prev = node;
                    queue.Add(path.nextNode);
                }
            }
        }

        public static List<Node> findPath(int start, int end, List<Node> nodes)
        {
            var startNode = nodes[start];
            var node = nodes[end];
            var path = new List<Node>();
            while(!node.Equals(startNode))
            {
                path.Add(node);
                node = node.prev;
            }
            path.Add(node);
            path.Reverse();
            return path;
        }

        public static void reset(List<Node> nodes)
        {
            foreach(var node in nodes)
            {
                node.prev = null;
                node.isDirty = false;
                node.value = int.MaxValue;
            }
        }

        public class Node
        {
            public int value = int.MaxValue;
            public List<Path> nextPaths = new List<Path>();
            public bool isDirty = false;
            public List<Node> prev = new List<Node>();
        }

        public class Path
        {
            public int value;
            public Node nextNode;
        }


        // no distance value 
        public static List<Point> InitPoints(int n, int[] s, int[] e)
        {
            var nodes = new List<Point>();
            for (var i = 0; i < n; i++) nodes.Add(new Point());
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
            public int value = int.MaxValue;
            public List<Point> nextNods = new List<Point>();
            public bool isDirty = false;
            public Point prev;
        }
    }
}
