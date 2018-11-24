using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphs
{ 
    // Weighted directed graph using an adjacency list.
    public class WeightedGraph<T> where T: IEquatable<T>
    {
        private Dictionary<T, List<Node<T>>> _adjList = new Dictionary<T, List<Node<T>>>();

        // List of all the vertices.
        public T[] Vertices
        {
            get { return _adjList.Keys.ToArray(); }
        }

        // Add a vertex.
        public void AddVertex(T vertex)
        {
            if(!_adjList.ContainsKey(vertex))
            {
                _adjList.Add(vertex, new List<Node<T>>());
            }
        }

        // Add an edge (Node) with cost from u to v.
        public void AddEdge(T u, T v, int cost)
        {
            if(!_adjList.ContainsKey(u))
            {
                _adjList.Add(u, new List<Node<T>>());
            }
            _adjList[u].Add(new Node<T>(v, cost));

            if(!_adjList.ContainsKey(v))
            {
                _adjList.Add(v, new List<Node<T>>());
            }
        }

        // Miniumum spanning tree using Kruska's algorithm. It processes the
        // edges in order of their weights, in asc order. It adds to the minimum
        // spanning tree each edge that doesn't form a cycle with the previously
        // added edges. 
        public List<Tuple<T, T, int>> MinimumSpanningTree()
        {
            List<Tuple<T, T, int>> path = new List<Tuple<T, T, int>>();
            List<Tuple<T, T, int>> edges = SortEdges();
            DisjointSetUnion<T> setUnion = new DisjointSetUnion<T>(Vertices);

            for(int idx = 0; idx < edges.Count; ++idx)
            {
                if(!setUnion.IsSameSubset(edges[idx].Item1, edges[idx].Item2))
                {
                    path.Add(edges[idx]);
                    setUnion.Union(edges[idx].Item1, edges[idx].Item2);
                }
            }

            return path;
        }

        // Shortest path from start to end using Dijkstra's algorithm.
        public List<T> ShortestPath(T start, T end)
        {
            Dictionary<T, int> distances = new Dictionary<T, int>();
            Dictionary<T, T> previousVertices = new Dictionary<T, T>();
            List<T> vertices = new List<T>();

            foreach(T vertex in Vertices)
            {
                distances.Add(vertex, int.MaxValue);
                previousVertices.Add(vertex, default(T));
                vertices.Add(vertex);
            }
            distances[start] = 0;

            while(vertices.Count > 0)
            {
                // Sort the list in asc order by distance from u to v.
                vertices.Sort((u, v) => distances[u] - distances[v]);
                T smallest = vertices[0];
                vertices.Remove(smallest);

                if(distances[smallest] == int.MaxValue)
                {
                    break;
                }

                // Update with smallest distance.
                foreach(Node<T> node in _adjList[smallest])
                {
                    int altDistance = distances[smallest] + node.Cost;
                    if(altDistance < distances[node.Vertex])
                    {
                        distances[node.Vertex] = altDistance;
                        previousVertices[node.Vertex] = smallest;
                    }
                }
            }

            // Following previousVertices gives the path in reverse order.
            // C# doesn't have a deque that I know of. I chose a LinkedList so
            // I can add items to the head of the list without having to shift all
            // of the elements. This makes O(1) time for insertion.
            T currentVertex = end;
            LinkedList<T> shortestPath = new LinkedList<T>();
            while(!previousVertices[currentVertex].Equals(default(T)))
            {
                shortestPath.AddFirst(currentVertex);
                currentVertex = previousVertices[currentVertex];
            }
            shortestPath.AddFirst(currentVertex);

            return shortestPath.ToList();
        }

        // Sort the edges in asc order by weight.
        private List<Tuple<T, T, int>> SortEdges()
        {
            if(_adjList.Count == 0)
            {
                return new List<Tuple<T, T, int>>();
            }

            List<Tuple<T, T, int>> edges = new List<Tuple<T, T, int>>();
            foreach(T vertex in Vertices)
            {
                foreach(Node<T> node in _adjList[vertex])
                {
                    edges.Add(Tuple.Create(vertex, node.Vertex, node.Cost));
                }
            }

            return edges.OrderBy(t => t.Item3).ToList();
        }

        // This is an edge node.
        class Node<U>
        {
            public U Vertex { get; set; }
            public int Cost { get; set; }

            public Node(U vertex, int cost)
            {
                Vertex = vertex;
                Cost = cost;
            }
        }

        class DisjointSetUnion<U> where U : IEquatable<T>
        {
            private readonly Subset<U>[] _subsets;
            private readonly U[] _vertices;

            internal DisjointSetUnion(U[] vertices)
            {
                _vertices = vertices;
                _subsets = new Subset<U>[_vertices.Length];
                for(int idx = 0; idx < _subsets.Length; ++idx)
                {
                    _subsets[idx] = new Subset<U>(_vertices[idx], 0);
                }
            }

            // Whether vertices x and y are in the same subset.
            internal bool IsSameSubset(U x, U y)
            {
                U u = Find(x, Array.IndexOf(_vertices, x));
                U v = Find(y, Array.IndexOf(_vertices, y));

                return u.Equals(v);
            }

            // Adds x and y to the same subset.
            internal void Union(U x, U y)
            {
                U u = Find(x, Array.IndexOf(_vertices, x));
                U v = Find(y, Array.IndexOf(_vertices, y));

                int uIndex = Array.IndexOf(_vertices, u);
                int vIndex = Array.IndexOf(_vertices, v);

                if(_subsets[uIndex].Rank >= _subsets[vIndex].Rank)
                {
                    _subsets[uIndex].Rank += _subsets[vIndex].Rank;
                    _subsets[vIndex].Parent = u;
                }
                else
                {
                    _subsets[vIndex].Rank += _subsets[uIndex].Rank;
                    _subsets[uIndex].Parent = v;
                }
            }

            // If the parent of vertex is itself, then it hasn't been traversed.
            // Otherwise it recursively searches until it finds the vertex's parent
            // or it reaches the root of the tree.
            private U Find(U vertex, int vertexIdx)
            {
                if(_subsets[vertexIdx].Parent.Equals(vertex))
                {
                    return vertex;
                }

                U parent = _subsets[vertexIdx].Parent;
                return Find(parent, Array.IndexOf(_vertices, parent));
            }

            class Subset<W>
            {
                public W Parent { get; set; }
                public int Rank { get; set; }

                internal Subset(W parent, int rank)
                {
                    Parent = parent;
                    Rank = rank;
                }
            }
        }
    }
}
