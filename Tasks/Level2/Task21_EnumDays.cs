using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level2;

/// <summary>Задание 21. enum дни недели.</summary>
public class Task21_EnumDays : TaskBase
{
    /// <summary>Дни недели.</summary>
    public enum DayOfWeek
    {
        /// <summary>Понедельник.</summary>
        Monday,

        /// <summary>Вторник.</summary>
        Tuesday,

        /// <summary>Среда.</summary>
        Wednesday,

        /// <summary>Четверг.</summary>
        Thursday,

        /// <summary>Пятница.</summary>
        Friday,

        /// <summary>Суббота.</summary>
        Saturday,

        /// <summary>Воскресенье.</summary>
        Sunday,
    }

    /// <summary>Создаёт задание.</summary>
    public Task21_EnumDays() : base(
        "enum дни недели",
        "Описать дни недели через enum и определить тип дня.",
        new[]
        {
            "enum DayOfWeek { Monday, ..., Sunday }",
            "IsWeekend(DayOfWeek) — true для субботы и воскресенья",
            "GetRussianName(DayOfWeek) — название на русском",
            "Показать на всех 7 днях",
        },
        "enum, Enum.GetValues, switch-выражение.")
    {
    }

    /// <inheritdoc />
    public override void Run()
    {
        foreach (DayOfWeek day in Enum.GetValues<DayOfWeek>())
        {
            Console.WriteLine($"{day,-10} {GetRussianName(day),-12} выходной: {(IsWeekend(day) ? "да" : "нет")}");
        }
    }

    /// <summary>Выходной ли день.</summary>
    public static bool IsWeekend(DayOfWeek day) => day == DayOfWeek.Saturday || day == DayOfWeek.Sunday;

    /// <summary>Название дня на русском.</summary>
    public static string GetRussianName(DayOfWeek day) => day switch
    {
        DayOfWeek.Monday => "Понедельник",
        DayOfWeek.Tuesday => "Вторник",
        DayOfWeek.Wednesday => "Среда",
        DayOfWeek.Thursday => "Четверг",
        DayOfWeek.Friday => "Пятница",
        DayOfWeek.Saturday => "Суббота",
        DayOfWeek.Sunday => "Воскресенье",
        _ => "Неизвестно",
    };
}
