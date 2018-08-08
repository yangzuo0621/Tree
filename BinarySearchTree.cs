using System;
using System.Collections.Generic;

namespace Tree.BST
{
    public class BinarySearchTree<T> where T : IComparable<T>
    {
        public BinarySearchTree() { }

        public BinarySearchTree(IEnumerable<T> collections)
        {
            Build(collections);
        }

        public void Build(IEnumerable<T> collections) {
            if (_Root != null) {
                _Root = null;
            }

            foreach (var item in collections) {
                Insert(item);
            }
        }

        public void Insert(T key) {
            BSTTreeNode<T> node = new BSTTreeNode<T>(key);
            BSTTreeNode<T> parentNode = null;
            BSTTreeNode<T> current = _Root;
            while (current != null) {
                parentNode = current;
                if (current.Value.CompareTo(key) > 0) {
                    current = current.Left;
                } else {
                    current = current.Right;
                }
            }
            node.Parent = parentNode;
            if (parentNode == null) {
                _Root = node;
                return;
            }
            
            if (parentNode.Value.CompareTo(key) > 0) {
                parentNode.Left = node;
            } else {
                parentNode.Right = node;
            }
        }

        public void Delete(BSTTreeNode<T> node) {
            BSTTreeNode<T> delNode = null;
            if (node.Left == null || node.Right == null) {
                delNode = node;
            } else {
                delNode = Successor(node);
            }
            
            BSTTreeNode<T> child = null;
            if (delNode.Left != null) {
                child = delNode.Left;
            } else {
                child = delNode.Right;
            }

            if (child != null) {
                child.Parent = delNode.Parent;
            }

            if (delNode.Parent == null) {
                _Root = child;
            } else {
                if (delNode.Parent.Left == delNode) {
                    delNode.Parent.Left = child;
                } else {
                    delNode.Parent.Right = child;
                }
            }

            if (node != delNode) {
                node.Value = delNode.Value;
            }
        }

        public BSTTreeNode<T> Search(T key) {
            BSTTreeNode<T> current = _Root;
            while (current != null && current.Value.CompareTo(key) != 0) {
                if (current.Value.CompareTo(key) > 0) {
                    current = current.Left;
                } else {
                    current = current.Right;
                }
            }
            return current;
        }

        public BSTTreeNode<T> Maximum() {
            if (_Root == null)
                throw new ArgumentNullException();

            return _Maximum(_Root);
        }

        public BSTTreeNode<T> Minimum() {
            if (_Root == null)
                throw new ArgumentNullException();

            return _Minimum(_Root);
        }

        public BSTTreeNode<T> Successor(BSTTreeNode<T> node) {
            if (node == null)
                throw new ArgumentNullException();
            
            if (node.Right != null) {
                return _Minimum(node.Right);
            }

            var current = node;
            var parent = node.Parent;
            while (parent != null && parent.Right == current) {
                current = parent;
                parent = parent.Parent;
            }
            return parent;
        }

        public BSTTreeNode<T> Predecessor(BSTTreeNode<T> node) {
            if (node == null)
                throw new ArgumentNullException();

            if (node.Left != null) {
                return _Maximum(node.Left);
            }

            var current = node;
            var parent = node.Parent;
            while (parent != null && parent.Left == current) {
                current = parent;
                parent = parent.Parent;
            }
            return parent;
        }

        public BSTTreeNode<T> Root { 
            get {
                return _Root; 
            }
        }

        private BSTTreeNode<T> _Maximum(BSTTreeNode<T> node) {
            BSTTreeNode<T> current = node;
            while (current.Right != null) {
                current = current.Right;
            }
            return current;
        }

        private BSTTreeNode<T> _Minimum(BSTTreeNode<T> node) {
            BSTTreeNode<T> current = node;
            while (current.Left != null) {
                current = current.Left;
            }
            return current;
        }

        private BSTTreeNode<T> _Root;
    }

    public class BSTTreeNode<T> where T : IComparable<T> {
        public BSTTreeNode(T t) => Value = t;

        public T Value { get; set; }
        public BSTTreeNode<T> Left { get; set; }
        public BSTTreeNode<T> Right { get; set; }
        public BSTTreeNode<T> Parent { get; set; }
    }
}
