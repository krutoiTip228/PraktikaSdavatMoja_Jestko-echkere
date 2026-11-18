using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level1;

/// <summary>Задание 9. Массивы: min/max/sum/avg.</summary>
public class Task09_ArrayStats : TaskBase
{
    /// <summary>Создаёт задание.</summary>
    public Task09_ArrayStats() : base(
        "Массивы: min/max/sum/avg",
        "Для массива чисел найти минимум, максимум, сумму и среднее.",
        new[]
        {
            "Заполнить массив случайными числами 1–100, размер 20",
            "Без LINQ (Min/Max/Sum/Average запрещены)",
            "4 отдельных метода",
            "Пустой массив обрабатывается без падения",
        },
        "Random, массивы, for.")
    {
    }

    /// <inheritdoc />
    public override void Run()
    {
        var rnd = new Random();
        int[] arr = new int[20];
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = rnd.Next(1, 101);
        }

        Console.WriteLine("Массив: " + string.Join(", ", arr));
        Print(arr);

        Console.WriteLine("Пустой массив:");
        Print(Array.Empty<int>());
    }

    private static void Print(int[] arr)
    {
        Console.WriteLine($"Min = {Min(arr)?.ToString() ?? "нет данных"}");
        Console.WriteLine($"Max = {Max(arr)?.ToString() ?? "нет данных"}");
        Console.WriteLine($"Sum = {Sum(arr)}");
        Console.WriteLine($"Avg = {Average(arr):F2}");
    }

    /// <summary>Минимум; null для пустого массива.</summary>
    public static int? Min(int[] a)
    {
        if (a.Length == 0)
        {
            return null;
        }

        int min = a[0];
        for (int i = 1; i < a.Length; i++)
        {
            if (a[i] < min)
            {
                min = a[i];
            }
        }

        return min;
    }

    /// <summary>Максимум; null для пустого массива.</summary>
    public static int? Max(int[] a)
    {
        if (a.Length == 0)
        {
            return null;
        }

        int max = a[0];
        for (int i = 1; i < a.Length; i++)
        {
            if (a[i] > max)
            {
                max = a[i];
            }
        }

        return max;
    }

    /// <summary>Сумма элементов (0 для пустого массива).</summary>
    public static long Sum(int[] a)
    {
        long sum = 0;
        foreach (int x in a)
        {
            sum += x;
        }

        return sum;
    }

    /// <summary>Среднее (0 для пустого массива).</summary>
    public static double Average(int[] a) => a.Length == 0 ? 0 : (double)Sum(a) / a.Length;
}
