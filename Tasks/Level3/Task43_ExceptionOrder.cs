using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level3;

/// <summary>Собственное исключение для демонстрации.</summary>
public class CustomException : Exception
{
    /// <summary>Создаёт исключение с сообщением.</summary>
    public CustomException(string message) : base(message)
    {
    }
}

/// <summary>Задание 43. Несколько типов исключений, порядок catch.</summary>
public class Task43_ExceptionOrder : TaskBase
{
    /// <summary>Создаёт задание.</summary>
    public Task43_ExceptionOrder() : base(
        "Исключения: порядок catch, finally",
        "Продемонстрировать порядок обработки исключений.",
        new[]
        {
            "Метод, бросающий FormatException, DivideByZeroException или CustomException",
            "Несколько catch от частного к общему",
            "Все три сценария",
            "finally с выводом «завершено»",
            "Объяснить в комментарии, почему порядок catch важен",
        },
        "try/catch/finally, собственный класс исключения.")
    {
    }

    /// <inheritdoc />
    public override void Run()
    {
        for (int scenario = 1; scenario <= 3; scenario++)
        {
            Console.WriteLine($"Сценарий {scenario}:");
            try
            {
                Risky(scenario);
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"  поймано FormatException: {ex.Message}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"  поймано DivideByZeroException: {ex.Message}");
            }
            catch (CustomException ex)
            {
                Console.WriteLine($"  поймано CustomException: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  поймано общее Exception: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("  завершено");
            }

            // Порядок catch важен: блоки проверяются сверху вниз, и срабатывает первый подходящий по типу.
            // Если поставить catch (Exception) первым, он перехватит всё, а частные блоки станут недостижимыми
            // (компилятор выдаст ошибку CS0160). Поэтому сначала идут частные типы, в конце — общий.
        }
    }

    /// <summary>Бросает исключение в зависимости от номера сценария.</summary>
    public static void Risky(int scenario)
    {
        switch (scenario)
        {
            case 1:
                _ = int.Parse("не число");
                break;
            case 2:
                int zero = 0;
                _ = 10 / zero;
                break;
            default:
                throw new CustomException("Что-то пошло не так в бизнес-логике.");
        }
    }
}
