using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Category : Auditable
    {
        public string Name { get; set; }
        public ICollection<Expense> Expenses { get; set; }
    }
}
