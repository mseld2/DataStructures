using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trees;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class KdTreeTests
    {
        private KdTree _tree;

        [TestInitialize]
        public void Setup()
        {
            _tree = new KdTree(2);
            _tree.Insert(new int[] {1, -3});
            _tree.Insert(new int[] {1, 2});
            _tree.Insert(new int[] {2, 4});
            _tree.Insert(new int[] {3, 5});
        }

        [TestMethod]
        public void NearestOrigin()
        {
            int[] nearest = _tree.Nearest(new int[] {0, 0});

            Assert.IsTrue(nearest.SequenceEqual(new int[] {1, 2}));
        }

        [TestMethod]
        public void NearestOther()
        {
            int[] nearest = _tree.Nearest(new int[] {2, 3});

            Assert.IsTrue(nearest.SequenceEqual(new int[] {2, 4}));
        }
    }
}
