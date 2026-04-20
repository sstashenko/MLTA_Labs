using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphLab10
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            char[] vUndir = { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };
            int[,] eUndir = { { 0, 4 }, { 0, 1 }, { 0, 2 }, { 1, 4 }, { 1, 3 }, { 4, 5 }, { 5, 2 }, { 5, 3 }, { 5, 6 }, { 2, 6 }, { 3, 6 } };

            char[] vDir = { 'a', 'b', 'c', 'd', 'e', 'f', 'h' };
            int[,] eDir = { { 0, 0 }, { 0, 1 }, { 0, 2 }, { 0, 4 }, { 1, 2 }, { 1, 3 }, { 2, 2 }, { 2, 5 }, { 3, 6 }, { 4, 6 }, { 5, 6 } };

            Console.WriteLine("==========================================================");
            Console.WriteLine("ЛАБОРАТОРНА РОБОТА 1.(НЕОРІЄНТОВАНИЙ ГРАФ)");
            RunFullLab(vUndir, eUndir, false);

            Console.WriteLine("\n\n==========================================================");
            Console.WriteLine("ЛАБОРАТОРНА РОБОТА 1. (ОРІЄНТОВАНИЙ ГРАФ)");
            RunFullLab(vDir, eDir, true);

            Console.WriteLine("\nНатисніть будь-яку клавішу для завершення...");
            Console.ReadKey();
        }

        static void RunFullLab(char[] vertices, int[,] sourceEdges, bool isDirected)
        {
            int n = vertices.Length;
            int m = sourceEdges.GetLength(0);

            // ЗАВДАННЯ 1: Створення та зберігання 4-ма способами

            Console.WriteLine("\n>>> ЗАВДАННЯ 1: ПРЕДСТАВЛЕННЯ ТА ВИВІД ГРАФА <<<");

            // а) Матриця суміжності
            int[,] adjMatrix = new int[n, n];
            for (int i = 0; i < m; i++)
            {
                adjMatrix[sourceEdges[i, 0], sourceEdges[i, 1]] = 1;
                if (!isDirected) adjMatrix[sourceEdges[i, 1], sourceEdges[i, 0]] = 1;
            }
            PrintAdjMatrix(adjMatrix, vertices);

            // б) Матриця інцидентності
            int[,] incMatrix = new int[n, m];
            for (int j = 0; j < m; j++)
            {
                int u = sourceEdges[j, 0];
                int v = sourceEdges[j, 1];
                if (isDirected)
                {
                    if (u == v) incMatrix[u, j] = 2; // петля
                    else { incMatrix[u, j] = 1; incMatrix[v, j] = -1; }
                }
                else { incMatrix[u, j] = 1; incMatrix[v, j] = 1; }
            }
            PrintIncMatrix(incMatrix, vertices);

            // в) Список ребер
            List<Tuple<int, int>> edgeList = new List<Tuple<int, int>>();
            for (int i = 0; i < m; i++)
                edgeList.Add(new Tuple<int, int>(sourceEdges[i, 0], sourceEdges[i, 1]));

            Console.WriteLine("\nв) Список ребер (пари вершин):");
            foreach (var e in edgeList)
                Console.WriteLine($"({vertices[e.Item1]}, {vertices[e.Item2]})");

            // г) Список суміжності
            Dictionary<int, List<int>> adjList = AdjToAdjList(adjMatrix, n);
            Console.WriteLine("\nг) Список суміжності:");
            PrintAdjList(adjList, vertices);


            // ЗАВДАННЯ 2: Конвертації
            Console.WriteLine("\n>>> ЗАВДАННЯ 2: КОНВЕРТАЦІЇ МІЖ ПРЕДСТАВЛЕННЯМИ <<<");

            Console.WriteLine("1. Матриця суміжності -> Матриця інцидентності:");
            PrintIncMatrix(AdjToInc(adjMatrix, isDirected), vertices);

            Console.WriteLine("\n2. Матриця інцидентності -> Список ребер:");
            var res2 = IncToEdgeList(incMatrix, isDirected);
            foreach (var e in res2) Console.WriteLine($"({vertices[e.Item1]}, {vertices[e.Item2]})");

            Console.WriteLine("\n3. Матриця суміжності -> Список суміжності:");
            PrintAdjList(AdjToAdjList(adjMatrix, n), vertices);

            Console.WriteLine("\n4. Матриця інцидентності -> Матриця суміжності:");
            PrintAdjMatrix(IncToAdj(incMatrix, n, isDirected), vertices);

            Console.WriteLine("\n5. Матриця суміжності -> Список ребер:");
            var res5 = AdjToEdgeList(adjMatrix, isDirected);
            foreach (var e in res5) Console.WriteLine($"({vertices[e.Item1]}, {vertices[e.Item2]})");

            Console.WriteLine("\n6. Матриця інцидентності -> Список суміжності:");
            PrintAdjList(IncToAdjList(incMatrix, n, isDirected), vertices);
        }

        #region Методи конвертації та логіки

        static int[,] AdjToInc(int[,] adj, bool isDirected)
        {
            var edges = AdjToEdgeList(adj, isDirected);
            int n = adj.GetLength(0);
            int[,] inc = new int[n, edges.Count];
            for (int j = 0; j < edges.Count; j++)
            {
                int u = edges[j].Item1, v = edges[j].Item2;
                if (isDirected)
                {
                    if (u == v) inc[u, j] = 2;
                    else { inc[u, j] = 1; inc[v, j] = -1; }
                }
                else { inc[u, j] = 1; inc[v, j] = 1; }
            }
            return inc;
        }

        static List<Tuple<int, int>> IncToEdgeList(int[,] inc, bool isDirected)
        {
            List<Tuple<int, int>> edges = new List<Tuple<int, int>>();
            int n = inc.GetLength(0), m = inc.GetLength(1);
            for (int j = 0; j < m; j++)
            {
                int start = -1, end = -1;
                for (int i = 0; i < n; i++)
                {
                    if (isDirected)
                    {
                        if (inc[i, j] == 2) { start = i; end = i; break; }
                        if (inc[i, j] == 1) start = i;
                        if (inc[i, j] == -1) end = i;
                    }
                    else
                    {
                        if (inc[i, j] == 1) { if (start == -1) start = i; else end = i; }
                    }
                }
                if (start != -1 && end != -1) edges.Add(new Tuple<int, int>(start, end));
            }
            return edges;
        }

        static Dictionary<int, List<int>> AdjToAdjList(int[,] adj, int n)
        {
            var list = new Dictionary<int, List<int>>();
            for (int i = 0; i < n; i++)
            {
                list[i] = new List<int>();
                for (int j = 0; j < n; j++) if (adj[i, j] == 1) list[i].Add(j);
            }
            return list;
        }

        static int[,] IncToAdj(int[,] inc, int n, bool isDirected)
        {
            int[,] adj = new int[n, n];
            var edges = IncToEdgeList(inc, isDirected);
            foreach (var e in edges)
            {
                adj[e.Item1, e.Item2] = 1;
                if (!isDirected) adj[e.Item2, e.Item1] = 1;
            }
            return adj;
        }

        static List<Tuple<int, int>> AdjToEdgeList(int[,] adj, bool isDirected)
        {
            var edges = new List<Tuple<int, int>>();
            int n = adj.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                for (int j = (isDirected ? 0 : i); j < n; j++)
                    if (adj[i, j] == 1) edges.Add(new Tuple<int, int>(i, j));
            }
            return edges;
        }

        static Dictionary<int, List<int>> IncToAdjList(int[,] inc, int n, bool isDirected)
        {
            return AdjToAdjList(IncToAdj(inc, n, isDirected), n);
        }
        #endregion

        #region Методи виводу
        static void PrintAdjMatrix(int[,] matrix, char[] v)
        {
            Console.WriteLine("а) Матриця суміжності:");
            Console.Write("  "); foreach (char c in v) Console.Write(c + " "); Console.WriteLine();
            for (int i = 0; i < v.Length; i++)
            {
                Console.Write(v[i] + " ");
                for (int j = 0; j < v.Length; j++) Console.Write(matrix[i, j] + " ");
                Console.WriteLine();
            }
        }

        static void PrintIncMatrix(int[,] matrix, char[] v)
        {
            Console.WriteLine("б) Матриця інцидентності:");
            int n = matrix.GetLength(0), m = matrix.GetLength(1);
            Console.Write("   "); for (int j = 1; j <= m; j++) Console.Write($"e{j} ".PadRight(4)); Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                Console.Write(v[i] + "  ");
                for (int j = 0; j < m; j++) Console.Write(matrix[i, j].ToString().PadRight(4));
                Console.WriteLine();
            }
        }

        static void PrintAdjList(Dictionary<int, List<int>> list, char[] v)
        {
            foreach (var kvp in list)
            {
                Console.Write($"{v[kvp.Key]}: ");
                Console.WriteLine(string.Join(", ", kvp.Value.Select(idx => v[idx])));
            }
        }
        #endregion
    }
}