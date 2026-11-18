using CSharpPracticum.Tasks.Level3;

namespace CSharpPracticum.Tests;

public class LibraryTests
{
    private static (Library Library, Book[] Books) CreateLibrary(int count = 4)
    {
        var library = new Library();
        var books = new Book[count];
        for (int i = 0; i < count; i++)
        {
            books[i] = new Book($"Книга {i + 1}", "Автор");
            library.AddBook(books[i]);
        }

        return (library, books);
    }

    [Fact]
    public void Borrow_AvailableBook_MarksUnavailableAndAddsToReader()
    {
        var (library, books) = CreateLibrary();
        var reader = new Reader("Анна");

        library.Borrow(reader, books[0]);

        Assert.False(books[0].IsAvailable);
        Assert.Contains(books[0], reader.BorrowedBooks);
    }

    [Fact]
    public void Borrow_AlreadyBorrowedBook_ThrowsBookNotAvailable()
    {
        var (library, books) = CreateLibrary();
        library.Borrow(new Reader("Анна"), books[0]);

        Assert.Throws<BookNotAvailableException>(() => library.Borrow(new Reader("Борис"), books[0]));
    }

    [Fact]
    public void Borrow_FourthBook_ThrowsLimitExceeded()
    {
        var (library, books) = CreateLibrary();
        var reader = new Reader("Анна");
        library.Borrow(reader, books[0]);
        library.Borrow(reader, books[1]);
        library.Borrow(reader, books[2]);

        Assert.Throws<BorrowLimitExceededException>(() => library.Borrow(reader, books[3]));
        Assert.True(books[3].IsAvailable);
    }

    [Fact]
    public void Return_BorrowedBook_MakesAvailableAndRemovesFromReader()
    {
        var (library, books) = CreateLibrary();
        var reader = new Reader("Анна");
        library.Borrow(reader, books[0]);

        library.Return(reader, books[0]);

        Assert.True(books[0].IsAvailable);
        Assert.Empty(reader.BorrowedBooks);
    }

    [Fact]
    public void Return_BookNotBorrowedByReader_Throws()
    {
        var (library, books) = CreateLibrary();

        Assert.Throws<InvalidOperationException>(() => library.Return(new Reader("Анна"), books[0]));
    }

    [Fact]
    public void Borrow_AfterReturn_AnotherReaderCanTakeBook()
    {
        var (library, books) = CreateLibrary();
        var anna = new Reader("Анна");
        var boris = new Reader("Борис");
        library.Borrow(anna, books[0]);
        library.Return(anna, books[0]);

        library.Borrow(boris, books[0]);

        Assert.Contains(books[0], boris.BorrowedBooks);
    }

    [Fact]
    public void Borrow_BookNotInLibrary_ThrowsArgumentException()
    {
        var (library, _) = CreateLibrary();

        Assert.Throws<ArgumentException>(() => library.Borrow(new Reader("Анна"), new Book("Чужая", "Неизвестен")));
    }

    [Fact]
    public void Borrow_AfterReturningOne_ReaderAtLimitCanBorrowAgain()
    {
        var (library, books) = CreateLibrary();
        var reader = new Reader("Анна");
        library.Borrow(reader, books[0]);
        library.Borrow(reader, books[1]);
        library.Borrow(reader, books[2]);
        library.Return(reader, books[0]);

        library.Borrow(reader, books[3]);

        Assert.Equal(Library.MaxBooksPerReader, reader.BorrowedBooks.Count);
    }
}
