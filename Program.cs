using System;

namespace Dijkstra_Git
{
    internal class Program
    {
        private static int V; // Number of vertices
        private static int[] parents; // Contains parent of each vertex
        
        public static void Main(string[] args)
        {
            int[,] graph = getGraph();
        }
        
        static int[,] getGraph() // Gets number of vertices and adjacent matrix from user
        {
            V = Convert.ToInt32(Console.ReadLine());
            int[,] graph = new int[V, V];
            for (int i = 0; i < V; i++)
            {
                String line = Console.ReadLine();
                String[] nums = line.Split(' ');
                for (int j = 0; j < V; j++)
                {
                    graph[i, j] = Int16.Parse(nums[j]);
                }
            }

            return graph;
        }

        static int minDistance(int[] dist, bool[] sptSet, int V)
        {
            int min = int.MaxValue, min_index = -1;

            for (int v = 0; v < V; v++)
            {
                if (sptSet[v] == false && dist[v] <= min)
                {
                    min = dist[v];
                    min_index = v;
                }
            }

            return min_index;
        }

        static void dijkstra(int[,] graph, int src)
        {
            int[] dist = new int[V];

            bool[] sptSet = new bool[V];

            for (int i = 0; i < V; i++)
            {
                dist[i] = int.MaxValue;
                sptSet[i] = false;
            }

            dist[src] = 0;

            for (int count = 0; count < V - 1; count++)
            {
                int u = minDistance(dist, sptSet, V);

                sptSet[u] = true;

                for (int v = 0; v < V; v++)
                {
                    if (!sptSet[v] && graph[u, v] != 0 && dist[u] != int.MaxValue && dist[u] + graph[u, v] < dist[v])
                    {
                        dist[v] = dist[u] + graph[u, v];
                        parents[v] = u;
                    }
                }
            }
        }
    }
}