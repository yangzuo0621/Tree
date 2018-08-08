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
            var current = _Root;
            while (current != null && current.Value.CompareTo(key) != 0) {
                if (current.Value.CompareTo(key) > 0) {
                    current = current.Left;
                } else {
                    current = current.Right;
                }
            }
            return current;
        }

        public RBTreeNode<T> Maximum() {
            if (_Root == null) {
                throw new ArgumentNullException();
            }

            return _Maximum(_Root);
        }

        public RBTreeNode<T> Minimum() {
            if (_Root == null) {
                throw new ArgumentNullException();
            }

            return _Minimum(_Root);
        }

        public RBTreeNode<T> Successor(RBTreeNode<T> node) {
            if (node == null) {
                throw new ArgumentNullException();
            }

            if (node.Right != _SentryNode) {
                return _Maximum(node.Right);
            }

            var current = node;
            var parent = current.Parent;
            while (parent != _SentryNode && parent.Right == current) {
                current = parent;
                parent = parent.Parent;
            }
            return parent;
        }

        public RBTreeNode<T> Predecessor(RBTreeNode<T> node) {
            if (node == null) {
                throw new ArgumentNullException();
            }

            if (node.Left != _SentryNode) {
                return _Maximum(node.Left);
            }

            var current = node;
            var parent = node.Parent;
            while (parent != _SentryNode && parent.Left == current) {
                current = parent;
                parent = parent.Parent;
            }
            return parent;
        }

        private void Left_Rotate(RBTreeNode<T> node) {
            var rightChild = node.Right;
            node.Right = rightChild.Left;
            if (rightChild.Left != _SentryNode) {
                rightChild.Left.Parent = node;
            }
            rightChild.Parent = node.Parent;
            if (node.Parent == _SentryNode) {
                _Root = rightChild;
            } else {
                if (node.Parent.Left == node) {
                    node.Parent.Left = rightChild;
                } else {
                    node.Parent.Right = rightChild;
                }
            }
            rightChild.Left = node;
            node.Parent = rightChild;
        }

        private void Right_Rotate(RBTreeNode<T> node) {
            var leftChild = node.Left;
            node.Left = leftChild.Right;
            if (leftChild.Right != null) {
                leftChild.Right.Parent = node;
            }
            leftChild.Parent = node.Parent;
            if (node.Parent == _SentryNode) {
                _Root = leftChild;
            } else {
                if (node.Parent.Left == node) {
                    node.Parent.Left = leftChild;
                } else {
                    node.Parent.Right = leftChild;
                }
            }
            leftChild.Right = node;
            node.Parent = leftChild;
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
        private RBTreeNode<T> _SentryNode = new RBTreeNode<T>(RBColor.BLACK);

        private RBTreeNode<T> _Maximum(RBTreeNode<T> node) {
            var current = node;
            while (current.Right != _SentryNode) {
                current = current.Right;
            }
            return current;
        }

        private RBTreeNode<T> _Minimum(RBTreeNode<T> node) {
            var current = node;
            while (current.Left != _SentryNode) {
                current = current.Left;
            }
            return current;
        }
    }

    public class RBTreeNode<T> where T : IComparable<T> {
        public RBTreeNode(T t) => (Value, Color) = (t, RBColor.RED);
        public RBTreeNode(RBColor color) => (Color) = (color);

        public T Value { get; set; }
        public RBColor Color { get; set; }
        public RBTreeNode<T> Left { get; set; }
        public RBTreeNode<T> Right { get; set; }
        public RBTreeNode<T> Parent { get; set; }
    }

    public enum RBColor {
        RED,
        BLACK,
    }
}