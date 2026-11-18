using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level2;

/// <summary>Абстрактное животное.</summary>
public abstract class Animal
{
    /// <summary>Создаёт животное с именем.</summary>
    protected Animal(string name) => Name = name;

    /// <summary>Имя животного.</summary>
    public string Name { get; }

    /// <summary>Издаёт звук.</summary>
    public abstract string MakeSound();

    /// <inheritdoc />
    public override string ToString() => $"{GetType().Name} «{Name}»";
}

/// <summary>Собака.</summary>
public class Dog : Animal
{
    /// <summary>Создаёт собаку.</summary>
    public Dog(string name) : base(name)
    {
    }

    /// <inheritdoc />
    public override string MakeSound() => "Гав!";
}

/// <summary>Кошка.</summary>
public class Cat : Animal
{
    /// <summary>Создаёт кошку.</summary>
    public Cat(string name) : base(name)
    {
    }

    /// <inheritdoc />
    public override string MakeSound() => "Мяу!";
}

/// <summary>Птица.</summary>
public class Bird : Animal
{
    /// <summary>Создаёт птицу.</summary>
    public Bird(string name) : base(name)
    {
    }

    /// <inheritdoc />
    public override string MakeSound() => "Чирик!";

    /// <summary>Птица летит.</summary>
    public string Fly() => $"{Name} летит";
}

/// <summary>Задание 24. ООП: Animal → Dog/Cat/Bird.</summary>
public class Task24_Animals : TaskBase
{
    /// <summary>Создаёт задание.</summary>
    public Task24_Animals() : base(
        "ООП: Animal → Dog/Cat/Bird",
        "Построить иерархию животных и показать полиморфизм.",
        new[]
        {
            "Абстрактный Animal: Name, абстрактный MakeSound(), ToString()",
            "Dog, Cat, Bird — наследники; у Bird ещё Fly()",
            "List<Animal> и вызов MakeSound() в цикле",
        },
        "abstract, override, наследование, List<T>, pattern matching (is Bird).")
    {
    }

    /// <inheritdoc />
    public override void Run()
    {
        var animals = new List<Animal> { new Dog("Шарик"), new Cat("Мурка"), new Bird("Кеша") };
        foreach (Animal a in animals)
        {
            Console.WriteLine($"{a} говорит: {a.MakeSound()}");
            if (a is Bird bird)
            {
                Console.WriteLine($"  {bird.Fly()}");
            }
        }
    }
}
