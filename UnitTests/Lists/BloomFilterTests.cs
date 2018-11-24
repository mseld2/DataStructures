using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lists;

namespace UnitTests
{
    [TestClass]
    public class BloomFilterTests
    {
        private BloomFilter<string> _bloomFilter;

        [TestInitialize]
        public void Setup()
        {
            _bloomFilter = new BloomFilter<string>();
            _bloomFilter.Insert("p-p-poker face");
            _bloomFilter.Insert("born this way");
            _bloomFilter.Insert("bad romance");
        }

        [TestMethod]
        public void ContainsFalse()
        {
            Assert.IsFalse(_bloomFilter.Contains("evolution"));
        }

        [TestMethod]
        public void ContainsTrue()
        {
            Assert.IsTrue(_bloomFilter.Contains("born this way"));
        }
    }
}
