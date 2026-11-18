using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level2;

/// <summary>Геометрическая фигура.</summary>
public interface IShape
{
    /// <summary>Площадь.</summary>
    double Area();

    /// <summary>Периметр.</summary>
    double Perimeter();
}

/// <summary>Круг.</summary>
public class Circle : IShape
{
    private readonly double _r;

    /// <summary>Создаёт круг с радиусом.</summary>
    public Circle(double r) => _r = r;

    /// <inheritdoc />
    public double Area() => Math.PI * _r * _r;

    /// <inheritdoc />
    public double Perimeter() => 2 * Math.PI * _r;

    /// <inheritdoc />
    public override string ToString() => $"Circle(r={_r})";
}

/// <summary>Прямоугольник.</summary>
public class Rectangle : IShape
{
    private readonly double _w;
    private readonly double _h;

    /// <summary>Создаёт прямоугольник.</summary>
    public Rectangle(double w, double h)
    {
        _w = w;
        _h = h;
    }

    /// <inheritdoc />
    public double Area() => _w * _h;

    /// <inheritdoc />
    public double Perimeter() => 2 * (_w + _h);

    /// <inheritdoc />
    public override string ToString() => $"Rectangle({_w}x{_h})";
}

/// <summary>Треугольник по трём сторонам.</summary>
public class Triangle : IShape
{
    private readonly double _a;
    private readonly double _b;
    private readonly double _c;

    /// <summary>Создаёт треугольник по сторонам.</summary>
    public Triangle(double a, double b, double c)
    {
        _a = a;
        _b = b;
        _c = c;
    }

    /// <inheritdoc />
    public double Area()
    {
        double p = Perimeter() / 2;
        return Math.Sqrt(p * (p - _a) * (p - _b) * (p - _c));
    }

    /// <inheritdoc />
    public double Perimeter() => _a + _b + _c;

    /// <inheritdoc />
    public override string ToString() => $"Triangle({_a}, {_b}, {_c})";
}

/// <summary>Задание 25. Интерфейс IShape и сортировка фигур.</summary>
public class Task25_Shapes : TaskBase
{
    /// <summary>Создаёт задание.</summary>
    public Task25_Shapes() : base(
        "Интерфейс IShape + сортировка фигур",
        "Реализовать интерфейс фигур и отсортировать их по площади.",
        new[]
        {
            "IShape: Area(), Perimeter()",
            "Circle, Rectangle, Triangle",
            "List<IShape> из 5+ фигур",
            "Сортировка по площади (List.Sort с лямбдой или OrderBy)",
            "Вывод до и после сортировки",
        },
        "interface, List<T>.Sort(Comparison<T>), формула Герона.")
    {
    }

    /// <inheritdoc />
    public override void Run()
    {
        var shapes = new List<IShape>
        {
            new Circle(3),
            new Rectangle(4, 5),
            new Triangle(3, 4, 5),
            new Circle(1),
            new Rectangle(2, 2),
            new Triangle(6, 7, 8),
        };

        Console.WriteLine("До сортировки:");
        Print(shapes);

        shapes.Sort((x, y) => x.Area().CompareTo(y.Area()));

        Console.WriteLine("После сортировки по площади:");
        Print(shapes);
    }

    private static void Print(List<IShape> shapes)
    {
        foreach (IShape s in shapes)
        {
            Console.WriteLine($"  {s,-22} S = {s.Area():F2}, P = {s.Perimeter():F2}");
        }
    }
}
