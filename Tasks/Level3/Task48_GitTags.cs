using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level3;

/// <summary>Задание 48. Git: теги (памятка).</summary>
public class Task48_GitTags : TaskBase
{
    /// <summary>Создаёт задание.</summary>
    public Task48_GitTags() : base(
        "Git: теги (v1.0.0)",
        "Поставить аннотированный тег (выполняется в терминале, здесь — памятка).",
        new[]
        {
            "На стабильный коммит в main: git tag -a v1.0.0 -m \"Release v1.0.0\"",
            "git push origin v1.0.0",
            "git tag и git show v1.0.0",
            "Вывод — в git-notes.md",
        },
        "Команды git в терминале. Тег должен быть виден и локально, и на GitHub/GitLab.")
    {
    }

    /// <inheritdoc />
    public override void Run()
    {
        Console.WriteLine(@"Шаги:
 1. git switch main   (убедиться, что dotnet build проходит)
 2. git tag -a v1.0.0 -m ""Release v1.0.0""
 3. git push origin v1.0.0
 4. git tag
 5. git show v1.0.0   (виден аннотированный тег + коммит)
 6. Вывод вставить в git-notes.md, раздел ""Task 48 — tags""");
    }
}
