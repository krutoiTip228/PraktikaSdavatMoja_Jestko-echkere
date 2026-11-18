using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level1;

/// <summary>Задание 15. Git: репозиторий, ветка, merge, конфликт (памятка).</summary>
public class Task15_GitBasics : TaskBase
{
    /// <summary>Создаёт задание.</summary>
    public Task15_GitBasics() : base(
        "Git: репозиторий → ветка → merge → конфликт",
        "Пройти базовый git-сценарий (выполняется в терминале, здесь — памятка).",
        new[]
        {
            "git init, 2–3 коммита с осмысленными сообщениями",
            "Ветка feature/git-basics, изменение, коммит",
            "merge в main, смоделировать и разрешить конфликт в README.md",
            "git log --graph --oneline --all → git-notes.md",
        },
        "Команды git в терминале. Результат фиксируется в git-notes.md.")
    {
    }

    /// <inheritdoc />
    public override void Run()
    {
        Console.WriteLine(@"Шаги:
 1. git init
 2. git add . ; git commit -m ""feat: task 01 - hello world""   (2–3 коммита)
 3. git switch -c feature/git-basics   → изменить файл → commit
 4. git switch main ; git merge feature/git-basics
 5. Конфликт: в main и в ветке изменить одну строку README.md,
    затем git merge <ветка> → убрать маркеры <<<<<<< ======= >>>>>>> → git add . ; git commit
 6. git log --graph --oneline --all > git-notes.md  (оформить по шаблону)");
    }
}
