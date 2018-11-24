using Microsoft.VisualStudio.TestTools.UnitTesting;
using Graphs;
using System.Collections.Generic;
using System.Linq;
using System;

namespace UnitTests
{
    [TestClass]
    public class WeightedGraphTests
    {
        private WeightedGraph<int> _graph;

        [TestInitialize]
        public void Setup()
        {
            _graph = new WeightedGraph<int>();
            _graph.AddEdge(0, 1, 4);
            _graph.AddEdge(0, 7, 8);
            _graph.AddEdge(1, 7, 11);
            _graph.AddEdge(7, 6, 1);
            _graph.AddEdge(3, 5, 14);
            _graph.AddEdge(8, 2, 2);
            _graph.AddEdge(3, 4, 9);
            _graph.AddEdge(7, 8, 7);
            _graph.AddEdge(1, 2, 8);
            _graph.AddEdge(6, 5, 2);
            _graph.AddEdge(2, 3, 7);
            _graph.AddEdge(5, 4, 10);
            _graph.AddEdge(2, 5, 4);
            _graph.AddEdge(8, 6, 6);
        }

        [TestMethod]
        public void ShortestPath()
        {
            List<int> actual = _graph.ShortestPath(1, 4);
            List<int> expected = new List<int>() { 1, 2, 5, 4 };

            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void MinimumSpanningTree()
        {
            List<Tuple<int, int, int>> actual = _graph.MinimumSpanningTree();

            List<Tuple<int, int, int>> expected = new List<Tuple<int, int, int>>()
            {
                Tuple.Create(7, 6, 1),
                Tuple.Create(6, 5, 2),
                Tuple.Create(8, 2, 2),
                Tuple.Create(0, 1, 4),
                Tuple.Create(2, 5, 4),
                Tuple.Create(2, 3, 7),
                Tuple.Create(0, 7, 8),
                Tuple.Create(3, 4, 9)
            };

            Assert.IsTrue(expected.SequenceEqual(actual));
        }
    }
}
