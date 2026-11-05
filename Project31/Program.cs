using Project31;

var repository = new ExpenseRepository();

while (true)
{
    Console.WriteLine();
    Console.WriteLine("Выберите пункт:");
    Console.WriteLine("1 - Найти по ID");
    Console.WriteLine("2 - Добавить трату");
    Console.WriteLine("3 - Показать все");
    Console.WriteLine("4 - Сумма расходов по категориям");
    Console.WriteLine("5 - Удалить трату");
    Console.WriteLine("exit - Выход");

    string choice = (Console.ReadLine() ?? "").Trim().ToLower();

    switch (choice)
    {
        case "1":
            FindById(repository);
            break;
        case "2":
            AddExpense(repository);
            break;
        case "3":
            ShowAll(repository);
            break;
        case "4":
            ShowTotals(repository);
            break;
        case "5":
            DeleteExpense(repository);
            break;
        case "exit":
            return;
        default:
            Console.WriteLine("Нет такого пункта");
            break;
    }
}

static void FindById(ExpenseRepository repository)
{
    Console.Write("Введите ID: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Нужно ввести число");
        return;
    }

    var expense = repository.GetById(id);

    if (expense == null)
    {
        Console.WriteLine("Такого ID не существует");
        return;
    }

    Console.WriteLine(expense);
}

static void AddExpense(ExpenseRepository repository)
{
    var expense = new Expense();

    Console.Write("Введите категорию: ");
    expense.Category = Console.ReadLine() ?? "";

    Console.Write("Введите описание: ");
    expense.Description = Console.ReadLine() ?? "";

    Console.Write("Введите сумму: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
    {
        Console.WriteLine("Сумма должна быть числом. Трата не добавлена.");
        return;
    }

    expense.Amount = amount;
    expense.Date = DateTime.Now;

    repository.Add(expense);
    Console.WriteLine("Трата добавлена.");
}

static void ShowAll(ExpenseRepository repository)
{
    var expenses = repository.GetAll();

    if (expenses.Count == 0)
    {
        Console.WriteLine("Трат пока нет.");
        return;
    }

    foreach (var expense in expenses)
    {
        Console.WriteLine(expense);
    }
}

static void ShowTotals(ExpenseRepository repository)
{
    var expenses = repository.GetAll();

    if (expenses.Count == 0)
    {
        Console.WriteLine("Трат пока нет.");
        return;
    }

    var byCategory = expenses.GroupBy(x => x.Category);

    foreach (var group in byCategory)
    {
        Console.WriteLine($"Категория: {group.Key}, общий расход: {group.Sum(x => x.Amount)}");
    }
}

static void DeleteExpense(ExpenseRepository repository)
{
    Console.Write("Введите ID: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Нужно ввести число");
        return;
    }

    Console.Write("Вы уверены, что хотите удалить эту трату? (y/n): ");
    string answer = (Console.ReadLine() ?? "").Trim().ToLower();

    if (answer != "y")
    {
        Console.WriteLine("Удаление отменено.");
        return;
    }

    if (repository.DeleteId(id))
    {
        Console.WriteLine($"Трата {id} удалена.");
    }
    else
    {
        Console.WriteLine("Такой траты не существует.");
    }
}