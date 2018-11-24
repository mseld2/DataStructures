using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphs
{
    // Directed graph using and adjacency list.
    public class DirectedGraph<T> where T: IEquatable<T>
    {
        private Dictionary<T, List<T>> _adjList = new Dictionary<T, List<T>>();

        // List of all the vertices.
        public T[] Vertices
        {
            get { return _adjList.Keys.ToArray(); }
        }

        // Add a vertex.
        public void AddVertex(T vertex)
        {
            if (!_adjList.ContainsKey(vertex))
            {
                _adjList.Add(vertex, new List<T>());
            }
        }

        // Add an edge from u to v.
        public void AddEdge(T u, T v)
        {
            if (!_adjList.ContainsKey(u))
            {
                _adjList.Add(u, new List<T>());
            }
            _adjList[u].Add(v);

            if (!_adjList.ContainsKey(v))
            {
                _adjList.Add(v, new List<T>());
            }
        }

        // Finds the shortest path from start to end using 
        // using the breadth-first-search algorithm.
        public List<T> ShortestPath(T start, T end)
        {
            if(!_adjList.ContainsKey(start))
            {
                throw new ArgumentException("Invalid starting vertex!");
            }

            Dictionary<T, T> edgeTo = BreadthFirstSearch(start);

            List<T> shortestPath = new List<T>();
            ShortestPath(start, end, edgeTo, shortestPath);

            return shortestPath;
        }

        // Finds all paths in the graph from start to end.
        public List<List<T>> FindAllPaths(T start, T end)
        {
            List<List<T>> paths = new List<List<T>>();

            FindAllPaths(start, end, new List<T>(), new Stack<T>(), paths);

            return paths;
        }

        private void FindAllPaths(T start, T end, List<T> visited, Stack<T> path, List<List<T>> paths)
        {
            visited.Add(start);
            path.Push(start);

            if(start.Equals(end))
            {
                List<T> p = path.ToList();
                p.Reverse();
                paths.Add(p);
            } 
            else
            {
                foreach(T v in _adjList[start])
                {
                    if(!visited.Contains(v))
                    {
                        FindAllPaths(v, end, visited, path, paths);
                    }
                }
            }

            path.Pop();
            visited.Remove(start);
        }

        private Dictionary<T, T> BreadthFirstSearch(T start)
        {
            Queue<T> queue = new Queue<T>();
            List<T> discovered = new List<T>();
            Dictionary<T, T> edgeTo = new Dictionary<T, T>();

            queue.Enqueue(start);
            discovered.Add(start);

            while (queue.Count > 0)
            {
                T u = queue.Dequeue();
                foreach (T v in _adjList[u])
                {
                    if (!discovered.Contains(v))
                    {
                        queue.Enqueue(v);
                        discovered.Add(v);
                        edgeTo.Add(v, u);
                    }
                }
            }

            return edgeTo;
        }

        // The breadth-first-search algorithm returns the path in reverse order.
        // This reverses the path.
        private void ShortestPath(T start, T end, Dictionary<T, T> edgeTo, List<T> path)
        {
            if(start.Equals(end))
            {
                path.Add(start);
            }
            else
            {
                ShortestPath(start, edgeTo[end], edgeTo, path);
                path.Add(end);
            }
        }
    }
}
