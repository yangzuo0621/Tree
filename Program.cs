using System;
using Tree.BST;
using Tree.Utils;

namespace tree
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new BinarySearchTree<int>();
            tree.Build(new int[] { 2, 1, 5, 3, 10, 9 });
            var node = tree.Search(5);
            tree.Delete(node);
            System.IO.File.WriteAllText("graph.gv", DotUtils<int>.generateDotString(tree.Root));
        }
    }
}
 