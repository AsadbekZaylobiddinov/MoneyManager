using Domain.Commons;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : Auditable
    {
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; } = UserRole.User;
        public ICollection<Day> Days { get; set; }
        public ICollection<Expense> Expenses { get; set; }
        public ICollection<Income> Incomes { get; set; }
    }
}
