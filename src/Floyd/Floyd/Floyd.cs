using System;

public class Floyd
{
    private double[,] shortestPaths;
    private int[,] route;
    private int size;

    public Floyd(double[,] graph)
    {
        size = graph.GetLength(0);
        shortestPaths = (double[,])graph.Clone();
        route = new int[size, size];

        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                route[i, j] = (!double.IsInfinity(shortestPaths[i, j]) && i != j) ? j : -1;

        ComputePaths();
    }

    private void ComputePaths()
    {
        for (int k = 0; k < size; k++)
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (shortestPaths[i, k] + shortestPaths[k, j] < shortestPaths[i, j])
                    {
                        shortestPaths[i, j] = shortestPaths[i, k] + shortestPaths[k, j];
                        route[i, j] = route[i, k];
                    }
    }

    public double GetShortestDistance(int from, int to)
    {
        return shortestPaths[from, to];
    }

    public void DisplayRoute(int from, int to)
    {
        if (route[from, to] == -1)
        {
            Console.WriteLine("Путь не найден.");
            return;
        }

        Console.Write((from + 1));
        while (from != to)
        {
            from = route[from, to];
            Console.Write(" - " + (from + 1));
        }
        Console.WriteLine();
    }
}
