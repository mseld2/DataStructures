using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trees;

namespace UnitTests
{
    [TestClass]
    public class BinarySearchTreeTests
    {
        private static List<int> _list = new List<int>();
        private BinarySearchTree<int> _tree;

        [TestInitialize]
        public void Setup()
        {
            int[] ary = new int[] { 13, 3, 4, 12, 14, 10, 5, 1, 8, 2, 7, 9, 11, 6, 18 };
            _tree = new BinarySearchTree<int>();

            foreach(int i in ary)
            {
                _tree.Insert(i);
            }

            _list.Clear();
        }

        [TestMethod]
        public void InOrderTraversal()
        {
            _tree.Traverse(Accumulator);
            List<int> expected = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 18 };

            Assert.IsTrue(expected.SequenceEqual(_list));
        }

        [TestMethod]
        public void PostOrderTraversal()
        {
            _tree.Traverse(Accumulator, TraversalType.PostOrder);

            List<int> expected = new List<int>() { 2, 1, 6, 7, 9, 8, 5, 11, 10, 12, 4, 3, 18, 14, 13 };

            Assert.IsTrue(expected.SequenceEqual(_list));
        }

        [TestMethod]
        public void PreOrderTraversal()
        {
            _tree.Traverse(Accumulator, TraversalType.PreOrder);

            List<int> expected = new List<int>() { 13, 3, 1, 2, 4, 12, 10, 5, 8, 7, 6, 9, 11, 14, 18 };

            Assert.IsTrue(expected.SequenceEqual(_list));
        }

        [TestMethod]
        public void FindTrue()
        {
            Assert.IsTrue(_tree.Find(10));
        }

        [TestMethod]
        public void FindFalse()
        {
            Assert.IsFalse(_tree.Find(27));
        }

        private static void Accumulator(int value)
        {
            _list.Add(value);
        }
    }
}
