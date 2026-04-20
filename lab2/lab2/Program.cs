using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphTraversalLab11
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // --- НЕОРІЄНТОВАНИЙ ГРАФ ---
            var adjUndir = new Dictionary<char, List<char>> {
                {'a', new List<char>{'b','c','e'}}, {'b', new List<char>{'a','d','e'}},
                {'c', new List<char>{'a','f','g'}}, {'d', new List<char>{'b','f','g'}},
                {'e', new List<char>{'a','b','f'}}, {'f', new List<char>{'c','d','e','g'}},
                {'g', new List<char>{'c','d','f'}}
            };

            // --- ОРГРАФ ---
            var adjDir = new Dictionary<char, List<char>> {
                {'a', new List<char>{'a','b','c','e'}}, {'b', new List<char>{'c','d'}},
                {'c', new List<char>{'c','f'}},         {'d', new List<char>{'h'}},
                {'e', new List<char>{'h'}},             {'f', new List<char>{'h'}},
                {'h', new List<char>{}}
            };

            Console.WriteLine("=== ЛАБОРАТОРНА РОБОТА 2. ===");

            Console.WriteLine("\n--- НЕОРІЄНТОВАНИЙ ГРАФ ---");
            RunTraversals(adjUndir, 'a');

            Console.WriteLine("\n--- ОРГРАФ ---");
            RunTraversals(adjDir, 'a');

            Console.ReadKey();
        }

        static void RunTraversals(Dictionary<char, List<char>> graph, char start)
        {
            Console.Write("DFS: ");
            DFS(start, new HashSet<char>(), graph);
            Console.WriteLine();

            Console.Write("BFS: ");
            BFS(start, graph);
            Console.WriteLine();
        }

        static void DFS(char v, HashSet<char> visited, Dictionary<char, List<char>> graph)
        {
            visited.Add(v);
            Console.Write(v + " ");
            foreach (var neighbor in graph[v].OrderBy(x => x))
                if (!visited.Contains(neighbor)) DFS(neighbor, visited, graph);
        }

        static void BFS(char start, Dictionary<char, List<char>> graph)
        {
            var visited = new HashSet<char> { start };
            var queue = new Queue<char>();
            queue.Enqueue(start);
            while (queue.Count > 0)
            {
                char v = queue.Dequeue();
                Console.Write(v + " ");
                foreach (var neighbor in graph[v].OrderBy(x => x))
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
            }
        }
    }
}