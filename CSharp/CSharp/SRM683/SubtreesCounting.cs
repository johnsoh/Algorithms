using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.SRM683
{
    class SubtreesCounting
    {
        public static void test()
        {
            var ten = sumOfSizes(3, 1, 1, 1, 1);
            var fiftyTwo = sumOfSizes(5,1,2,3,100);
        }

        public static int sumOfSizes(int n, int a0, int b, int c, int m)
        {
            var nodes = new Node[n];
            // make tree
            var a = new int[n];
            a[0] = a0;
            for (var i = 1; i <= n - 2; i++) a[i] = (b * a[i - 1] + c) % m;
            for (var i = 0; i < n; i++) nodes[i] = new Node();
            for (var i = 1; i <= n - 1; i++)
            {
                var j = a[i - 1] % i;
                nodes[i].nextList.Add(nodes[j]);
                nodes[j].nextList.Add(nodes[i]);
            }
    

            // init tree
            foreach (Node node in nodes)
            {
                node.childCount = node.nextList.Count;
            }

            // pre process from top to get each node's value 
            process(nodes[0], 1);

            // do dfs from the top ? 
            foreach (var node in nodes) node.isDirty = false; 
            nodes[0].isDirty = true;
            return parse(nodes[0]);
        }

        public static void process(Node node, int len)
        {
            node.isDirty = true;
            node.value = len;
            foreach(var side in node.nextList.Where(x => !x.isDirty))
            {
                process(node, len + 1);
            }
        }

        public static int parse(Node node)
        {
            var eligibleChildren = 0;
            if (node.childCount == 1 && !node.nextList[0].isDirty) eligibleChildren = 1;
            else if (node.childCount == 2 && (node.nextList[0].isDirty != node.nextList[1].isDirty)) eligibleChildren = 1;
            else if (node.childCount == 2 && (node.nextList[0].isDirty == false)) eligibleChildren = 2;

            node.isDirty = true;

            if (eligibleChildren == 0)
            {
                return node.value;
            }
            if (eligibleChildren == 1)
            {
                var sum = 0;
                foreach (var side in node.nextList.Where(x => !x.isDirty))
                {
                    sum += parse(side);
                }
                return node.value + sum;
            }
            // elibibleChildren == 2
            var first = 1 + parse(node.nextList[0]);
            var second = 1 + parse(node.nextList[1]);
            return first * second;
        }
    }

    public class Node
    {
        public List<Node> nextList;
        public int childCount;
        public int value;
        public bool isDirty;

        public Node()
        {
            nextList = new List<Node>();
            childCount = -1;
            isDirty = false; 
        }
    }
}
