using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level3;

/// <summary>Задание 46. Git: stash / stash pop (памятка).</summary>
public class Task46_GitStash : TaskBase
{
    /// <summary>Создаёт задание.</summary>
    public Task46_GitStash() : base(
        "Git: stash / stash pop",
        "Продемонстрировать работу со stash (выполняется в терминале, здесь — памятка).",
        new[]
        {
            "Внести незакоммиченные изменения в файл",
            "git stash, затем git stash list",
            "Переключиться на другую ветку и вернуться",
            "git stash pop",
            "Вывод git stash list до и после — в git-notes.md",
        },
        "Команды git в терминале. Результат фиксируется в git-notes.md.")
    {
    }

    /// <inheritdoc />
    public override void Run()
    {
        Console.WriteLine(@"Шаги:
 1. Изменить любой отслеживаемый файл, не коммитить
 2. git stash                      (изменения спрятаны)
 3. git stash list                 (запись stash@{0} видна)  → сохранить вывод
 4. git switch <другая-ветка> ; git switch main
 5. git stash pop                  (изменения вернулись)
 6. git stash list                 (список пуст)             → сохранить вывод
 7. Оба вывода вставить в git-notes.md, раздел ""Task 46 — git stash""");
    }
}
