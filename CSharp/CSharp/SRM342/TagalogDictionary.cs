using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.SRM342
{
    // assume no duplicate words
    class TagalogDictionary
    {
        static string orderString = "a b k d e g h i l m n ng o p r s t u w y";

        public static void test()
        {
            var zero = sortWords(new string[] { "abakada", "alpabet", "tagalog", "ako" });
            var one = sortWords(new string[] { "ang", "ano", "anim", "alak", "alam", "alab" });
        }

        public static String[] sortWords(String[] words)
        {
            var root = new Node { symbol = string.Empty };
            // insert all words into node
            foreach(var word in words)
            {
                insert(root, word);
            }

            var results = dfs(root);

            return results.ToArray();
        }

        public static List<string> dfs(Node root)
        {
            var ordering = orderString.Split(' '); // parse node to get ordered words based on my rannking 
            var queue = new Stack<Node>();
            queue.Push(root);
            var results = new List<string>();
            while (queue.Count != 0)
            {
                var node = queue.Pop();
                if(node.nextSteps.ContainsKey("*")) results.Add(node.value);
                foreach (var order in ordering.Where(o => node.nextSteps.ContainsKey(o)))
                {
                    var nextNode = node.nextSteps[order];
                    nextNode.value = node.value + nextNode.symbol;
                    queue.Push(nextNode);
                }
            }
            return results;
        }

        public static void insert(Node root, string wordString)
        {
            var word = new List<string>();

            // special treatment for 2 chars as one. 
            for(var i = 0; i < wordString.Length; i++)
            {
                word.Add((wordString[i] == 'n' && i < wordString.Length - 1 && wordString[i + 1] == 'g') ?
                            wordString[i++] + "g" :
                            wordString[i] + string.Empty);
            }

            // populate the prefix tree
            var currentNode = root;
            var n = word.Count;
            var ptr = 0;
            while(ptr < n)
            {
                var thisLetter = word[ptr++];
                if (!currentNode.nextSteps.ContainsKey(thisLetter)) {
                    currentNode.nextSteps.Add(thisLetter, new Node { symbol = thisLetter });
                }
                currentNode = currentNode.nextSteps[thisLetter];
            }
            // end with a star
            currentNode.nextSteps.Add("*", new Node { symbol = "*" }); // optimization: no need add the extra node. 
        }
    }

    public class Node
    {
        public string symbol;
        public Dictionary<string, Node> nextSteps = new Dictionary<string, Node>();
        public string value = string.Empty;
    }
}
