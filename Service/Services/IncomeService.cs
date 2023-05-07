using AutoMapper;
using Data.IRepositories;
using Domain.Entities;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Income> incomeRepository;
        public IncomeService(
            IMapper mapper,
            IRepository<Income> repository)
        {
            this.mapper = mapper;
            this.incomeRepository = repository;
        }
        public async ValueTask<Income> CreateAsync(Income income)
        {
            // Добавляем новый объект Day в базу данных
            await incomeRepository.InsertAsync(income);
            await incomeRepository.SaveChangesAsync();

            return income;
        }
        public async ValueTask<Income> GetByIdAsync(long Id)
        {
            var income = await incomeRepository.SelectAsync(e => e.Id.Equals(Id));

            return income;
        }
        public async ValueTask<IQueryable<Income>> GetAll(long user_id, long day_id)
        {
            var incomes = incomeRepository.SelectAll(e => e.UserId == user_id && e.DayId == day_id);

            return incomes;
        }
    }
}
