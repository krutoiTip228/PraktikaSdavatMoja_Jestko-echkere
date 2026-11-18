using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level2;

/// <summary>Задание 35. Git: .gitignore, PR, Conventional Commits, rebase (памятка).</summary>
public class Task35_GitAdvanced : TaskBase
{
    /// <summary>Создаёт задание.</summary>
    public Task35_GitAdvanced() : base(
        "Git: .gitignore, PR, Conventional Commits, rebase",
        "Пройти продвинутый git-сценарий (выполняется в терминале и на GitHub, здесь — памятка).",
        new[]
        {
            ".gitignore и .editorconfig отдельным коммитом",
            "Ветка feature/task-35, коммиты по Conventional Commits, push",
            "Pull Request в main, merge (можно squash)",
            "feature/rebase-demo: 3 коммита → git rebase -i HEAD~3 (squash)",
            "git log --oneline → git-notes.md",
        },
        "Команды git в терминале, GitHub/GitLab для PR.")
    {
    }

    /// <inheritdoc />
    public override void Run()
    {
        Console.WriteLine(@"Шаги:
 1. Добавить .gitignore (bin/, obj/, .vs/, *.user) и .editorconfig
    git commit -m ""chore: add gitignore and editorconfig""
 2. git switch -c feature/task-35 → 2–3 коммита (feat: / fix: / docs: ...)
 3. git push -u origin feature/task-35
 4. Открыть Pull Request в main: что сделано, что проверял
 5. Смержить PR (merge или squash)
 6. git switch -c feature/rebase-demo → 3 маленьких коммита
 7. git rebase -i HEAD~3  (pick + squash/fixup)
 8. git log --oneline >> git-notes.md");
    }
}
