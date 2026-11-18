# SQL notes

> Учебная БД: PracticeDB. Скрипт создания — `init.sql`. Запросы выполнялись в SSMS.
> Результаты даны для исходных данных из `init.sql`.

---

# Уровень 1 — Базовый

## Task 01 — Создание базы и таблиц

### Запрос

```sql
CREATE DATABASE PracticeDB;
GO
USE PracticeDB;
GO

CREATE TABLE Departments (
    DepartmentId   INT PRIMARY KEY IDENTITY(1,1),
    DepartmentName NVARCHAR(100) NOT NULL
);

CREATE TABLE Employees (
    EmployeeId   INT PRIMARY KEY IDENTITY(1,1),
    EmployeeName NVARCHAR(100) NOT NULL,
    Salary       DECIMAL(10,2) NOT NULL,
    HireDate     DATE NOT NULL,
    DepartmentId INT NOT NULL,
    ManagerId    INT NULL,
    CONSTRAINT FK_Employees_Departments FOREIGN KEY (DepartmentId) REFERENCES Departments(DepartmentId),
    CONSTRAINT FK_Employees_Manager FOREIGN KEY (ManagerId) REFERENCES Employees(EmployeeId)
);

CREATE TABLE Projects (
    ProjectId   INT PRIMARY KEY IDENTITY(1,1),
    ProjectName NVARCHAR(100) NOT NULL,
    StartDate   DATE NOT NULL
);

CREATE TABLE EmployeeProjects (
    EmployeeId INT NOT NULL,
    ProjectId  INT NOT NULL,
    Hours      INT NOT NULL DEFAULT 0,
    CONSTRAINT PK_EmployeeProjects PRIMARY KEY (EmployeeId, ProjectId),
    CONSTRAINT FK_EP_Employees FOREIGN KEY (EmployeeId) REFERENCES Employees(EmployeeId),
    CONSTRAINT FK_EP_Projects  FOREIGN KEY (ProjectId)  REFERENCES Projects(ProjectId)
);
GO

-- Проверка
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE';
```

### Результат

Создано 4 таблицы: Departments, Employees, Projects, EmployeeProjects. Внешних ключей 4: `FK_Employees_Departments`, `FK_Employees_Manager`, `FK_EP_Employees`, `FK_EP_Projects`.

### Вывод

`ManagerId` — внешний ключ на ту же таблицу и допускает `NULL`, потому что у части сотрудников руководителя нет.

---

## Task 02 — INSERT: заполнение данными

### Запрос

```sql
INSERT INTO Departments (DepartmentName) VALUES
    (N'Разработка'), (N'Аналитика'), (N'Тестирование'), (N'Менеджмент');

INSERT INTO Employees (EmployeeName, Salary, HireDate, DepartmentId, ManagerId) VALUES
    (N'Анна Иванова',     65000, '2020-03-15', 1, NULL),
    (N'Пётр Сидоров',     72000, '2019-07-01', 1, NULL),
    (N'Мария Кузнецова',  55000, '2021-11-20', 1, NULL),
    (N'Иван Петров',      48000, '2023-02-10', 1, NULL),
    (N'Ольга Смирнова',   61000, '2020-09-05', 2, NULL),
    (N'Дмитрий Орлов',    58000, '2022-04-18', 2, NULL),
    (N'Елена Волкова',    52000, '2023-06-01', 2, NULL),
    (N'Сергей Морозов',   45000, '2021-12-12', 3, NULL),
    (N'Наталья Соколова', 43000, '2024-01-15', 3, NULL),
    (N'Алексей Лебедев',  90000, '2018-05-25', 4, NULL);

UPDATE Employees SET ManagerId = 1  WHERE EmployeeId = 4;
UPDATE Employees SET ManagerId = 2  WHERE EmployeeId = 3;
UPDATE Employees SET ManagerId = 5  WHERE EmployeeId = 6;
UPDATE Employees SET ManagerId = 10 WHERE EmployeeId = 9;

INSERT INTO Projects (ProjectName, StartDate) VALUES
    (N'CRM System', '2023-01-10'), (N'Mobile App', '2023-05-01'),
    (N'Data Warehouse', '2022-11-20'), (N'AI Assistant', '2024-02-01'),
    (N'Legacy Refactor', '2021-06-15');

INSERT INTO EmployeeProjects (EmployeeId, ProjectId, Hours) VALUES
    (1, 1, 120), (1, 3, 80), (2, 1, 150), (2, 4, 60), (3, 2, 100), (4, 2, 90), (4, 5, 40),
    (5, 3, 110), (6, 3, 70), (6, 4, 50), (7, 2, 95), (8, 5, 130), (10, 4, 200), (10, 1, 30);

-- Проверка
SELECT 'Departments' AS TableName, COUNT(*) AS Cnt FROM Departments
UNION ALL SELECT 'Employees', COUNT(*) FROM Employees
UNION ALL SELECT 'Projects', COUNT(*) FROM Projects
UNION ALL SELECT 'EmployeeProjects', COUNT(*) FROM EmployeeProjects;
```

### Результат

| TableName | Cnt |
|---|---|
| Departments | 4 |
| Employees | 10 |
| Projects | 5 |
| EmployeeProjects | 14 |

### Вывод

Менеджеров проставляем отдельными `UPDATE`, потому что при вставке сотрудников их `EmployeeId` ещё не существуют. Кириллицу пишем с `N'...'`.

