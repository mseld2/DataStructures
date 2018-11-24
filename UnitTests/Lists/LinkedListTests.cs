using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lists;

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
                _linkedList.InsertBack(item);
            }
        }

        [TestMethod]
        public void RemoveFront()
        {
            _linkedList.RemoveFront();

            Assert.AreEqual("[4, 12, 14, 10, 5, 1, 8, 2, 7, 9, 11, 6, 18]", _linkedList.ToString());
        }

        [TestMethod]
        public void RemoveBack()
        {
            _linkedList.RemoveBack();

            Assert.AreEqual("[3, 4, 12, 14, 10, 5, 1, 8, 2, 7, 9, 11, 6]", _linkedList.ToString());
        }

        [TestMethod]
        public void RemoveBefore()
        {
            LinkedListNode<int> nextNode = _linkedList.Find(5);
            _linkedList.RemoveBefore(nextNode);

            Assert.AreEqual("[3, 4, 12, 14, 5, 1, 8, 2, 7, 9, 11, 6, 18]", _linkedList.ToString());
        }

        [TestMethod]
        public void RemoveAfter()
        {
            LinkedListNode<int> previousNode = _linkedList.Find(5);
            _linkedList.RemoveAfter(previousNode);

            Assert.AreEqual("[3, 4, 12, 14, 10, 5, 8, 2, 7, 9, 11, 6, 18]", _linkedList.ToString());
        }

        [TestMethod]
        public void InsertFront()
        {
            _linkedList.InsertFront(27);

            Assert.AreEqual("[27, 3, 4, 12, 14, 10, 5, 1, 8, 2, 7, 9, 11, 6, 18]", _linkedList.ToString());
        }

        [TestMethod]
        public void InsertAfter()
        {
            LinkedListNode<int> previousNode = _linkedList.Find(5);
            _linkedList.InsertBefore(27, previousNode);

            Assert.AreEqual("[3, 4, 12, 14, 10, 5, 27, 1, 8, 2, 7, 9, 11, 6, 18]", _linkedList.ToString());
        }

        [TestMethod]
        public void InsertBefore()
        {
            LinkedListNode<int> nextNode = _linkedList.Find(5);
            _linkedList.InsertAfter(27, nextNode);

            Assert.AreEqual("[3, 4, 12, 14, 10, 27, 5, 1, 8, 2, 7, 9, 11, 6, 18]", _linkedList.ToString());
        }
   
        [TestMethod]
        public void Reverse()
        {
            _linkedList.Reverse();

            Assert.AreEqual("[18, 6, 11, 9, 7, 2, 8, 1, 5, 10, 14, 12, 4, 3]", _linkedList.ToString());
        }
    }
}
