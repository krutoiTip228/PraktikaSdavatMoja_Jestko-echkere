using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level2;

/// <summary>Сотрудник.</summary>
public class Employee
{
    /// <summary>Создаёт сотрудника.</summary>
    public Employee(string name, int age, decimal salary, string department)
    {
        Name = name;
        Age = age;
        Salary = salary;
        Department = department;
    }

    /// <summary>Имя.</summary>
    public string Name { get; }

    /// <summary>Возраст.</summary>
    public int Age { get; }

    /// <summary>Зарплата.</summary>
    public decimal Salary { get; }

    /// <summary>Отдел.</summary>
    public string Department { get; }
}

/// <summary>Задание 33. LINQ: сотрудники.</summary>
public class Task33_LinqEmployees : TaskBase
{
    /// <summary>Создаёт задание.</summary>
    public Task33_LinqEmployees() : base(
        "LINQ: сотрудники",
        "LINQ-запросы по списку сотрудников.",
        new[]
        {
            "Employee: Name, Age, Salary, Department; список из 10+",
            "Топ-3 по зарплате (убывание)",
            "Средняя зарплата по отделам (GroupBy + Average)",
            "Сотрудники старше 30 (Where)",
        },
        "System.Linq: OrderByDescending, Take, GroupBy, Average, Where.")
    {
    }

    /// <inheritdoc />
    public override void Run()
    {
        var staff = new List<Employee>
        {
            new("Анна", 28, 120000, "IT"),
            new("Борис", 35, 150000, "IT"),
            new("Вера", 42, 90000, "HR"),
            new("Георгий", 31, 110000, "Sales"),
            new("Дарья", 25, 80000, "HR"),
            new("Егор", 39, 175000, "IT"),
            new("Жанна", 45, 130000, "Sales"),
            new("Игорь", 30, 95000, "Support"),
            new("Ксения", 33, 100000, "Support"),
            new("Лев", 27, 105000, "Sales"),
            new("Мария", 50, 160000, "Management"),
        };

        Console.WriteLine("Топ-3 по зарплате:");
        foreach (Employee e in staff.OrderByDescending(e => e.Salary).Take(3))
        {
            Console.WriteLine($"  {e.Name} — {e.Salary:N0}");
        }

        Console.WriteLine("Средняя зарплата по отделам:");
        foreach (var g in staff.GroupBy(e => e.Department))
        {
            Console.WriteLine($"  {g.Key}: {g.Average(e => e.Salary):N0}");
        }

        Console.WriteLine("Старше 30 лет:");
        foreach (Employee e in staff.Where(e => e.Age > 30))
        {
            Console.WriteLine($"  {e.Name}, {e.Age}");
        }
    }
}
