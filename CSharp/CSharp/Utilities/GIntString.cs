﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Utilities
{
    class GIntString
    { 
        public static void multiPalSolve(string[] words)
        {
            var root = new Node();
            var n = words.Length;
            for (var i = 0; i < n; i++)
            {
                addWord(root, words[i], "@"+i);
                var arr = words[i].ToCharArray().Reverse().ToArray();
                addWord(root, new string(arr), "$"+i); // optimization: reverse function
            }

            var results = countCombiPal(root);
        }

        public static List<List<int>> countCombiPal(Node root)
        {
            // memory init: saveStack maps child nodes to result at a point in time
            var fullResults = new List<List<int>>();
            var queue = new List<Node>() { root };
            var saveStack = new Dictionary<Node, List<string>>();
            var savedTerminators = new List<string>();

            // do iterative depth first search
            while (queue.Count != 0)
            {
                // initialize: take top from queue
                var lastIndex = queue.Count - 1;
                var node = queue[lastIndex]; queue.RemoveAt(lastIndex);
                if (saveStack.ContainsKey(node))
                {
                    savedTerminators = saveStack[node];
                    saveStack.Remove(node); // TODO: check syntax
                }
                // check and update terminators
                if (node.Terminators != null)
                {
                    var thisTermSymbols = node.Terminators;
                    resolveTerminators(savedTerminators, thisTermSymbols, fullResults); // llaa <--> ll case
                    resolveTerminators(thisTermSymbols, thisTermSymbols, fullResults); // bat <--> tab case
                }
                // create save state if there are more than 1 child
                var saveState = node.ChildCount > 1;
                foreach (var nextNode in node.GetChildren())
                {
                    queue.Add(nextNode);
                    if (saveState)
                    {
                        saveStack.Add(nextNode, savedTerminators.ToList());
                    }
                }
            }
            return fullResults;
        }

        private static void resolveTerminators(List<string> higher, List<string> lower, List<List<int>> results)
        {
            for(var i = 0; i < higher.Count; i++)
            {
                for(var j = i+1; j < lower.Count; j++)
                {
                    var higherTerm = higher[i];
                    var lowerTerm = lower[j];
                    if (higherTerm[0] == lowerTerm[0]) continue;
                    var indexOne = int.Parse((higherTerm[0] == '@' ? higherTerm : lowerTerm).Substring(1));
                    var indexTwo = int.Parse((higherTerm[0] == '$' ? higherTerm : lowerTerm).Substring(1));
                    results.Add(new List<int> { indexOne, indexTwo });
                }
            }
        }

        #region Trie Implementation

        public static void addWord(Node root, string word, string terminator)
        {
            var node = root;
            var ptr = 0;
            var n = word.Length;
            while (ptr < word.Length)
            {
                char c = word[ptr];
                node.AddChild(c);
                node = node.children[c];
            }
            node.AddTerminator(terminator);
        }

        public class Node
        {
            public Dictionary<char, Node> children;
            public int ChildCount = 0;
            public List<string> Terminators;

            // supporting method
            public List<Node> GetChildren()
            {
                return children.Values.ToList(); // need to optimize. to list too slow. 
            }

            public void AddChild(char c)
            {
                if (children == null) children = new Dictionary<char, Node>();
                if (!children.ContainsKey(c)) children.Add(c, new Node());
                ChildCount++;
            }

            public void AddTerminator(string terminator)
            {
                if (Terminators == null) Terminators = new List<string>();
                Terminators.Add(terminator);
            }

            public Node()
            {
                children = new Dictionary<char, Node>();
            }
        }

        #endregion


    }
}
