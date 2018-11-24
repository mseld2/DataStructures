using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sorting;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class MaxHeapTests
    {
        private MaxHeap _heap;

        [TestInitialize]
        public void Setup()
        {
            int[] ary = new int[] { 13, 3, 4, 12, 14, 10, 5, 1, 8, 2, 7, 9, 11, 6, 18 };
            _heap = new MaxHeap(ary);
        }

        [TestMethod]
        public void Sort()
        {
            int[] actual = _heap.Sort();
            int[] expected = new int[] { 18, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        [TestMethod]
        public void Insert()
        {
            _heap.Insert(-3);
            int[] actual = _heap.Sort();
            int[] expected = new int[] { 18, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, -3 };

            Assert.IsTrue(actual.SequenceEqual(expected));
        }
    }
}
