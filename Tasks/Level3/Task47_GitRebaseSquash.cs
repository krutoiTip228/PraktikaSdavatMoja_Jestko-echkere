using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level3;

/// <summary>Задание 47. Git: rebase -i, сквош коммитов (памятка).</summary>
public class Task47_GitRebaseSquash : TaskBase
{
    /// <summary>Создаёт задание.</summary>
    public Task47_GitRebaseSquash() : base(
        "Git: rebase -i (сквош коммитов)",
        "Объединить несколько коммитов в один (выполняется в терминале, здесь — памятка).",
        new[]
        {
            "Ветка feature/squash-demo, 4 маленьких коммита",
            "git rebase -i HEAD~4, объединить в один (squash)",
            "git log --oneline до и после",
            "Оба вывода — в git-notes.md",
        },
        "Команды git в терминале. В редакторе rebase оставьте pick у первого коммита, у остальных замените на squash (s).")
    {
    }

    /// <inheritdoc />
    public override void Run()
    {
        Console.WriteLine(@"Шаги:
 1. git switch -c feature/squash-demo
 2. 4 маленьких коммита (feat/fix/docs ...)
 3. git log --oneline              → сохранить вывод ""до""
 4. git rebase -i HEAD~4           (pick + 3 × squash), отредактировать итоговое сообщение
 5. git log --oneline              → сохранить вывод ""после"" (4 коммита стали одним)
 6. Оба вывода вставить в git-notes.md, раздел ""Task 47 — git rebase -i""");
    }
}