---

## Task 03 — SELECT + WHERE

### Запрос

```sql
SELECT * FROM Employees WHERE Salary > 50000;
SELECT * FROM Employees WHERE YEAR(HireDate) = 2023;
SELECT * FROM Employees WHERE EmployeeName LIKE N'А%';
SELECT * FROM Employees WHERE DepartmentId = 2;
SELECT * FROM Employees WHERE Salary BETWEEN 40000 AND 60000;
```

### Результат

| Запрос | Строк | Сотрудники |
|---|---|---|
| Зарплата > 50000 | 7 | Иванова, Сидоров, Кузнецова, Смирнова, Орлов, Волкова, Лебедев |
| Нанятые в 2023 | 2 | Иван Петров, Елена Волкова |
| Имя на «А» | 2 | Анна Иванова, Алексей Лебедев |
| Отдел 2 | 3 | Ольга Смирнова, Дмитрий Орлов, Елена Волкова |
| Зарплата 40000–60000 | 6 | Кузнецова, Петров, Орлов, Волкова, Морозов, Соколова |

### Вывод

`BETWEEN` включает обе границы. В `LIKE` буква «А» должна быть кириллической.

---

## Task 04 — ORDER BY + TOP

### Запрос

```sql
SELECT TOP 3 EmployeeName, Salary FROM Employees ORDER BY Salary DESC;
SELECT TOP 5 EmployeeName, HireDate FROM Employees ORDER BY HireDate ASC;
SELECT DepartmentId, EmployeeName, Salary FROM Employees ORDER BY DepartmentId ASC, Salary DESC;
```

### Результат

**Топ-3 по зарплате:** Алексей Лебедев (90000), Пётр Сидоров (72000), Анна Иванова (65000).

**Топ-5 самых давних:** Алексей Лебедев (2018-05-25), Пётр Сидоров (2019-07-01), Анна Иванова (2020-03-15), Ольга Смирнова (2020-09-05), Мария Кузнецова (2021-11-20).

**По отделу и зарплате:**

| DepartmentId | EmployeeName | Salary |
|---|---|---|
| 1 | Пётр Сидоров | 72000.00 |
| 1 | Анна Иванова | 65000.00 |
| 1 | Мария Кузнецова | 55000.00 |
| 1 | Иван Петров | 48000.00 |
| 2 | Ольга Смирнова | 61000.00 |
| 2 | Дмитрий Орлов | 58000.00 |
| 2 | Елена Волкова | 52000.00 |
| 3 | Сергей Морозов | 45000.00 |
| 3 | Наталья Соколова | 43000.00 |
| 4 | Алексей Лебедев | 90000.00 |

### Вывод

`TOP` без `ORDER BY` возвращает произвольные строки, поэтому сортировка обязательна.

---

## Task 05 — Агрегаты

### Запрос

```sql
SELECT
    COUNT(*)              AS TotalEmployees,
    SUM(Salary)           AS TotalSalary,
    ROUND(AVG(Salary), 2) AS AvgSalary,
    MIN(Salary)           AS MinSalary,
    MAX(Salary)           AS MaxSalary
FROM Employees;

SELECT COUNT(*) AS HighSalaryCount FROM Employees WHERE Salary > 50000;
```

### Результат

| TotalEmployees | TotalSalary | AvgSalary | MinSalary | MaxSalary |
|---|---|---|---|---|
| 10 | 589000.00 | 58900.000000 | 43000.00 | 90000.00 |

Сотрудников с зарплатой > 50000: **7**.

### Вывод

Ручная проверка: 589000 / 10 = 58900. `ROUND` не убирает лишние нули в `AVG` (тип `DECIMAL` с 6 знаками).

---

## Task 06 — GROUP BY

### Запрос

```sql
SELECT DepartmentId, COUNT(*) AS EmployeeCount,
       ROUND(AVG(Salary), 2) AS AvgSalary, SUM(Salary) AS TotalSalary
FROM Employees
GROUP BY DepartmentId
ORDER BY AvgSalary DESC;
```

### Результат

| DepartmentId | EmployeeCount | AvgSalary | TotalSalary |
|---|---|---|---|
| 4 | 1 | 90000.00 | 90000.00 |
| 1 | 4 | 60000.00 | 240000.00 |
| 2 | 3 | 57000.00 | 171000.00 |
| 3 | 2 | 44000.00 | 88000.00 |

### Вывод

Каждый столбец в `SELECT` — либо в `GROUP BY`, либо внутри агрегата.

---

## Task 07 — GROUP BY + HAVING

### Запрос

```sql
SELECT DepartmentId, COUNT(*) AS Cnt FROM Employees GROUP BY DepartmentId HAVING COUNT(*) > 2;
SELECT DepartmentId, AVG(Salary) AS Avg FROM Employees GROUP BY DepartmentId HAVING AVG(Salary) > 55000;
SELECT DepartmentId, SUM(Salary) AS Total FROM Employees GROUP BY DepartmentId HAVING SUM(Salary) > 150000;
```

### Результат

| Условие | Отделы |
|---|---|
| Больше 2 сотрудников | 1 (4 чел.), 2 (3 чел.) |
| Средняя зарплата > 55000 | 1 (60000), 2 (57000), 4 (90000) |
| Суммарная зарплата > 150000 | 1 (240000), 2 (171000) |

### Вывод

`WHERE` фильтрует строки до группировки, `HAVING` — группы после неё.

