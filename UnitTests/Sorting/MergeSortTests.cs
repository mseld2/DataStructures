using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sorting;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class MergeSortTests
    {
        [TestMethod]
        public void SortInOrder()
        {
            int[] ary = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 18 };
            MergeSort<int> sorter = new MergeSort<int>();
            sorter.Sort(ary);

            int[] expected = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 18 };
            Assert.IsTrue(ary.SequenceEqual(expected));
        }

        [TestMethod]
        public void SortReverse()
        {
            int[] ary = new int[] { 18, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            MergeSort<int> sorter = new MergeSort<int>();
            sorter.Sort(ary);

            int[] expected = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 18 };
            Assert.IsTrue(ary.SequenceEqual(expected));
        }

        [TestMethod]
        public void SortMixed()
        {
            int[] ary = new int[] { 13, 3, 4, 12, 14, 10, 5, 1, 8, 2, 7, 9, 11, 6, 18 };
            MergeSort<int> sorter = new MergeSort<int>();
            sorter.Sort(ary);

            int[] expected = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 18 };
            Assert.IsTrue(ary.SequenceEqual(expected));
        }
    }
}
