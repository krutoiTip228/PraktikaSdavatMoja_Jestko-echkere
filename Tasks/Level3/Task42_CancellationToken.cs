using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level3;

/// <summary>Задание 42. CancellationToken — отмена через 2 секунды.</summary>
public class Task42_CancellationToken : TaskBase
{
    /// <summary>Создаёт задание.</summary>
    public Task42_CancellationToken() : base(
        "CancellationToken — отмена через 2 сек",
        "Реализовать долгую операцию с поддержкой отмены.",
        new[]
        {
            "async Task LongOperationAsync(CancellationToken token): 10 раз Task.Delay(500, token)",
            "CancellationTokenSource, отмена через 2 секунды",
            "Обработать OperationCanceledException",
            "Вывести «операция отменена»",
        },
        "CancellationTokenSource.CancelAfter, OperationCanceledException.")
    {
    }

    /// <inheritdoc />
    public override void Run() => RunAsync().GetAwaiter().GetResult();

    private static async Task RunAsync()
    {
        using var cts = new CancellationTokenSource();
        cts.CancelAfter(TimeSpan.FromSeconds(2));

        try
        {
            await LongOperationAsync(cts.Token);
            Console.WriteLine("Операция завершена.");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("операция отменена");
        }
    }

    /// <summary>Долгая операция из 10 шагов по 500 мс с поддержкой отмены.</summary>
    public static async Task LongOperationAsync(CancellationToken token)
    {
        for (int i = 1; i <= 10; i++)
        {
            await Task.Delay(500, token);
            Console.WriteLine($"  шаг {i}/10");
        }
    }
}