---

## Task 08 — INNER JOIN

### Запрос

```sql
SELECT e.EmployeeName, d.DepartmentName, e.Salary
FROM Employees e
INNER JOIN Departments d ON d.DepartmentId = e.DepartmentId
ORDER BY d.DepartmentName, e.Salary DESC;
```

### Результат

| EmployeeName | DepartmentName | Salary |
|---|---|---|
| Ольга Смирнова | Аналитика | 61000.00 |
| Дмитрий Орлов | Аналитика | 58000.00 |
| Елена Волкова | Аналитика | 52000.00 |
| Алексей Лебедев | Менеджмент | 90000.00 |
| Пётр Сидоров | Разработка | 72000.00 |
| Анна Иванова | Разработка | 65000.00 |
| Мария Кузнецова | Разработка | 55000.00 |
| Иван Петров | Разработка | 48000.00 |
| Сергей Морозов | Тестирование | 45000.00 |
| Наталья Соколова | Тестирование | 43000.00 |

### Вывод

10 строк: у каждого сотрудника есть отдел.

---

## Task 09 — LEFT JOIN: сотрудники без проектов

### Запрос

```sql
SELECT e.EmployeeId, e.EmployeeName
FROM Employees e
LEFT JOIN EmployeeProjects ep ON ep.EmployeeId = e.EmployeeId
WHERE ep.EmployeeId IS NULL;
```

### Результат

| EmployeeId | EmployeeName |
|---|---|
| 9 | Наталья Соколова |

### Вывод

`LEFT JOIN` оставляет всех сотрудников, а у тех, кто без проектов, правая часть равна `NULL`.

---

## Task 10 — Множественный JOIN

### Запрос

```sql
SELECT e.EmployeeName, d.DepartmentName, p.ProjectName, ep.Hours
FROM Employees e
INNER JOIN Departments d ON d.DepartmentId = e.DepartmentId
INNER JOIN EmployeeProjects ep ON ep.EmployeeId = e.EmployeeId
INNER JOIN Projects p ON p.ProjectId = ep.ProjectId
ORDER BY d.DepartmentName, e.EmployeeName, p.ProjectName;
```

### Результат

| EmployeeName | DepartmentName | ProjectName | Hours |
|---|---|---|---|
| Дмитрий Орлов | Аналитика | AI Assistant | 50 |
| Дмитрий Орлов | Аналитика | Data Warehouse | 70 |
| Елена Волкова | Аналитика | Mobile App | 95 |
| Ольга Смирнова | Аналитика | Data Warehouse | 110 |
| Алексей Лебедев | Менеджмент | AI Assistant | 200 |
| Алексей Лебедев | Менеджмент | CRM System | 30 |
| Анна Иванова | Разработка | CRM System | 120 |
| Анна Иванова | Разработка | Data Warehouse | 80 |
| Иван Петров | Разработка | Legacy Refactor | 40 |
| Иван Петров | Разработка | Mobile App | 90 |
| Мария Кузнецова | Разработка | Mobile App | 100 |
| Пётр Сидоров | Разработка | AI Assistant | 60 |
| Пётр Сидоров | Разработка | CRM System | 150 |
| Сергей Морозов | Тестирование | Legacy Refactor | 130 |

### Вывод

14 строк — по числу записей в `EmployeeProjects`. Натальи Соколовой нет, потому что у неё нет проектов.

---

# Уровень 2 — Средний

> Задания 16, 17 и 18 оставляют данные в исходном состоянии (откат транзакции, тестовые записи, возврат значения), поэтому остальные результаты не меняются.

## Task 11 — Self-join

### Запрос

```sql
SELECT e.EmployeeName, m.EmployeeName AS ManagerName
FROM Employees e
LEFT JOIN Employees m ON m.EmployeeId = e.ManagerId
ORDER BY e.EmployeeId;
```

### Результат

| EmployeeName | ManagerName |
|---|---|
| Анна Иванова | NULL |
| Пётр Сидоров | NULL |
| Мария Кузнецова | Пётр Сидоров |
| Иван Петров | Анна Иванова |
| Ольга Смирнова | NULL |
| Дмитрий Орлов | Ольга Смирнова |
| Елена Волкова | NULL |
| Сергей Морозов | NULL |
| Наталья Соколова | Алексей Лебедев |
| Алексей Лебедев | NULL |

### Вывод

Таблица подключается к самой себе под двумя алиасами. С `INNER JOIN` пропали бы сотрудники без руководителя.

---

## Task 12 — Подзапросы в WHERE

### Запрос

```sql
SELECT EmployeeName, Salary FROM Employees
WHERE Salary > (SELECT AVG(Salary) FROM Employees)
ORDER BY Salary DESC;

SELECT EmployeeName, Salary FROM Employees
WHERE Salary = (SELECT MAX(Salary) FROM Employees);
```

### Результат

Средняя зарплата — 58900.

| Выше средней | Salary |
|---|---|
| Алексей Лебедев | 90000.00 |
| Пётр Сидоров | 72000.00 |
| Анна Иванова | 65000.00 |
| Ольга Смирнова | 61000.00 |

Максимальная зарплата: Алексей Лебедев — 90000.00.

### Вывод

Подзапрос в сравнении должен вернуть одно значение. Дмитрий Орлов (58000) чуть ниже средней и не попал в выборку.

---

## Task 13 — Скалярные подзапросы в SELECT

### Запрос

