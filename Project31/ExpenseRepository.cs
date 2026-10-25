using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace Project31
{
    internal class ExpenseRepository
    {
        private readonly string _connectionString = "Data Source=expenses.db";

        public ExpenseRepository()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var create = connection.CreateCommand();
            create.CommandText = @"
                CREATE TABLE IF NOT EXISTS Expenses (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Category TEXT,
                    Description TEXT,
                    Amount REAL,
                    Date TEXT
                )";
            create.ExecuteNonQuery();
        }

        public void Add(Expense expense)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var insert = connection.CreateCommand();
            insert.CommandText = @"
                INSERT INTO Expenses (Category, Description, Amount, Date)
                VALUES ($category, $description, $amount, $date)";
            insert.Parameters.AddWithValue("$category", expense.Category);
            insert.Parameters.AddWithValue("$description", expense.Description);
            insert.Parameters.AddWithValue("$amount", (double)expense.Amount);
            insert.Parameters.AddWithValue("$date", expense.Date.ToString("yyyy-MM-dd"));
            insert.ExecuteNonQuery();

        }

        public List<Expense> GetAll()
        {
            var expenses = new List<Expense>();

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var select = connection.CreateCommand();
            select.CommandText = "SELECT Id, Category, Description, Amount, Date FROM Expenses";

            using var reader = select.ExecuteReader();
            while (reader.Read())
            {
                var expense = new Expense();
                expense.Id = reader.GetInt32(0);
                expense.Category = reader.GetString(1);
                expense.Description = reader.GetString(2);
                expense.Amount = (decimal)reader.GetDouble(3);
                expense.Date = DateTime.Parse(reader.GetString(4));

                expenses.Add(expense);
            }

            return expenses;

        }
    }
}
