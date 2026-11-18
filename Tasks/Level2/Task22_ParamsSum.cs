using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level2;

/// <summary>Задание 22. params-метод и перегрузка.</summary>
public class Task22_ParamsSum : TaskBase
{
    /// <summary>Создаёт задание.</summary>
    public Task22_ParamsSum() : base(
        "params-метод + перегрузка",
        "Написать метод суммы с params и его перегрузку.",
        new[]
        {
            "Sum(params int[] numbers)",
            "Перегрузка для коллекции (Sum(IEnumerable<int>))",
            "Вызовы: Sum(1, 2, 3), Sum(new[] {1, 2, 3}), Sum()",
            "Пустой вызов возвращает 0",
        },
        "params, перегрузка методов. Примечание: Sum(params int[]) и Sum(int[]) имеют одинаковую сигнатуру (ошибка CS0111), поэтому перегрузка принимает IEnumerable<int>.")
    {
    }

    /// <inheritdoc />
    public override void Run()
    {
        Console.WriteLine($"Sum(1, 2, 3)            = {Sum(1, 2, 3)}");
        Console.WriteLine($"Sum(new[] {{1, 2, 3}})    = {Sum(new[] { 1, 2, 3 })}");
        Console.WriteLine($"Sum()                   = {Sum()}");
        Console.WriteLine($"Sum(List {{1, 2, 3}})     = {Sum(new List<int> { 1, 2, 3 })}");
    }

    /// <summary>Сумма произвольного числа аргументов (или массива).</summary>
    public static int Sum(params int[] numbers)
    {
        int sum = 0;
        foreach (int n in numbers)
        {
            sum += n;
        }

        return sum;
    }

    /// <summary>Перегрузка: сумма элементов коллекции.</summary>
    public static int Sum(IEnumerable<int> numbers) => Sum(numbers.ToArray());
}
