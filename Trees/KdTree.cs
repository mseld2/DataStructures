using System;

namespace Trees
{
    public class KdTree
    {
        private readonly int _dimension;
        private KdTreeNode _root;
        private double _refereceDistance = double.MaxValue;
        private int[] _referenceValue;

        public KdTree(int dimension)
        {
            _dimension = dimension;
        }

        public void Insert(int[] value)
        {
            if(_root == null)
            {
                _root = new KdTreeNode(value);
                return;
            }

            Insert(value, _root, 0);
        }

        public int[] Nearest(int[] value)
        {
            if(_root == null)
            {
                return null;
            }

            _referenceValue = _root.Data;
            _refereceDistance = double.MaxValue;

            Nearest(value, _root, 0);

            return _referenceValue;
        }

        private void Nearest(int[] value, KdTreeNode root, int depth)
        {
            if(root == null)
            {
                return;
            }

            double refDist = Distance(value, root.Data);
            if(root.Right == null && root.Left == null)
            {
                if(refDist < _refereceDistance)
                {
                    _referenceValue = root.Data;
                    _refereceDistance = refDist;
                }

                return;
            }

            int cd = depth % _dimension;
            if(value[cd] <= root.Data[cd])
            {
                if(value[cd] - _referenceValue[cd] <= root.Data[cd])
                {
                    Nearest(value, root.Left, depth + 1);
                }
                if(value[cd] + _referenceValue[cd] > root.Data[cd])
                {
                    Nearest(value, root.Right, depth + 1);
                }
            }
            else
            {
                if (value[cd] + _referenceValue[cd] > root.Data[cd])
                {
                    Nearest(value, root.Right, depth + 1);
                }
                if (value[cd] - _referenceValue[cd] <= root.Data[cd])
                {
                    Nearest(value, root.Left, depth + 1);
                }
            }

            if(refDist < _refereceDistance)
            {
                _referenceValue = root.Data;
                _refereceDistance = refDist;
            }
        }

        private double Distance(int[] left, int[] right)
        {
            if(left.Length != right.Length)
            {
                return double.MaxValue;
            }

            double sum = 0.0;
            for(int dimension = 0; dimension < left.Length; ++dimension)
            {
                sum += Math.Pow(left[dimension] - right[dimension], 2.0);
            }

            return Math.Sqrt(sum);
        }

        private KdTreeNode Insert(int[] value, KdTreeNode root, int depth)
        {
            if (root == null)
            {
                return new KdTreeNode(value);
            }

            int cd = depth % _dimension;
            if (value[cd] <= root.Data[cd])
            {
                root.Left = Insert(value, root.Left, depth + 1);
            }
            else
            {
                root.Right = Insert(value, root.Right, depth + 1);
            }

            return root;
        }

        class KdTreeNode
        {
            internal int[] Data { get; set; }
            internal KdTreeNode Left { get; set; }
            internal KdTreeNode Right { get; set; }

            internal KdTreeNode(int[] data, KdTreeNode left = null, 
                KdTreeNode right = null)
            {
                Data = data ?? throw new ArgumentNullException("data");
                Left = left;
                Right = right;
            }
        }
    }
}
