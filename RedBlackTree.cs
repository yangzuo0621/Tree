using System;
using System.Collections.Generic;

namespace Tree.BST {
    public class RedBlackTree<T> where T : IComparable<T> {

        public RedBlackTree() { }

        public RedBlackTree(IEnumerable<T> collections)
        {
            Build(collections);
        }

        public void Build(IEnumerable<T> collections) {
            if (_Root == null) {
                _Root = null;
            }

            foreach (var item in collections) {
                Insert(item);
            }
        }

        public void Insert(T key) {
            throw new NotImplementedException();
        }

        public void Delete(RBTreeNode<T> node) {
            throw new NotImplementedException();
        }

        public RBTreeNode<T> Search(T key) {
            throw new NotImplementedException();
        }

        public RBTreeNode<T> Maximum() {
            throw new NotImplementedException();
        }

        public RBTreeNode<T> Minimum() {
            throw new NotImplementedException();
        }

        public RBTreeNode<T> Successor(RBTreeNode<T> node) {
            throw new NotImplementedException();
        }

        public RBTreeNode<T> Predecessor(RBTreeNode<T> node) {
            throw new NotImplementedException();
        }

        private RBTreeNode<T> Left_Rotate(RBTreeNode<T> node) {
            throw new NotImplementedException();
        }

        private RBTreeNode<T> Right_Rotate(RBTreeNode<T> node) {
            throw new NotImplementedException();
        }

        public RBTreeNode<T> Root {
            get {
                return _Root;
            }
        }

        public RBTreeNode<T> SentryNode {
            get {
                return _SentryNode;
            }
        }

        private RBTreeNode<T> _Root;
        private RBTreeNode<T> _SentryNode;
    }

    public class RBTreeNode<T> where T : IComparable<T> {
        public RBTreeNode(T t) => (Value, Color) = (t, RBColor.BLACK);
        public RBTreeNode(T t, RBColor color) => (Value, Color) = (t, color);

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