```sql
SELECT e.EmployeeName, e.Salary, e.DepartmentId,
       CAST(ROUND(e.Salary - (SELECT AVG(e2.Salary) FROM Employees e2
                              WHERE e2.DepartmentId = e.DepartmentId), 2) AS DECIMAL(10,2)) AS DiffFromDeptAvg
FROM Employees e
ORDER BY e.EmployeeId;
```

### Результат

| EmployeeName | Salary | DepartmentId | DiffFromDeptAvg |
|---|---|---|---|
| Анна Иванова | 65000.00 | 1 | 5000.00 |
| Пётр Сидоров | 72000.00 | 1 | 12000.00 |
| Мария Кузнецова | 55000.00 | 1 | -5000.00 |
| Иван Петров | 48000.00 | 1 | -12000.00 |
| Ольга Смирнова | 61000.00 | 2 | 4000.00 |
| Дмитрий Орлов | 58000.00 | 2 | 1000.00 |
| Елена Волкова | 52000.00 | 2 | -5000.00 |
| Сергей Морозов | 45000.00 | 3 | 1000.00 |
| Наталья Соколова | 43000.00 | 3 | -1000.00 |
| Алексей Лебедев | 90000.00 | 4 | 0.00 |

### Вывод

Подзапрос коррелированный: он выполняется для каждого сотрудника со средней по его отделу (60000, 57000, 44000, 90000).

---

## Task 14 — EXISTS / NOT EXISTS

### Запрос

```sql
SELECT e.EmployeeId, e.EmployeeName FROM Employees e
WHERE EXISTS (SELECT 1 FROM EmployeeProjects ep WHERE ep.EmployeeId = e.EmployeeId);

SELECT e.EmployeeId, e.EmployeeName FROM Employees e
WHERE NOT EXISTS (SELECT 1 FROM EmployeeProjects ep WHERE ep.EmployeeId = e.EmployeeId);
```

### Результат

- `EXISTS` — 9 сотрудников (все, кроме Натальи Соколовой).
- `NOT EXISTS` — 1 сотрудник: Наталья Соколова (Id = 9).

### Вывод

Результат совпадает с заданием 9 (без проектов) и с заданием 10 (по проектам). `EXISTS` не дублирует строки, как `JOIN`.

---

## Task 15 — UNION / UNION ALL

### Запрос

```sql
SELECT EmployeeId, EmployeeName FROM Employees WHERE Salary > 60000
UNION
SELECT EmployeeId, EmployeeName FROM Employees WHERE HireDate < '2022-01-01';

SELECT EmployeeId, EmployeeName FROM Employees WHERE Salary > 60000
UNION ALL
SELECT EmployeeId, EmployeeName FROM Employees WHERE HireDate < '2022-01-01';
```

### Результат

| | Строк | Состав |
|---|---|---|
| Запрос 1 (зарплата > 60000) | 4 | сотрудники 1, 2, 5, 10 |
| Запрос 2 (нанят до 2022) | 6 | сотрудники 1, 2, 3, 5, 8, 10 |
| `UNION` | **6** | 1, 2, 3, 5, 8, 10 |
| `UNION ALL` | **10** | 1, 2, 5, 10 повторяются дважды |

### Вывод

Разница в 4 строки — это сотрудники, попавшие в оба запроса. `UNION` убирает дубли, `UNION ALL` — нет.

---

## Task 16 — UPDATE

### Запрос

```sql
BEGIN TRANSACTION;

SELECT EmployeeId, Salary, HireDate FROM Employees ORDER BY EmployeeId;   -- ДО

UPDATE Employees SET Salary = Salary * 1.10 WHERE DepartmentId = 1;
UPDATE Employees SET Salary = Salary + 5000 WHERE HireDate < '2022-01-01';
UPDATE Employees SET HireDate = '2024-02-01' WHERE EmployeeId = 9;

SELECT EmployeeId, Salary, HireDate FROM Employees ORDER BY EmployeeId;   -- ПОСЛЕ

ROLLBACK TRANSACTION;   -- возвращаем исходные данные
```

### Результат

Затронуто строк: 4 (отдел 1), 6 (нанятые до 2022), 1 (дата найма).

| EmployeeId | Salary до | Salary после | Причина |
|---|---|---|---|
| 1 | 65000.00 | 76500.00 | +10% и +5000 |
| 2 | 72000.00 | 84200.00 | +10% и +5000 |
| 3 | 55000.00 | 65500.00 | +10% и +5000 |
| 4 | 48000.00 | 52800.00 | +10% |
| 5 | 61000.00 | 66000.00 | +5000 |
| 6 | 58000.00 | 58000.00 | — |
| 7 | 52000.00 | 52000.00 | — |
| 8 | 45000.00 | 50000.00 | +5000 |
| 9 | 43000.00 | 43000.00 | `HireDate`: 2024-01-15 → 2024-02-01 |
| 10 | 90000.00 | 95000.00 | +5000 |

### Вывод

Изменились только целевые строки. После `ROLLBACK` значения снова исходные.

---

## Task 17 — DELETE

### Запрос

