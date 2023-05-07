using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IExpenseService
    {
        public ValueTask<Expense> CreateAsync(Expense expense);
        public ValueTask<Expense> GetByIdAsync(long Id);
        public ValueTask<IQueryable<Expense>> GetAll(long user_id, long day_id);

    }
}
