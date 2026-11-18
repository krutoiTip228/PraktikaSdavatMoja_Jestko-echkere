using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level2;

/// <summary>Задание 20. Обобщения: Swap и Max.</summary>
public class Task20_Generics : TaskBase
{
    /// <summary>Создаёт задание.</summary>
    public Task20_Generics() : base(
        "Обобщения: Swap<T>, Max<T>",
        "Написать обобщённые методы Swap и Max с ограничением IComparable<T>.",
        new[]
        {
            "Swap<T>(ref T a, ref T b) — без ограничений",
            "Max<T>(T a, T b) where T : IComparable<T>",
            "Показать на int, string, double",
        },
        "Обобщённые методы, where T : IComparable<T>, ref-параметры.")
    {
    }

    /// <inheritdoc />
    public override void Run()
    {
        int a = 1, b = 2;
        Swap(ref a, ref b);
        Console.WriteLine($"int: Swap → a={a}, b={b}; Max = {Max(a, b)}");

        string s1 = "apple", s2 = "banana";
        Swap(ref s1, ref s2);
        Console.WriteLine($"string: Swap → {s1}, {s2}; Max = {Max(s1, s2)}");

        double d1 = 3.14, d2 = 2.71;
        Swap(ref d1, ref d2);
        Console.WriteLine($"double: Swap → {d1}, {d2}; Max = {Max(d1, d2)}");
    }

    /// <summary>Меняет значения местами.</summary>
    public static void Swap<T>(ref T a, ref T b) => (a, b) = (b, a);

    /// <summary>Возвращает большее из двух значений.</summary>
    public static T Max<T>(T a, T b) where T : IComparable<T> => a.CompareTo(b) >= 0 ? a : b;
}
