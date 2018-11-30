using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lists;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class LinkedListTests
    {
        private LinkedList<int> _linkedList;

        [TestInitialize]
        public void Setup()
        {
            _linkedList = new LinkedList<int>();
            int[] ary = new int[] { 3, 4, 12, 14, 10, 5, 1, 8, 2, 7, 9, 11, 6, 18 };
            foreach(int item in ary)
            {
                _linkedList.Add(item);
            }
        }

        [TestMethod]
        public void Pop()
        {
            _linkedList.Pop();
            int[] actual = _linkedList.ToArray();
            int[] expected = new int[] { 4, 12, 14, 10, 5, 1, 8, 2, 7, 9, 11, 6, 18 };

            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        [TestMethod]
        public void RemoveLast()
        {
            _linkedList.RemoveLast();
            int[] actual = _linkedList.ToArray();
            int[] expected = new int[] { 3, 4, 12, 14, 10, 5, 1, 8, 2, 7, 9, 11, 6 };

            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        [TestMethod]
        public void RemoveAtFront()
        {
            _linkedList.RemoveAt(0);
            int[] actual = _linkedList.ToArray();
            int[] expected = new int[] { 4, 12, 14, 10, 5, 1, 8, 2, 7, 9, 11, 6, 18 };

            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        [TestMethod]
        public void RemoveAtBack()
        {
            _linkedList.RemoveAt(_linkedList.Count - 1);
            int[] actual = _linkedList.ToArray();
            int[] expected = new int[] { 3, 4, 12, 14, 10, 5, 1, 8, 2, 7, 9, 11, 6 };

            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        [TestMethod]
        public void RemoveAt()
        {
            _linkedList.RemoveAt(5);
            int[] actual = _linkedList.ToArray();
            int[] expected = new int[] { 3, 4, 12, 14, 10, 1, 8, 2, 7, 9, 11, 6, 18 };

            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        [TestMethod]
        public void IndexerGetFront()
        {
            Assert.AreEqual(_linkedList[0], 3);
        }

        [TestMethod]
        public void IndexerGetBack()
        {
            Assert.AreEqual(_linkedList[_linkedList.Count - 1], 18);
        }

        [TestMethod]
        public void IndexerGet()
        {
            int val = _linkedList[5];
            Assert.AreEqual(_linkedList[5], 5);
        }

        [TestMethod]
        public void IndexerSetFront()
        {
            _linkedList[0] = -3;
            Assert.AreEqual(_linkedList[0], -3);
        }

        [TestMethod]
        public void IndexerSetBack()
        {
            _linkedList[_linkedList.Count - 1] = 27;
            Assert.AreEqual(_linkedList[_linkedList.Count - 1], 27);
        }

        [TestMethod]
        public void IndexerSet()
        {
            _linkedList[5] = 55;
            Assert.AreEqual(_linkedList[5], 55);
        }

        [TestMethod]
        public void InsertAtFront()
        {
            _linkedList.InsertAt(-3, 0);

            int[] expected = new int[] { -3, 3, 4, 12, 14, 10, 5, 1, 8, 2, 7, 9, 11, 6, 18 };
            Assert.IsTrue(expected.SequenceEqual(_linkedList.ToArray()));
        }

        [TestMethod]
        public void InsertAtBack()
        {
            _linkedList.InsertAt(27, _linkedList.Count);

            int[] expected = new int[] { 3, 4, 12, 14, 10, 5, 1, 8, 2, 7, 9, 11, 6, 18, 27 };
            Assert.IsTrue(expected.SequenceEqual(_linkedList.ToArray()));
        }

        [TestMethod]
        public void InsertAt()
        {
            _linkedList.InsertAt(55, 5);

            int[] expected = new int[] { 3, 4, 12, 14, 10, 55, 5, 1, 8, 2, 7, 9, 11, 6, 18 };
            Assert.IsTrue(expected.SequenceEqual(_linkedList.ToArray()));
        }

        [TestMethod]
        public void RemoveFront()
        {
            _linkedList.Remove(3);

            int[] expected = new int[] { 4, 12, 14, 10, 5, 1, 8, 2, 7, 9, 11, 6, 18 };
            Assert.IsTrue(expected.SequenceEqual(_linkedList.ToArray()));
        }

        [TestMethod]
        public void RemoveBack()
        {
            _linkedList.Remove(18);

            int[] expected = new int[] { 3, 4, 12, 14, 10, 5, 1, 8, 2, 7, 9, 11, 6 };
            Assert.IsTrue(expected.SequenceEqual(_linkedList.ToArray()));
        }

        [TestMethod]
        public void Remove()
        {
            _linkedList.Remove(5);

            int[] expected = new int[] { 3, 4, 12, 14, 10, 1, 8, 2, 7, 9, 11, 6, 18 };
            Assert.IsTrue(expected.SequenceEqual(_linkedList.ToArray()));
        }
    }
}
