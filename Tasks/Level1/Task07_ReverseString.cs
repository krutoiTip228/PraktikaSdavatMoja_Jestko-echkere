using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level1;

/// <summary>Задание 7. Реверс строки без LINQ.</summary>
public class Task07_ReverseString : TaskBase
{
    /// <summary>Создаёт задание.</summary>
    public Task07_ReverseString() : base(
        "Реверс строки без LINQ",
        "Перевернуть введённую строку.",
        new[]
        {
            "Без Array.Reverse и LINQ",
            "Цикл for от конца к началу (или char[] и swap)",
            "Пустая строка и один символ обрабатываются корректно",
        },
        "char[], swap через кортеж, for.")
    {
    }

    /// <inheritdoc />
    public override void Run()
    {
        string s = ConsoleHelper.ReadLine("Введите строку: ");
        Console.WriteLine($"Результат: \"{Reverse(s)}\"");
        Console.WriteLine($"Проверки: abc → {Reverse("abc")}, a → {Reverse("a")}, пусто → \"{Reverse("")}\"");
    }

    /// <summary>Переворачивает строку через char[] и swap.</summary>
    public static string Reverse(string s)
    {
        char[] chars = s.ToCharArray();
        for (int i = 0, j = chars.Length - 1; i < j; i++, j--)
        {
            (chars[i], chars[j]) = (chars[j], chars[i]);
        }

        return new string(chars);
    }
}
