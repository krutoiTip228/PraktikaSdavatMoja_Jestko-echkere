using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level2;

/// <summary>Тип уведомления.</summary>
public enum NotificationType
{
    /// <summary>Email.</summary>
    Email,

    /// <summary>SMS.</summary>
    Sms,

    /// <summary>Push.</summary>
    Push,
}

/// <summary>Уведомление.</summary>
public interface INotification
{
    /// <summary>Отправляет сообщение.</summary>
    void Send(string message);
}

/// <summary>Email-уведомление.</summary>
public class EmailNotification : INotification
{
    /// <inheritdoc />
    public void Send(string message) => Console.WriteLine($"[Email] {message}");
}

/// <summary>SMS-уведомление.</summary>
public class SmsNotification : INotification
{
    /// <inheritdoc />
    public void Send(string message) => Console.WriteLine($"[SMS] {message}");
}

/// <summary>Push-уведомление.</summary>
public class PushNotification : INotification
{
    /// <inheritdoc />
    public void Send(string message) => Console.WriteLine($"[Push] {message}");
}

/// <summary>Фабрика уведомлений.</summary>
public static class NotificationFactory
{
    /// <summary>Создаёт уведомление нужного типа.</summary>
    public static INotification Create(NotificationType type) => type switch
    {
        NotificationType.Email => new EmailNotification(),
        NotificationType.Sms => new SmsNotification(),
        NotificationType.Push => new PushNotification(),
        _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Неизвестный тип уведомления."),
    };
}

/// <summary>Задание 29. Паттерн «Фабрика» (уведомления).</summary>
public class Task29_NotificationFactory : TaskBase
{
    /// <summary>Создаёт задание.</summary>
    public Task29_NotificationFactory() : base(
        "Паттерн «Фабрика» (уведомления)",
        "Реализовать фабрику уведомлений.",
        new[]
        {
            "INotification.Send(string message)",
            "EmailNotification, SmsNotification, PushNotification",
            "NotificationFactory.Create(NotificationType type)",
            "enum NotificationType { Email, Sms, Push }",
        },
        "interface, enum, switch-выражение, статический фабричный метод.")
    {
    }

    /// <inheritdoc />
    public override void Run()
    {
        foreach (NotificationType type in Enum.GetValues<NotificationType>())
        {
            INotification n = NotificationFactory.Create(type);
            Console.Write($"{type} → {n.GetType().Name}: ");
            n.Send("Привет из фабрики!");
        }
    }
}
