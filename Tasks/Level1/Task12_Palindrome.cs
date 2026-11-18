using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level1;

/// <summary>Задание 12. Палиндром (без учёта регистра и пробелов).</summary>
public class Task12_Palindrome : TaskBase
{
    /// <summary>Создаёт задание.</summary>
    public Task12_Palindrome() : base(
        "Палиндром",
        "Проверить, является ли строка палиндромом.",
        new[]
        {
            "Игнорировать регистр",
            "Игнорировать пробелы и знаки пунктуации",
            "Без LINQ Reverse",
            "Пустая строка и один символ — палиндром",
        },
        "char.IsLetterOrDigit, char.ToLower, два индекса (с начала и с конца).")
    {
    }

    /// <inheritdoc />
    public override void Run()
    {
        string s = ConsoleHelper.ReadLine("Введите строку: ");
        Console.WriteLine($"\"{s}\" → {IsPalindrome(s)}");
        Console.WriteLine($"Проверки: {IsPalindrome("А роза упала на лапу Азора")} (ожидается True), {IsPalindrome("hello")} (ожидается False)");
    }

    /// <summary>Проверяет палиндром, игнорируя регистр и всё, кроме букв и цифр.</summary>
    public static bool IsPalindrome(string s)
    {
        int i = 0;
        int j = s.Length - 1;
        while (i < j)
        {
            if (!char.IsLetterOrDigit(s[i]))
            {
                i++;
            }
            else if (!char.IsLetterOrDigit(s[j]))
            {
                j--;
            }
            else
            {
                if (char.ToLower(s[i]) != char.ToLower(s[j]))
                {
                    return false;
                }

                i++;
                j--;
            }
        }

        return true;
    }
}
