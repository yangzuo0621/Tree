using System;

namespace Tree.BST {
    public class RedBlackTree<T> where T : IComparable<T> {

    }

    public class BRTreeNode<T> where T : IComparable<T> {
        public BRTreeNode(T t) => (Value, Color) = (t, RBColor.BLACK);

        public T Value { get; set; }
        public RBColor Color { get; set; }
        public BSTTreeNode<T> Left { get; set; }
        public BSTTreeNode<T> Right { get; set; }
        public BSTTreeNode<T> Parent { get; set; }
    }

    public enum RBColor {
        RED,
        BLACK,
    }
}