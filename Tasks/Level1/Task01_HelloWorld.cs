using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level1;

/// <summary>Задание 1. Hello, World + текущая дата.</summary>
public class Task01_HelloWorld : TaskBase
{
    /// <summary>Создаёт задание.</summary>
    public Task01_HelloWorld() : base(
        "Hello, World + текущая дата",
        "Вывести приветствие и текущую дату.",
        new[]
        {
            "Использовать Console.WriteLine",
            "Дату получить через DateTime.Now, формат dd.MM.yyyy",
            "Вывести две строки: приветствие и дату",
        },
        "Console.WriteLine, DateTime.Now, ToString(\"dd.MM.yyyy\") (пространство имён System).")
    {
    }

    /// <inheritdoc />
    public override void Run()
    {
        Console.WriteLine("Hello, World!");
        Console.WriteLine(DateTime.Now.ToString("dd.MM.yyyy"));
    }
}
