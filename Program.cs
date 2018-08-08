using System;
using Tree.BST;
using Tree.Utils;

namespace tree
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new RedBlackTree<double>();
            tree.Build(new double[] { 11, 2, 1, 7, 5, 8, 14, 15 });
            tree.Insert(6);
            tree.Insert(13);
            tree.Insert(13.5);
            string text = RBUtils<double>.generateDotString(tree.Root, tree.SentryNode);
            System.IO.File.WriteAllText("graph.gv", text);
        }
    }
}
 