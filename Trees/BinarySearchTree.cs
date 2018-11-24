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

        public void Insert(T data)
        {
            _root = Insert(_root, data);
        }

        public bool Find(T data)
        {
            return Find(_root, data);
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

        private void InOrderTraversal(Action<T> func, BinarySearchTreeNode<T> root)
        {
            if (root.Left != null)
            {
                InOrderTraversal(func, root.Left);
            }

            func(root.Data);

            if (root.Right != null)
            {
                InOrderTraversal(func, root.Right);
            }
        }

        private void PostOrderTraversal(Action<T> func, BinarySearchTreeNode<T> root)
        {
            if (root.Left != null)
            {
                PostOrderTraversal(func, root.Left);
            }

            if (root.Right != null)
            {
                PostOrderTraversal(func, root.Right);
            }

            func(root.Data);
        }

        private void PreOrderTraversal(Action<T> func, BinarySearchTreeNode<T> root)
        {
            func(root.Data);

            if(root.Left != null)
            {
                PreOrderTraversal(func, root.Left);
            }

            if(root.Right != null)
            {
                PreOrderTraversal(func, root.Right);
            }
        }

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

        class BinarySearchTreeNode<U>
        {
            internal BinarySearchTreeNode<U> Left { get; set; }
            internal BinarySearchTreeNode<U> Right { get; set; }
            internal U Data { get; private set; }

            internal BinarySearchTreeNode(U data, BinarySearchTreeNode<U> left = null,
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
        }
    }
}
