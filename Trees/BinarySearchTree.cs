using System;

namespace Trees
{
    public enum TraversalType
    {
        InOrder = 1,
        PostOrder = 2,
        PreOrder = 3
    }

    public class BinarySearchTree<T> where T: IComparable<T>
    {
        private BinarySearchTreeNode<T> _root;

        // Count of nodes in tree
        public int Size => Count(_root);

        // Distance to lowest leaf
        public int Height => Depth(_root);

        // Insert new item into tree. Allows duplicates
        public void Insert(T data)
        {
            _root = Insert(_root, data);
        }

        // Find whether value is in tree
        public bool Find(T data)
        {
            return Find(_root, data);
        }

        public void Remove(T data)
        {
            Remove(_root, data);
        }

        public void Traverse(Action<T> func, TraversalType traversalType = TraversalType.InOrder)
        {
            switch(traversalType)
            {
                case TraversalType.InOrder:
                    InOrderTraversal(func, _root);
                    break;
                case TraversalType.PreOrder:
                    PreOrderTraversal(func, _root);
                    break;
                case TraversalType.PostOrder:
                    PostOrderTraversal(func, _root);
                    break;
            }
        }

        // O(h). Returns right most leaf value
        public T  Maximum()
        {
            if(_root == null)
            {
                return default(T);
            }

            var node = _root;
            while(node.HasRightChild)
            {
                node = node.Right;
            }

            return node.Data;
        }

        // O(h). Returns left most leaf value
        public T Minimum()
        {
            if(_root == null)
            {
                return default(T);
            }

            var minimumNode = MinimumNode(_root);

            return minimumNode.Data;
        }

        public override string ToString()
        {
            return ToString(_root);
        }

        private string ToString(BinarySearchTreeNode<T> root)
        {
            if(root == null)
            {
                return ".";
            }

            if(root.IsLeaf)
            {
                return root.ToString();
            }

            return $"{ToString(root.Left)} <- {root.ToString()} -> {ToString(root.Right)}";
        }

        // O(n).
        private void InOrderTraversal(Action<T> func, BinarySearchTreeNode<T> root)
        {
            if (root.HasLeftChild)
            {
                InOrderTraversal(func, root.Left);
            }

            func(root.Data);

            if (root.HasRightChild)
            {
                InOrderTraversal(func, root.Right);
            }
        }

        // O(n).
        private void PostOrderTraversal(Action<T> func, BinarySearchTreeNode<T> root)
        {
            if (root.HasLeftChild)
            {
                PostOrderTraversal(func, root.Left);
            }

            if (root.HasRightChild)
            {
                PostOrderTraversal(func, root.Right);
            }

            func(root.Data);
        }

        // O(n).
        private void PreOrderTraversal(Action<T> func, BinarySearchTreeNode<T> root)
        {
            func(root.Data);

            if(root.HasLeftChild)
            {
                PreOrderTraversal(func, root.Left);
            }

            if(root.HasRightChild)
            {
                PreOrderTraversal(func, root.Right);
            }
        }

        // O(h). Inserts new value into tree
        private BinarySearchTreeNode<T> Insert(BinarySearchTreeNode<T> root, T data)
        {
            if(root == null)
            {
                return new BinarySearchTreeNode<T>(data);
            }

            if (data.CompareTo(root.Data) < 0)
            {
                root.Left = Insert(root.Left, data);
            }
            else
            {
                root.Right = Insert(root.Right, data);
            }

            return root;
        }

        // O(h). Find whether the specified value exists in tree
        private bool Find(BinarySearchTreeNode<T> root, T data)
        {
            if(root == null)
            {
                return false;
            }

            int cmp = data.CompareTo(root.Data);

            if(cmp == 0)
            {
                return true;
            }

            if(cmp < 0)
            {
                return Find(root.Left, data);
            }

            return Find(root.Right, data);
        }

        // O(n). Returns the number of nodes in the tree
        private int Count(BinarySearchTreeNode<T> root)
        {
            if(root == null)
            {
                return 0;
            }

            if(root.IsLeaf)
            {
                return 1;
            }

            return Count(root.Left) + 1 + Count(root.Right);
        }

        // O(n). Returns the height of the tree
        private int Depth(BinarySearchTreeNode<T> root)
        {
            if(root == null)
            {
                return -1;
            }

            if(root.IsLeaf)
            {
                return 1;
            }

            return 1 + Max(Depth(root.Left), Depth(root.Right));
        }

        private BinarySearchTreeNode<T> MinimumNode(BinarySearchTreeNode<T> root)
        {
            var node = root;
            while (node.HasLeftChild)
            {
                node = node.Left;
            }

            return node;
        }

        // O(h). Remove a value from the tree
        private BinarySearchTreeNode<T> Remove(BinarySearchTreeNode<T> root, T data)
        {
            if(root == null)
            {
                return root;
            }

            int cmp = data.CompareTo(root.Data);
            if(cmp < 0)
            {
                root.Left = Remove(root.Left, data);
            } 
            else if(cmp > 0)
            {
                root.Right = Remove(root.Right, data);
            }
            else
            {
                if(!root.HasLeftChild)
                {
                    return root.Right;
                }
                if (!root.HasRightChild)
                {
                    return root.Left;
                }

                var node = MinimumNode(root.Right);
                root.Data = node.Data;


                root.Right = Remove(root.Right, node.Data);
            }

            return root;
        }

        private int Max(int x, int y)
        {
            return x > y ? x : y;
        }

        class BinarySearchTreeNode<U>
        {
            internal BinarySearchTreeNode<U> Left { get; set; }
            internal BinarySearchTreeNode<U> Right { get; set; }
            internal U Data { get; set; }

            internal bool IsLeaf => Right == null && Left == null;

            internal bool HasLeftChild => Left != null;

            internal bool HasRightChild => Right != null;

            internal BinarySearchTreeNode(U data, BinarySearchTreeNode < U> left = null, 
                BinarySearchTreeNode<U> right = null)
            {
                if (data == null)
                {
                    throw new ArgumentNullException("data");
                }

                Data = data;
                Left = left;
                Right = right;
            }

            public override string ToString()
            {
                return Data.ToString();
            }
        }
    }
}
