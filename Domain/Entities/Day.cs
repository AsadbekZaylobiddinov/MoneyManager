using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Day :Auditable
    {
        public long UserId { get; set; }
        public User User { get; set; }
        public long MonthId { get; set; }
        public Month Month { get; set; }
        public int Number { get; set; }
        public ICollection<Expense> Expenses { get; set; }
        public ICollection<Income> Incomes { get; set; }
    }
}
