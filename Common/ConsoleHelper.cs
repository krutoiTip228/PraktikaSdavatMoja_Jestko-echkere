using System.Globalization;

namespace CSharpPracticum.Common;

/// <summary>Вспомогательные методы для ввода-вывода в консоли.</summary>
public static class ConsoleHelper
{
    /// <summary>Выводит приглашение и читает строку (пустая строка, если ввод закрыт).</summary>
    public static string ReadLine(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? string.Empty;
    }

    /// <summary>Запрашивает целое число, повторяя запрос при ошибке.</summary>
    public static int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
    {
        while (true)
        {
            string s = ReadLine(prompt);
            if (int.TryParse(s, out int value) && value >= min && value <= max)
            {
                return value;
            }

            Console.WriteLine("Некорректный ввод, попробуйте ещё раз.");
        }
    }

    /// <summary>Запрашивает вещественное число (принимает и точку, и запятую).</summary>
    public static double ReadDouble(string prompt)
    {
        while (true)
        {
            string s = ReadLine(prompt).Replace(',', '.');
            if (double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
            {
                return value;
            }

            Console.WriteLine("Некорректный ввод, попробуйте ещё раз.");
        }
    }

    /// <summary>Выводит разделительную линию.</summary>
    public static void Line(char c = '-', int length = 50) => Console.WriteLine(new string(c, length));
}
