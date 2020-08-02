using System;
using System.Collections.Generic;

namespace Dijkstra_Git
{
    internal class Program
    {
        private static int V; // Number of vertices
        private static int[] parents; // Contains parent of each vertex
        private static List<int>[] tree; // Final tree that we have after dijkstra
        private static int[] counter; // Final answer. Times we have to pass through a vertex
        
        public static void Main(string[] args)
        {
            int[,] graph = getGraph();
            int source = 0; // We consider vertex 0 as source

            initStaticVars();
            dijkstra(graph, source);
            createTree();
            numberOfNodesOfSubstree(source);
            printCounter();
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
        
        static void initStaticVars()
        {
            parents = new int[V];
            counter = new int[V];
            tree = new List<int>[V];
            for (int i = 0; i < V; i++)
            {
                tree[i] = new List<int>();
            }
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
        
        static void createTree()
        {
            for (int i = 0; i < V; i++)
            {
                tree[parents[i]].Add(i); // i is child of parents[i]
            }
        }
        
        static void numberOfNodesOfSubstree(int source)
        {
            counter[source] = 1; // At least we have to pass a node once to get to itself
            
            foreach (int child in tree[source])
            {

                if (child == source)
                {
                    continue;
                }
                
                numberOfNodesOfSubstree(child);

                counter[source] += counter[child];
            }
        }
        
        
        static void printCounter()
        {
            for (int i = 0; i < V; i++)
            {
                Console.Write(counter[i] + " ");
            }
        }
        
    }
}