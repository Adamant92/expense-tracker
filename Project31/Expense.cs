using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project31
{
    internal class Expense
    {
        public Expense(int id) 
        {
            Id = id;
        }
        public int Id { get; set; }
        public string Category { get; set; }
        
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            return $"Категория: {Category}, Сумма расхода: {Amount}, Дата создания ID: {Date}, Описание: {Description}";
        }

    }
}
