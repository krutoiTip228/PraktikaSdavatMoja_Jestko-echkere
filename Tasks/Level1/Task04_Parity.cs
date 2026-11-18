using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level1;

/// <summary>Задание 4. Чётность числа (два способа).</summary>
public class Task04_Parity : TaskBase
{
    /// <summary>Создаёт задание.</summary>
    public Task04_Parity() : base(
        "Чётность числа (два способа)",
        "Определить, чётное ли введённое число, двумя способами.",
        new[]
        {
            "Способ 1: % 2 == 0",
            "Способ 2: тернарный оператор ? :",
            "Оба способа — отдельными методами",
            "Вывести результат обоих способов",
        },
        "Оператор %, тернарный оператор ? :.")
    {
    }

    /// <inheritdoc />
    public override void Run()
    {
        int n = ConsoleHelper.ReadInt("Введите целое число: ");
        Console.WriteLine($"Способ 1 (%): {(IsEvenModulo(n) ? "чётное" : "нечётное")}");
        Console.WriteLine($"Способ 2 (?:): {IsEvenTernary(n)}");
    }

    /// <summary>Способ 1: проверка через остаток от деления.</summary>
    public static bool IsEvenModulo(int n) => n % 2 == 0;

    /// <summary>Способ 2: тернарный оператор, возвращает текст.</summary>
    public static string IsEvenTernary(int n) => n % 2 == 0 ? "чётное" : "нечётное";
}