```sql
SELECT COUNT(*) FROM EmployeeProjects;                          -- 14
DELETE FROM EmployeeProjects WHERE Hours = 0;                   -- 0 строк
SELECT COUNT(*) FROM EmployeeProjects;                          -- 14

-- Тестовый сотрудник, чтобы не удалять настоящих
DECLARE @NewId INT;
INSERT INTO Employees (EmployeeName, Salary, HireDate, DepartmentId)
VALUES (N'Тест Тестов', 30000, '2024-03-01', 3);
SET @NewId = SCOPE_IDENTITY();
INSERT INTO EmployeeProjects (EmployeeId, ProjectId, Hours) VALUES (@NewId, 1, 10);

DELETE FROM EmployeeProjects WHERE EmployeeId = @NewId;         -- сначала назначения (FK)
SELECT COUNT(*) FROM EmployeeProjects;                          -- 14
DELETE FROM Employees WHERE EmployeeId = @NewId;                -- затем сотрудник
SELECT COUNT(*) FROM Employees;                                 -- 10

-- DELETE с WHERE и без — на копии таблицы
SELECT * INTO Employees_Copy FROM Employees;
DELETE FROM Employees_Copy WHERE DepartmentId = 3;
SELECT COUNT(*) FROM Employees_Copy;                            -- 8
DELETE FROM Employees_Copy;
SELECT COUNT(*) FROM Employees_Copy;                            -- 0
DROP TABLE Employees_Copy;
```

### Результат

| Шаг | Затронуто строк | `COUNT(*)` после |
|---|---|---|
| `DELETE ... Hours = 0` | 0 | EmployeeProjects: 14 |
| Удаление назначений тестового сотрудника | 1 | EmployeeProjects: 14 |
| Удаление тестового сотрудника | 1 | Employees: 10 |
| Копия: `DELETE ... WHERE DepartmentId = 3` | 2 | 8 |
| Копия: `DELETE` без `WHERE` | 8 | 0 |

### Вывод

Назначений с `Hours = 0` нет, поэтому удалено 0 строк. Сначала удаляем дочерние записи, потом родительскую, иначе ошибка FK (547). `DELETE` без `WHERE` показан только на копии.

---

## Task 18 — Транзакции

### Запрос

```sql
SELECT Salary FROM Employees WHERE EmployeeId = 1;              -- 65000

BEGIN TRANSACTION;
UPDATE Employees SET Salary = Salary + 1000 WHERE EmployeeId = 1;
SELECT Salary FROM Employees WHERE EmployeeId = 1;              -- 66000 (внутри транзакции)
ROLLBACK TRANSACTION;
SELECT Salary FROM Employees WHERE EmployeeId = 1;              -- 65000

BEGIN TRANSACTION;
UPDATE Employees SET Salary = Salary + 1000 WHERE EmployeeId = 1;
COMMIT TRANSACTION;
SELECT Salary FROM Employees WHERE EmployeeId = 1;              -- 66000

UPDATE Employees SET Salary = 65000 WHERE EmployeeId = 1;       -- возвращаем исходное значение
```

### Результат

| Шаг | Salary сотрудника 1 |
|---|---|
| Исходное | 65000.00 |
| Внутри транзакции | 66000.00 |
| После `ROLLBACK` | 65000.00 |
| После `COMMIT` | 66000.00 |
| После возврата | 65000.00 |

### Вывод

`ROLLBACK` отменяет изменение, `COMMIT` сохраняет. Последний `UPDATE` нужен только чтобы вернуть данные.

---

## Task 19 — VIEW

### Запрос

```sql
CREATE VIEW vw_EmployeeDetails AS
SELECT e.EmployeeName, d.DepartmentName, p.ProjectName, ep.Hours
FROM Employees e
INNER JOIN Departments d ON d.DepartmentId = e.DepartmentId
INNER JOIN EmployeeProjects ep ON ep.EmployeeId = e.EmployeeId
INNER JOIN Projects p ON p.ProjectId = ep.ProjectId;
GO

SELECT * FROM vw_EmployeeDetails ORDER BY DepartmentName, EmployeeName, ProjectName;
GO

DROP VIEW vw_EmployeeDetails;
GO
```

### Результат

Представление создано, `SELECT` вернул 14 строк (те же, что в задании 10), `DROP VIEW` выполнен без ошибок.

### Вывод

`Employees` и `Projects` связаны через `EmployeeProjects`, поэтому она тоже в представлении. `CREATE VIEW` должен быть первой командой в пакете (поэтому `GO`).

---

## Task 20 — Индексы

### Запрос

```sql
SET STATISTICS IO ON;

-- ДО индексов (включить Actual Execution Plan: Ctrl+M)
SELECT EmployeeId, EmployeeName, Salary FROM Employees
WHERE DepartmentId = 1 AND Salary > 50000;

CREATE INDEX IX_Employees_DepartmentId ON Employees(DepartmentId);
CREATE INDEX IX_Employees_Salary ON Employees(Salary);

-- ПОСЛЕ индексов
SELECT EmployeeId, EmployeeName, Salary FROM Employees
WHERE DepartmentId = 1 AND Salary > 50000;
```

### Результат

Запрос возвращает 3 строки: Пётр Сидоров, Анна Иванова, Мария Кузнецова.

> Планы и `logical reads` нужно взять из SSMS (вкладка *Messages* и скриншот плана) и заполнить:

| | Оператор в плане | logical reads |
|---|---|---|
| До индексов | _заполнить_ | _заполнить_ |
| После индексов | _заполнить_ | _заполнить_ |

_Скриншоты планов — вставить сюда._

### Вывод

В таблице всего 10 строк (одна страница данных), поэтому оптимизатор, скорее всего, оставит `Scan` и разницы в `logical reads` почти не будет. Индексы дают выигрыш на больших таблицах и при селективных условиях.

