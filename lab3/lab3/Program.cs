using System;

class Program
{
    const int INF = 1000000;
    static string[] names = { "a", "b", "c", "d", "e", "f", "h" };

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        int n = 7;
        int[,] graph = {
            { 0, 2, 7, INF, 6, INF, INF },
            { INF, 0, 6, 3, INF, INF, INF },
            { INF, INF, 0, INF, INF, 11, INF },
            { INF, INF, INF, 0, INF, INF, 4 },
            { INF, INF, INF, INF, 0, INF, 3 },
            { INF, INF, INF, INF, INF, 0, 1 },
            { INF, INF, INF, INF, INF, INF, 0 }
        };

        Dijkstra(graph, 0, n);
        Floyd(graph, n);
        Console.ReadKey();
    }

    static void Dijkstra(int[,] weight, int start, int n)
    {
        int[] d = new int[n];
        bool[] used = new bool[n];
        for (int i = 0; i < n; i++) d[i] = INF;
        d[start] = 0;

        for (int i = 0; i < n; i++)
        {
            int v = -1;
            for (int j = 0; j < n; j++)
                if (!used[j] && (v == -1 || d[j] < d[v])) v = j;

            if (d[v] == INF) break;
            used[v] = true;

            for (int j = 0; j < n; j++)
                if (weight[v, j] != INF)
                    d[j] = Math.Min(d[j], d[v] + weight[v, j]);
        }

        Console.WriteLine("Алгоритм Дейкстри вiд вершини a:");
        for (int i = 0; i < n; i++)
            Console.WriteLine($"Шлях до {names[i]}: {(d[i] == INF ? "inf" : d[i].ToString())}");
    }

    static void Floyd(int[,] weight, int n)
    {
        int[,] a = (int[,])weight.Clone();
        for (int k = 0; k < n; k++)
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (a[i, k] != INF && a[k, j] != INF)
                        a[i, j] = Math.Min(a[i, j], a[i, k] + a[k, j]);

        Console.WriteLine("\nМатриця алгоритму Флойда:");
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
                Console.Write((a[i, j] == INF ? "inf" : a[i, j].ToString()).PadLeft(5));
            Console.WriteLine();
        }
    }
}