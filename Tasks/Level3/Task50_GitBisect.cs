using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level3;

/// <summary>Задание 50. Git: симуляция bisect (памятка).</summary>
public class Task50_GitBisect : TaskBase
{
    /// <summary>Создаёт задание.</summary>
    public Task50_GitBisect() : base(
        "Git: bisect (поиск «плохого» коммита)",
        "Найти «плохой» коммит через git bisect (выполняется в терминале, здесь — памятка).",
        new[]
        {
            "Репозиторий с 10+ коммитами",
            "В одном из средних коммитов намеренно сломать функцию",
            "git bisect start, bad (текущий) и good <hash>",
            "На каждом шаге git bisect good / git bisect bad",
            "Найти «плохой» коммит, git bisect reset",
            "Вывод — в git-notes.md",
        },
        "Команды git в терминале. Проверка на каждом шаге — запуск программы (dotnet run) и сравнение результата.")
    {
    }

    /// <inheritdoc />
    public override void Run()
    {
        Console.WriteLine(@"Шаги:
 1. Сделать 10+ коммитов, в одном из средних сломать метод (например, Add возвращает a - b)
 2. git bisect start
 3. git bisect bad                 (текущий коммит)
 4. git bisect good <hash>         (первый рабочий коммит)
 5. На каждом шаге проверить программу и ответить: git bisect good / git bisect bad
 6. Git покажет «<hash> is the first bad commit» → сохранить вывод
 7. git bisect reset
 8. Вывод вставить в git-notes.md, раздел ""Task 50 — bisect""");
    }
}
