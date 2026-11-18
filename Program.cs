using System.Data;
using Microsoft.Data.SqlClient;

class Program
{
    // Строка подключения. Сначала берётся из переменной окружения PRACTICE_DB_CONNECTION,
    // а если её нет — используется эта (вход под вашей учётной записью Windows, пароль не нужен).
    static string connectionString =
        Environment.GetEnvironmentVariable("PRACTICE_DB_CONNECTION")
        ?? "Server=DESKTOP-U6EEQNA;Database=PracticeDB;Trusted_Connection=True;TrustServerCertificate=True;";

    static async Task Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("1. Список сотрудников");
            Console.WriteLine("2. Добавить сотрудника");
            Console.WriteLine("3. Отчёт по отделам");
            Console.WriteLine("0. Выход");
            Console.Write("Выбор: ");
            string choice = Console.ReadLine();

            if (choice == "1") await ShowEmployees();
            else if (choice == "2") await AddEmployee();
            else if (choice == "3") await ShowReport();
            else if (choice == "0") break;
            else Console.WriteLine("Неизвестный пункт меню.");
        }
    }

    // Задание 28: список сотрудников
    static async Task ShowEmployees()
    {
        try
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            SqlCommand command = new SqlCommand(
                "SELECT EmployeeName, Salary FROM Employees ORDER BY EmployeeId", connection);
            using SqlDataReader reader = await command.ExecuteReaderAsync();

            int count = 0;
            while (await reader.ReadAsync())
            {
                string name = reader.GetString(0);
                decimal salary = reader.GetDecimal(1);
                Console.WriteLine(name.PadRight(20) + salary.ToString("F2").PadLeft(10));
                count++;
            }
            Console.WriteLine("Всего: " + count);
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Ошибка SQL #" + ex.Number + ": " + ex.Message);
        }
    }

    // Задание 29: добавить сотрудника (параметры, без склейки строк)
    static async Task AddEmployee()
    {
        Console.Write("Имя: ");
        string name = Console.ReadLine();

        Console.Write("Зарплата: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal salary))
        {
            Console.WriteLine("Зарплата должна быть числом.");
            return;
        }

        Console.Write("Id отдела: ");
        if (!int.TryParse(Console.ReadLine(), out int deptId))
        {
            Console.WriteLine("Id отдела должен быть числом.");
            return;
        }

        if (string.IsNullOrWhiteSpace(name) || salary <= 0)
        {
            Console.WriteLine("Имя не должно быть пустым, зарплата должна быть больше 0.");
            return;
        }

        try
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            string insertSql =
                "INSERT INTO Employees (EmployeeName, Salary, HireDate, DepartmentId) " +
                "OUTPUT INSERTED.EmployeeId " +
                "VALUES (@name, @salary, CAST(GETDATE() AS DATE), @deptId)";

            SqlCommand insert = new SqlCommand(insertSql, connection);
            insert.Parameters.AddWithValue("@name", name.Trim());
            insert.Parameters.AddWithValue("@salary", salary);
            insert.Parameters.AddWithValue("@deptId", deptId);

            int newId = (int)await insert.ExecuteScalarAsync();

            // Проверка: читаем добавленную запись
            SqlCommand select = new SqlCommand(
                "SELECT EmployeeId, EmployeeName, Salary FROM Employees WHERE EmployeeId = @id", connection);
            select.Parameters.AddWithValue("@id", newId);

            using SqlDataReader reader = await select.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                Console.WriteLine("Добавлен: Id=" + reader.GetInt32(0) +
                                  ", " + reader.GetString(1) +
                                  ", " + reader.GetDecimal(2).ToString("F2"));
            }
        }
        catch (SqlException ex)
        {
            if (ex.Number == 547)
                Console.WriteLine("Ошибка: отдела с таким Id не существует.");
            else
                Console.WriteLine("Ошибка SQL #" + ex.Number + ": " + ex.Message);
        }
    }

    // Задание 30: отчёт по отделам
    static async Task ShowReport()
    {
        try
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            string sql =
                "SELECT d.DepartmentName, COUNT(e.EmployeeId) AS EmpCount, AVG(e.Salary) AS AvgSalary " +
                "FROM Departments d " +
                "LEFT JOIN Employees e ON d.DepartmentId = e.DepartmentId " +
                "GROUP BY d.DepartmentName " +
                "ORDER BY d.DepartmentName";

            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader reader = await command.ExecuteReaderAsync();

            Console.WriteLine("Отдел".PadRight(15) + "Сотрудников".PadLeft(12) + "Средняя зарплата".PadLeft(20));
            Console.WriteLine(new string('-', 47));

            int rows = 0;
            while (await reader.ReadAsync())
            {
                string name = reader.GetString(0);
                int count = reader.GetInt32(1);
                string avg = reader.IsDBNull(2) ? "N/A" : reader.GetDecimal(2).ToString("F2");

                Console.WriteLine(name.PadRight(15) + count.ToString().PadLeft(12) + avg.PadLeft(20));
                rows++;
            }

            if (rows == 0)
                Console.WriteLine("Данных нет.");
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Ошибка SQL #" + ex.Number + ": " + ex.Message);
        }
    }
}
