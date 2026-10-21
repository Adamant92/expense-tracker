using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