---

# Уровень 3 — Продвинутый

## Task 21 — Хранимые процедуры

### Запрос

```sql
CREATE OR ALTER PROCEDURE sp_GetEmployeesByDepartment @DeptId INT
AS
BEGIN
    SELECT EmployeeId, EmployeeName, Salary, HireDate
    FROM Employees
    WHERE DepartmentId = @DeptId
    ORDER BY EmployeeId;
END;
GO

CREATE OR ALTER PROCEDURE sp_GetAvgSalaryByDepartment
    @DeptId INT,
    @AvgSalary DECIMAL(10,2) OUTPUT
AS
BEGIN
    SELECT @AvgSalary = CAST(AVG(Salary) AS DECIMAL(10,2))
    FROM Employees
    WHERE DepartmentId = @DeptId;
END;
GO

EXEC sp_GetEmployeesByDepartment @DeptId = 1;

DECLARE @Avg DECIMAL(10,2);
EXEC sp_GetAvgSalaryByDepartment @DeptId = 1, @AvgSalary = @Avg OUTPUT;
SELECT @Avg AS AvgSalary;
```

### Результат

| EmployeeId | EmployeeName | Salary | HireDate |
|---|---|---|---|
| 1 | Анна Иванова | 65000.00 | 2020-03-15 |
| 2 | Пётр Сидоров | 72000.00 | 2019-07-01 |
| 3 | Мария Кузнецова | 55000.00 | 2021-11-20 |
| 4 | Иван Петров | 48000.00 | 2023-02-10 |

Средняя зарплата отдела 1: **60000.00**. Для несуществующего отдела (например, 99) параметр остаётся `NULL`.

### Вывод

`OUTPUT`-параметр нужно указывать и при вызове процедуры.

---

## Task 22 — Функции

### Запрос

```sql
CREATE OR ALTER FUNCTION dbo.fn_GetAnnualSalary (@MonthlySalary DECIMAL(10,2))
RETURNS DECIMAL(12,2)
AS
BEGIN
    RETURN @MonthlySalary * 12;
END;
GO

CREATE OR ALTER FUNCTION dbo.fn_GetEmployeesBySalaryRange (@Min DECIMAL(10,2), @Max DECIMAL(10,2))
RETURNS TABLE
AS
RETURN (
    SELECT EmployeeId, EmployeeName, Salary, DepartmentId
    FROM Employees
    WHERE Salary BETWEEN @Min AND @Max
);
GO

SELECT EmployeeName, Salary, dbo.fn_GetAnnualSalary(Salary) AS AnnualSalary
FROM Employees ORDER BY EmployeeId;

SELECT * FROM dbo.fn_GetEmployeesBySalaryRange(50000, 60000) ORDER BY Salary DESC;
```

### Результат

Годовая зарплата (Salary × 12):

| EmployeeName | Salary | AnnualSalary |
|---|---|---|
| Анна Иванова | 65000.00 | 780000.00 |
| Пётр Сидоров | 72000.00 | 864000.00 |
| Мария Кузнецова | 55000.00 | 660000.00 |
| Иван Петров | 48000.00 | 576000.00 |
| Ольга Смирнова | 61000.00 | 732000.00 |
| Дмитрий Орлов | 58000.00 | 696000.00 |
| Елена Волкова | 52000.00 | 624000.00 |
| Сергей Морозов | 45000.00 | 540000.00 |
| Наталья Соколова | 43000.00 | 516000.00 |
| Алексей Лебедев | 90000.00 | 1080000.00 |

Диапазон 50000–60000: Дмитрий Орлов (58000), Мария Кузнецова (55000), Елена Волкова (52000).

### Вывод

Параметры объявлены как `DECIMAL(10,2)`: `DECIMAL` без точности — это `DECIMAL(18,0)`, и копейки отбрасывались бы. Скалярная функция вызывается в `SELECT`, табличная — в `FROM`.

---

## Task 23 — ROW_NUMBER, RANK, DENSE_RANK

### Запрос

```sql
SELECT EmployeeName, DepartmentId, Salary,
       ROW_NUMBER() OVER (ORDER BY Salary DESC)                           AS RowNum,
       RANK()       OVER (PARTITION BY DepartmentId ORDER BY Salary DESC) AS RankInDept,
       DENSE_RANK() OVER (PARTITION BY DepartmentId ORDER BY Salary DESC) AS DenseRankInDept
FROM Employees
ORDER BY RowNum;

-- Разница RANK / DENSE_RANK при равных значениях (ранжируем по отделу)
SELECT EmployeeName, DepartmentId,
       RANK()       OVER (ORDER BY DepartmentId) AS Rnk,
       DENSE_RANK() OVER (ORDER BY DepartmentId) AS DenseRnk
FROM Employees
ORDER BY DepartmentId, EmployeeId;
```

### Результат

