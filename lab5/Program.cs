﻿using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Упражнение 6.2: Умножение матриц");
        PerformExercise6_2();
        Console.WriteLine("\nУпражнение 6.3: Средняя температура");
        PerformExercise6_3();
        Console.WriteLine("\nДомашнее задание 6.2: Умножение матриц с использованием коллекций");
        PerformHomework6_2();
        Console.WriteLine("\nДомашнее задание 6.3: Средняя температура с использованием Dictionary");
        PerformHomework6_3();
    }

    // Упражнение 6.2 Матрицы
    static void PerformExercise6_2()
    {
        Console.WriteLine("Введите размеры матрицы A (строки и столбцы через enter):");
        int aRows = int.Parse(Console.ReadLine());
        int aCols = int.Parse(Console.ReadLine());
        int[,] matrixA = new int[aRows, aCols];

        Console.WriteLine("Введите элементы матрицы A (через enter):");
        for (int i = 0; i < aRows; i++)
        {
            for (int j = 0; j < aCols; j++)
            {
                matrixA[i, j] = int.Parse(Console.ReadLine());
            }
        }

        Console.WriteLine("Введите размеры матрицы B (строки):");
        int bCols = int.Parse(Console.ReadLine());
        int[,] matrixB = new int[aCols, bCols];

        Console.WriteLine("Введите элементы матрицы B:");
        for (int i = 0; i < aCols; i++)
        {
            for (int j = 0; j < bCols; j++)
            {
                matrixB[i, j] = int.Parse(Console.ReadLine());
            }
        }

        int[,] resultMatrix = MultiplyMatrices(matrixA, matrixB);
        Console.WriteLine("Произведение матриц:");
        PrintMatrix(resultMatrix);
    }

    static int[,] MultiplyMatrices(int[,] a, int[,] b)
    {
        int aRows = a.GetLength(0);
        int aCols = a.GetLength(1);
        int bCols = b.GetLength(1);
        int[,] result = new int[aRows, bCols];

        for (int i = 0; i < aRows; i++)
        {
            for (int j = 0; j < bCols; j++)
            {
                for (int k = 0; k < aCols; k++)
                {
                    result[i, j] += a[i, k] * b[k, j];
                }
            }
        }
        return result;
    }

    static void PrintMatrix(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }

    // Упражнение 6.3 Средняя температура
    static void PerformExercise6_3()
    {
        double[,] temperatures = GenerateRandomTemperatures();
        Console.WriteLine("Средние температуры по месяцам");
        double[] averageTemps = new double[12];

        for (int month = 0; month < 12; month++)
        {
            averageTemps[month] = CalculateAverage(temperatures, month);
            Console.WriteLine($"Средняя температура в месяце {GetMonthName(month)}: {averageTemps[month]:F2}");
        }

        Array.Sort(averageTemps);
        Console.WriteLine("Отсортированные средние температуры:");
        foreach (var temp in averageTemps)
        {
            Console.WriteLine(temp);
        }
    }

    static double[,] GenerateRandomTemperatures()
    {
        double[,] temperatures = new double[12, 30];
        Random rand = new Random();

        for (int month = 0; month < 12; month++)
        {
            for (int day = 0; day < 30; day++)
            {
                temperatures[month, day] = Math.Round(rand.NextDouble() * 45 - 10, 2);
            }
        }
        return temperatures;
    }

    static double CalculateAverage(double[,] temps, int month)
    {
        double total = 0;
        for (int day = 0; day < 30; day++)
        {
            total += temps[month, day];
        }
        return total / 30;
    }
    static string GetMonthName(int month)
    {
        switch (month)
        {
            case 0: return "Январь";
            case 1: return "Февраль";
            case 2: return "Март";
            case 3: return "Апрель";
            case 4: return "Май";
            case 5: return "Июнь";
            case 6: return "Июль";
            case 7: return "Август";
            case 8: return "Сентябрь";
            case 9: return "Октябрь";
            case 10: return "Ноябрь";
            case 11: return "Декабрь";
            default: return null;
        }
    }

    // Домашнее задание 6.2 Матрицы LinkedList
    static void PerformHomework6_2()
    {
        var tasks = new LinkedList<LinkedList<string>>();
        Console.WriteLine("Введите количество заданий:");
        int taskCount = int.Parse(Console.ReadLine());

        for (int i = 1; i <= taskCount; i++)
        {
            Console.Write($"Введите описание задания {i}: ");
            var taskDetails = new LinkedList<string>();
            taskDetails.AddLast(Console.ReadLine());
            tasks.AddLast(taskDetails);
        }

        Console.WriteLine("Список заданий:");
        foreach (var task in tasks)
        {
            foreach (var detail in task)
            {
                Console.WriteLine(detail);
            }
        }
    }

    // Домашнее задание 6.3 Средние температуры Dictionary
    static void PerformHomework6_3()
    {
        var monthlyTemps = new Dictionary<string, double[]>();
        double[,] temperatures = GenerateRandomTemperatures();

        for (int month = 0; month < 12; month++)
        {
            double[] dailyTemps = new double[30];
            for (int day = 0; day < 30; day++)
            {
                dailyTemps[day] = temperatures[month, day];
            }
            monthlyTemps.Add(GetMonthName(month), dailyTemps);
        }

        Console.WriteLine("Средние температуры по месяцам");
        foreach (var monthData in monthlyTemps)
        {
            double averageTemp = CalculateAverage(monthData.Value);
            Console.WriteLine($"Средняя температура в {monthData.Key}: {averageTemp:F2}");
        }
    }

    static double CalculateAverage(double[] temps)
    {
        double total = 0;
        foreach (var temp in temps)
        {
            total += temp;
        }
        return total / temps.Length;
    }
}