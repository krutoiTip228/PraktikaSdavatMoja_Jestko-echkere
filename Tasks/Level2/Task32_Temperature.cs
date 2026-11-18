using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level2;

/// <summary>Температура с валидацией.</summary>
public class Temperature
{
    private double _celsius;

    /// <summary>Температура в градусах Цельсия (не ниже −273.15).</summary>
    /// <exception cref="ArgumentOutOfRangeException">Значение ниже абсолютного нуля.</exception>
    public double Celsius
    {
        get => _celsius;
        set
        {
            if (value < -273.15)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, "Температура не может быть ниже -273.15 °C.");
            }

            _celsius = value;
        }
    }

    /// <summary>Температура в градусах Фаренгейта (только чтение).</summary>
    public double Fahrenheit => (_celsius * 9 / 5) + 32;
}

/// <summary>Задание 32. Свойства с валидацией (Temperature).</summary>
public class Task32_Temperature : TaskBase
{
    /// <summary>Создаёт задание.</summary>
    public Task32_Temperature() : base(
        "Свойства с валидацией (Temperature)",
        "Класс Temperature с валидацией в set.",
        new[]
        {
            "Celsius: в set проверка не ниже −273.15",
            "Иначе — ArgumentOutOfRangeException",
            "Вычисляемое свойство Fahrenheit (только get)",
            "Показать корректное и некорректное присваивание",
        },
        "Свойства с логикой в set, ArgumentOutOfRangeException.")
    {
    }

    /// <inheritdoc />
    public override void Run()
    {
        var t = new Temperature();
        foreach (double value in new[] { 25.0, 0.0, -300.0 })
        {
            try
            {
                t.Celsius = value;
                Console.WriteLine($"Celsius = {t.Celsius} → Fahrenheit = {t.Fahrenheit:F1}");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Ошибка при значении {value}: {ex.Message}");
            }
        }

        Console.WriteLine($"Текущее значение осталось: {t.Celsius} °C");
    }
}
