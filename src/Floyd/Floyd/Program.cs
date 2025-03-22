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
            Console.WriteLine("Файл не найден");
            return;
        }

        double[,] graph = LoadGraphFromFile(path);
        int size = graph.GetLength(0);

        int startNode = InputVertex("начальную точку", size) - 1;
        int endNode = InputVertex("конечную точку", size) - 1;

        Floyd pathFinder = new Floyd(graph);

        double result = pathFinder.GetShortestDistance(startNode, endNode);
        if (double.IsInfinity(result))
        {
            Console.WriteLine("Путь не существует");
        }
        else
        {
            pathFinder.DisplayRoute(startNode, endNode);
            Console.WriteLine($"Длина маршрута: {result:F2} км");
        }
    }

    static double[,] LoadGraphFromFile(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        int size = int.Parse(lines[0]);
        var matrix = new double[size, size];

        for (int i = 0; i < size; i++)
        {
            var values = lines[i + 1]
                .Trim('{', '}', ' ')
                .Split(',');

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
        while (value < 1 || value > max)
        {
            Console.WriteLine($"Введите {label} (1-{max}): ");
            if (int.TryParse(Console.ReadLine(), out value) && value >= 1 && value <= max)
                return value;

            Console.WriteLine("Некорректный ввод");
        }
        return value;
    }
}
