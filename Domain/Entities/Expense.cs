using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Expense : Auditable
    {
        public long UserId { get; set; }
        public User User { get; set; }
        public long DayId { get; set; }
        public Day Day { get; set; }
        public long Amount { get; set; }
        public long CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public string Comment { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
