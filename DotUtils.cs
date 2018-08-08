using System;
using System.Collections.Generic;
using Tree.BST;

namespace Tree.Utils {
    public static class DotUtils<T> where T : IComparable<T> {

        public static string generateDotString(BSTTreeNode<T> root) {
            var contents = new List<string>();
            var dictionary = new Dictionary<BSTTreeNode<T>, string>();
            PreOrderTraversal(root, contents, dictionary);
            foreach (var item in dictionary) {
                contents.Add($"{item.Value}[label = \"<f0> |<f1> {item.Key.Value}|<f2> \"];");
            }
            var result = $"digraph g {{\nnode [shape = record,height=.1]; {string.Join("\n", contents)} \n}}";
            return result;
        }

        private static void PreOrderTraversal(BSTTreeNode<T> root, List<string> contents, Dictionary<BSTTreeNode<T>, string> dictionary) {
            if (root != null) {

                var rootName = GetTreeNodeName(root, dictionary);
                if (root.Left != null) {
                    var leftName = GetTreeNodeName(root.Left, dictionary);
                    contents.Add($"\"{rootName}\":f0 -> \"{leftName}\":f1;");
                }
                if (root.Right != null) {
                    var rightName = GetTreeNodeName(root.Right, dictionary);
                    contents.Add($"\"{rootName}\":f2 -> \"{rightName}\":f1;");
                }

                PreOrderTraversal(root.Left, contents, dictionary);
                PreOrderTraversal(root.Right, contents, dictionary);
            }
        }

        private static string GetTreeNodeName(BSTTreeNode<T> node, Dictionary<BSTTreeNode<T>, string> dictionary) {
            if (dictionary.ContainsKey(node)) {
                return dictionary[node];
            }

            var nodeName = $"node{_Count}";
            dictionary[node] = nodeName;
            _Count++;
            return nodeName;
        }

        private static int _Count = 0;
    } 
}
