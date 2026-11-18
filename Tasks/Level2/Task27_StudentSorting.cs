using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level2;

/// <summary>Студент; естественный порядок — по среднему баллу (по убыванию).</summary>
public class Student : IComparable<Student>
{
    /// <summary>Создаёт студента.</summary>
    public Student(string name, double averageGrade)
    {
        Name = name;
        AverageGrade = averageGrade;
    }

    /// <summary>Имя.</summary>
    public string Name { get; }

    /// <summary>Средний балл.</summary>
    public double AverageGrade { get; }

    /// <inheritdoc />
    public int CompareTo(Student? other) =>
        other is null ? 1 : other.AverageGrade.CompareTo(AverageGrade);

    /// <inheritdoc />
    public override string ToString() => $"{Name} ({AverageGrade:F1})";
}

/// <summary>Сравнение студентов по имени.</summary>
public class ByNameComparer : IComparer<Student>
{
    /// <inheritdoc />
    public int Compare(Student? x, Student? y) =>
        string.Compare(x?.Name, y?.Name, StringComparison.CurrentCultureIgnoreCase);
}

/// <summary>Задание 27. IComparable / IComparer для Student.</summary>
public class Task27_StudentSorting : TaskBase
{
    /// <summary>Создаёт задание.</summary>
    public Task27_StudentSorting() : base(
        "IComparable / IComparer<T> для Student",
        "Научить Student сортироваться двумя способами.",
        new[]
        {
            "Student: Name, AverageGrade",
            "IComparable<Student> — по среднему баллу (убывание)",
            "ByNameComparer : IComparer<Student> — по имени",
            "Показать обе сортировки на List<Student>",
        },
        "IComparable<T>, IComparer<T>, List<T>.Sort().")
    {
    }

    /// <inheritdoc />
    public override void Run()
    {
        var students = new List<Student>
        {
            new("Борис", 4.2), new("Анна", 4.9), new("Вера", 3.8), new("Георгий", 4.5),
        };

        students.Sort();
        Console.WriteLine("По среднему баллу (убывание): " + string.Join(", ", students));

        students.Sort(new ByNameComparer());
        Console.WriteLine("По имени:                      " + string.Join(", ", students));
    }
}
