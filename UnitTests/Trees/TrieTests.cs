using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trees;

namespace UnitTests
{
    [TestClass]
    public class TrieTests
    {
        private Trie _trie;

        [TestInitialize]
        public void Setup()
        {
            _trie = new Trie();
            _trie.Add("bear");
            _trie.Add("bell");
            _trie.Add("bid");
            _trie.Add("bull");
            _trie.Add("buy");
            _trie.Add("sell");
            _trie.Add("stock");
            _trie.Add("stop");
        }

        [TestMethod]
        public void FoundFalse()
        {
            Assert.IsFalse(_trie.Find("fox"));
        }

        [TestMethod]
        public void FoundTrue()
        {
            Assert.IsTrue(_trie.Find("bull"));
        }
    }
}
