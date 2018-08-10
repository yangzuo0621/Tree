using System;
using System.Collections.Generic;

namespace Tree.BST {
    public class RedBlackTree<T> where T : IComparable<T> {

        public RedBlackTree() {
            _SentryNode = new RBTreeNode<T>(RBColor.BLACK);
            _Root = _SentryNode;
        }

        public RedBlackTree(IEnumerable<T> collections)
        {
            _SentryNode = new RBTreeNode<T>(RBColor.BLACK);
            _Root = _SentryNode;
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
            var parent = _SentryNode;
            var current = _Root;
            while (current != _SentryNode) {
                parent = current;
                if (current.Value.CompareTo(key) > 0) {
                    current = current.Left;
                } else {
                    current = current.Right;
                }
            }
            var node = new RBTreeNode<T>(key);
            node.Parent = parent;
            if (parent == _SentryNode) {
                _Root = node;
            } else {
                if (parent.Value.CompareTo(key) > 0) {
                    parent.Left = node;
                } else {
                    parent.Right = node;
                }
            }
            node.Left = _SentryNode;
            node.Right = _SentryNode;
            node.Color = RBColor.RED;

            RBInsertFixUp(node);
        }

        public void Delete(RBTreeNode<T> node) {
            RBTreeNode<T> delNode = null;
            if (node.Left == _SentryNode || node.Right == _SentryNode) {
                delNode = node;
            } else {
                delNode = Successor(node);
            }

            RBTreeNode<T> child = null;
            if (delNode.Left != _SentryNode) {
                child = delNode.Left;
            } else {
                child = delNode.Right;
            }

            child.Parent = delNode.Parent;

            if (delNode.Parent == _SentryNode) {
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

            if (delNode.Color == RBColor.BLACK) {
                RBDeleteFixUp(child);
            }
        }

        public RBTreeNode<T> Search(T key) {
            var current = _Root;
            while (current != _SentryNode && current.Value.CompareTo(key) != 0) {
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

        public void InOrder() {
            InOrderHelper(_Root);
        }

        private void InOrderHelper(RBTreeNode<T> root) {
            if (root != _SentryNode) {
                InOrderHelper(root.Left);
                Console.WriteLine(root.Value);
                InOrderHelper(root.Right);
            }
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

        private void LeftRotate(RBTreeNode<T> node) {
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

        private void RightRotate(RBTreeNode<T> node) {
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

        private void RBInsertFixUp(RBTreeNode<T> node) {
            var current = node;
            while (current.Parent.Color == RBColor.RED) {
                if (current.Parent == current.Parent.Parent.Left) {
                    var right = current.Parent.Parent.Right;
                    if (right.Color == RBColor.RED) {
                        current.Parent.Color = RBColor.BLACK;
                        right.Color = RBColor.BLACK;
                        current.Parent.Parent.Color = RBColor.RED;
                        current = current.Parent.Parent;
                    } else {
                        if (current == current.Parent.Right) {
                            current = current.Parent;
                            LeftRotate(current);
                        }
                        current.Parent.Color = RBColor.BLACK;
                        current.Parent.Parent.Color = RBColor.RED;
                        RightRotate(current.Parent.Parent);
                    }
                } else {
                    var left = current.Parent.Parent.Left;
                    if (left.Color == RBColor.RED) {
                        current.Parent.Color = RBColor.BLACK;
                        left.Color = RBColor.BLACK;
                        current.Parent.Parent.Color = RBColor.RED;
                        current = current.Parent.Parent;
                    } else {
                        if (current == current.Parent.Left) {
                            current = current.Parent;
                            RightRotate(current);
                        }
                        current.Parent.Color = RBColor.BLACK;
                        current.Parent.Parent.Color = RBColor.RED;
                        LeftRotate(current.Parent.Parent);
                    }
                }
            }
            _Root.Color = RBColor.BLACK;
        }

        private void RBDeleteFixUp(RBTreeNode<T> node) {
            while (node != _SentryNode && node.Color == RBColor.BLACK) {
                
            }
        }

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

        private RBTreeNode<T> _Root;
        private RBTreeNode<T> _SentryNode;
    }

    public class RBTreeNode<T> where T : IComparable<T> {
        public RBTreeNode() { }
        public RBTreeNode(T t) => (Value) = (t);
        public RBTreeNode(RBColor color) => (Color) = (color);
        public RBTreeNode(T t, RBColor color) => (Value, Color) = (t, color); 

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