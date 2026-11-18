using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level2;

/// <summary>Обёртка над StreamWriter с корректным освобождением ресурса.</summary>
public class FileWriter : IDisposable
{
    private readonly StreamWriter _writer;
    private bool _disposed;

    /// <summary>Открывает файл для дозаписи.</summary>
    public FileWriter(string path) => _writer = new StreamWriter(path, append: true);

    /// <summary>Записывает строку в файл.</summary>
    /// <exception cref="ObjectDisposedException">Писатель уже закрыт.</exception>
    public void WriteLine(string text)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        _writer.WriteLine(text);
    }

    /// <summary>Закрывает файл (повторный вызов безопасен).</summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _writer.Dispose();
        _disposed = true;
        Console.WriteLine("  [FileWriter закрыт]");
    }
}

/// <summary>Задание 31. IDisposable + using.</summary>
public class Task31_Disposable : TaskBase
{
    /// <summary>Создаёт задание.</summary>
    public Task31_Disposable() : base(
        "IDisposable + using",
        "Реализовать IDisposable для класса, работающего с файлом.",
        new[]
        {
            "FileWriter с полем StreamWriter",
            "Dispose() закрывает StreamWriter",
            "Использование через using",
            "Дополнительно: using-декларация (using var)",
        },
        "IDisposable, using-блок, using var, System.IO.StreamWriter.")
    {
    }

    /// <inheritdoc />
    public override void Run()
    {
        string path = Path.Combine(Path.GetTempPath(), "practicum_task31.txt");
        File.Delete(path);

        Console.WriteLine("1) using-блок:");
        using (var w = new FileWriter(path))
        {
            w.WriteLine("Строка 1 (using-блок)");
        }

        Console.WriteLine("2) using-декларация (using var):");
        using var w2 = new FileWriter(path);
        w2.WriteLine("Строка 2 (using var)");

        w2.Dispose();
        w2.Dispose(); // повторный Dispose не падает
        try
        {
            w2.WriteLine("после закрытия");
        }
        catch (ObjectDisposedException)
        {
            Console.WriteLine("  Запись в закрытый FileWriter → ObjectDisposedException (поймано)");
        }

        Console.WriteLine($"Содержимое {path}:");
        Console.WriteLine(File.ReadAllText(path));
    }
}
