using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lists;

namespace UnitTests
{
    [TestClass]
    public class HashTableTests
    {
        private HashTable<string, int> _hashTable;

        [TestInitialize]
        public void Setup()
        {
            _hashTable = new HashTable<string, int>();
            _hashTable.Insert("one", 1);
            _hashTable.Insert("uno", 1);
            _hashTable.Insert("two", 2);
            _hashTable.Insert("dos", 2);
            _hashTable.Insert("three", 3);
            _hashTable.Insert("tres", 3);
            _hashTable.Insert("four", 4);
            _hashTable.Insert("cuatro", 4);
            _hashTable.Insert("five", 5);
            _hashTable.Insert("cinco", 5);
            _hashTable.Insert("six", 6);
            _hashTable.Insert("seis", 6);
            _hashTable.Insert("seven", 7);
            _hashTable.Insert("siete", 7);
            _hashTable.Insert("eight", 8);
            _hashTable.Insert("ocho", 8);
            _hashTable.Insert("nine", 9);
            _hashTable.Insert("nueve", 9);
        }

        [TestMethod]
        public void GetValue()
        {
            Assert.AreEqual(7, _hashTable.GetValue("siete"));
        }

        [TestMethod]
        public void Insert()
        {
            _hashTable.Insert("cero", 0);

            Assert.AreEqual(0, _hashTable.GetValue("cero"));
        }

        [TestMethod]
        public void Remove()
        {
            _hashTable.Remove("six");

            Assert.AreEqual(default(int), _hashTable.GetValue("six"));
        }
    }
}
