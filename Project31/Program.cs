using Project31;

List<Expense> expenses = new List<Expense>();

var expense1 = new Expense(1);
expense1.Category = "Еда";
expense1.Description = "Обед в столовой";
expense1.Amount = 100;
expense1.Date = DateTime.Now;

var expense2 = new Expense(2);
expense2.Category = "Транспорт";
expense2.Description = "Грузовики";
expense2.Amount = 234500;
expense2.Date = DateTime.Now;

expenses.Add(expense1);
expenses.Add(expense2);

while (true)
{
    Console.WriteLine();
    Console.WriteLine("Выберите пункт:");
    Console.WriteLine("1 - Найти по ID");
    Console.WriteLine("2 - Добавить трату");   
    Console.WriteLine("3 - Показать все");
    Console.WriteLine("4 - Сумма расхода по категориям");
    Console.WriteLine("exit - Выход");

    string choice = Console.ReadLine().ToLower();  

    if (choice == "1")
    {
        
        Console.Write("Введите id: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Нужно ввести число");
            continue;   
        }

        bool found = false;
        foreach (var expense in expenses)
        {
            if (id == expense.Id)
            {
                Console.WriteLine($"{expense.Id}, {expense.Category}, {expense.Description}, {expense.Amount}, {expense.Date}");
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("Такого ID не существует");
        }
    }
    else if (choice == "2")
    {
        Console.Write("Введите ID новой траты (цифрами): ");
        if (!int.TryParse(Console.ReadLine(), out int newId))
        {
            Console.WriteLine("Нужно ввести число");
            continue;
        }

        bool exists = false;
        foreach (var item in expenses)
        {
            if (item.Id == newId)
            {
                exists = true;
            }
        }

        if (exists)
        {
            Console.WriteLine("Такой ID уже существует. Выберите другой.");
            continue;   
        }

        var newExpense = new Expense(newId);

        Console.Write("Введите категорию: ");
        newExpense.Category = Console.ReadLine();

        Console.Write("Введите описание: ");
        newExpense.Description = Console.ReadLine();

        Console.Write("Введите сумму расхода: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
        {
            Console.WriteLine("Сумма должна быть числом. Трата не добавлена.");
            continue;
        }
        newExpense.Amount = amount;

        newExpense.Date = DateTime.Now;

        expenses.Add(newExpense);   
        Console.WriteLine("Трата добавлена.");
    }
    else if (choice == "3")
    {
        foreach (var item in expenses)
        {
            Console.WriteLine(item);
        }
    }

    else if (choice == "4")
    {
        var byCategory = expenses.GroupBy(x => x.Category);

        foreach (var item in byCategory)
        {
            Console.WriteLine($"Категория: {item.Key}, общий расход: {item.Sum(s => s.Amount)}");
        }
    }

    else if (choice == "exit")
    {
        break;
    }
    else
    {
        Console.WriteLine("Нет такого пункта");
    }
}