| EmployeeName | DepartmentId | Salary | RowNum | RankInDept | DenseRankInDept |
|---|---|---|---|---|---|
| Алексей Лебедев | 4 | 90000.00 | 1 | 1 | 1 |
| Пётр Сидоров | 1 | 72000.00 | 2 | 1 | 1 |
| Анна Иванова | 1 | 65000.00 | 3 | 2 | 2 |
| Ольга Смирнова | 2 | 61000.00 | 4 | 1 | 1 |
| Дмитрий Орлов | 2 | 58000.00 | 5 | 2 | 2 |
| Мария Кузнецова | 1 | 55000.00 | 6 | 3 | 3 |
| Елена Волкова | 2 | 52000.00 | 7 | 3 | 3 |
| Иван Петров | 1 | 48000.00 | 8 | 4 | 4 |
| Сергей Морозов | 3 | 45000.00 | 9 | 1 | 1 |
| Наталья Соколова | 3 | 43000.00 | 10 | 2 | 2 |

Второй запрос (равные значения):

| DepartmentId | Сотрудников | Rnk | DenseRnk |
|---|---|---|---|
| 1 | 4 | 1 | 1 |
| 2 | 3 | 5 | 2 |
| 3 | 2 | 8 | 3 |
| 4 | 1 | 10 | 4 |

### Вывод

Зарплаты у всех разные, поэтому в основной таблице `RANK` и `DENSE_RANK` совпадают. Второй запрос показывает разницу: `RANK` пропускает номера после равных значений, `DENSE_RANK` — нет.

---

## Task 24 — Оконные агрегаты

### Запрос

```sql
SELECT EmployeeName, DepartmentId, Salary,
       SUM(Salary) OVER (PARTITION BY DepartmentId) AS DeptTotal,
       CAST(AVG(Salary) OVER (PARTITION BY DepartmentId) AS DECIMAL(10,2)) AS DeptAvg,
       CAST(Salary - AVG(Salary) OVER (PARTITION BY DepartmentId) AS DECIMAL(10,2)) AS DiffFromAvg
FROM Employees
ORDER BY DepartmentId, Salary DESC;
```

### Результат

| EmployeeName | DepartmentId | Salary | DeptTotal | DeptAvg | DiffFromAvg |
|---|---|---|---|---|---|
| Пётр Сидоров | 1 | 72000.00 | 240000.00 | 60000.00 | 12000.00 |
| Анна Иванова | 1 | 65000.00 | 240000.00 | 60000.00 | 5000.00 |
| Мария Кузнецова | 1 | 55000.00 | 240000.00 | 60000.00 | -5000.00 |
| Иван Петров | 1 | 48000.00 | 240000.00 | 60000.00 | -12000.00 |
| Ольга Смирнова | 2 | 61000.00 | 171000.00 | 57000.00 | 4000.00 |
| Дмитрий Орлов | 2 | 58000.00 | 171000.00 | 57000.00 | 1000.00 |
| Елена Волкова | 2 | 52000.00 | 171000.00 | 57000.00 | -5000.00 |
| Сергей Морозов | 3 | 45000.00 | 88000.00 | 44000.00 | 1000.00 |
| Наталья Соколова | 3 | 43000.00 | 88000.00 | 44000.00 | -1000.00 |
| Алексей Лебедев | 4 | 90000.00 | 90000.00 | 90000.00 | 0.00 |

### Вывод

Суммы и средние совпадают с `GROUP BY` из задания 6. В отличие от `GROUP BY`, оконные функции не схлопывают строки.

---

## Task 25 — CTE

### Запрос

```sql
-- Сотрудники выше средней (то же, что задание 12)
WITH AvgSalary AS (
    SELECT AVG(Salary) AS AvgValue FROM Employees
)
SELECT e.EmployeeId, e.EmployeeName, e.Salary
FROM Employees e
CROSS JOIN AvgSalary a
WHERE e.Salary > a.AvgValue
ORDER BY e.Salary DESC;

-- Топ-3 отдела по средней зарплате
WITH DeptAvg AS (
    SELECT DepartmentId, AVG(Salary) AS AvgSalary
    FROM Employees
    GROUP BY DepartmentId
)
SELECT TOP 3 d.DepartmentName, CAST(da.AvgSalary AS DECIMAL(10,2)) AS AvgSalary
FROM DeptAvg da
INNER JOIN Departments d ON d.DepartmentId = da.DepartmentId
ORDER BY da.AvgSalary DESC;
```

### Результат

Выше средней — те же 4 человека, что в задании 12: Алексей Лебедев (90000), Пётр Сидоров (72000), Анна Иванова (65000), Ольга Смирнова (61000).

Топ-3 отдела: Менеджмент — 90000.00, Разработка — 60000.00, Аналитика — 57000.00.

### Вывод

CTE читается сверху вниз и даёт промежуточному результату имя. Для одного простого значения подзапрос (задание 12) короче, CTE удобнее, когда шагов несколько.

---

## Task 26 — CASE

### Запрос

```sql
SELECT EmployeeName, Salary,
       CASE
           WHEN Salary > 60000 THEN 'High'
           WHEN Salary BETWEEN 40000 AND 60000 THEN 'Medium'
           ELSE 'Low'
       END AS SalaryLevel,
       HireDate,
       CASE
           WHEN HireDate < '2020-01-01' THEN 'Senior'
           WHEN HireDate < '2023-01-01' THEN 'Mid'
           ELSE 'Junior'
       END AS Tenure
FROM Employees
ORDER BY EmployeeId;
```

### Результат

