namespace CSharpPracticum.Common;

/// <summary>Единый контракт задания практикума.</summary>
public interface ITask
{
    /// <summary>Короткое название задания.</summary>
    string Title { get; }

    /// <summary>Описание: что нужно сделать.</summary>
    string Description { get; }

    /// <summary>Список требований к решению.</summary>
    string[] Requirements { get; }

    /// <summary>Подсказка: какие классы, методы и пространства имён использовать.</summary>
    string Hint { get; }

    /// <summary>Запускает решение задания.</summary>
    void Run();
}
