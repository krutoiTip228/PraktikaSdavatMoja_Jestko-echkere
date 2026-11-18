using System.Text.Json;
using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level3;

/// <summary>Задача в списке дел.</summary>
public class TaskItem
{
    /// <summary>Идентификатор.</summary>
    public int Id { get; set; }

    /// <summary>Название.</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>Выполнена ли задача.</summary>
    public bool IsDone { get; set; }
}

/// <summary>Хранилище задач с сохранением в JSON-файл.</summary>
public class TaskRepository
{
    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };
    private readonly string _path;
    private readonly List<TaskItem> _items = new();

    /// <summary>Создаёт хранилище и загружает данные из файла (если он есть).</summary>
    public TaskRepository(string path)
    {
        _path = path;
        Load();
    }

    /// <summary>Все задачи.</summary>
    public IReadOnlyList<TaskItem> GetAll() => _items;

    /// <summary>Ищет задачу по Id.</summary>
    public TaskItem? Find(int id) => _items.Find(t => t.Id == id);

    /// <summary>Добавляет задачу и сохраняет файл.</summary>
    public TaskItem Add(string title)
    {
        var item = new TaskItem { Id = _items.Count == 0 ? 1 : _items.Max(t => t.Id) + 1, Title = title };
        _items.Add(item);
        Save();
        return item;
    }

    /// <summary>Отмечает задачу выполненной.</summary>
    public bool MarkDone(int id)
    {
        TaskItem? item = Find(id);
        if (item == null)
        {
            return false;
        }

        item.IsDone = true;
        Save();
        return true;
    }

    /// <summary>Удаляет задачу.</summary>
    public bool Remove(int id)
    {
        int removed = _items.RemoveAll(t => t.Id == id);
        if (removed > 0)
        {
            Save();
        }

        return removed > 0;
    }

    private void Load()
    {
        if (!File.Exists(_path))
        {
            return;
        }

        try
        {
            var items = JsonSerializer.Deserialize<List<TaskItem>>(File.ReadAllText(_path));
            if (items != null)
            {
                _items.AddRange(items);
            }
        }
        catch (JsonException)
        {
            Console.WriteLine("Файл tasks.json повреждён — начинаем с пустого списка.");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Не удалось прочитать файл: {ex.Message}");
        }
    }

    private void Save()
    {
        try
        {
            File.WriteAllText(_path, JsonSerializer.Serialize(_items, Options));
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            Console.WriteLine($"Не удалось сохранить файл: {ex.Message}");
        }
    }
}

/// <summary>Бизнес-логика списка задач.</summary>
public class TaskService
{
    private readonly TaskRepository _repository;

    /// <summary>Создаёт сервис поверх хранилища.</summary>
    public TaskService(TaskRepository repository) => _repository = repository;

    /// <summary>Добавляет задачу.</summary>
    /// <exception cref="ArgumentException">Пустое название.</exception>
    public TaskItem Add(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Название задачи не может быть пустым.");
        }

        return _repository.Add(title.Trim());
    }

    /// <summary>Все задачи.</summary>
    public IReadOnlyList<TaskItem> GetAll() => _repository.GetAll();

    /// <summary>Отмечает задачу выполненной.</summary>
    public bool Complete(int id) => _repository.MarkDone(id);

    /// <summary>Удаляет задачу.</summary>
    public bool Delete(int id) => _repository.Remove(id);
}

/// <summary>Задание 44. Мини-проект «Todo List» (классы + JSON).</summary>
public class Task44_TodoList : TaskBase
{
    /// <summary>Создаёт задание.</summary>
    public Task44_TodoList() : base(
        "Мини-проект «Todo List» (JSON)",
        "Консольное приложение для списка задач с сохранением в JSON.",
        new[]
        {
            "Классы: TaskItem (Id, Title, IsDone), TaskRepository (CRUD), TaskService (логика)",
            "Добавить, вывести все, отметить выполненной, удалить",
            "Сохранение/загрузка через System.Text.Json (tasks.json)",
            "Меню с выбором действий",
            "Обработка ошибок: некорректный ввод, отсутствующий файл",
        },
        "System.Text.Json.JsonSerializer, File.ReadAllText/WriteAllText. Файл tasks.json лежит рядом с exe.")
    {
    }

    /// <inheritdoc />
    public override void Run()
    {
        string path = Path.Combine(AppContext.BaseDirectory, "tasks.json");
        var service = new TaskService(new TaskRepository(path));
        Console.WriteLine($"Файл данных: {path}");

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("1. Добавить задачу");
            Console.WriteLine("2. Показать все");
            Console.WriteLine("3. Отметить выполненной");
            Console.WriteLine("4. Удалить");
            Console.WriteLine("0. Выход в меню");

            switch (ConsoleHelper.ReadLine("Действие: ").Trim())
            {
                case "1":
                    try
                    {
                        TaskItem added = service.Add(ConsoleHelper.ReadLine("Название: "));
                        Console.WriteLine($"Добавлено: #{added.Id} {added.Title}");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }

                    break;
                case "2":
                    if (service.GetAll().Count == 0)
                    {
                        Console.WriteLine("Список пуст.");
                    }

                    foreach (TaskItem t in service.GetAll())
                    {
                        Console.WriteLine($"  #{t.Id} [{(t.IsDone ? 'x' : ' ')}] {t.Title}");
                    }

                    break;
                case "3":
                    Console.WriteLine(service.Complete(ConsoleHelper.ReadInt("Id задачи: ")) ? "Готово." : "Задача не найдена.");
                    break;
                case "4":
                    Console.WriteLine(service.Delete(ConsoleHelper.ReadInt("Id задачи: ")) ? "Удалено." : "Задача не найдена.");
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неизвестное действие.");
                    break;
            }
        }
    }
}
