using System;
using System.Globalization;
using System.IO;

class Program
{
    static void Main()
    {
        Console.WriteLine("Введите путь к файлу с графом:");
        string path = Console.ReadLine();

        if (!File.Exists(path))
        {
            Console.WriteLine("Файл не найден.");
            return;
        }

        double[,] graph;
        try
        {
            graph = LoadGraphFromFile(path);
        }
        catch
        {
            Console.WriteLine("Ошибка при чтении файла. Проверьте формат данных.");
            return;
        }

        int size = graph.GetLength(0);

        int startNode = InputVertex("начальную точку", size);
        int endNode = InputVertex("конечную точку", size);

        if (startNode == endNode)
        {
            Console.WriteLine("Начальная и конечная вершины не должны совпадать.");
            return;
        }

        Floyd pathFinder = new Floyd(graph);
        double result = pathFinder.GetShortestDistance(startNode, endNode);

        if (double.IsInfinity(result))
        {
            Console.WriteLine("Путь не существует.");
        }
        else
        {
            pathFinder.DisplayRoute(startNode, endNode);
            Console.WriteLine($"Длина маршрута: {result:F2} км");
        }
    }

    static double[,] LoadGraphFromFile(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        int size = int.Parse(lines[0]);
        double[,] matrix = new double[size, size];

        for (int i = 0; i < size; i++)
        {
            string[] values = lines[i + 1]
                .Trim('{', '}', ' ')
                .Split(',', StringSplitOptions.RemoveEmptyEntries);

            for (int j = 0; j < size; j++)
            {
                double value = double.Parse(values[j].Trim(), CultureInfo.InvariantCulture);
                matrix[i, j] = (i != j && value == 0) ? double.PositiveInfinity : value;
            }
        }

        return matrix;
    }

    static int InputVertex(string label, int max)
    {
        int value = -1;
        while (true)
        {
            Console.WriteLine($"Введите {label} (1-{max}):");
            string input = Console.ReadLine();
            if (int.TryParse(input, out value) && value >= 1 && value <= max)
                return value - 1;

            Console.WriteLine("Некорректный ввод. Введите число в допустимом диапазоне.");
        }
    }
}
