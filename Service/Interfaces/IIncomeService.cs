using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IIncomeService
    {

        public ValueTask<Income> CreateAsync(Income income);
        public ValueTask<Income> GetByIdAsync(long Id);
        public ValueTask<IQueryable<Income>> GetAll(long user_id, long day_id);
    }
}