| EmployeeName | Salary | SalaryLevel | HireDate | Tenure |
|---|---|---|---|---|
| Анна Иванова | 65000.00 | High | 2020-03-15 | Mid |
| Пётр Сидоров | 72000.00 | High | 2019-07-01 | Senior |
| Мария Кузнецова | 55000.00 | Medium | 2021-11-20 | Mid |
| Иван Петров | 48000.00 | Medium | 2023-02-10 | Junior |
| Ольга Смирнова | 61000.00 | High | 2020-09-05 | Mid |
| Дмитрий Орлов | 58000.00 | Medium | 2022-04-18 | Mid |
| Елена Волкова | 52000.00 | Medium | 2023-06-01 | Junior |
| Сергей Морозов | 45000.00 | Medium | 2021-12-12 | Mid |
| Наталья Соколова | 43000.00 | Medium | 2024-01-15 | Junior |
| Алексей Лебедев | 90000.00 | High | 2018-05-25 | Senior |

### Вывод

`NULL` нет. Категории `Low` нет, потому что минимальная зарплата — 43000. `CASE` берёт первое подошедшее условие.

---

## Task 27 — Работа с датами

### Запрос

```sql
SELECT EmployeeName, HireDate,
       DATEDIFF(YEAR, HireDate, GETDATE())  AS YearsDiff,
       DATEDIFF(MONTH, HireDate, GETDATE()) AS MonthsDiff,
       DATEADD(YEAR, 1, HireDate)           AS OneYearLater,
       FORMAT(HireDate, 'dd.MM.yyyy')       AS HireDateFmt
FROM Employees
ORDER BY EmployeeId;
```

### Результат

Расчёт на дату 14.11.2026 (при другой дате запуска цифры изменятся):

| EmployeeName | HireDate | YearsDiff | MonthsDiff | OneYearLater | HireDateFmt |
|---|---|---|---|---|---|
| Анна Иванова | 2020-03-15 | 6 | 80 | 2021-03-15 | 15.03.2020 |
| Пётр Сидоров | 2019-07-01 | 7 | 88 | 2020-07-01 | 01.07.2019 |
| Мария Кузнецова | 2021-11-20 | 5 | 60 | 2022-11-20 | 20.11.2021 |
| Иван Петров | 2023-02-10 | 3 | 45 | 2024-02-10 | 10.02.2023 |
| Ольга Смирнова | 2020-09-05 | 6 | 74 | 2021-09-05 | 05.09.2020 |
| Дмитрий Орлов | 2022-04-18 | 4 | 55 | 2023-04-18 | 18.04.2022 |
| Елена Волкова | 2023-06-01 | 3 | 41 | 2024-06-01 | 01.06.2023 |
| Сергей Морозов | 2021-12-12 | 5 | 59 | 2022-12-12 | 12.12.2021 |
| Наталья Соколова | 2024-01-15 | 2 | 34 | 2025-01-15 | 15.01.2024 |
| Алексей Лебедев | 2018-05-25 | 8 | 102 | 2019-05-25 | 25.05.2018 |

### Вывод

`DATEDIFF` считает пересечённые границы года и месяца, а не полные периоды. У Марии Кузнецовой и Сергея Морозова годовщина ещё не наступила, поэтому полных лет у них 4, хотя `DATEDIFF(YEAR)` показывает 5.

---

# Задания 28–30 — C# + SQL

Код — в проекте `PracticeDbApp` (консольное приложение .NET 8, файл `Program.cs`, пакет `Microsoft.Data.SqlClient`). Строка подключения берётся из переменной окружения `PRACTICE_DB_CONNECTION`; если её нет, используется запасная строка для локального сервера с Windows-аутентификацией (без пароля).

Запуск: `dotnet run` в папке проекта или F5 в Visual Studio.

## Task 28 — Подключение и SELECT

Метод `ShowEmployees`: открывает соединение через `OpenAsync`, выполняет `SELECT EmployeeName, Salary FROM Employees`, читает через `SqlDataReader`, ошибки перехватывает `catch (SqlException)`.

Вывод программы:

```
Анна Иванова          65000.00
Пётр Сидоров          72000.00
Мария Кузнецова       55000.00
Иван Петров           48000.00
Ольга Смирнова        61000.00
Дмитрий Орлов         58000.00
Елена Волкова         52000.00
Сергей Морозов        45000.00
Наталья Соколова      43000.00
Алексей Лебедев       90000.00
Всего: 10
```

## Task 29 — Параметризованный INSERT

Метод `AddEmployee`: имя, зарплата и Id отдела передаются параметрами `@name`, `@salary`, `@deptId` (без склейки строк). После `INSERT` выполняется `SELECT` по новому Id. Ошибка 547 (нет такого отдела) обрабатывается отдельным сообщением.

Проверки для демо:

1. Имя «Тест Тестов», зарплата 50000, отдел 1 — сотрудник добавлен, программа выводит его данные.
2. Отдел 99 — сообщение «отдела с таким Id не существует».
3. Имя `Robert'); DROP TABLE Employees;--` — сохраняется как обычный текст, таблица цела.

Тестовых сотрудников после демо можно удалить: `DELETE FROM Employees WHERE EmployeeName LIKE N'Тест%';`

## Task 30 — Отчёт по отделам

Метод `ShowReport`: запрос из задания через `ExecuteReaderAsync`, вывод таблицей с выравниванием, `NULL` в средней зарплате заменяется на `N/A`, пустой результат — «Данных нет.».

Вывод программы (до добавления тестовых сотрудников):

```
Отдел           Сотрудников    Средняя зарплата
-----------------------------------------------
Аналитика                 3            57000.00
Менеджмент                1            90000.00
Разработка                4            60000.00
Тестирование              2            44000.00
```
