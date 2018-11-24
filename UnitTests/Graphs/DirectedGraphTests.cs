using Microsoft.VisualStudio.TestTools.UnitTesting;
using Graphs;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class DirectedGraphTests
    {
        private DirectedGraph<int> _graph;

        [TestInitialize]
        public void Setup()
        {
            _graph = new DirectedGraph<int>();
            _graph.AddEdge(1, 2);
            _graph.AddEdge(2, 5);
            _graph.AddEdge(1, 5);
            _graph.AddEdge(2, 3);
            _graph.AddEdge(1, 6);
            _graph.AddEdge(5, 4);
            _graph.AddEdge(3, 4);
        }

        [TestMethod]
        public void FindAllPaths()
        {
            List<List<int>> actual = _graph.FindAllPaths(1, 4);

            List<List<int>> expected = new List<List<int>>()
            {
                new List<int>() { 1, 2, 5, 4 },
                new List<int>() { 1, 2, 3, 4 },
                new List<int>() { 1, 5, 4 }
            };

            int index = 0;
            foreach(List<int> path in actual)
            {
                Assert.IsTrue(path.SequenceEqual(expected[index++]));
            }
        }

        [TestMethod]
        public void ShortestPath()
        {
            List<int> actual = _graph.ShortestPath(1, 4);

            List<int> expected = new List<int>() { 1, 5, 4 };

            Assert.IsTrue(expected.SequenceEqual(actual));
        }
    }
}
