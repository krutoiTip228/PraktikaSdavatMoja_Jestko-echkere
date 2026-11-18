using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level2;

/// <summary>Задание 18. TryParse против try/catch.</summary>
public class Task18_TryParseVsTryCatch : TaskBase
{
    /// <summary>Создаёт задание.</summary>
    public Task18_TryParseVsTryCatch() : base(
        "TryParse vs try/catch",
        "Сравнить два способа обработки некорректного ввода числа.",
        new[]
        {
            "Способ 1: int.TryParse — bool + значение через out",
            "Способ 2: int.Parse в try/catch (FormatException)",
            "Оба — отдельными методами",
            "Показать на корректном и некорректном вводе",
        },
        "int.TryParse, out-параметры, FormatException.")
    {
    }

    /// <inheritdoc />
    public override void Run()
    {
        foreach (string s in new[] { "123", "abc" })
        {
            bool ok = TryParseWay(s, out int v1);
            int? v2 = TryCatchWay(s);
            Console.WriteLine($"\"{s}\": TryParse → {(ok ? v1.ToString() : "ошибка")}, try/catch → {(v2?.ToString() ?? "ошибка")}");
        }

        string user = ConsoleHelper.ReadLine("Введите своё значение: ");
        bool okUser = TryParseWay(user, out int r);
        Console.WriteLine(okUser ? $"Получено число {r}" : "Это не число");
    }

    /// <summary>Способ 1: возвращает успех, значение — через out.</summary>
    public static bool TryParseWay(string s, out int value) => int.TryParse(s, out value);

    /// <summary>Способ 2: ловит FormatException; null при ошибке.</summary>
    public static int? TryCatchWay(string s)
    {
        try
        {
            return int.Parse(s);
        }
        catch (FormatException)
        {
            return null;
        }
        catch (OverflowException)
        {
            return null;
        }
    }
}
