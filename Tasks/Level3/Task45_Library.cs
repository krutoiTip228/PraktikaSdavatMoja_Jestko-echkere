using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level3;

/// <summary>Книга.</summary>
public class Book
{
    /// <summary>Создаёт книгу.</summary>
    public Book(string title, string author)
    {
        Title = title;
        Author = author;
    }

    /// <summary>Название.</summary>
    public string Title { get; }

    /// <summary>Автор.</summary>
    public string Author { get; }

    /// <summary>Доступна ли книга для выдачи.</summary>
    public bool IsAvailable { get; internal set; } = true;

    /// <inheritdoc />
    public override string ToString() => $"«{Title}» — {Author}";
}

/// <summary>Читатель.</summary>
public class Reader
{
    internal List<Book> Borrowed { get; } = new();

    /// <summary>Создаёт читателя.</summary>
    public Reader(string name) => Name = name;

    /// <summary>Имя читателя.</summary>
    public string Name { get; }

    /// <summary>Книги, которые сейчас на руках.</summary>
    public IReadOnlyList<Book> BorrowedBooks => Borrowed;
}

/// <summary>Книга уже выдана.</summary>
public class BookNotAvailableException : InvalidOperationException
{
    /// <summary>Создаёт исключение.</summary>
    public BookNotAvailableException(Book book) : base($"Книга {book} уже выдана.")
    {
    }
}

/// <summary>Превышен лимит книг на читателя.</summary>
public class BorrowLimitExceededException : InvalidOperationException
{
    /// <summary>Создаёт исключение.</summary>
    public BorrowLimitExceededException(Reader reader, int limit)
        : base($"Читатель {reader.Name} уже взял максимум книг ({limit}).")
    {
    }
}

/// <summary>Библиотека: выдача и возврат книг.</summary>
public class Library
{
    /// <summary>Максимум книг на одного читателя.</summary>
    public const int MaxBooksPerReader = 3;

    private readonly List<Book> _books = new();

    /// <summary>Книги библиотеки.</summary>
    public IReadOnlyList<Book> Books => _books;

    /// <summary>Добавляет книгу в фонд.</summary>
    public void AddBook(Book book) => _books.Add(book);

    /// <summary>Выдаёт книгу читателю.</summary>
    /// <exception cref="ArgumentException">Книги нет в библиотеке.</exception>
    /// <exception cref="BookNotAvailableException">Книга уже выдана.</exception>
    /// <exception cref="BorrowLimitExceededException">Лимит читателя превышен.</exception>
    public void Borrow(Reader reader, Book book)
    {
        if (!_books.Contains(book))
        {
            throw new ArgumentException("Такой книги нет в библиотеке.", nameof(book));
        }

        if (!book.IsAvailable)
        {
            throw new BookNotAvailableException(book);
        }

        if (reader.Borrowed.Count >= MaxBooksPerReader)
        {
            throw new BorrowLimitExceededException(reader, MaxBooksPerReader);
        }

        book.IsAvailable = false;
        reader.Borrowed.Add(book);
    }

    /// <summary>Принимает книгу от читателя.</summary>
    /// <exception cref="InvalidOperationException">Читатель не брал эту книгу.</exception>
    public void Return(Reader reader, Book book)
    {
        if (!reader.Borrowed.Remove(book))
        {
            throw new InvalidOperationException($"Читатель {reader.Name} не брал книгу {book}.");
        }

        book.IsAvailable = true;
    }
}

/// <summary>Задание 45. Мини-проект «Библиотека» + юнит-тесты.</summary>
public class Task45_Library : TaskBase
{
    /// <summary>Создаёт задание.</summary>
    public Task45_Library() : base(
        "Мини-проект «Библиотека» + юнит-тесты",
        "Классы библиотеки с выдачей и возвратом книг; тесты лежат в проекте CSharpPracticum.Tests.",
        new[]
        {
            "Book (Title, Author, IsAvailable), Reader (Name, BorrowedBooks), Library (выдача/возврат)",
            "Лимит — не больше 3 книг на читателя",
            "Книга уже выдана / лимит превышен — исключения",
            "5+ юнит-тестов (xUnit), все проходят",
        },
        "Тесты: Test Explorer в Visual Studio или команда dotnet test (проект CSharpPracticum.Tests).")
    {
    }

    /// <inheritdoc />
    public override void Run()
    {
        var library = new Library();
        var books = new[]
        {
            new Book("Мастер и Маргарита", "Булгаков"),
            new Book("Война и мир", "Толстой"),
            new Book("Преступление и наказание", "Достоевский"),
            new Book("Анна Каренина", "Толстой"),
        };
        foreach (Book b in books)
        {
            library.AddBook(b);
        }

        var anna = new Reader("Анна");
        var boris = new Reader("Борис");

        Try("Анна берёт 3 книги", () =>
        {
            library.Borrow(anna, books[0]);
            library.Borrow(anna, books[1]);
            library.Borrow(anna, books[2]);
        });
        Try("Анна берёт 4-ю книгу (лимит)", () => library.Borrow(anna, books[3]));
        Try("Борис берёт уже выданную книгу", () => library.Borrow(boris, books[0]));
        Try("Анна возвращает «Мастера и Маргариту»", () => library.Return(anna, books[0]));
        Try("Борис берёт её же", () => library.Borrow(boris, books[0]));

        Console.WriteLine($"У Анны: {string.Join("; ", anna.BorrowedBooks)}");
        Console.WriteLine($"У Бориса: {string.Join("; ", boris.BorrowedBooks)}");
    }

    private static void Try(string title, Action action)
    {
        try
        {
            action();
            Console.WriteLine($"OK:     {title}");
        }
        catch (Exception ex) when (ex is InvalidOperationException or ArgumentException)
        {
            Console.WriteLine($"ОШИБКА: {title} → {ex.Message}");
        }
    }
}
