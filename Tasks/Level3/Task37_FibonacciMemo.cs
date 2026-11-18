using System.Diagnostics;
using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level3;

/// <summary>Задание 37. Мемоизация Фибоначчи через Dictionary.</summary>
public class Task37_FibonacciMemo : TaskBase
{
    /// <summary>Создаёт задание.</summary>
    public Task37_FibonacciMemo() : base(
        "Мемоизация Фибоначчи (Dictionary)",
        "Реализовать мемоизированную функцию Фибоначчи и сравнить с обычной рекурсией.",
        new[]
        {
            "Обычная рекурсивная версия",
            "Мемоизированная версия с Dictionary<int, long>",
            "Замер Stopwatch на N = 30–35",
            "Вывести оба времени и разницу",
        },
        "Dictionary<int,long>.TryGetValue, Stopwatch, рекурсия.")
    {
    }

    /// <inheritdoc />
    public override void Run()
    {
        int n = ConsoleHelper.ReadInt("N (30–35): ", 30, 35);

        var sw = Stopwatch.StartNew();
        long plain = FibRecursive(n);
        sw.Stop();
        double plainMs = sw.Elapsed.TotalMilliseconds;

        sw.Restart();
        long memo = FibMemo(n, new Dictionary<int, long>());
        sw.Stop();
        double memoMs = sw.Elapsed.TotalMilliseconds;

        Console.WriteLine($"Рекурсия:   Fib({n}) = {plain}, {plainMs:F3} мс");
        Console.WriteLine($"Мемоизация: Fib({n}) = {memo}, {memoMs:F3} мс");
        Console.WriteLine($"Разница: {plainMs - memoMs:F3} мс; результаты совпадают: {plain == memo}");
    }

    /// <summary>Обычная рекурсия (экспоненциальная сложность).</summary>
    public static long FibRecursive(int n) => n < 2 ? n : FibRecursive(n - 1) + FibRecursive(n - 2);

    /// <summary>Рекурсия с кэшем (линейная сложность).</summary>
    public static long FibMemo(int n, Dictionary<int, long> cache)
    {
        if (n < 2)
        {
            return n;
        }

        if (cache.TryGetValue(n, out long cached))
        {
            return cached;
        }

        long value = FibMemo(n - 1, cache) + FibMemo(n - 2, cache);
        cache[n] = value;
        return value;
    }
}
