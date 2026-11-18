using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level2;

/// <summary>Точка на плоскости с равенством по значению.</summary>
public class Point : IEquatable<Point>
{
    /// <summary>Создаёт точку.</summary>
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    /// <summary>Координата X.</summary>
    public int X { get; }

    /// <summary>Координата Y.</summary>
    public int Y { get; }

    /// <inheritdoc />
    public bool Equals(Point? other) => other is not null && X == other.X && Y == other.Y;

    /// <inheritdoc />
    public override bool Equals(object? obj) => Equals(obj as Point);

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(X, Y);

    /// <inheritdoc />
    public override string ToString() => $"({X}, {Y})";
}

/// <summary>Задание 28. Equals / GetHashCode для Point.</summary>
public class Task28_PointEquals : TaskBase
{
    /// <summary>Создаёт задание.</summary>
    public Task28_PointEquals() : base(
        "Equals / GetHashCode для Point",
        "Переопределить Equals и GetHashCode для Point(x, y).",
        new[]
        {
            "Переопределить Equals(object) и GetHashCode()",
            "Реализовать IEquatable<Point>",
            "Точки с одинаковыми координатами равны",
            "HashSet<Point>: дубликат не увеличивает Count",
        },
        "IEquatable<T>, HashCode.Combine, HashSet<T>.")
    {
    }

    /// <inheritdoc />
    public override void Run()
    {
        var p1 = new Point(1, 2);
        var p2 = new Point(1, 2);
        var p3 = new Point(3, 4);

        Console.WriteLine($"p1.Equals(p2) = {p1.Equals(p2)} (ожидается True)");
        Console.WriteLine($"p1.Equals(p3) = {p1.Equals(p3)} (ожидается False)");
        Console.WriteLine($"p1.Equals((object)p2) = {p1.Equals((object)p2)}");

        var set = new HashSet<Point> { p1, p3 };
        bool added = set.Add(p2);
        Console.WriteLine($"Добавление дубликата (1, 2): {added}, Count = {set.Count} (ожидается 2)");
    }
}
