using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP
{
    public class Program
    {
        public class Graph
        {
            private int[,] adjacencyMatrix;
            private int numVertices;

            public Graph(int numVertices)
            {
                this.numVertices = numVertices;
                adjacencyMatrix = new int[numVertices, numVertices];

            }

            public void AddEdge(int
         u, int v, int weight)
            {
                adjacencyMatrix[u, v] = weight;
                adjacencyMatrix[v, u] = weight;
            }

            public List<int> GreedyTSP()
            {
                var edges = GetSortedEdges();

                var visited = new HashSet<int>();
                var path = new List<int>();
                int startVertex = 0;
                visited.Add(startVertex);
                path.Add(startVertex);

                while (visited.Count < numVertices)
                {
                    bool foundEdge = false;
                    foreach (var edge in edges)
                    {
                        int u = edge.Item1;
                        int v = edge.Item2;
                        if (visited.Contains(u) && !visited.Contains(v))
                        {
                            path.Add(v);
                            visited.Add(v);
                            foundEdge = true;
                            break;
                        }
                    }

                    if (!foundEdge)
                    {
                        Console.WriteLine("Do thi khong co chu trinh Hamilton");
                        return new List<int>();
                    }
                }

                return path;
            }

            private List<(int, int, int)> GetSortedEdges()
            {
                var edges = new List<(int, int, int)>();
                for (int i = 0; i < numVertices; i++)
                {
                    for (int j = i + 1; j < numVertices; j++)
                    {
                        if (adjacencyMatrix[i, j] != 0)
                        {
                            edges.Add((i, j, adjacencyMatrix[i, j]));
                        }
                    }
                }
                return edges.OrderBy(e => e.Item3).ToList(); 
            }
            public static void Main(string[] args)
            {

                Graph graph = new Graph(5);
                graph.AddEdge(0, 1, 2);
                graph.AddEdge(0, 2, 5);
                graph.AddEdge(1, 2, 4);
                graph.AddEdge(1, 3, 6);
                graph.AddEdge(2, 3, 3);
                graph.AddEdge(2, 4, 2);
                graph.AddEdge(3, 4, 5);
                var path = graph.GreedyTSP();
                if (path.Count > 0)
                {
                    Console.WriteLine("Chu trinh Hamilton:");
                    Console.WriteLine(string.Join(" -> ", path));
                }
                else
                {
                    Console.WriteLine("Do thi khong co chu trinh Hamilton");
                }
                Console.ReadKey();
            }
        }
    }
}