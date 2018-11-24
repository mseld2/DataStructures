using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sorting;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class MinHeapTests
    {
        private MinHeap _heap;

        [TestInitialize]
        public void Setup()
        {
            int[] ary = new int[] { 13, 3, 4, 12, 14, 10, 5, 1, 8, 2, 7, 9, 11, 6, 18 };
            _heap = new MinHeap(ary);
        }

        [TestMethod]
        public void Sort()
        {
            int[] actual = _heap.Sort();
            int[] expected = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 18 };

            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        [TestMethod]
        public void Insert()
        {
            _heap.Insert(-3);
            int[] actual = _heap.Sort();
            int[] expected = new int[] { -3, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 18 };

            Assert.IsTrue(actual.SequenceEqual(expected));
        }
    }
}
