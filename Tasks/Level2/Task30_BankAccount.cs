using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level2;

/// <summary>Недостаточно средств на счёте.</summary>
public class InsufficientFundsException : Exception
{
    /// <summary>Создаёт исключение с сообщением.</summary>
    public InsufficientFundsException(string message) : base(message)
    {
    }
}

/// <summary>Банковский счёт.</summary>
public class BankAccount
{
    private decimal _balance;

    /// <summary>Текущий баланс.</summary>
    public decimal Balance => _balance;

    /// <summary>Пополняет счёт.</summary>
    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Сумма должна быть положительной.");
        }

        _balance += amount;
    }

    /// <summary>Снимает деньги со счёта.</summary>
    /// <exception cref="InsufficientFundsException">Сумма больше баланса.</exception>
    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Сумма должна быть положительной.");
        }

        if (amount > _balance)
        {
            throw new InsufficientFundsException($"Недостаточно средств: баланс {_balance}, запрошено {amount}.");
        }

        _balance -= amount;
    }
}

/// <summary>Задание 30. BankAccount + свой Exception.</summary>
public class Task30_BankAccount : TaskBase
{
    /// <summary>Создаёт задание.</summary>
    public Task30_BankAccount() : base(
        "BankAccount + свой Exception",
        "Реализовать банковский счёт с собственным исключением.",
        new[]
        {
            "BankAccount с приватным балансом",
            "Deposit(decimal), Withdraw(decimal)",
            "InsufficientFundsException : Exception",
            "Withdraw бросает исключение, если сумма больше баланса",
            "Показать успешное снятие и неудачное (try/catch)",
        },
        "Собственный класс исключения, throw, try/catch.")
    {
    }

    /// <inheritdoc />
    public override void Run()
    {
        var acc = new BankAccount();
        acc.Deposit(500m);
        Console.WriteLine($"Баланс после пополнения: {acc.Balance}");

        acc.Withdraw(200m);
        Console.WriteLine($"Баланс после снятия 200: {acc.Balance}");

        try
        {
            acc.Withdraw(1000m);
        }
        catch (InsufficientFundsException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }

        Console.WriteLine($"Итоговый баланс: {acc.Balance}");
    }
}
