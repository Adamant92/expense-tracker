using Project31;

var repository = new ExpenseRepository();

List<Expense> expenses = new List<Expense>();


while (true)
{
    Console.WriteLine();
    Console.WriteLine("Выберите пункт:");
    Console.WriteLine("1 - Найти по ID");
    Console.WriteLine("2 - Добавить трату");   
    Console.WriteLine("3 - Показать все");
    Console.WriteLine("4 - Сумма расхода по категориям");
    Console.WriteLine("5 - Удалить трату");
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
        var idfound = repository.GetById(id);

        if (idfound != null)
        {
            Console.WriteLine($"{idfound.Id}, {idfound.Category}, {idfound.Description}, {idfound.Amount}, {idfound.Date}");
        }

        else
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

        repository.Add(newExpense);   
        Console.WriteLine("Трата добавлена.");
    }

    else if (choice == "3")
    {
        foreach (var item in repository.GetAll())
        {
            Console.WriteLine(item);
        }
    }

    else if (choice == "4")
    {
        var byCategory = repository.GetAll().GroupBy(x => x.Category);

        foreach (var item in byCategory)
        {
            Console.WriteLine($"Категория: {item.Key}, общий расход: {item.Sum(s => s.Amount)}");
        }
    }

    else if (choice == "5")
    {
        Console.Write("Введите id: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Нужно ввести число");
        }

        Console.WriteLine("Вы уверены что хотиет удалить этот ID? (y/ -)");
        string inputdel = Console.ReadLine();

        if (inputdel.ToLower() == "y")
        {
            var idfound = repository.DeleteId(id);
            if (idfound)
            {
                Console.WriteLine($"Элемент {id} был удалён!");
            }
            else
            {
                Console.WriteLine("Такого элемента не существует");
            }
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

