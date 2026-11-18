using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level3;

/// <summary>Задание 49. Git: восстановление через reflog (памятка).</summary>
public class Task49_GitReflog : TaskBase
{
    /// <summary>Создаёт задание.</summary>
    public Task49_GitReflog() : base(
        "Git: восстановление через reflog",
        "Удалить коммит и восстановить его (выполняется в терминале, здесь — памятка).",
        new[]
        {
            "Сделать коммит",
            "git reset --hard HEAD~1",
            "Найти хеш в git reflog",
            "git reset --hard <hash>",
            "git reflog и git log --oneline — в git-notes.md",
        },
        "Команды git в терминале. Делайте это в учебной ветке: reset --hard стирает незакоммиченные изменения.")
    {
    }

    /// <inheritdoc />
    public override void Run()
    {
        Console.WriteLine(@"Шаги:
 1. git commit -m ""docs: task 49 - commit to lose""
 2. git reset --hard HEAD~1        (коммит «пропал»)
 3. git reflog                     (найти строку с потерянным коммитом, скопировать хеш)
 4. git reset --hard <hash>        (коммит восстановлен)
 5. git reflog ; git log --oneline
 6. Вывод вставить в git-notes.md, раздел ""Task 49 — reflog""");
    }
}
