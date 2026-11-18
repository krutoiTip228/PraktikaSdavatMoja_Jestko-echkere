using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level2;

/// <summary>Стратегия расчёта скидки.</summary>
public interface IDiscountStrategy
{
    /// <summary>Возвращает цену после применения скидки.</summary>
    decimal Apply(decimal price);
}

/// <summary>Без скидки.</summary>
public class NoDiscount : IDiscountStrategy
{
    /// <inheritdoc />
    public decimal Apply(decimal price) => price;
}

/// <summary>VIP-скидка 10%.</summary>
public class VipDiscount : IDiscountStrategy
{
    /// <inheritdoc />
    public decimal Apply(decimal price) => price * 0.90m;
}

/// <summary>Промо-скидка 20%.</summary>
public class PromoDiscount : IDiscountStrategy
{
    /// <inheritdoc />
    public decimal Apply(decimal price) => price * 0.80m;
}

/// <summary>Калькулятор цены с подставляемой стратегией.</summary>
public class PriceCalculator
{
    private readonly IDiscountStrategy _strategy;

    /// <summary>Создаёт калькулятор с выбранной стратегией.</summary>
    public PriceCalculator(IDiscountStrategy strategy) => _strategy = strategy;

    /// <summary>Считает итоговую цену.</summary>
    public decimal Calculate(decimal price) => _strategy.Apply(price);
}

/// <summary>Задание 26. Паттерн «Стратегия» (скидки).</summary>
public class Task26_DiscountStrategy : TaskBase
{
    /// <summary>Создаёт задание.</summary>
    public Task26_DiscountStrategy() : base(
        "Паттерн «Стратегия» (скидки)",
        "Реализовать калькулятор скидок через паттерн «Стратегия».",
        new[]
        {
            "IDiscountStrategy.Apply(decimal price)",
            "NoDiscount, VipDiscount (10%), PromoDiscount (20%)",
            "PriceCalculator принимает стратегию в конструкторе",
            "Расчёт для каждой стратегии",
        },
        "interface, внедрение зависимости через конструктор, decimal.")
    {
    }

    /// <inheritdoc />
    public override void Run()
    {
        decimal price = 1000m;
        var strategies = new (string Name, IDiscountStrategy Strategy)[]
        {
            ("Без скидки", new NoDiscount()),
            ("VIP (10%)", new VipDiscount()),
            ("Промо (20%)", new PromoDiscount()),
        };

        foreach (var (name, strategy) in strategies)
        {
            var calc = new PriceCalculator(strategy);
            Console.WriteLine($"{name,-12}: {price} → {calc.Calculate(price)}");
        }
    }
}
