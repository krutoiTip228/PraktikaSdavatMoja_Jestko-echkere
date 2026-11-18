namespace CSharpPracticum.Common;

/// <summary>Базовый класс задания: хранит метаданные, логику задаёт наследник.</summary>
public abstract class TaskBase : ITask
{
    /// <summary>Создаёт задание с метаданными.</summary>
    protected TaskBase(string title, string description, string[] requirements, string hint)
    {
        Title = title;
        Description = description;
        Requirements = requirements;
        Hint = hint;
    }

    /// <inheritdoc />
    public string Title { get; }

    /// <inheritdoc />
    public string Description { get; }

    /// <inheritdoc />
    public string[] Requirements { get; }

    /// <inheritdoc />
    public string Hint { get; }

    /// <inheritdoc />
    public abstract void Run();
}
